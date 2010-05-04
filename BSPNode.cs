//11-04-2010
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;

namespace InteractingMeshes
{
    /// <summary>
    /// Polygon positions to the plane
    /// </summary>
    public enum PolygonOnPlanePosition
    {
        /* Coplanar polygons treated as being in front of plane */
        POLYGON_IN_FRONT_OF_PLANE = 0,
        POLYGON_BEHIND_PLANE = 1,
        POLYGON_STRADDLING_PLANE = 2,
        POLYGON_COPLANAR_WITH_PLANE = 3
    }

    /// <summary>
    /// Points posisions on the plane
    /// </summary>
    public enum PointOnPlanePosition
    {
        POINT_IN_FRONT_OF_PLANE = 1,
        POINT_BEHIND_PLANE = -1,
        POINT_ON_PLANE = 0
    }

    /// <summary>
    /// Binary Space Partitioning Node
    /// </summary>
    public class BSPNode
    {
        /// <summary>
        /// Max depth
        /// </summary>
        public static readonly int MaxDepth = 128;

        /// <summary>
        /// Min leaf size
        /// </summary>
        public static readonly int MinLeafSize = -1;

        /// <summary>
        /// Left BSP tree
        /// </summary>
        public BSPNode frontTree = null;

        /// <summary>
        /// Righ BSP tree
        /// </summary>
        public BSPNode backTree = null;

        List<Polygon> Polygons = new List<Polygon>();


        #region --- Creating and destroying objects ---

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_polygons"></param>
        public BSPNode(List<Polygon> _polygons)
        {
            this.Polygons.AddRange(_polygons);
        }

        /// <summary>
        ///Construktor
        /// </summary>
        /// <param name="_frontTree"></param>
        /// <param name="_backTree"></param>
        public BSPNode(BSPNode _frontTree, BSPNode _backTree)
        {
            this.frontTree = _frontTree;
            this.backTree = _backTree;
            if (this.frontTree != null)
            {
                this.Polygons.AddRange(_frontTree.Polygons);
            }
            if (this.backTree != null)
            {
                this.Polygons.AddRange(_backTree.Polygons);
            }
        }

        #endregion

        #region --- Public static methods ---

        /// <summary>
        /// Constructs BSP tree from an input list of polygons.
        /// </summary>
        /// <param name="_polygons"></param>
        /// <param name="_depth">Depth</param>
        /// <returns></returns>
        public static BSPNode BuildBSPTree(List<Polygon> _polygons, int _depth, int _ID)
        {
            if (_polygons.Count == 0 || !BSPNode.DifferentObjectsExist(_polygons))
            {
                return null;
            }

            int numPolygons = _polygons.Count;

            if (_depth >= MaxDepth || numPolygons <= MinLeafSize)
            {
                return new BSPNode(_polygons);
            }

            //(List<Polygon>)(from polygon in _polygons where polygon.Points[0].ID == _ID select new { }); 
            var polygons = new List<Polygon>();

            foreach (var polygon in _polygons.Where(polygon => polygon.Points[0].ID == _ID))
            {
                polygons.Add(polygon);
            }

            Plane splitPlane = PickSplittingPlane(polygons,_polygons);

            var frontList = new List<Polygon>();
            var backList = new List<Polygon>();
            var coplanarList = new List<Polygon>();

            //test each polygon against the dividing plane, adding them to the front list, back list, or both, as appropriate
            for (var i = 0; i < numPolygons; ++i)
            {
                Polygon poly = _polygons[i];
                switch (ClassifyPolygonToPlane(poly, splitPlane))
                {
                    case PolygonOnPlanePosition.POLYGON_COPLANAR_WITH_PLANE: //COPLANAR_WITH_PLANE, next case
                        {
                            coplanarList.Add(poly);
                            break;
                        }
                    case PolygonOnPlanePosition.POLYGON_IN_FRONT_OF_PLANE: //IN_FRONT_OF_PLANE
                        frontList.Add(poly);
                        break;
                    case PolygonOnPlanePosition.POLYGON_BEHIND_PLANE: //BEHIND_PLANE
                        backList.Add(poly);
                        break;
                    case PolygonOnPlanePosition.POLYGON_STRADDLING_PLANE: //STRADDLING_PLANE
                        List<Polygon> frontPart;
                        List<Polygon> backPart;
                        SplitPolygon(poly, splitPlane, out frontPart, out backPart);
                        frontList.AddRange(frontPart);
                        backList.AddRange(backPart);
                        break;
                }
            }

            int frontIDs = 0;
            var backIDs = 0;
            for (var k = 0; k < frontList.Count; ++k )
            {
                if (frontList[k].Points[0].ID == _ID)
                {
                    frontIDs += 1;
                }
            }
            for (var k = 0; k < backList.Count; ++k)
            {
                if (backList[k].Points[0].ID == _ID)
                {
                    backIDs += 1;
                }
            }

            if (frontIDs > backIDs)
            {
                frontList.AddRange(coplanarList);
            }
            else
            {
                backList.AddRange(coplanarList);
            }

            // Recursively build child subtrees and return new tree root combining them
            BSPNode frontTree = BuildBSPTree(frontList, _depth + 1, _ID);
            BSPNode backTree = BuildBSPTree(backList, _depth + 1, _ID);
            if (frontTree == null && backTree == null)
            {
                return null;
            }
            return new BSPNode(frontTree, backTree);
        }



        #endregion

        #region --- Private static methods ---



        /// <summary>
        /// Splitting polygons by a plane
        /// </summary>
        /// <param name="poly"></param>
        /// <param name="_splitPlane"></param>
        /// <param name="_frontPart"></param>
        /// <param name="_backPart"></param>
        private static void SplitPolygon(Polygon poly, Plane _splitPlane, out List<Polygon> _frontPart, out List<Polygon> _backPart)
        {
            List<Vertex> frontVerts = new List<Vertex>();
            List<Vertex> backVerts = new List<Vertex>();

            // Test all edges (a, b) starting with edge from last to first vertex
            int numVerts = poly.Points.Count;
            Vertex a = poly.Points[numVerts - 1];
            PointOnPlanePosition aSide = ClassifyPointToPlane(a, _splitPlane);
            // Loop over all edges given by vertex pair (n - 1, n)
            for (int n = 0; n < numVerts; n++)
            {
                Vertex b = poly.Points[n];// .GetVertex(n);
                PointOnPlanePosition bSide = ClassifyPointToPlane(b, _splitPlane);
                if (bSide == PointOnPlanePosition.POINT_IN_FRONT_OF_PLANE)
                {
                    if (aSide == PointOnPlanePosition.POINT_BEHIND_PLANE)
                    {
                        // Edge (a, b) straddles, output intersection point to both sides
                        Vector3 i = IntersectEdgeAgainstPlane(a, b, _splitPlane);
                        /////////////////assert(ClassifyPointToPlane(i, _splitPlane) == POINT_ON_PLANE);
                        frontVerts.Add(new Vertex(i,a.ID));
                        backVerts.Add(new Vertex(i,a.ID));
                    }
                    // In all three cases, output b to the front side
                    frontVerts.Add(b);//new Vertex(b.Vector, a.ID));
                }
                else if (bSide == PointOnPlanePosition.POINT_BEHIND_PLANE)
                {
                    if (aSide == PointOnPlanePosition.POINT_IN_FRONT_OF_PLANE)
                    {
                        // Edge (a, b) straddles plane, output intersection point
                        Microsoft.DirectX.Vector3 i = IntersectEdgeAgainstPlane(a, b, _splitPlane);
                        //assert(ClassifyPointToPlane(i, plane) == POINT_ON_PLANE);
                        frontVerts.Add(new Vertex(i, a.ID));
                        backVerts.Add(new Vertex(i,a.ID));
                    }
                    else if (aSide == PointOnPlanePosition.POINT_ON_PLANE)
                    {
                        // Output a when edge (a, b) goes from ‘
                        backVerts.Add(a);
                    }
                    // In all three cases, output b to the back side
                    backVerts.Add(b);
                }
                else
                {
                    // b is on the plane. In all three cases output b to the front side
                    frontVerts.Add(b);
                    // In one case, also output b to back side
                    if (aSide == PointOnPlanePosition.POINT_BEHIND_PLANE)
                    {
                        backVerts.Add(b);
                    }
                }
                // Keep b as the starting point of the next edge
                a = b;
                aSide = bSide;
            }
            // Create (and return) two new polygons from the two vertex lists
            Polygon frontPoly = new Polygon(frontVerts);
            Polygon backPoly = new Polygon(backVerts);
            _frontPart = new List<Polygon>();
            _backPart = new List<Polygon>();
            _frontPart.Add(frontPoly);
            _backPart.Add(backPoly);
        }

        /// <summary>
        /// Intersecting edge against plane
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="_splitPlane"></param>
        /// <returns></returns>
        private static Vector3 IntersectEdgeAgainstPlane(Vertex A, Vertex B, Plane _splitPlane)
        {
            Microsoft.DirectX.Vector3 V = A.Vector - B.Vector;
            Microsoft.DirectX.Vector3 N = GeometricUtils.GetNormal(_splitPlane);
            double D = _splitPlane.D;
            double W = (Vector3.Dot(N, B.Vector) + D) / Vector3.Dot(N, V);
            //Wyznaczamy punkt przecięcia P’:
            return (B.Vector - Vector3.Multiply(V, (float)W));
        }

        /// <summary>
        /// Classify polygon to plane
        /// </summary>
        /// <param name="_poly"></param>
        /// <param name="_plane"></param>
        /// <returns></returns>
        private static PolygonOnPlanePosition ClassifyPolygonToPlane(Polygon _poly, Plane _plane)
        {
            // Loop over all polygon vertices and count how many vertices
            // lie in front of and how many lie behind of the thickened plane
            int numInFront = 0, numBehind = 0;
            int numVerts = _poly.Points.Count;
            for (int i = 0; i < numVerts; i++)
            {
                Vertex p = _poly.Points[i];
                switch (ClassifyPointToPlane(p, _plane))
                {
                    case PointOnPlanePosition.POINT_IN_FRONT_OF_PLANE:
                        numInFront++;
                        break;
                    case PointOnPlanePosition.POINT_BEHIND_PLANE:
                        numBehind++;
                        break;
                }
            }
            // If vertices on both sides of the plane, the polygon is straddling
            if (numBehind != 0 && numInFront != 0)
            {
                return PolygonOnPlanePosition.POLYGON_STRADDLING_PLANE;
            }
            // If one or more vertices in front of the plane and no vertices behind
            // the plane, the polygon lies in front of the plane
            if (numInFront != 0)
            {
                return PolygonOnPlanePosition.POLYGON_IN_FRONT_OF_PLANE;
            }
            // Ditto, the polygon lies behind the plane if no vertices in front of
            // the plane, and one or more vertices behind the plane
            if (numBehind != 0)
            {
                return PolygonOnPlanePosition.POLYGON_BEHIND_PLANE;
            }
            // All vertices lie on the plane so the polygon is coplanar with the plane
            return PolygonOnPlanePosition.POLYGON_COPLANAR_WITH_PLANE;
        }

        /// <summary>
        /// Given a vector of polygons, attempts to compute a good splitting plane
        /// </summary>
        /// <param name="_polygons"></param>
        /// <returns></returns>
        private static Plane PickSplittingPlane(List<Polygon> _polygons0, List<Polygon> _polygons)
        {
            // Blend factor for optimizing for balance or splits (should be tweaked)
            float K = 0.2f;
            // Variables for tracking best splitting plane seen so far
            Plane bestPlane = Plane.Empty;
            float bestScore = float.MaxValue;
            // Try the plane of each polygon as a dividing plane
            for (int i = 0; i < _polygons0.Count; i++)
            {
                int numInFront = 0, numBehind = 0, numStraddling = 0, numCoplanar = 0;
                Plane plane = GetPlaneFromPolygon(_polygons0[i]);

                // Test against all other polygons
                for (int j = 0; j < _polygons.Count; j++)
                {
                    // Ignore testing against self
                    if (i == j) continue;
                    // Keep standing count of the various poly-plane relationships
                    switch (ClassifyPolygonToPlane(_polygons[j], plane))
                    {
                        case PolygonOnPlanePosition.POLYGON_COPLANAR_WITH_PLANE:
                        /* Coplanar polygons treated as being in front of plane */
                            numCoplanar++;
                            break;
                        case PolygonOnPlanePosition.POLYGON_IN_FRONT_OF_PLANE:
                            numInFront++;
                            break;
                        case PolygonOnPlanePosition.POLYGON_BEHIND_PLANE:
                            numBehind++;
                            break;
                        case PolygonOnPlanePosition.POLYGON_STRADDLING_PLANE:
                            numStraddling++;
                            break;
                    }
                }
                // Compute score as a weighted combination (based on K, with K in range
                // 0..1) between balance and splits (lower score is better)
                if (numInFront < numBehind)
                {
                    numInFront += numCoplanar;
                }
                else
                {
                    numBehind += numCoplanar;
                }

                float score = K * numStraddling + (1.0f - K) * System.Math.Abs(numInFront - numBehind);
                if (score < bestScore)
                {
                    bestScore = score;
                    bestPlane = plane;
                }
            }
            return bestPlane;
        }

        /// <summary>
        /// Gets plane from polygon
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        private static Plane GetPlaneFromPolygon(Polygon polygon)
        {
            Microsoft.DirectX.Vector3 v1 = polygon.Points[1].Vector - polygon.Points[0].Vector;
            Microsoft.DirectX.Vector3 v2 = polygon.Points[2].Vector - polygon.Points[0].Vector;
            Microsoft.DirectX.Vector3 normal = Microsoft.DirectX.Vector3.Cross(v1, v2);
            normal.Normalize();
            float D = Vector3.Dot(normal, polygon.Points[0].Vector);
            D *= -1;

            Plane plane = new Plane(normal.X, normal.Y, normal.Z, D);
            return plane;
        }

        /// <summary>
        /// Classify point <c>_point</c> to a plane thickened by a fiven thickness epsilon
        /// </summary>
        /// <param name="_vertex"></param>
        /// <param name="_plane"></param>
        /// <returns></returns>
        private static PointOnPlanePosition ClassifyPointToPlane(Vertex _vertex, Plane _plane)
        {
            //compute signed distance of point from plane
            double dist = GeometricUtils.GetDistanceSigned(_plane, _vertex.Vector);
            if (dist > 0.01)
            {
                return PointOnPlanePosition.POINT_IN_FRONT_OF_PLANE; //in front of plane
            }
            if (dist < -0.01)
            {
                return PointOnPlanePosition.POINT_BEHIND_PLANE;//behind the plane
            }
            return PointOnPlanePosition.POINT_ON_PLANE;
        }

        /// <summary>
        /// Tests does the list of given polygons belongs to min. 2 different objects
        /// </summary>
        /// <param name="_polygons"></param>
        /// <returns></returns>
        public static bool DifferentObjectsExist(List<Polygon> _polygons)
        {
            int n = _polygons.Count;
            if (n > 1)
            {
                int id = _polygons[0].Points[0].ID;
                for (int i = 0; i < n; ++i)
                {
                    int m = _polygons[i].Points.Count;
                    for (int j = 0; j < m; ++j)
                    {
                        if (_polygons[i].Points[j].ID != id)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region --- Public methods ---

        /// <summary>
        /// Test if collision exist in BSPNode
        /// </summary>
        /// <returns></returns>
        public bool CollisionExist()
        {
            if (frontTree != null || backTree != null)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
