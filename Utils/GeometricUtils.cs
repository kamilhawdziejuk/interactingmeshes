//20.04.2010

using System;
using Microsoft.DirectX;

namespace InteractingMeshes
{
    public class GeometricUtils
    {
        public static Vector3 GetNormal(Plane _plane)
        {
            var normal = new Vector3(_plane.A, _plane.B, _plane.C);
            normal.Normalize();
            return normal;
        }

        public static bool PlaneEquality(Plane _plane1, Plane _plane2)
        {
            if (_plane2 == null || _plane1 == null)
            {
                return false;
            }
            if (Math.Abs(_plane1.A - _plane2.A) + Math.Abs(_plane1.B - _plane2.B) + Math.Abs(_plane1.C - _plane2.C) + Math.Abs(_plane1.D - _plane2.D) < 0.001)
            {
                return true;
            }
            return false;
        }

        public static Vector3 GetPointOnPlane(Plane _plane)
        {
            if (_plane.A != 0)
            {
                return new Vector3(-_plane.D/_plane.A, 0, 0);
            }
            else if (_plane.B != 0)
            {
                return new Vector3(0, -_plane.D/_plane.B, 0);
            }
            else if (_plane.C != 0)
            {
                return new Vector3(0, 0, -_plane.D/_plane.C);
            }
            else if (_plane.D == 0)
            {
                return new Vector3(0, 0, 0);
            }
            else
            {
                throw new Exception("Plaszczyzna sprzeczna");
            }
        }

        /// <summary>
        ///		Used when a Vector3 is multiplied by another vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 MultiplyVectors(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X*right.X, left.Y*right.Y, left.Z*right.Z);
        }

        public static bool EqualsVectors(Vector3 _first, Vector3 _second)
        {
            if (Math.Abs(_first.X - _second.X) + Math.Abs(_first.Y - _second.Y) + Math.Abs(_first.Z - _second.Z) < 0.01)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///		Compares the supplied vector and updates it's x/y/z components of they are lower in _value.
        /// </summary>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static Vector3 FloorVector(Vector3 _this, Vector3 compare)
        {
            var result = new Vector3(_this.X, _this.Y, _this.Z);

            if (compare.X < _this.X)
                result.X = compare.X;
            if (compare.Y < _this.Y)
                result.Y = compare.Y;
            if (compare.Z < _this.Z)
                result.Z = compare.Z;
            return result;
        }

        /// <summary>
        ///		Compares the supplied vector and updates it's x/y/z components of they are higher in _value.
        /// </summary>
        /// <param name="compare"></param>
        public static Vector3 CeilVector(Vector3 _this, Vector3 compare)
        {
            var result = new Vector3(_this.X, _this.Y, _this.Z);
            if (compare.X > _this.X)
                result.X = compare.X;
            if (compare.Y > _this.Y)
                result.Y = compare.Y;
            if (compare.Z > _this.Z)
                result.Z = compare.Z;

            return result;
        }

        /// <summary>
        ///		matrix * vector [3x3 * 3x1 = 3x1]
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Vector3 MultiplyMatrixVector(Matrix matrix, Vector3 vector)
        {
            var product = new Vector3();

            product.X = matrix.M11*vector.X + matrix.M12*vector.Y + matrix.M13*vector.Z;
            product.Y = matrix.M21*vector.X + matrix.M22*vector.Y + matrix.M23*vector.Z;
            product.Z = matrix.M31*vector.X + matrix.M32*vector.Y + matrix.M33*vector.Z;

            return product;
        }

        public static Matrix MultiplyFactorMatrix(double _factor, Matrix _matrix)
        {
            var result = new Matrix();
            result.M11 *= (float) _factor;
            result.M12 *= (float) _factor;
            result.M13 *= (float) _factor;
            result.M21 *= (float) _factor;
            result.M22 *= (float) _factor;
            result.M23 *= (float) _factor;
            result.M31 *= (float) _factor;
            result.M32 *= (float) _factor;
            result.M33 *= (float) _factor;
            return result;
        }

        /// <summary>
        /// Zwraca odleglosc ze znakiem punktu od plaszczyzny.
        /// Jezeli punkt jest nad plaszczyzna (dodatnia normalna) to odleglosc bedzie dodatnia.
        /// </summary>
        public static double GetDistanceSigned(Plane _plane, Vector3 _point)
        {
            Vector3 w = _point - GetPointOnPlane(_plane);
            double d = Vector3.Dot(w, GetNormal(_plane));
            return d;
        }

        public static double GetD(Plane _plane)
        {
            double n = Math.Sqrt(_plane.A*_plane.A + _plane.B*_plane.B + _plane.C*_plane.C);
            double d = _plane.D/n;
            return n;
        }


        public static double Distance(Plane _plane, Vector3 _vector)
        {
            double top = Math.Abs(_plane.A*_vector.X + _plane.B*_vector.Y + _plane.C*_vector.Z + _plane.D);
            double bottom = Math.Sqrt(_plane.A*_plane.A + _plane.B*_plane.B + _plane.C*_plane.C);

            return top/bottom;
        }
    }
}