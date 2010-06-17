//Kamil Hawdziejuk
//09-06-2010

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractingMeshes
{
    public interface ICollisionDetector
    {
        bool DetectCollision(GeometricObject obj1, GeometricObject obj2, double tolerance);

        string Name
        {
            get;
        }

        Options.CollisionStage CollisionLevel
        {
            get;
        }
    }
}
