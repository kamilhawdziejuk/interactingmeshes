//11-04-2010
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace InteractingMeshes.BSP
{
    /// <summary>
    /// Polygon, contains vertices (for now it is a triangle)
    /// </summary>
    public class Polygon
    {
        #region --- Public fields ---

        /// <summary>
        /// Points
        /// </summary>
        public List<Vector3> Points = new List<Vector3>();

        #endregion

        #region --- Public methods ---

        /// <summary>
        /// Adds point to the polygon
        /// </summary>
        /// <param name="_point"></param>
        public void Add(Vector3 _point)
        {
            this.Points.Add(_point);
        }

        #endregion

        #region --- Creating and destroying objects ---

        /// <summary>
        /// Construktor
        /// </summary>
        public Polygon()
        {

        }

        /// <summary>
        /// Construktor
        /// </summary>
        /// <param name="_points"></param>
        public Polygon(List<Vector3> _points)
        {
            this.Points.AddRange(_points);
        }


        #endregion
    }
}
