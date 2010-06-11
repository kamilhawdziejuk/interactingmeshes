//Kamil Hawdziejuk
//09-06-2010

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractingMeshes.Tests
{
    public class CollisionTest : IEquatable<CollisionTest>
    {
        #region --- Static fields ---

        //private static TimeSpan SumCollidedTime = TimeSpan.Zero;
        //private static TimeSpan SumNotCollidedTime = TimeSpan.Zero;

        //private static int CountCollided = 0;
        //private static int CountNotCollided = 0;

        #endregion

        #region --- Private fields ---

        private string description = String.Empty;
        private DateTime startTime;
        private DateTime stopTime;
        private bool result;
        private GeometricObject obj1;
        private GeometricObject obj2;

        #endregion

        #region --- Public properties ---

        /// <summary>
        /// Starts the test
        /// </summary>
        public void Start()
        {
            startTime = DateTime.Now;
        }

        /// <summary>
        /// Stops the test
        /// </summary>
        public void Stop(bool _result)
        {
            stopTime = DateTime.Now;
            this.result = _result;
            //if (_result)
            //{
            //    CountCollided++;
            //    SumCollidedTime += this.Duration;
            //}
            //else
            //{
            //    CountNotCollided++;
            //    SumNotCollidedTime += this.Duration;
            //}
        }

        #endregion

        #region --- Public properties ---

        /// <summary>
        /// Gets the duration of a test
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                return stopTime - startTime;
            }
        }

        /// <summary>
        /// Gets the result of a test
        /// </summary>
        public bool Result
        {
            get
            {
                return this.result;
            }
        }

        #endregion

        #region --- Creating and destroying objects ---

        /// <summary>
        /// Construktor
        /// </summary>
        /// <param name="_description"></param>
        /// <param name="_obj1"></param>
        /// <param name="_obj2"></param>
        public CollisionTest(string _description, GeometricObject _obj1, GeometricObject _obj2)
        {
            this.description = _description;
            this.obj1 = _obj1;
            this.obj2 = _obj2;
        }

        #endregion

        #region --- Methods overloaded ---

        public override string ToString()
        {
            string result =  "[" + description + "]   " + this.obj1.ToString() + "(" + this.obj1.Polygons.Count.ToString() + ") + " + this.obj2.ToString() + "(" + this.obj2.Polygons.Count.ToString() + ") result=" + this.result.ToString() + "   duration=" + this.Duration.ToString();
            return result;
        }

        #endregion

        #region --- Static methods ---

        //public static string PresentsStatistics()
        //{
        //    string result = String.Empty;
        //   // result =  "[" + description + "]   " + this.obj1.ToString() + "(" + this.obj1.Polygons.Count.ToString() + ") + " + this.obj2.ToString() + "(" + this.obj2.Polygons.Count.ToString() + ") result="
        //}

        #endregion

        #region IEquatable<CollisionTest> Members

        public bool Equals(CollisionTest other)
        {
            if (other.obj1 == this.obj1 && other.obj2 == this.obj2 && other.result == this.result && this.description.Equals(other.description))
            {
                return true;
            }
            return false;
            //throw new NotImplementedException();
        }

        #endregion
    }
}
