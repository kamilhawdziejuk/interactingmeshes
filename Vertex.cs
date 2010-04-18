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
        #region --- Static fields ---

        /// <summary>
        /// Vertex format
        /// </summary>
        public static readonly Direct3D.VertexFormats FVF_Flags = Direct3D.VertexFormats.Position | Direct3D.VertexFormats.Diffuse;

        #endregion

        #region --- Public fields ---

        /// <summary>
        /// Position x of vertex in 3D space
        /// </summary>
        public float x;
        /// <summary>
        /// Position y of vertex in 3D space
        /// </summary>
        public float y;
        /// <summary>
        /// Position z of vertex in 3D space
        /// </summary>
        public float z;
        /// <summary>
        /// Diffuse color of vertex
        /// </summary>
        public int color;

        #endregion

        #region --- Creating and destroying objects ---

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

        #endregion

        #region --- Public properties ---

        /// <summary>
        /// Vector
        /// </summary>
        public Vector3 Vector
        {
            get
            {
                return new Vector3(x, y, z);
            }
        }

        #endregion

    };
}
