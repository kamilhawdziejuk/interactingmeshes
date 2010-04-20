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

        public static double Distance(Plane _plane, Vector3 _vector)
        {
            double top = Math.Abs(_plane.A * _vector.X + _plane.B * _vector.Y + _plane.C * _vector.Z + _plane.D);
            double bottom = Math.Sqrt(_plane.A * _plane.A + _plane.B * _plane.B + _plane.C * _plane.C);
         
            return top / bottom;
        }
    }
}
