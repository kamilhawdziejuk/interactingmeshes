//20-02-2010

using System;
using System.Collections.Generic;
using System.Text;
using Direct3D = Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;


namespace InteractingMeshes
{
    /// <summary>
    /// Geometric object in scene, that has mesh & position, scale & rotation
    /// </summary>
    public class GeometricObject
    {
        #region --- Fields ---

        /// <summary>
        /// Mesh
        /// </summary>
        private Direct3D.Mesh mesh = null;

        /// <summary>
        /// Position
        /// </summary>
        private Vector3 position = new Vector3(0,0,0);

        public Matrix ScaleMatrix = Matrix.Identity;

        private Vector3 rotation = new Vector3(0, 0, 0);

        /// <summary>
        /// Orientation
        /// </summary>
        //public Matrix orientation = Matrix.Identity;
        //private Mogre.Mesh ogreMesh = null;
        public Matrix GeometryMatrix = Matrix.Identity;

        private string id = String.Empty;

        #endregion

        #region --- Public properties ---

        /// <summary>
        /// Position
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                if (this.position != value)
                {
                    Vector3 translation = value - this.position;
                    this.GeometryMatrix *= Matrix.Translation(translation);
                    this.position = value;
                }
            }
        }

        /// <summary>
        /// Rotation
        /// </summary>
        public Vector3 Rotation
        {
            get
            {
                return this.rotation;
            }
            set
            {
                if (this.rotation != value)
                {
                    Vector3 rotat = value - this.rotation;
                    Matrix rot = Matrix.RotationYawPitchRoll(rotat.Y, rotat.X, rotat.Z);// (value.Y, value.X, value.Z);
                    this.GeometryMatrix *= rot;
                    this.rotation = value;
                }
            }
        }

        /// <summary>
        /// Mesh
        /// </summary>
        public Direct3D.Mesh Mesh
        {
            get
            {
                return this.mesh;
            }
            set
            {
                this.mesh = value;
            }
        }

        /// <summary>
        /// Points of a mesh
        /// </summary>
        public List<Vector3> Points
        {
            get
            {
                return MeshUtils.GetPoints(this.Mesh, this.Position);
            }
        }

        /// <summary>
        /// Polygons (faces) of a mesh
        /// </summary>
        public List<Polygon> Polygons
        {
            get
            {
                return MeshUtils.GetPolygons(this.Mesh);
            }
        }

        #endregion 

        #region --- Constructing and destroying objects ---

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_position"></param>
        /// <param name="_scaleMatrix"></param>
        /// <param name="_rotationMatrix"></param>
        public GeometricObject(string _id, Vector3 _position, Matrix _scaleMatrix, Vector3 _rotation)
        {
            this.id = _id;
            this.Position = _position;
            this.ScaleMatrix = _scaleMatrix;
            this.Rotation = _rotation;
        }

        #endregion
    }
}
