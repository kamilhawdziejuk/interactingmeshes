//Kamil Hawdziejuk
//Space application http://code.google.com/p/interactingmeshes/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;
using InteractingMeshes.Tests;

namespace InteractingMeshes
{
    /// <summary>
    /// Space application main class
    /// </summary>
    public class SpaceApplication : Form
    {
        #region --- Static fields ---

        private static bool graphicslost;

        /// <summary>
        /// Nazwa frameworku
        /// </summary>
        private static string framework = "Framework 1.0";

        /// <summary>
        /// Szerokoœæ ekranu
        /// </summary>
        private static int screenwidth = 640;

        /// <summary>
        /// Wysokoœæ ekranu
        /// </summary>
        private static int screenheight = 640;

        private static Timer gametimer;
        private static bool paused;

        public static Options Options;

        #endregion

        #region --- Private fields ---

        /// <summary>
        /// View of a camera
        /// </summary>
        private readonly Camera camera = new Camera();

        /// <summary>
        /// Region of a screen
        /// </summary>
        private readonly Region viewRegion = new Region(new Vector3(-16, -16, -16), new Vector3(16, 16, 16));

        /// <summary>
        /// Drawing device
        /// </summary>
        private Device device;

        // DirectSound.Device sound = null;
        //DirectInput.Device keyboard = null;
        //DirectInput.Device mouse = null;
        //DirectInput.Device gameinput = null;

        /// <summary>
        /// Does the mouse is thieft
        /// </summary>
        private bool isMousing;

        /// <summary>
        /// Manager for objects
        /// </summary>
        private static ObjectsManager manager;

        /// <summary>
        /// Current mouse position
        /// </summary>
        private Point ptCurrentMousePosition;

        /// <summary>
        /// Last mouse position
        /// </summary>
        private Point ptLastMousePosition;

        /// <summary>
        /// Speed modifier
        /// </summary>
        private float speedmodifier = 1.0f;

        public static SpaceApplication Instance;

        private List<GeometricObject> collided = new List<GeometricObject>();

        #endregion

        #region --- Events ---

        /// <summary>
        /// This event-handler is a good place to create and initialize any 
        /// Direct3D related objects, which may become invalid during a 
        /// device reset.
        /// </summary>
        public void OnResetDevice(object sender, EventArgs e)
        {
            var deviceSender = (Device) sender;
            deviceSender.Transform.Projection =
                Matrix.PerspectiveFovLH(Geometry.DegreeToRadian(45.0f),
                                        (float) ClientSize.Width/ClientSize.Height,
                                        0.1f, 100.0f);

            deviceSender.RenderState.Lighting = false;
            deviceSender.RenderState.CullMode = Cull.None;
            deviceSender.RenderState.FillMode = FillMode.WireFrame;


            Manager.Reset();
        }

        /// <summary>
        /// Environment resizing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void EnvironmentResizing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
            {
                Manager.Move.Z += 1;
            }
            else
            {
                Manager.Move.Z -= 1;
            }
        }

        /// <summary>
        /// Raising after mouse is clicked
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            //this.objects[1].Position = new Vector3(e.X, e.Y, this.objects[1].Position.Z);
        }

        /// <summary>
        /// Raising when key is down
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs _e)
        {
            switch (_e.KeyValue)
            {
                case 97:
                    {
                        Manager.Add("sphere");
                        break;
                    }
                case 98:
                    {
                        Manager.Add("box");
                        break;
                    }

                case 99:
                    {
                        Manager.Add("torus");
                        break;
                    }

                case 100:
                    {
                        Manager.Add("polygon");
                        break;
                    }
                case 101:
                    {
                        Manager.Add("teapot");
                        break;
                    }

                case 102:
                    {
                        Manager.Add("torus2");
                        break;
                    }

                    //'c' - camera move
                case 67:
                    camera.Position.X += (float) Math.Cos(1);
                    //this.camera.Position.Y += (float)Math.Sin(1);
                    break;
                    //'x' - camera move
                case 88:
                    camera.Position.X -= (float) Math.Sin(1);
                    //this.camera.Position.Y -= (float)Math.Cos(1);
                    break;
                    //'v' - camera move
                case 86:
                    camera.Position.Y += 1;
                    break;
                    //'b' - camera move
                case 66:
                    camera.Position.Y -= 1;
                    break;
                    //+
                case 187:
                    Manager.Move.Z -= 1;
                    break;
                    //-
                case 189:
                    Manager.Move.Z += 1;
                    break;
                    //left arrow
                case 37:
                    Manager.Move.X -= 1;
                    break;
                    //right arrow
                case 39:
                    Manager.Move.X += 1;
                    break;
                    //up arrow
                case 38:
                    Manager.Move.Y += 1;
                    break;
                    //down arrow
                case 40:
                    Manager.Move.Y -= 1;
                    break;
                case 107:
                    Manager.Move.Z += 1;
                    break;
                case 109:
                    Manager.Move.Z -= 1;
                    break;
            }

            //deleting active object
            if (_e.KeyValue == 46)
            {
                Manager.RemoveActiveObject();
            }


            //changing objects
            if (_e.KeyValue >= 49 && _e.KeyValue <= 58)
            {
                ActiveObject = Manager.GetObject(_e.KeyValue - 49);
            }

            switch (_e.KeyCode)
            {
                case Keys.Escape:
                    Dispose();
                    break;

                case Keys.Space:
                    //orbitOn = !orbitOn;
                    break;

                case Keys.F1:
                    ++speedmodifier;
                    break;

                case Keys.F2:
                    --speedmodifier;
                    break;
                case Keys.W:
                    Manager.Rotate.Y += 0.1f;
                    break;
                case Keys.A:
                    Manager.Rotate.X += 0.1f;
                    break;
            }
        }

        #endregion

        #region --- Public properties ---

        /// <summary>
        /// Region of a view
        /// </summary>
        public Region ViewRegion
        {
            get { return viewRegion; }
        }

        public static ObjectsManager Manager
        {
            get { return manager; }
        }

        /// <summary>
        /// Operating object
        /// </summary>
        public GeometricObject ActiveObject
        {
            get { return Manager.ActiveObject; }
            set { Manager.ActiveObject = value; }
        }

        #endregion

        public void TestDrawingTriangle()
        {
            CustomVertex.TransformedColored[] vertexes = null;
            //I simply declared an empty array of transformed and colored vertexes
            vertexes = new CustomVertex.TransformedColored[3];
            // top vertex:
            vertexes[0].X = screenwidth/2.0f; // halfway across the screen
            vertexes[0].Y = screenheight/3.0f; // 1/3 down screen
            vertexes[0].Z = 0.0f;
            vertexes[0].Color = Color.Aquamarine.ToArgb();
            // right vertex:
            vertexes[1].X = (screenwidth/3.0f)*2.0f; // 2/3 across the screen
            vertexes[1].Y = (screenheight/3.0f)*2.0f; // 2/3 down screen
            vertexes[1].Z = 0.0f;
            vertexes[1].Color = Color.CornflowerBlue.ToArgb();
            // left vertex:
            vertexes[2].X = screenwidth/3.0f; // 1/3 across the screen
            vertexes[2].Y = (screenheight/3.0f)*2.0f; // 2/3 down screen
            vertexes[2].Z = 0.0f;
            vertexes[2].Color = Color.LemonChiffon.ToArgb();
            device.VertexFormat = CustomVertex.TransformedColored.Format;

            //this.device.DrawUserPrimitives(Direct3D.PrimitiveType.TriangleList, 1, vertexes);
        }

        /// <summary>
        /// Initialize components
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SpaceApplication
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "SpaceApplication";
            this.Text = "Collisions symulation";
            this.ResumeLayout(false);

        }

        #region --- Creating and destroying objects ---

        public SpaceApplication()
        {
            ClientSize = new Size(screenwidth, screenheight); // Specify the client size
            Text = "Intersacting meshes"; // Specify the title
            Options = new Options();
            Instance = this;
        }

        #endregion

        #region --- Initializing ---

        //Next you must tell the device how to render to the screen and handle any exceptions.
        public bool InitializeDirect3D()
        {
            try
            {
                var pps = new PresentParameters();
                pps.Windowed = true; // Specify that it will be in a window
                pps.SwapEffect = SwapEffect.Discard;
                    // After the current screen is drawn, it will be automatically deleted from memory
                device = new Device(0, DeviceType.Hardware, this,
                                    CreateFlags.SoftwareVertexProcessing, pps); // Put everything into the device

                device.RenderState.CullMode = Cull.None;

                // For the projection matrix, set up a perspective transform (which
                // transforms geometry from 3-D view space to 2-D viewport space, with
                // a perspective divide that makes objects smaller in the distance). To build
                // a perspective transform, you need the field of view (1/4 PI is common),
                // the aspect ratio, and the near and far clipping planes (which define at
                // the distances at which geometry should be no longer be rendered).
                device.Transform.Projection =
                    Matrix.PerspectiveFovLH(Geometry.DegreeToRadian(70.0f),
                                            (float) ClientSize.Width/ClientSize.Height,
                                            0.1f, 100.0f);

                manager = new ObjectsManager(device);

                //events
                //device.DeviceLost += new EventHandler(this.InvalidateDeviceObjects);
                //device.DeviceReset += new EventHandler(this.RestoreDeviceObjects);
                //device.Disposing += new EventHandler(this.DeleteDeviceObjects);
                device.DeviceResizing += EnvironmentResizing;
                // Register an event-handler for DeviceReset and call it to continue
                // our setup.
                device.DeviceReset += OnResetDevice;

                OnResetDevice(device, null);
                return true;
            }
            catch (DirectXException e)
            {
                MessageBox.Show(e.ToString(), "Error"); // Handle all the exceptions
                return false;
            }
        }

        #endregion

        #region --- Rendering ---

        // The next function handles the main rendering that is done all in one neat package.
        private void Render()
        {
            if (device == null) // If the device is empty don't bother rendering
            {
                return;
            }

            try
            {
                device.Clear(ClearFlags.Target, Color.Black, 1.0f, 0); // Clear the window to black
                device.BeginScene();
                device.VertexFormat = CustomVertex.TransformedColored.Format;
                //speedmodifier += 0.01f;

                this.ProcessTransformations();
                this.ProcessCollisions();
                this.ProcessVisualization();

                device.Transform.World = Matrix.Identity;
                device.Transform.View = Matrix.LookAtLH(camera.Position, // Camera position
                                                        camera.LookAtPoint, // Look-at point
                                                        camera.UpVector); // Up vector
                
                CollisionTestsManager.PresentStatistics();


                device.EndScene();
                device.Present();
            }
                // device has been lost, and it cannot be re-initialized yet
            catch (DeviceLostException)
            {
                graphicslost = true;
            }
        }

        /// <summary>
        /// Do transformations (moves, rotates ActiveObject)
        /// </summary>
        private void ProcessTransformations()
        {
            foreach (GeometricObject obj in Manager.Objects)
            {
                if (obj == ActiveObject)
                {
                    //transformations:
                    ActiveObject.Rotation += Manager.Rotate;
                    Manager.Rotate = new Vector3(0, 0, 0);

                    ActiveObject.Position += Manager.Move;

                    if (!ViewRegion.Contains(ActiveObject.Position))
                    {
                        ActiveObject.Position -= Manager.Move;
                    }
                    Manager.Move = new Vector3(0, 0, 0);
                }
            }
        }

        /// <summary>
        /// Do visualization
        /// </summary>
        /// <param name="_objects"></param>
        private void ProcessVisualization()
        {
            foreach (GeometricObject obj in Manager.Objects)
            {
                if (obj == ActiveObject)
                {
                    device.Transform.World = ActiveObject.GeometryMatrix;
                    ActiveObject.Mesh.DrawSubset(0);

                    obj.Mesh = MeshUtils.ChangeMeshColor(obj.Mesh, Color.Green, device);
                }
                else
                {
                    if (CollisionManager.CollidedObjects.Contains(obj))
                    {
                        obj.Mesh = MeshUtils.ChangeMeshColor(obj.Mesh, Color.Red, device);
                    }
                    else
                    {
                        obj.Mesh = MeshUtils.ChangeMeshColor(obj.Mesh, Color.White, device);
                    }
                }
                device.Transform.World = obj.GeometryMatrix;
                obj.Mesh.DrawSubset(0);
            }
        }

        /// <summary>
        /// Do collision operations
        /// </summary>
        private void ProcessCollisions()
        {
            if (ActiveObject != null && ActiveObject.IsChanged)
            {
                //collided.Clear();
                CollisionManager.ClearCollidedObjects();
                foreach (GeometricObject obj in Manager.Objects)
                {
                    if (obj != ActiveObject)
                    {
                        CollisionManager.CollisionTest(obj, ActiveObject);
                    }
                }
                ActiveObject.IsChanged = false;
            }
        }

        #endregion

        #region --- MAIN FUNCTION ---

        /// <summary>
        /// The main function
        /// </summary>
        private static void Main()
        {
            var mainForm = new SpaceApplication(); // Create the form

            if (mainForm.InitializeDirect3D() == false) // Check if D3D could be initialized
            {
                MessageBox.Show("Could not initialize Direct3D.", "Error");
                return;
            }
            mainForm.Show(); // When everything is initialized, show the form
            Options.Show();
            while (mainForm.Created) // This is our message loop
            {
                mainForm.Render(); // Keep rendering until the program terminates
                Application.DoEvents(); // Process the events, like keyboard and mouse input 
            }
        }

        #endregion
    }
}