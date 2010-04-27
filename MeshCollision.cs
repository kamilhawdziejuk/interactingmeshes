//22-04-2010

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
        /// <param name="_mesh1"></param>
        /// <param name="_mesh2"></param>
        /// <returns></returns>
        public static bool TestBSPCollision(GeometricObject _first, GeometricObject _second)
        {
            List<Polygon> polygons1 = _first.Polygons;
            List<Polygon> polygons2 = _second.Polygons;

            List<Polygon> both = new List<Polygon>(polygons1);
            both.AddRange(polygons2);

            BSPNode bspNode = BSPNode.BuildBSPTree(both, 3);
            if (bspNode == null)
            {
                return false;
            }
            return true;
            //return bspNode.CollisionExist();
        }
    }
}
