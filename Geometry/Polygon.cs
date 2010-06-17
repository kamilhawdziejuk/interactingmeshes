//11-04-2010
using System.Collections.Generic;
using System;

namespace InteractingMeshes
{
    /// <summary>
    /// Polygon, contains vertices (for now it is a triangle)
    /// </summary>
    public class Polygon : IEquatable<Polygon>
    {
        #region --- Public fields ---

        /// <summary>
        /// Points
        /// </summary>
        public List<Vertex> Points = new List<Vertex>();

        #endregion

        #region --- Public methods ---

        /// <summary>
        /// Adds point to the polygon
        /// </summary>
        /// <param name="_point"></param>
        public void Add(Vertex _point)
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
        public Polygon(List<Vertex> _points)
        {
            this.Points.AddRange(_points);
        }


        #endregion

        #region IEquatable<Polygon> Members

        public bool Equals(Polygon other)
        {

            if (this.Points.Count != other.Points.Count)
            {
                return false;
            }

            int cnt = 0;
            foreach (Vertex v in this.Points)
            {
                foreach (Vertex v2 in other.Points)
                {
                    if (v.Equals(v2))
                    {
                        cnt++;
                    }
                }
            }

            return (cnt == this.Points.Count);
        }

        #endregion
    }
}
