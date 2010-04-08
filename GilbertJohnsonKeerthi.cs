using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using System;

namespace InteractingMeshes
{
    ///// <summary> 
    ///// Contains extension methods for the Vector3 class to save us some typing, e.g. instead of Vector3.Dot(v1, v2) just write v1.Dot(v2) 
    ///// </summary> 
    //public static class Vector3Extensions
    //{
    //    public static float Dot(this Vector3 op1, Vector3 op2)
    //    {
    //        return Vector3.Dot(op1, op2);
    //    }

    //    public static Vector3 Cross(this Vector3 op1, Vector3 op2)
    //    {
    //        return Vector3.Cross(op1, op2);
    //    }
    //}

/// <summary> 
    /// Implements the Gilbert-Johnson-Keerthi algorithm for collision detection in 3D, as described in the video lecture at http://mollyrocket.com/849 
    /// See also http://www.cse.ttu.edu.tw/~jmchen/compg/fall04/students-presentation/GJK.pdf 
    /// </summary> 
    public static class GilbertJohnsonKeerthi 
    { 
        //to prevent infinite loops - if an intersection is not found in 20 rounds, consider there is no intersection 
        private const int MaxIterations = 20; 

        /// <summary> 
        /// Given the vertices (in any order) of two convex 3D bodies, calculates whether they intersect 
        /// </summary> 
        public static bool BodiesIntersect(IList<Vector3> shape1, IList<Vector3> shape2) 
        { 
            //for initial point, just take the difference between any two vertices (in this case - the first ones) 
            Vector3 initialPoint = shape1[0] - shape2[0]; 
            Vector3 S = MaxPointInMinkDiffAlongDir(shape1, shape2, initialPoint); 
            Vector3 D = -S; 
            List<Vector3> simplex = new List<Vector3>(); 
            simplex.Add(S); 

            for (int i = 0; i < MaxIterations; i++) 
            { 
                Vector3 A = MaxPointInMinkDiffAlongDir(shape1, shape2, D); 
                if (Vector3.Dot(A, D) < 0) 
                { 
                    return false; 
                } 

                simplex.Add(A); 

                if (UpdateSimplexAndDirection(simplex, ref D)) 
                { 
                    return true; 
                } 
            } 

            return false; 
        } 


        /// <summary> 
        /// Updates the current simplex and the direction in which to look for the origin. Called DoSimplex in the video lecture. 
        /// </summary> 
        /// <param name="simplex">A list of points in the current simplex. The last point in the list must be the last point added to the simplex</param> 
        /// <param name="direction"></param> 
        /// <returns></returns> 
        private static bool UpdateSimplexAndDirection(List<Vector3> simplex, ref Vector3 direction) 
        { 
            //if the simplex is a line 
            if (simplex.Count == 2) 
            { 
                //A is the point added last to the simplex 
                Vector3 A = simplex[1]; 
                Vector3 B = simplex[0]; 
                Vector3 AB = B - A; 
                Vector3 AO = -A; 

                //if (AB.Dot(AO) > 0)
                if (Vector3.Dot(AB, AO) > 0)
                {
                    direction = Vector3.Cross(Vector3.Cross(AB, AO), AB);// AB.Cross(AO).Cross(AB); 
                } 
                else 
                { 
                    direction = AO; 
                } 
            } 
            //if the simplex is a triangle 
            else if (simplex.Count == 3) 
            { 
                //A is the point added last to the simplex 
                Vector3 A = simplex[2]; 
                Vector3 B = simplex[1]; 
                Vector3 C = simplex[0]; 
                Vector3 AO = -A; 

                Vector3 AB = B - A; 
                Vector3 AC = C - A; 
                Vector3 ABC = Vector3.Cross(AB,AC); 

                if (Vector3.Dot(Vector3.Cross(ABC,AC), AO) > 0) 
                {
                    if (Vector3.Dot(AC,AO) > 0) 
                    { 
                        simplex.Clear(); 
                        simplex.Add(C); 
                        simplex.Add(A); 
                        direction = Vector3.Cross(Vector3.Cross(AC,AO),AC); 
                    } 
                    else
                        if (Vector3.Dot(AB,AO) > 0) 
                        { 
                            simplex.Clear(); 
                            simplex.Add(B); 
                            simplex.Add(A); 
                            direction = Vector3.Cross(Vector3.Cross(AB,AO),AB); 
                        } 
                        else 
                        { 
                            simplex.Clear(); 
                            simplex.Add(A); 
                            direction = AO; 
                        } 
                } 
                else 
                {
                    if (Vector3.Dot(Vector3.Cross(AB,ABC),AO) > 0) 
                    {
                        if (Vector3.Dot(AB,AO) > 0) 
                        { 
                            simplex.Clear(); 
                            simplex.Add(B); 
                            simplex.Add(A);
                            direction = Vector3.Cross(Vector3.Cross(AB, AO), AB);  
                        } 
                        else 
                        { 
                            simplex.Clear(); 
                            simplex.Add(A); 
                            direction = AO; 
                        } 
                    } 
                    else 
                    {
                        if (Vector3.Dot(ABC,AO) > 0) 
                        { 
                            //the simplex stays A, B, C 
                            direction = ABC; 
                        } 
                        else 
                        { 
                            simplex.Clear(); 
                            simplex.Add(B); 
                            simplex.Add(C); 
                            simplex.Add(A); 

                            direction = -ABC; 
                        } 
                    } 
                } 
            } 
            //if the simplex is a tetrahedron 
            else //if (simplex.Count == 4) 
            { 
                //A is the point added last to the simplex 
                Vector3 A = simplex[3]; 
                Vector3 B = simplex[2]; 
                Vector3 C = simplex[1]; 
                Vector3 D = simplex[0]; 

                Vector3 AO = -A; 
                Vector3 AB = B - A; 
                Vector3 AC = C - A; 
                Vector3 AD = D - A; 
                Vector3 ABC = Vector3.Cross(AB, AC); 
                Vector3 ACD = Vector3.Cross(AC,AD); 
                Vector3 ADB = Vector3.Cross(AD,AB); 

                //the side (positive or negative) of B, C and D relative to the planes of ACD, ADB and ABC respectively 
                int BsideOnACD = Math.Sign(Vector3.Dot(ACD,AB)); 
                int CsideOnADB = Math.Sign(Vector3.Dot(ADB,AC)); 
                int DsideOnABC = Math.Sign(Vector3.Dot(ABC,AD)); 

                //whether the origin is on the same side of ACD/ADB/ABC as B, C and D respectively 
                bool ABsameAsOrigin = Math.Sign(Vector3.Dot(ACD,AO)) == BsideOnACD; 
                bool ACsameAsOrigin = Math.Sign(Vector3.Dot(ADB,AO)) == CsideOnADB; 
                bool ADsameAsOrigin = Math.Sign(Vector3.Dot(ABC,AO)) == DsideOnABC; 

                //if the origin is on the same side as all B, C and D, the origin is inside the tetrahedron and thus there is a collision 
                if (ABsameAsOrigin && ACsameAsOrigin && ADsameAsOrigin) 
                { 
                    return true; 
                } 
                    //if the origin is not on the side of B relative to ACD 
                else if (!ABsameAsOrigin) 
                { 
                    //B is farthest from the origin among all of the tetrahedron's points, so remove it from the list and go on with the triangle case 
                    simplex.Remove(B); 
                    //the new direction is on the other side of ACD, relative to B 
                    direction = ACD * -BsideOnACD;                    
                } 
                //if the origin is not on the side of C relative to ADB 
                else if (!ACsameAsOrigin) 
                { 
                    //C is farthest from the origin among all of the tetrahedron's points, so remove it from the list and go on with the triangle case 
                    simplex.Remove(C); 
                    //the new direction is on the other side of ADB, relative to C 
                    direction = ADB * -CsideOnADB; 
                } 
                //if the origin is not on the side of D relative to ABC 
                else //if (!ADsameAsOrigin) 
                { 
                    //D is farthest from the origin among all of the tetrahedron's points, so remove it from the list and go on with the triangle case 
                    simplex.Remove(D); 
                    //the new direction is on the other side of ABC, relative to D 
                    direction = ABC * -DsideOnABC; 
                } 

                //go on with the triangle case 
                //TODO: maybe we should restrict the depth of the recursion, just like we restricted the number of iterations in BodiesIntersect? 
                return UpdateSimplexAndDirection(simplex, ref direction); 
                
            } 

            //no intersection found on this iteration 
            return false; 
        } 

        /// <summary> 
        /// Finds the farthest point along a given direction of the Minkowski difference of two convex polyhedra. 
        /// Called Support in the video lecture: max(D.Ai) - max(-D.Bj) 
        /// </summary> 
        private static Vector3 MaxPointInMinkDiffAlongDir(IList<Vector3> shape1, IList<Vector3> shape2, Vector3 direction) 
        {
            Vector3 negateDir = new Vector3(-direction.X, -direction.Y, -direction.Z);
            return MaxPointAlongDirection(shape1, direction) - MaxPointAlongDirection(shape2, negateDir);//Vector3.Negate(direction)); 
        } 

        /// <summary> 
        /// Finds the farthest point along a given direction of a convex polyhedron 
        /// </summary> 
        /// <param name="shape"></param> 
        /// <param name="direction"></param> 
        /// <returns></returns> 
        private static Vector3 MaxPointAlongDirection(IList<Vector3> shape, Vector3 direction) 
        { 
            Vector3 max = shape[0]; 
            foreach (Vector3 point in shape) 
            { 
                if (Vector3.Dot(max,direction) < Vector3.Dot(point,direction)) 
                { 
                    max = point; 
                } 
            } 

            return max; 
        } 
    }
}
