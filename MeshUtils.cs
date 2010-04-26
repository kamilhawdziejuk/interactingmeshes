//20-02-2010

using System;
using System.Collections.Generic;
using System.Text;
using Direct3D = Microsoft.DirectX.Direct3D;
using System.Drawing;
using Microsoft.DirectX;

namespace InteractingMeshes
{
    /// <summary>
    /// Mesh utils (changing color, getting points, getting polygons, etc.)
    /// </summary>
    public class MeshUtils
    {
        #region --- Static methods ---

        /// <summary>
        /// Changing the mesh color
        /// </summary>
        /// <param name="_mesh"></param>
        /// <param name="_color"></param>
        /// <param name="_device"></param>
        /// <returns></returns>
        public static Direct3D.Mesh ChangeMeshColor(Direct3D.Mesh _mesh, Color _color, Direct3D.Device _device)
        {
            Direct3D.Mesh tempMesh = _mesh.Clone(_mesh.Options.Value, Vertex.FVF_Flags, _device);
            Vertex[] vertData =
                (Vertex[])tempMesh.VertexBuffer.Lock(0, typeof(Vertex),
                                                       Direct3D.LockFlags.None,
                                                       tempMesh.NumberVertices);

            for (int i = 0; i < vertData.Length; ++i)
            {
                vertData[i].color = _color.ToArgb();
            }

            tempMesh.VertexBuffer.Unlock();
            return tempMesh;
        }

        /// <summary>
        /// Get vertices from mesh
        /// </summary>
        /// <param name="_mesh"></param>
        /// <returns></returns>
        public static Vertex[] GetVertexes(Direct3D.Mesh _mesh)
        {
            Vertex[] vertData =
                (Vertex[])_mesh.VertexBuffer.Lock(0, typeof(Vertex),
                                                       Direct3D.LockFlags.None,
                                                       _mesh.NumberVertices);
            _mesh.VertexBuffer.Unlock();
            return vertData;
        }

        /// <summary>
        /// Get indices from mesh
        /// </summary>
        /// <param name="_mesh"></param>
        /// <returns></returns>
        public static Face[] GetIndices(Direct3D.Mesh _mesh)
        {
            Face[] intData = 
                (Face[])_mesh.IndexBuffer.Lock(0, typeof(Face), Direct3D.LockFlags.NoOverwrite, _mesh.NumberFaces);
            _mesh.IndexBuffer.Unlock();
            return intData;
        }

        /// <summary>
        /// Get points from mesh
        /// </summary>
        /// <param name="_mesh"></param>
        /// <param name="_position"></param>
        /// <returns></returns>
        public static List<Vector3> GetPoints(Direct3D.Mesh _mesh, Vector3 _position)
        {
            Vertex[] vertices = GetVertexes(_mesh);
            List<Vector3> points = new List<Vector3>();

            foreach (Vertex v in vertices)
            {
                if (!points.Contains(v.Vector + _position))
                {
                    points.Add(v.Vector + _position);
                }
            }
            return points;
        }

        /// <summary>
        /// Get triangle polygons from a mesh
        /// </summary>
        /// <param name="_mesh"></param>
        /// <returns></returns>
        public static List<Polygon> GetPolygons(Direct3D.Mesh _mesh)
        {
            Vertex[] vertices = GetVertexes(_mesh);
            Face[] indices = GetIndices(_mesh);
           // int[] adjecency = new int[_mesh.NumberFaces * 3];
           // _mesh.GenerateAdjacency((float)0.01, adjecency);

            List<Polygon> polygonLst = new List<Polygon>();

            for (int i = 0; i < indices.Length; ++i)
            {
                Vertex p0 = vertices[indices[i].v1];
                Vertex p1 = vertices[indices[i].v2];
                Vertex p2 = vertices[indices[i].v3];
                Polygon poly = new Polygon();
                poly.Add(p0);
                poly.Add(p1);
                poly.Add(p2);
                polygonLst.Add(poly);
            }

            return polygonLst;
        }

        #endregion
    }
}
