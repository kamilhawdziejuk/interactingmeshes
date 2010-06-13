//22-04-2010

using System.Collections.Generic;
using System;

namespace InteractingMeshes
{
    public class MeshCollision
    {

        /// <summary>
        /// Main function for testing mesh-mesh collision
        /// </summary>
        /// <param name="_first"></param>
        /// <param name="_second"></param>
        /// <returns></returns>
        public static bool TestBspCollision(GeometricObject _first, GeometricObject _second)
        {
            List<Polygon> polygons1 = _first.Polygons;
            List<Polygon> polygons2 = _second.Polygons;

            var both = new List<Polygon>(polygons1);
            both.AddRange(polygons2);
            BSPNode bspNode = null;

            int max = BSPNode.Depth;
            if (polygons1.Count < polygons2.Count)
            {
                bspNode = BSPNode.BuildBSPTree(both, 3, _first.Polygons[0].Points[0].ID);
            }
            else
            {
                bspNode = BSPNode.BuildBSPTree(both, 3, _second.Polygons[0].Points[0].ID);
            }

            if (max < BSPNode.Depth)
            {
                Console.WriteLine(BSPNode.Depth);
            }
            return bspNode != null;
        }
    }
}