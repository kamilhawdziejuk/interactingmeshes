//Kamil Hawdziejuk
//03-06-2010

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractingMeshes
{
    /// <summary>
    /// Collision Manager
    /// </summary>
    public class CollisionManager
    {
        /// <summary>
        /// Main function, that tests if two geometricObjects collide
        /// </summary>
        /// <param name="_obj1"></param>
        /// <param name="_obj2"></param>
        /// <returns></returns>
        public static bool CollisionTest(GeometricObject _obj1, GeometricObject _obj2)
        {
            bool isCollision = false;
            Options.CollisionStage collisionLevel = Options.CollisionLevel;

            if (AcceptTest(collisionLevel, Options.CollisionStage.Box))
            {
                if (BoxCollision.TestOverlap(_obj1, _obj2, 0.01))
                {
                    isCollision = true;
                }
            }

            if (isCollision && AcceptTest(collisionLevel, Options.CollisionStage.GJK))
            {
                isCollision = false;
                if (GilbertJohnsonKeerthi.BodiesIntersect(_obj1.Points, _obj2.Points))
                {
                    isCollision = true;
                }
            }

            if (isCollision && AcceptTest(collisionLevel, Options.CollisionStage.BSP))
            {
                isCollision = false;
                if (MeshCollision.TestBspCollision(_obj1, _obj2))
                {
                    isCollision = true;
                }
            }

            return isCollision;
        }

        /// <summary>
        /// Does the level of collision testing has flag for testing this level
        /// </summary>
        /// <param name="_level"></param>
        /// <param name="_stage"></param>
        /// <returns></returns>
        public static bool AcceptTest(Options.CollisionStage _level, Options.CollisionStage _flag)
        {
            if ((_level & _flag) == _flag)
            {
                return true;
            }
            return false;
        }
    }
}
