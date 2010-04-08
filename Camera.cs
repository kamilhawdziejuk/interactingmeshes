using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace InteractingMeshes
{
    /// <summary>
    /// A camera
    /// </summary>
    public class Camera
    {
        public Vector3 Position = new Vector3(0.0f, 2.0f, -25.0f);

        public Vector3 LookAtPoint = new Vector3(0, 0, 0);

        public Vector3 UpVector = new Vector3(0,1,0);
    }
}
