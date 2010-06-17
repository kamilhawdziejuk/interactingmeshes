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
    public struct Vertex : IEquatable<Vertex>
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_vector"></param>
        /// <param name="_id"></param>
        public Vertex(Vector3 _vector, int _id)
        {
            x = _vector.X;
            y = _vector.Y;
            z = _vector.Z;
            color = _id;
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
            set
            {
                this.x = value.X;
                this.y = value.Y;
                this.z = value.Z;
            }
        }

        /// <summary>
        /// ID of a vertex - means the object, which contains that vertex
        /// </summary>
        public int ID
        {
            get
            {
                return color;
            }
        }

        #endregion


        #region IEquatable<Vertex> Members

        public bool Equals(Vertex other)
        {
            if (Math.Abs(this.Vector.X - other.Vector.X) + Math.Abs(this.Vector.Y - other.Vector.Y) + Math.Abs(this.Vector.Z - other.Vector.Z) < 0.01)
            {
                return true;
            }
            return false;
        }

        #endregion
    };
}
