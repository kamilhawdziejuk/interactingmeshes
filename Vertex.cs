//20-02-2010

using System;
using System.Collections.Generic;
using System.Text;
using Direct3D = Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace InteractingMeshes
{
    /// <summary>
    /// Vertex class
    /// </summary>
    public struct Vertex
    {
        public float x, y, z; // Position of vertex in 3D space
        public int color;     // Diffuse color of vertex

        /// <summary>
        /// Contruktor
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_z"></param>
        /// <param name="_color"></param>
        public Vertex(float _x, float _y, float _z, int _color)
        {
            x = _x; y = _y; z = _z;
            color = _color;
        }

        public Vector3 Vector
        {
            get
            {
                return new Vector3(x, y, z);
            }
        }

        public static readonly Direct3D.VertexFormats FVF_Flags = Direct3D.VertexFormats.Position | Direct3D.VertexFormats.Diffuse;
    };
}
