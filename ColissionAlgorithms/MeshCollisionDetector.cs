//22-04-2010

using System.Collections.Generic;
using System;
using Microsoft.DirectX;

namespace InteractingMeshes
{
    public class MeshCollisionDetector : ICollisionDetector
    {

        #region --- CollisionDetector Members ---`

        public string Name
        {
            get
            {
                return "BSP";
            }
        }

        public Options.CollisionStage CollisionLevel
        {
            get
            {
                return Options.CollisionStage.BSP;
            }
        }

        /// <summary>
        /// Main function for testing mesh-mesh collision
        /// </summary>
        /// <param name="_first"></param>
        /// <param name="_second"></param>
        /// <returns></returns>
        public bool DetectCollision(GeometricObject _first, GeometricObject _second, double tolerance)
        {
            List<Polygon> polygons1 = _first.Polygons;
            List<Polygon> polygons2 = _second.Polygons;

            var both = new List<Polygon>(polygons1);
            both.AddRange(polygons2);
            BSPNode bspNode = null;

            int max = BSPNode.Depth;
            Plane p = Plane.Empty;
            if (polygons1.Count < polygons2.Count)
            {
                bspNode = BSPNode.BuildBSPTree(ref both, 3, _first.Polygons[0].Points[0].ID, p);
            }
            else
            {
                bspNode = BSPNode.BuildBSPTree(ref both, 3, _second.Polygons[0].Points[0].ID, p);
            }

            if (max < BSPNode.Depth)
            {
                Console.WriteLine(BSPNode.Depth);
            }
            return bspNode != null;
        }

        #endregion
    }
}