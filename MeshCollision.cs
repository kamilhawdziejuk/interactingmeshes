﻿//22-04-2010

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Direct3D = Microsoft.DirectX.Direct3D;

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

            List<Polygon> both = new List<Polygon>(polygons1);
            both.AddRange(polygons2);
            BSPNode bspNode = null;
            if (polygons1.Count < polygons2.Count)
            {
                bspNode = BSPNode.BuildBSPTree(both, 3, _first.Polygons[0].Points[0].ID);
            }
            else
            {
                bspNode = BSPNode.BuildBSPTree(both, 3, _second.Polygons[0].Points[0].ID);  

            }

            return bspNode != null;
        }
    }
}
