//05-05-2010
//Kamil Hawdziejuk

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
using Direct3D = Microsoft.DirectX.Direct3D;

namespace InteractingMeshes
{
    /// <summary>
    /// For managing objects in the application
    /// </summary>
    public class ObjectsManager
    {
        #region --- Private fields ---

        /// <summary>
        /// Objects in scene
        /// </summary>
        private List<GeometricObject> objects = new List<GeometricObject>();

        /// <summary>
        /// Active object to manipulate
        /// </summary>
        private GeometricObject activeObject = null;

        /// <summary>
        /// Movement of active object
        /// </summary>
        public Vector3 Move = new Vector3(0, 0, 0);

        /// <summary>
        /// Rotation of active objects on all axies
        /// </summary>
        public Vector3 Rotate = new Vector3(0, 0, 0);

        /// <summary>
        /// Device
        /// </summary>
        private readonly Device device = null;

        #endregion


        #region --- Constructing and destroying objects ---

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_device"></param>
        public ObjectsManager(Device _device)
        {
            this.device = _device;
        }

        #endregion

        #region --- Public properties ---

        /// <summary>
        ///  Operating object
        /// </summary>
        public GeometricObject ActiveObject
        {
            get
            {
                return this.activeObject;
            }
            set
            {
                if (this.activeObject != null)
                {
                    this.activeObject.Mesh = MeshUtils.ChangeMeshColor(this.activeObject.Mesh, Color.White, device);
                }
                if (value != null)
                {
                    value.Mesh = MeshUtils.ChangeMeshColor(value.Mesh, Color.Green, device);
                    this.activeObject = value;
                }
            }
        }

        public List<GeometricObject> Objects
        {
            get
            {
                return this.objects;
            }
            set
            {
                this.objects = value;
            }
        }

        #endregion

        #region --- Public methods ---

        public bool RemoveActiveObject()
        {
            this.objects.Remove(this.ActiveObject);
            this.ActiveObject = this.GetObject(0);
            return true;
        }

        public GeometricObject GetObject(int nr)
        {
            if (this.Objects.Count == 0)
            {
                return null;
            }
            else if (nr >= this.objects.Count)
            {
                return this.Objects[0];
            }
            else
            {
                return this.Objects[nr];
            }
        }

        /// <summary>
        /// Adding the mesh
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public bool Add(string _name)
        {
            var obj = new GeometricObject(_name, new Vector3(0, 0, 0), Matrix.Identity, new Vector3(0, 0, 0));

            if (_name.Equals("torus"))
            {
                obj.Mesh = Direct3D.Mesh.Torus(device, 2.0f, 5, 8, 8);
                obj.ScaleMatrix.Scale(50.0f, 50.0f, 50.0f);
            }
            else if (_name.Equals("sphere"))
            {
                obj.Mesh = Direct3D.Mesh.Sphere(this.device, 5, 8, 8);
            }
            else if (_name.Equals("box"))
            {
                obj.Mesh = Direct3D.Mesh.Box(this.device, 4, 3, 2); 
            }
            else if (_name.Equals("polygon"))
            {
                obj.Mesh = Direct3D.Mesh.Polygon(this.device, 10, 4);
            }
            else if (_name.Equals("teapot"))
            {
                obj.Mesh = Direct3D.Mesh.Teapot(this.device);
            }

            if (obj.Mesh != null)
            {
                this.objects.Add(obj);
                this.activeObject = obj;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Resets the meshes
        /// </summary>
        public void Reset()
        {
            this.Add("torus");
            //this.Add("spehere");
            //this.Add("box");
            //this.Add("polygon");
            //this.Add("teapot");

            this.ActiveObject = this.objects[0];
        }

        #endregion
    }
}
