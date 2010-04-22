//11-04-2010
using System;
using System.Collections.Generic;
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
        public static readonly int MaxDepth = 8;

        public static readonly int MinLeafSize = -1;

        public BSPNode parent;
        public BSPNode frontTree = null;
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
        public static BSPNode BuildBSPTree(List<Polygon> _polygons, int _depth)
        {
            if (_polygons.Count == 0)
            {
                return null;
            }

            int numPolygons = _polygons.Count;

            if (_depth >= MaxDepth || numPolygons <= MinLeafSize)
            {
                return new BSPNode(_polygons);
            }


            Plane splitPlane = PickSplittingPlane(_polygons);

            List<Polygon> frontList = new List<Polygon>();
            List<Polygon> backList = new List<Polygon>();

            //test each polygon against the dividing plane, adding them to the front list, back list, or both, as appropriate
            for (int i = 0; i < numPolygons; ++i)
            {
                Polygon poly = _polygons[i];
                switch (ClassifyPolygonToPlane(poly, splitPlane))
                {
                    case PolygonOnPlanePosition.POLYGON_COPLANAR_WITH_PLANE: //COPLANAR_WITH_PLANE, next case
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
            // Recursively build child subtrees and return new tree root combining them
            BSPNode frontTree = BuildBSPTree(frontList, _depth + 1);
            BSPNode backTree = BuildBSPTree(backList, _depth + 1);
            return new BSPNode(frontTree, backTree);
            //return null;
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
            int numFront = 0, numBack = 0;
            List<Microsoft.DirectX.Vector3> frontVerts = new List<Microsoft.DirectX.Vector3>();
            List<Microsoft.DirectX.Vector3> backVerts = new List<Microsoft.DirectX.Vector3>();

            // Test all edges (a, b) starting with edge from last to first vertex
            int numVerts = poly.Points.Count;
            Microsoft.DirectX.Vector3 a = poly.Points[numVerts - 1];
            PointOnPlanePosition aSide = ClassifyPointToPlane(a, _splitPlane);
            // Loop over all edges given by vertex pair (n - 1, n)
            for (int n = 0; n < numVerts; n++)
            {
                Microsoft.DirectX.Vector3 b = poly.Points[n];// .GetVertex(n);
                PointOnPlanePosition bSide = ClassifyPointToPlane(b, _splitPlane);
                if (bSide == PointOnPlanePosition.POINT_IN_FRONT_OF_PLANE)
                {
                    if (aSide == PointOnPlanePosition.POINT_BEHIND_PLANE)
                    {
                        // Edge (a, b) straddles, output intersection point to both sides
                        Microsoft.DirectX.Vector3 i = IntersectEdgeAgainstPlane(a, b, _splitPlane);
                        /////////////////assert(ClassifyPointToPlane(i, _splitPlane) == POINT_ON_PLANE);
                        frontVerts.Add(i);
                        backVerts.Add(i);
                       // frontVerts[numFront++] = backVerts[numBack++] = i;
                    }
                    // In all three cases, output b to the front side
                    frontVerts.Add(b);
                    //frontVerts[numFront++] = b;
                }
                else if (bSide == PointOnPlanePosition.POINT_BEHIND_PLANE)
                {
                    if (aSide == PointOnPlanePosition.POINT_IN_FRONT_OF_PLANE)
                    {
                        // Edge (a, b) straddles plane, output intersection point
                        Microsoft.DirectX.Vector3 i = IntersectEdgeAgainstPlane(a, b, _splitPlane);
                        //assert(ClassifyPointToPlane(i, plane) == POINT_ON_PLANE);
                        frontVerts.Add(i);
                        backVerts.Add(i);
                        //frontVerts[numFront++] = backVerts[numBack++] = i;
                    }
                    else if (aSide == PointOnPlanePosition.POINT_ON_PLANE)
                    {
                        // Output a when edge (a, b) goes from ‘
                        //backVerts[numBack++] = a;
                        backVerts.Add(a);
                    }
                    // In all three cases, output b to the back side
                    //backVerts[numBack++] = b;
                    backVerts.Add(b);
                }
                else
                {
                    // b is on the plane. In all three cases output b to the front side
                   // frontVerts[numFront++] = b;
                    frontVerts.Add(b);
                    // In one case, also output b to back side
                    if (aSide == PointOnPlanePosition.POINT_BEHIND_PLANE)
                    {
                        backVerts.Add(b);
                    }
                        //backVerts[numBack++] = b;
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

        private static Microsoft.DirectX.Vector3 IntersectEdgeAgainstPlane(Microsoft.DirectX.Vector3 A, Microsoft.DirectX.Vector3 B, Plane _splitPlane)
        {
            Microsoft.DirectX.Vector3 V = A - B;
            Microsoft.DirectX.Vector3 N = GeometricUtils.GetNormal(_splitPlane);
            double D = _splitPlane.D;
            double W = (Microsoft.DirectX.Vector3.Dot(N, B) + D) / Microsoft.DirectX.Vector3.Dot(N, V);
            //Wyznaczamy punkt przecięcia P’:
            return (B - Microsoft.DirectX.Vector3.Multiply(V, (float)W));// W * V);
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
                Microsoft.DirectX.Vector3 p = _poly.Points[i];
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
        private static Plane PickSplittingPlane(List<Polygon> _polygons)
        {
            // Blend factor for optimizing for balance or splits (should be tweaked)
            float K = 0.2f;
            // Variables for tracking best splitting plane seen so far
            Plane bestPlane = Plane.Empty;
            float bestScore = float.MaxValue;
            // Try the plane of each polygon as a dividing plane
            for (int i = 0; i < _polygons.Count; i++)
            {
                int numInFront = 0, numBehind = 0, numStraddling = 0;
                Plane plane = GetPlaneFromPolygon(_polygons[i]);

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
                float score = K * numStraddling + (1.0f - K) * System.Math.Abs(numInFront - numBehind);
                if (score < bestScore)
                {
                    bestScore = score;
                    bestPlane = plane;
                }
            }
            return bestPlane;
        }

        private static Plane GetPlaneFromPolygon(Polygon polygon)
        {
            Microsoft.DirectX.Vector3 v1 = polygon.Points[1] - polygon.Points[0];
            Microsoft.DirectX.Vector3 v2 = polygon.Points[2] - polygon.Points[0];
            Microsoft.DirectX.Vector3 normal = Microsoft.DirectX.Vector3.Cross(v1, v2);
            normal.Normalize();
            float D = normal.X * polygon.Points[0].X + normal.Y * polygon.Points[0].Y + normal.Z * polygon.Points[0].Z;
            D *= -1;

            Plane plane = new Plane(normal.X, normal.Y, normal.Z, D);// GeometricUtils.GetNormal( new Plane(new Vector3(normal.X, normal.Y, normal.Z), new Vector3(polygon.Points[0].X, polygon.Points[0].Y, polygon.Points[0].Z));
            return plane;
        }

        /// <summary>
        /// Classify point <c>_point</c> to a plane thickened by a fiven thickness epsilon
        /// </summary>
        /// <param name="_point"></param>
        /// <param name="_plane"></param>
        /// <returns></returns>
        private static PointOnPlanePosition ClassifyPointToPlane(Microsoft.DirectX.Vector3 _point, Plane _plane)
        {
            //compute signed distance of point from plane
            double dist = Vector3.Dot(GeometricUtils.GetNormal(_plane), _point) - _plane.D;
            //classify p based on the signed distance
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

        #endregion
    }
}
