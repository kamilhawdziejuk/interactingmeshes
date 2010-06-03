//20-02-2010

using System.Collections.Generic;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

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
        public static Mesh ChangeMeshColor(Mesh _mesh, Color _color, Device _device)
        {
            Mesh tempMesh = _mesh.Clone(_mesh.Options.Value, Vertex.FVF_Flags, _device);
            var vertData =
                (Vertex[]) tempMesh.VertexBuffer.Lock(0, typeof (Vertex),
                                                      LockFlags.None,
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
        public static Vertex[] GetVertexes(Mesh _mesh)
        {
            var vertData =
                (Vertex[]) _mesh.VertexBuffer.Lock(0, typeof (Vertex),
                                                   LockFlags.None,
                                                   _mesh.NumberVertices);
            _mesh.VertexBuffer.Unlock();
            return vertData;
        }

        /// <summary>
        /// Get indices from mesh
        /// </summary>
        /// <param name="_mesh"></param>
        /// <returns></returns>
        public static Face[] GetIndices(Mesh _mesh)
        {
            var intData =
                (Face[]) _mesh.IndexBuffer.Lock(0, typeof (Face), LockFlags.NoOverwrite, _mesh.NumberFaces);
            _mesh.IndexBuffer.Unlock();
            return intData;
        }

        /// <summary>
        /// Get points from mesh
        /// </summary>
        /// <param name="_mesh"></param>
        /// <param name="_position"></param>
        /// <returns></returns>
        public static List<Vector3> GetPoints(Mesh _mesh, Vector3 _position)
        {
            Vertex[] vertices = GetVertexes(_mesh);
            var points = new List<Vector3>();

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
        public static List<Polygon> GetPolygons(Mesh _mesh, Vector3 _position)
        {
            Vertex[] vertices = GetVertexes(_mesh);
            Face[] indices = GetIndices(_mesh);
            // int[] adjecency = new int[_mesh.NumberFaces * 3];
            // _mesh.GenerateAdjacency((float)0.01, adjecency);

            var polygonLst = new List<Polygon>();

            for (int i = 0; i < indices.Length; ++i)
            {
                Vertex p0 = vertices[indices[i].v1];
                Vertex p1 = vertices[indices[i].v2];
                Vertex p2 = vertices[indices[i].v3];
                p0.Vector = p0.Vector + _position;
                p1.Vector = p1.Vector + _position;
                p2.Vector = p2.Vector + _position;
                var poly = new Polygon();
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