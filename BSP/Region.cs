//06-04-2010

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace InteractingMeshes.KdTree
{
    /// <summary>
    /// Class that represent 3D part of a space (a region)
    /// </summary>
    public class Region
    {
        #region --- Public Fields ---

        /// <summary>
        /// Chosen point
        /// </summary>
        public Vector3 CenterPoint;

        /// <summary>
        /// right-top corner
        /// </summary>
        public Vector3 HighBound;

        /// <summary>
        /// left-bottom corner
        /// </summary>
        public Vector3 LowBound;

        #endregion

        #region --- Contructing ---

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_lowBound"></param>
        /// <param name="_highBound"></param>
        public Region(Vector3 _lowBound, Vector3 _highBound)
        {
            LowBound = _lowBound;
            HighBound = _highBound;
        }

        #endregion

        #region --- public methods ---

        /// <summary>
        /// Does the point is inside the region
        /// </summary>
        /// <param name="_point"></param>
        /// <returns></returns>
        public bool Contains(Vector3 _point)
        {
            return (LowBound.X <= _point.X && LowBound.Y <= _point.Y && LowBound.Z <= _point.Z &&
                    HighBound.X >= _point.X && HighBound.Y >= _point.Y && HighBound.Z >= _point.Z);
        }

        #endregion
    }
}
