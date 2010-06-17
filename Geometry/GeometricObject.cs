//20-02-2010

using System;
using System.Collections.Generic;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace InteractingMeshes
{
    /// <summary>
    /// Geometric object in scene, that has mesh & position, scale & rotation
    /// </summary>
    public class GeometricObject
    {
        #region --- Fields ---

        /// <summary>
        /// Orientation
        /// </summary>
        public Matrix GeometryMatrix = Matrix.Identity;

        public Matrix ScaleMatrix = Matrix.Identity;

        /// <summary>
        /// Id
        /// </summary>
        public string id = String.Empty;

        /// <summary>
        /// Position
        /// </summary>
        private Vector3 position = new Vector3(0, 0, 0);

        /// <summary>
        /// Rotation
        /// </summary>
        private Vector3 rotation = new Vector3(0, 0, 0);

        /// <summary>
        /// An AABox
        /// </summary>
        private AABox boundingBox = AABox.UnitBox;

        private string name = String.Empty;

        /// <summary>
        /// Is ActiveObject changed? Flag is activated every time we move,rotate,add object or when we change algoritms for collision testing
        /// Collisions are tested only if flag is active, for  optimizaions:)
        /// </summary>
        public bool IsChanged = false;

        #endregion

        #region --- Public properties ---


        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// BoundingBox
        /// </summary>
        public AABox BoundingBox
        {
            get
            {
                //if (this.boundingBox.Equals(AABox.UnitBox))
                {
                    Vector3 minPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
                    Vector3 maxPoint = new Vector3(float.MinValue, float.MinValue, float.MinValue);

                    foreach (var point in this.Points)
                    {
                        if (point.X < minPoint.X)
                        {
                            minPoint.X = point.X;
                        }
                        if (point.Y < minPoint.Y)
                        {
                            minPoint.Y = point.Y;
                        }
                        if (point.Z < minPoint.Z)
                        {
                            minPoint.Z = point.Z;
                        }

                        if (point.X > maxPoint.X)
                        {
                            maxPoint.X = point.X;
                        }
                        if (point.Y > maxPoint.Y)
                        {
                            maxPoint.Y = point.Y;
                        }
                        if (point.Z > maxPoint.Z)
                        {
                            maxPoint.Z = point.Z;
                        }
                    }

                    this.boundingBox = new AABox(minPoint, maxPoint);
                }
                return this.boundingBox;
            }
        }

        /// <summary>
        /// Transformacja obiektu względem sceny
        /// </summary>
        public Matrix GlobalTransformation
        {
            get
            {
                Matrix m = this.GeometryMatrix;
                m.Translate(this.Position);
                return m;
            }
        }

        /// <summary>
        /// Position
        /// </summary>
        public Vector3 Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    Vector3 translation = value - position;
                    GeometryMatrix *= Matrix.Translation(translation);
                    position = value;
                    this.IsChanged = true;
                }
            }
        }

        /// <summary>
        /// Rotation
        /// </summary>
        public Vector3 Rotation
        {
            get { return rotation; }
            set
            {
                if (rotation != value)
                {
                    Vector3 rotat = value - rotation;
                    Matrix rot = Matrix.RotationX(1);// .RotationYawPitchRoll(rotat.Y, rotat.X, rotat.Z);
                    GeometryMatrix *= rot;
                    rotation = value;
                    this.IsChanged = true;
                }
            }
        }

        /// <summary>
        /// Mesh
        /// </summary>
        public Mesh Mesh { get; set; }

        /// <summary>
        /// Points of a mesh
        /// </summary>
        public List<Vector3> Points
        {
            get { return MeshUtils.GetPoints(Mesh, Position); }
        }

        /// <summary>
        /// Polygons (faces) of a mesh
        /// </summary>
        public List<Polygon> Polygons
        {
            get { return MeshUtils.GetPolygons(Mesh, Position); }
        }

        #endregion

        #region --- Constructing and destroying objects ---

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_position"></param>
        /// <param name="_scaleMatrix"></param>
        /// <param name="_rotation"></param>
        public GeometricObject(string _id, Vector3 _position, Matrix _scaleMatrix, Vector3 _rotation)
        {
            id = _id;
            Position = _position;
            ScaleMatrix = _scaleMatrix;
            Rotation = _rotation;
        }

        #endregion

        #region --- Overloading methods ---

        public override string ToString()
        {
            return this.id.ToString();
        }

        #endregion
    }
}