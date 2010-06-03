//05-05-2010
//Kamil Hawdziejuk

using System.Collections.Generic;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace InteractingMeshes
{
    /// <summary>
    /// For managing objects in the application
    /// </summary>
    public class ObjectsManager
    {
        #region --- Private fields ---

        /// <summary>
        /// Device
        /// </summary>
        private readonly Device device;

        /// <summary>
        /// Movement of active object
        /// </summary>
        public Vector3 Move = new Vector3(0, 0, 0);

        /// <summary>
        /// Rotation of active objects on all axies
        /// </summary>
        public Vector3 Rotate = new Vector3(0, 0, 0);

        /// <summary>
        /// Active object to manipulate
        /// </summary>
        private GeometricObject activeObject;

        /// <summary>
        /// Objects in scene
        /// </summary>
        private List<GeometricObject> objects = new List<GeometricObject>();

        #endregion

        #region --- Constructing and destroying objects ---

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_device"></param>
        public ObjectsManager(Device _device)
        {
            device = _device;
        }

        #endregion

        #region --- Public properties ---

        /// <summary>
        ///  Operating object
        /// </summary>
        public GeometricObject ActiveObject
        {
            get { return activeObject; }
            set
            {
                if (activeObject != null)
                {
                    activeObject.Mesh = MeshUtils.ChangeMeshColor(activeObject.Mesh, Color.White, device);
                }
                if (value != null)
                {
                    value.Mesh = MeshUtils.ChangeMeshColor(value.Mesh, Color.Green, device);
                    activeObject = value;
                }
            }
        }

        public List<GeometricObject> Objects
        {
            get { return objects; }
            set { objects = value; }
        }

        #endregion

        #region --- Public methods ---

        public bool ResizeActiveObject(int res)
        {
            if (res > 0)
            {
                //if (this.ActiveObject.id.Equals("torus"))
                {
                    //     this.RemoveActiveObject();
                }
                // this.Mesh = Direct3D.Mesh.Torus(device, 1.0f, 1.5f, 8, 8);
                // obj.ScaleMatrix.Scale(50.0f, 50.0f, 50.0f);
                /// if (this.ActiveObject.Mesh   Direct3D.Mesh.)
            }
            return true;
        }

        public bool RemoveActiveObject()
        {
            objects.Remove(ActiveObject);
            ActiveObject = GetObject(0);
            return true;
        }

        public bool RemoveObject(GeometricObject _obj)
        {
            objects.Remove(_obj);
            ActiveObject = GetObject(0);
            return true;
        }

        public GeometricObject GetObject(int nr)
        {
            if (Objects.Count == 0)
            {
                return null;
            }
            else if (nr >= objects.Count)
            {
                return Objects[0];
            }
            else
            {
                return Objects[nr];
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
                obj.Mesh = Mesh.Torus(device, 2.0f, 3.5f, 8, 8);
                //obj.ScaleMatrix.Scale(50.0f, 50.0f, 50.0f);
            }
            else if (_name.Equals("torus2"))
            {
                obj.Mesh = Mesh.Torus(device, 1.0f, 4.5f, 10, 10);
            }
            else if (_name.Equals("sphere"))
            {
                obj.Mesh = Mesh.Sphere(device, 5, 8, 8);
            }
            else if (_name.Equals("box"))
            {
                obj.Mesh = Mesh.Box(device, 4, 3, 2);
            }
            else if (_name.Equals("polygon"))
            {
                obj.Mesh = Mesh.Polygon(device, 10, 4);
            }
            else if (_name.Equals("teapot"))
            {
                obj.Mesh = Mesh.Teapot(device);
                obj.ScaleMatrix.Scale(50.0f, 50.0f, 50.0f);
            }

            if (obj.Mesh != null)
            {
                
                objects.Add(obj);
                SpaceApplication.Options.ListObjects.Items.Add(obj);
                activeObject = obj;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Resets the meshes
        /// </summary>
        public void Reset()
        {
            //Add("torus");
            //this.Add("spehere");
            //this.Add("box");
            //this.Add("polygon");
            //this.Add("teapot");

            //ActiveObject = objects[0];
        }

        #endregion
    }
}