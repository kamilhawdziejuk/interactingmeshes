//20.04.2010

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;

namespace InteractingMeshes
{
    public class GeometricUtils
    {
        public static Vector3 GetNormal(Plane _plane)
        {
            Vector3 normal = new Vector3(_plane.A, _plane.B, _plane.C);
            normal.Normalize();
            return normal;
        }


        public static Vector3 GetPointOnPlane(Plane _plane)
        {
            if (_plane.A != 0)
            {
                return new Vector3(-_plane.D/_plane.A, 0, 0);
            }
            else if (_plane.B != 0)
            {
                return new Vector3(0, -_plane.D / _plane.B, 0);
            }
            else if (_plane.C != 0)
            {
                return new Vector3(0, 0, -_plane.D / _plane.C);
            }
            else if (_plane.D == 0)
            {
                return new Vector3(0,0,0);
            }
            else
            {
               throw  new Exception("Plaszczyzna sprzeczna");
               
            }
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

        public static  double GetD(Plane _plane)
        {
            double n = Math.Sqrt(_plane.A*_plane.A + _plane.B*_plane.B + _plane.C*_plane.C);
            double d = _plane.D/n;
            return n;
        }


        public static double Distance(Plane _plane, Vector3 _vector)
        {
            double top = Math.Abs(_plane.A * _vector.X + _plane.B * _vector.Y + _plane.C * _vector.Z + _plane.D);
            double bottom = Math.Sqrt(_plane.A * _plane.A + _plane.B * _plane.B + _plane.C * _plane.C);
         
            return top / bottom;
        }
    }
}
