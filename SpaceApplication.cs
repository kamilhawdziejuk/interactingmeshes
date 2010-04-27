//Kamil Hawdziejuk
//Space application http://code.google.com/p/interactingmeshes/

using Microsoft.DirectX;
using Direct3D = Microsoft.DirectX.Direct3D;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;

namespace InteractingMeshes
{
    /// <summary>
    /// Space application main class
    /// </summary>
    public class SpaceApplication : Form
    {
        #region --- Static fields ---

        static bool graphicslost = false;
        /// <summary>
        /// Nazwa frameworku
        /// </summary>
        static string framework = "Framework 1.0";
        /// <summary>
        /// Szerokoœæ ekranu
        /// </summary>
        static int screenwidth = 800;

        /// <summary>
        /// Wysokoœæ ekranu
        /// </summary>
        static int screenheight = 600;
        static Timer gametimer = null;
        static bool paused = false;

        #endregion 

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
        private Vector3 move = new Vector3(0,0,0);

        /// <summary>
        /// Rotation of active objects on all axies
        /// </summary>
        private Vector3 rotate = new Vector3(0, 0, 0);

        /// <summary>
        /// Region of a screen
        /// </summary>
        private Region viewRegion = new Region(new Vector3(-16, -16, -16), new Vector3(16, 16, 16));

        /// <summary>
        /// View of a camera
        /// </summary>
        private Camera camera = new Camera();

        /// <summary>
        /// Drawing device
        /// </summary>
        private Direct3D.Device device = null;    
       // DirectSound.Device sound = null;
        //DirectInput.Device keyboard = null;
        //DirectInput.Device mouse = null;
        //DirectInput.Device gameinput = null;

       /// <summary>
       /// Does the mouse is thieft
       /// </summary>
        private bool isMousing = false;

        /// <summary>
        /// Last mouse position
        /// </summary>
        private Point ptLastMousePosition;
        /// <summary>
        /// Current mouse position
        /// </summary>
        private Point ptCurrentMousePosition;

        /// <summary>
        /// Speed modifier
        /// </summary>
        private float speedmodifier = 1.0f;       

        #endregion

        #region --- Events ---


        /// <summary>
        /// This event-handler is a good place to create and initialize any 
        /// Direct3D related objects, which may become invalid during a 
        /// device reset.
        /// </summary>
        public void OnResetDevice(object sender, EventArgs e)
        {
            Direct3D.Device deviceSender = (Direct3D.Device)sender;
            deviceSender.Transform.Projection =
                Matrix.PerspectiveFovLH(Direct3D.Geometry.DegreeToRadian(45.0f),
                (float)this.ClientSize.Width / this.ClientSize.Height,
                0.1f, 100.0f);

            deviceSender.RenderState.Lighting = false;
            deviceSender.RenderState.CullMode = Cull.None;
            deviceSender.RenderState.FillMode = FillMode.WireFrame;    

            GeometricObject torus = new GeometricObject("torus", new Vector3(0, 0, 0), Matrix.Identity, new Vector3(0,0,0));
            torus.Mesh = Direct3D.Mesh.Torus(device, 2.0f, 7, 5, 6);
            torus.ScaleMatrix.Scale(50.0f, 50.0f, 50.0f);

            GeometricObject sphere = new GeometricObject("sphere", new Vector3(0, 0, 0), Matrix.Identity, new Vector3(0, 0, 0));
            sphere.Mesh = Direct3D.Mesh.Sphere(this.device, 4, 10, 20);

            GeometricObject box = new GeometricObject("box", new Vector3(0, 0, 0), Matrix.Identity, new Vector3(0, 0, 0));
            box.Mesh = Direct3D.Mesh.Box(this.device, 8, 8, 8);


            GeometricObject box2 = new GeometricObject("box", new Vector3(0, 0, 0), Matrix.Identity, new Vector3(0, 0, 0));
            box2.Mesh = Direct3D.Mesh.Box(this.device, 1, 2, 2);


            GeometricObject teapot = new GeometricObject("teapot", new Vector3(0, 0, 0), Matrix.Identity, new Vector3(0, 0, 0));
            teapot.Mesh = Direct3D.Mesh.Teapot(this.device);// .Box(this.device, 3, 4, 5);


            //this.objects.Add(sphere);
            this.objects.Add(torus);
            this.objects.Add(box2);
            //this.objects.Add(box2);
            //this.objects.Add(teapot);

            this.ActiveObject = box;
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
                this.move.Z += 1;
            }
            else
            {
                this.move.Z -= 1;
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
        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs _e)
        {
            
            switch (_e.KeyValue)
            {
                    //'c' - camera move
                case 67:
                    this.camera.Position.X += (float)Math.Cos(1);
                    //this.camera.Position.Y += (float)Math.Sin(1);
                    break;
                    //'x' - camera move
                case 88:
                    this.camera.Position.X -= (float)Math.Sin(1);
                    //this.camera.Position.Y -= (float)Math.Cos(1);
                    break;
                    //'v' - camera move
                case 86:
                    this.camera.Position.Y += 1;
                    break;
                    //'b' - camera move
                case 66:
                    this.camera.Position.Y -= 1;
                    break;
                    //+
                case 187:
                    this.move.Z -= 1;
                    break;
                    //-
                case 189:
                    this.move.Z += 1;
                    break;
                    //left arrow
                case 37:
                    this.move.X -= 1;
                    break;
                    //right arrow
                case 39:
                    this.move.X += 1;
                    break;
                    //up arrow
                case 38:
                    this.move.Y += 1;
                    break;
                    //down arrow
                case 40:
                    this.move.Y -= 1;
                    break;
                case 107:
                    this.move.Z += 1;
                    break;
                case 109:
                    this.move.Z -= 1;
                    break;
                    //1
                case 49:
                    this.ActiveObject = this.objects[0];
                    break;
                    //2
                case 50:
                    this.ActiveObject = this.objects[1];
                    break;
                    //3
                case 51:
                    if (this.objects.Count > 2)
                    {
                        this.ActiveObject = this.objects[2];
                    }
                    break;
                case 52:
                    if (this.objects.Count > 3)
                    {
                        this.ActiveObject = this.objects[3];
                    }
                    break;
            }


            //this.activeObject.Position = this.move;

            switch (_e.KeyCode)
            {
                case System.Windows.Forms.Keys.Escape:
                    this.Dispose();
                    break;

                case System.Windows.Forms.Keys.Space:
                    //orbitOn = !orbitOn;
                    break;

                case System.Windows.Forms.Keys.F1:
                    ++this.speedmodifier;
                    break;

                case System.Windows.Forms.Keys.F2:
                    --this.speedmodifier;
                    break;
                case System.Windows.Forms.Keys.W:
                    this.rotate.Y += 0.1f;
                    break;
                case System.Windows.Forms.Keys.A:
                    this.rotate.X += 0.1f;
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
            get
            {
                return viewRegion;
            }
        }

        /// <summary>
        /// Operating object
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
                value.Mesh = MeshUtils.ChangeMeshColor(value.Mesh, Color.Green, device);
                this.activeObject = value;
            }
        }

        #endregion

        public void  TestDrawingTriangle()
        {
            Direct3D.CustomVertex.TransformedColored[] vertexes = null;
            //I simply declared an empty array of transformed and colored vertexes
            vertexes = new Direct3D.CustomVertex.TransformedColored[3];
            // top vertex:
            vertexes[0].X = screenwidth / 2.0f; // halfway across the screen
            vertexes[0].Y = screenheight / 3.0f; // 1/3 down screen
            vertexes[0].Z = 0.0f;
            vertexes[0].Color = Color.Aquamarine.ToArgb();
                        // right vertex:
            vertexes[1].X = (screenwidth / 3.0f) * 2.0f; // 2/3 across the screen
            vertexes[1].Y = (screenheight / 3.0f) * 2.0f; // 2/3 down screen
            vertexes[1].Z = 0.0f;
            vertexes[1].Color = Color.CornflowerBlue.ToArgb();
            // left vertex:
            vertexes[2].X = screenwidth / 3.0f; // 1/3 across the screen
            vertexes[2].Y = (screenheight / 3.0f) * 2.0f; // 2/3 down screen
            vertexes[2].Z = 0.0f;
            vertexes[2].Color = Color.LemonChiffon.ToArgb();
            this.device.VertexFormat = Direct3D.CustomVertex.TransformedColored.Format;

            //this.device.DrawUserPrimitives(Direct3D.PrimitiveType.TriangleList, 1, vertexes);
        }

        #region --- Creating and destroying objects ---

        public SpaceApplication()
		{
            this.ClientSize = new Size(screenwidth, screenheight);// Specify the client size
			this.Text = "My First DirectX Program"; // Specify the title
        }

        #endregion

        #region --- Initializing ---
        
        //Next you must tell the device how to render to the screen and handle any exceptions.
		public bool InitializeDirect3D()
		{
			try
			{
                Direct3D.PresentParameters pps = new Direct3D.PresentParameters();
				pps.Windowed = true;    // Specify that it will be in a window
                pps.SwapEffect = Direct3D.SwapEffect.Discard;    // After the current screen is drawn, it will be automatically deleted from memory
                device = new Direct3D.Device(0, Direct3D.DeviceType.Hardware, this,
                    Direct3D.CreateFlags.SoftwareVertexProcessing, pps); // Put everything into the device

                device.RenderState.CullMode = Direct3D.Cull.None;

                // For the projection matrix, set up a perspective transform (which
                // transforms geometry from 3-D view space to 2-D viewport space, with
                // a perspective divide that makes objects smaller in the distance). To build
                // a perspective transform, you need the field of view (1/4 PI is common),
                // the aspect ratio, and the near and far clipping planes (which define at
                // the distances at which geometry should be no longer be rendered).
                device.Transform.Projection =
                     Matrix.PerspectiveFovLH(Direct3D.Geometry.DegreeToRadian(70.0f),
                    (float)this.ClientSize.Width / this.ClientSize.Height,
                    0.1f, 100.0f);

                //events
                //device.DeviceLost += new EventHandler(this.InvalidateDeviceObjects);
                //device.DeviceReset += new EventHandler(this.RestoreDeviceObjects);
                //device.Disposing += new EventHandler(this.DeleteDeviceObjects);
                device.DeviceResizing += new CancelEventHandler(this.EnvironmentResizing);
                // Register an event-handler for DeviceReset and call it to continue
                // our setup.
                device.DeviceReset += new System.EventHandler(this.OnResetDevice);
                this.OnResetDevice(device, null);
				return true;
			}
			catch(DirectXException e)
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
            if(device == null)   // If the device is empty don't bother rendering
            {
                return;
            }

            try
            {
               // Mogre.Root root = new Mogre.Root("../../plugins.cfg", "../ogre.cfg", "ogre.log");
                //Mogre.SceneManager sceneManager = 

                device.Clear(Direct3D.ClearFlags.Target, Color.Black, 1.0f, 0);  // Clear the window to black
                device.BeginScene();
                device.VertexFormat = Direct3D.CustomVertex.TransformedColored.Format;
                this.speedmodifier += 0.01f;
                // Vertex[] vertData = MeshUtils.GetVertexes(this.activeObject.Mesh);
                bool isCollision = false;
                foreach (GeometricObject obj in this.objects)
                {
                    if (obj != this.ActiveObject)
                    {
                        if (GilbertJohnsonKeerthi.BodiesIntersect(obj.Points, this.ActiveObject.Points))
                        {
                            if (MeshCollision.TestBSPCollision(obj, this.ActiveObject))
                            {
                                isCollision = true;
                                obj.Mesh = MeshUtils.ChangeMeshColor(obj.Mesh, Color.Red, device);
                            }
                        }
                        if (!isCollision)
                        {
                            obj.Mesh = MeshUtils.ChangeMeshColor(obj.Mesh, Color.White, device);
                            //obj.GeometryMatrix.RotateAxis(new Vector3(1, 1, 1), this.speedmodifier);
                        }
                    }
                    else
                    {
                        
                        obj.Mesh = MeshUtils.ChangeMeshColor(obj.Mesh, Color.Green, device);

                        obj.Rotation += this.rotate;
                        this.rotate = new Vector3(0, 0, 0);

                        obj.Position += this.move;

                        if (!this.ViewRegion.Contains(obj.Position))
                        {
                            //MessageBox.Show("Out of the screen");
                            obj.Position -= this.move;
                        }
                        this.move = new Vector3(0, 0, 0);
                    }
                    device.Transform.World = obj.GeometryMatrix;
                    obj.Mesh.DrawSubset(0);
                }
              //  this.TestDrawingTriangle();
                device.Transform.World = Matrix.Identity;
                device.Transform.View = Matrix.LookAtLH(this.camera.Position, // Camera position
                        this.camera.LookAtPoint,   // Look-at point
                        this.camera.UpVector);  // Up vector


                device.EndScene();
                device.Present();
            }
            // device has been lost, and it cannot be re-initialized yet
            catch (Direct3D.DeviceLostException)
            {
                graphicslost = true;
            }
        }

        #endregion

        #region --- MAIN FUNCTION ---

        /// <summary>
        /// The main function
        /// </summary>
        static void Main() 
		{
            SpaceApplication form = new SpaceApplication(); // Create the form

            if (form.InitializeDirect3D() == false) // Check if D3D could be initialized
            {
                MessageBox.Show("Could not initialize Direct3D.", "Error");
                return;
            }
            form.Show();    // When everything is initialized, show the form

            while(form.Created) // This is our message loop
            {
                form.Render();  // Keep rendering until the program terminates
                Application.DoEvents(); // Process the events, like keyboard and mouse input 
            }
        }

        #endregion

        /// <summary>
        /// Initialize components
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UsingDirectX
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "UsingDirectX";
            //this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UsingDirectX_MouseMove);
            this.ResumeLayout(false);

        }
    }
}