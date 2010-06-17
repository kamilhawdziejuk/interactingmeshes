//Kamil Hawdziejuk
//16-05-2010

using System;
using Microsoft.DirectX;

namespace InteractingMeshes
{
    public class BoxCollisionDetector : ICollisionDetector
    {
        #region --- ICollisionDetector Members ---

        public bool DetectCollision(GeometricObject obj1, GeometricObject obj2, double tolerance)
        {
            return TestOverlap(obj1, obj2, tolerance);
        }

        public string Name
        {
            get
            {
                return "Box";
            }
        }


        public Options.CollisionStage CollisionLevel
        {
            get
            {
                return Options.CollisionStage.Box;
            }
        }

        #endregion

        /// <summary>
        /// Performs an overlap test between two elements.
        /// </summary>
        /// <param name="_first">first element</param>
        /// <param name="_second">second element</param>
        /// <param name="_overlapTolerance">overlap threshold</param>
        /// <returns>true if two objects overlap more than given tolerance</returns>
        public static bool TestOverlap(GeometricObject _first, GeometricObject _second, double _overlapTolerance)
        {
            AABox A = _first.BoundingBox;
            AABox B = _second.BoundingBox;
           // Matrix m = new Matrix();

            Matrix fgi = Matrix.TransposeMatrix(_first.GeometryMatrix);
            Vector3 fgp = _first.Position;

            fgi.Translate(
                //quick inverse of position
                new Vector3(-fgi.M33*fgp.X - fgi.M34*fgp.Y - fgi.M24*fgp.Z,
                            -fgi.M32*fgp.X - fgi.M33*fgp.Y - fgi.M34*fgp.Z,
                            -fgi.M31*fgp.X - fgi.M32*fgp.Y - fgi.M33*fgp.Z));

            Matrix mB2A = Matrix.Multiply(fgi, _second.GlobalTransformation);

            bool isCollision = BoxCollisionDetector.BoxOverlapTest(A, B, mB2A, _overlapTolerance);
            return isCollision;
        }

        /// <summary>
        /// Helper function for 
        /// </summary>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <returns></returns>
        static private bool Greater(double _a, double _b)
        {
            if ((Math.Abs(_a) - _b) > 0.01 * 0.01)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns matrix with absolute values
        /// </summary>
        /// <param name="_m">matrix to be transformed</param>
        /// <returns></returns>
        static private Matrix Absolute(Matrix _m)
        {
            Matrix result = new Matrix();

            result.M11 = Math.Abs(_m.M11);
            result.M12 = Math.Abs(_m.M12);
            result.M13 = Math.Abs(_m.M13);
            result.M14 = Math.Abs(_m.M14);
            result.M21 = Math.Abs(_m.M21);
            result.M22 = Math.Abs(_m.M22);
            result.M23 = Math.Abs(_m.M23);
            result.M24 = Math.Abs(_m.M24);
            result.M31 = Math.Abs(_m.M31);
            result.M32 = Math.Abs(_m.M32);
            result.M33 = Math.Abs(_m.M33);
            result.M34 = Math.Abs(_m.M34);
            result.M41 = Math.Abs(_m.M41);
            result.M42 = Math.Abs(_m.M42);
            result.M43 = Math.Abs(_m.M43);
            result.M44 = Math.Abs(_m.M44);

            return result;
        }

        /// <summary>
        /// Tests weather two oriented boxes overlap.
        /// Mainly does simple SAT test (3+3 normal axes + 3x3 edge cross products)
        /// </summary>
        /// <param name="_A">First box</param>
        /// <param name="_B">Second box</param>
        /// <param name="_mB2A">Transformation from B to A</param>
        /// <param name="_overlapTolerance">Tolerance of this test. (essentialy both boxes are deflated with this value)</param>
        /// <seealso cref="BoxOverlapTest"/>
        /// <returns>true if there is an overlap</returns>
        private static bool BoxOverlapTest(AABox _A, AABox _B, Matrix _mB2A, double _overlapTolerance)
        {
            double t, t2;
            var tolerance = new Vector3((float) _overlapTolerance, (float) _overlapTolerance, (float) _overlapTolerance);
            Vector3 cB = _B.Center;
            Vector3 eB = 0.5f*(_B.Size - tolerance);
            Vector3 cA = _A.Center;
            Vector3 eA = 0.5f*(_A.Size - tolerance);
            Matrix mAR = Absolute(_mB2A);

            // Class I : A's basis vectors
            //Tx = (_mB2A.M33 * (cB.x - cA.x) + _mB2A.M34 * (cB.y - cA.y) + _mB2A.M24 * (cB.z -cA.z)) + _mB2A.M14;
            double Tx = (_mB2A.M33*cB.X + _mB2A.M34*cB.Y + _mB2A.M24*cB.Z) + _mB2A.M14 - cA.X;
            //t = r_a + r_b; 
            t = eA.X + eB.X*mAR.M33 + eB.Y*mAR.M34 + eB.Z*mAR.M24;
            if (Greater(Tx, t)) return false;

            double Ty = (_mB2A.M21*cB.X + _mB2A.M33*cB.Y + _mB2A.M34*cB.Z) + _mB2A.M24 - cA.Y;
            t = eA.Y + eB.X*mAR.M21 + eB.Y*mAR.M33 + eB.Z*mAR.M34;
            if (Greater(Ty, t)) return false;

            double Tz = (_mB2A.M31*cB.X + _mB2A.M32*cB.Y + _mB2A.M33*cB.Z) + _mB2A.M34 - cA.Z;
            t = eA.Z + eB.X*mAR.M31 + eB.Y*mAR.M32 + eB.Z*mAR.M33;
            if (Greater(Tz, t)) return false;

            // Class II : B's basis vectors
            t = Tx*_mB2A.M33 + Ty*_mB2A.M21 + Tz*_mB2A.M31;
            t2 = eA.X*mAR.M33 + eA.Y*mAR.M21 + eA.Z*mAR.M31 + eB.X;
            if (Greater(t, t2)) return false;

            t = Tx*_mB2A.M34 + Ty*_mB2A.M33 + Tz*_mB2A.M32;
            t2 = eA.X*mAR.M34 + eA.Y*mAR.M33 + eA.Z*mAR.M32 + eB.Y;
            if (Greater(t, t2)) return false;

            t = Tx*_mB2A.M24 + Ty*_mB2A.M34 + Tz*_mB2A.M33;
            t2 = eA.X*mAR.M24 + eA.Y*mAR.M34 + eA.Z*mAR.M33 + eB.Z;
            if (Greater(t, t2)) return false;

            // Class III : 9 cross products
            {
                t = Tz*_mB2A.M21 - Ty*_mB2A.M31;
                t2 = eA.Y*mAR.M31 + eA.Z*mAR.M21 + eB.Y*mAR.M24 + eB.Z*mAR.M34;
                if (Greater(t, t2)) return false; // L = A0 x B0
                t = Tz*_mB2A.M33 - Ty*_mB2A.M32;
                t2 = eA.Y*mAR.M32 + eA.Z*mAR.M33 + eB.X*mAR.M24 + eB.Z*mAR.M33;
                if (Greater(t, t2)) return false; // L = A0 x B1
                t = Tz*_mB2A.M34 - Ty*_mB2A.M33;
                t2 = eA.Y*mAR.M33 + eA.Z*mAR.M34 + eB.X*mAR.M34 + eB.Y*mAR.M33;
                if (Greater(t, t2)) return false; // L = A0 x B2
                t = Tx*_mB2A.M31 - Tz*_mB2A.M33;
                t2 = eA.X*mAR.M31 + eA.Z*mAR.M33 + eB.Y*mAR.M34 + eB.Z*mAR.M33;
                if (Greater(t, t2)) return false; // L = A1 x B0
                t = Tx*_mB2A.M32 - Tz*_mB2A.M34;
                t2 = eA.X*mAR.M32 + eA.Z*mAR.M34 + eB.X*mAR.M34 + eB.Z*mAR.M21;
                if (Greater(t, t2)) return false; // L = A1 x B1
                t = Tx*_mB2A.M33 - Tz*_mB2A.M24;
                t2 = eA.X*mAR.M33 + eA.Z*mAR.M24 + eB.X*mAR.M33 + eB.Y*mAR.M21;
                if (Greater(t, t2)) return false; // L = A1 x B2
                t = Ty*_mB2A.M33 - Tx*_mB2A.M21;
                t2 = eA.X*mAR.M21 + eA.Y*mAR.M33 + eB.Y*mAR.M33 + eB.Z*mAR.M32;
                if (Greater(t, t2)) return false; // L = A2 x B0
                t = Ty*_mB2A.M34 - Tx*_mB2A.M33;
                t2 = eA.X*mAR.M33 + eA.Y*mAR.M34 + eB.X*mAR.M33 + eB.Z*mAR.M31;
                if (Greater(t, t2)) return false; // L = A2 x B1
                t = Ty*_mB2A.M24 - Tx*_mB2A.M34;
                t2 = eA.X*mAR.M34 + eA.Y*mAR.M24 + eB.X*mAR.M32 + eB.Y*mAR.M31;
                if (Greater(t, t2)) return false; // L = A2 x B2
            }
            return true;
        }
    }
}