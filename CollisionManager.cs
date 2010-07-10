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
        /// Tolerance
        /// </summary>
        public static double Tolerance = 0.01;

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

            foreach (ICollisionDetector detector in collisionDetectors)
            {
                if (AcceptTest(collisionLevel, detector.CollisionLevel))
                {
                   // CollisionTest test = new CollisionTest(detector.Name, _obj1, _obj2);
                    //test.Start();

                    if (detector.DetectCollision(_obj1, _obj2, Tolerance))
                    {
                        isCollision = true;
                        //break;
                    }
                    else
                    {
                        isCollision = false;
                    }
                    //test.Stop(isCollision);
                    //CollisionTestsManager.AddTest(test);
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
