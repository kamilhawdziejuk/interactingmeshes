//Kamil Hawdziejuk
//03-06-2010

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InteractingMeshes.Tests;

namespace InteractingMeshes
{
    /// <summary>
    /// Collision Manager
    /// </summary>
    public class CollisionManager
    {
        #region --- Static fields ---

        /// <summary>
        /// List of collided objects
        /// </summary>
        private static List<GeometricObject> collidedObjects = new List<GeometricObject>();


        /// <summary>
        /// List of collided objects
        /// </summary>
        public static List<GeometricObject> CollidedObjects
        {
            get
            {
                return collidedObjects;
            }
        }

        /// <summary>
        /// List of collision detectos
        /// </summary>
        private static List<ICollisionDetector> collisionDetectors = new List<ICollisionDetector>();

        /// <summary>
        /// Clears collided objects
        /// </summary>
        public static void ClearCollidedObjects()
        {
            collidedObjects.Clear();
        }

        /// <summary>
        /// Register the detectos
        /// </summary>
        /// <param name="collisionDetector"></param>
        public static void RegisterDetector(ICollisionDetector collisionDetector)
        {
            collisionDetectors.Add(collisionDetector);
        }

        #endregion

        #region --- Private methods ---

        /// <summary>
        /// Adds collided object
        /// </summary>
        /// <param name="obj"></param>
        private static void AddCollidedObject(GeometricObject obj)
        {
            if (!collidedObjects.Contains(obj))
            {
                collidedObjects.Add(obj);
            }
        }

        #endregion

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
            CollisionTest boxTest = new CollisionTest("BoxCollisionTesting", _obj1, _obj2);
            CollisionTest gjkTest = new CollisionTest("GJKCollisionTesting", _obj1, _obj2);
            CollisionTest bspXYZTest = new CollisionTest("BspXYZCollisionTesting", _obj1, _obj2);
            CollisionTest bspAutoTest = new CollisionTest("BspAUTOCollisionTesting", _obj1, _obj2);

            if (AcceptTest(collisionLevel, Options.CollisionStage.Box))
            {
                boxTest.Start();
                if (BoxCollision.TestOverlap(_obj1, _obj2, 0.01))
                {
                    isCollision = true;
                }
                boxTest.Stop(isCollision);
                CollisionTestsManager.AddTest(boxTest);
            }

            if (isCollision && AcceptTest(collisionLevel, Options.CollisionStage.GJK))
            {
                isCollision = false;
                gjkTest.Start();
                if (GilbertJohnsonKeerthi.BodiesIntersect(_obj1.Points, _obj2.Points))
                {
                    isCollision = true;
                }
                gjkTest.Stop(isCollision);
                CollisionTestsManager.AddTest(gjkTest);
            }

            if (isCollision && AcceptTest(collisionLevel, Options.CollisionStage.BSP))
            {
                isCollision = false;

                if (BSPNode.Autopartitioning)
                {
                    bspAutoTest.Start();
                }
                else
                {
                    bspXYZTest.Start();
                }
                if (MeshCollision.TestBspCollision(_obj1, _obj2))
                {
                    isCollision = true;
                }

                if (BSPNode.Autopartitioning)
                {
                    bspAutoTest.Stop(isCollision);
                    CollisionTestsManager.AddTest(bspAutoTest);
                }
                else
                {
                    bspXYZTest.Stop(isCollision);
                    CollisionTestsManager.AddTest(bspXYZTest);
                }
                
            }

            if (isCollision)
            {
                AddCollidedObject(_obj1);
                AddCollidedObject(_obj2);
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
