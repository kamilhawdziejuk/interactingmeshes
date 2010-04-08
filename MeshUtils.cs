//20-02-2010

using System;
using System.Collections.Generic;
using System.Text;
using Direct3D = Microsoft.DirectX.Direct3D;
using System.Drawing;
using Microsoft.DirectX;

namespace InteractingMeshes
{
    public class MeshUtils
    {
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
            return vertData;
        }

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

        //public static Vector3[] GetNormals(Direct3D.Mesh _mesh)
        //{
        //    //int[] ib = new int[_mesh.IndexBuffer.SizeInBytes];// .IndexBuffer.Count];

        //    //mesh.IndexBuffer.CopyTo(ib, 0);

        //    Vector3[] normals = new Vector3[_mesh.IndexBuffer.SizeInBytes / 3];

        //    for (int i = 0, conta = 0; i < _mesh.IndexBuffer.SizeInBytes; i += 3, conta++)
        //    {

        //        Vector3 v0 = vb[_mesh.IndexBuffer[i]].Position;

        //        Vector3 v1 = vb[_mesh.IndexBuffer[i + 1]].Position;

        //        Vector3 v2 = vb[_mesh.IndexBuffer[i + 2]].Position;

        //        Vector3 edge1 = v1 - v0;

        //        Vector3 edge2 = v2 - v0;

        //        Vector3 normal = Vector3.Cross(edge1, edge2);

        //        normal.Normalize();

        //        normals[conta] = normal;

        //    }
        //}
    }
}
