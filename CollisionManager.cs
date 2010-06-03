using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractingMeshes
{
    public class CollisionManager
    {
        public static bool CollisionTest(GeometricObject _obj1, GeometricObject _obj2)
        {
            bool isCollision = false;
            if ((Options.CollisionLevel & Options.CollisionStage.Box) ==
                Options.CollisionStage.Box)
            {
                if (BoxCollision.TestOverlap(_obj1, _obj2, 0.01))
                {
                    isCollision = true;
                }
            }


            if (isCollision && (Options.CollisionLevel & Options.CollisionStage.GJK) ==
                Options.CollisionStage.GJK)
            {
                isCollision = false;
                if (GilbertJohnsonKeerthi.BodiesIntersect(_obj1.Points, _obj2.Points))
                {
                    isCollision = true;
                }
            }

            if (isCollision && (Options.CollisionLevel & Options.CollisionStage.BSP) ==
                Options.CollisionStage.BSP)
            {
                isCollision = false;
                if (MeshCollision.TestBspCollision(_obj1, _obj2))
                {
                    isCollision = true;
                }
            }

            return isCollision;
        }
    }
}
