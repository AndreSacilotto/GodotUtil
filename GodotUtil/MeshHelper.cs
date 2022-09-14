
using System;
using Godot;
using Util.Vector;

namespace Util
{
    public static class MeshHelper
    {

        #region Mesh Util

        public static void Generate(this SurfaceTool st, bool generateNormals, bool generateTangents, bool generateIndex)
        {
            if (generateNormals)
                st.GenerateNormals();
            if (generateTangents)
                st.GenerateTangents();
            if (generateIndex)
                st.Index();
        }

        #endregion

        #region Rotation

        public static void RotateVectorArray(Vector3[] points, float rotation, Vector3 axis)
        {
            var b = new Basis(axis, rotation);
            for (int i = 0; i < points.Length; i++) 
            {
                //points[i] = points[i].Rotated(axis, rotation);
                points[i] = b.Mult(points[i]);
            }
        }

        public static void RotateVectorArray(Vector2[] points, float rotation)
        {
            var s = Mathf.Sin(rotation);
            var c = Mathf.Cos(rotation);
            for (int i = 0; i < points.Length; i++) 
                points[i] = VectorMath.RotatedNoTrig(points[i], c, s);
        }

        public static void RotateVectorArray(Vector2[] points, Vector2 pivot, float rotation)
        {
            var s = Mathf.Sin(rotation);
            var c = Mathf.Cos(rotation);
            for (int i = 0; i < points.Length; i++)
                points[i] = VectorMath.RotatedNoTrig(points[i], pivot, c, s);
        }

        #endregion

        public static Vector2[] ErrorReturn2D()
        {
            return Array.Empty<Vector2>();
            //return null;
        }

        public static Vector3[] ErrorReturn3D()
        {
            return Array.Empty<Vector3>();
            //return null;
        }

        // GODOT seems to use CLOCKWISE

        #region Create 2D

        /// <param name="r">Radius/Size</param>
        public static Vector2[] Square(float r) 
        {
            var rn = -r;
            return new Vector2[4] {
                new Vector2(rn, rn),
                new Vector2(r, rn),
                new Vector2(r, r),
                new Vector2(rn, r),
            };
        }

        /// <param name="h">Horizontal Size</param>
        /// <param name="v">Vertical Size</param>
        public static Vector2[] Retangle(float h, float v)
        {
            var vn = -v;
            var hn = -h;
            return new Vector2[4] {
                new Vector2(hn, vn),
                new Vector2(h, vn),
                new Vector2(h, v),
                new Vector2(hn, v),
            };
        }

        /// <param name="h">Horizontal Size</param>
        /// <param name="v">Vertical Size</param>
        public static Vector2[] Rhombus(float h, float v)
        {
            return new Vector2[4] {
                new Vector2(0f, -v),
                new Vector2(h, 0f),
                new Vector2(0f, v),
                new Vector2(-h, 0f),
            };
        }
        
        /// <param name="n">Near - bottom width</param>
        /// <param name="f">Far - top width</param>
        public static Vector2[] Trapezoid(float n, float f, float height)
        {
            return new Vector2[4] {
                new Vector2(-f, -height),
                new Vector2(f, -height),
                new Vector2(n, height),
                new Vector2(-n, height),
            };
        }

        /// <summary> 
        /// https://math.stackexchange.com/questions/1344690/is-it-possible-to-find-the-vertices-of-an-equilateral-triangle-given-its-center
        /// </summary>
        /// <param name="r">Radius/Size - Negative value flip the polygon</param>
        public static Vector2[] TriangleEquilateral(float r)
        {
            var t = Mathf.Tan(MathUtil.TAU_30) * r;

            return new Vector2[3] {
                new Vector2(0f, t * -2f),
                new Vector2(r, t),
                new Vector2(-r, t),
            };
        }

        /// <param name="radius">Radius/Size - Negative value flip the polygon</param>
        /// <param name="sides">Number of sides of the primivite polygon</param>
        public static Vector2[] SimplePolygon2D(float radius, int sides)
        {
            if (sides < 3)
                return ErrorReturn2D();

            var points = new Vector2[sides];

            var rad = Mathf.Tau / sides;

            var s = Mathf.Sin(rad);
            var c = Mathf.Cos(rad);

            points[0] = new Vector2(0, -radius);
            for (int i = 1; i < sides; i++)
                points[i] = VectorMath.RotatedNoTrig(points[i - 1], c, s);

            return points;
        }

        public static Vector2[] SemiCircle(float radius, int density = 10)
        {
            if (density < 2)
                return ErrorReturn2D();

            var points = new Vector2[density];

            var rad = Mathf.Pi / density;

            var s = Mathf.Sin(rad);
            var c = Mathf.Cos(rad);

            points[0] = new Vector2(-radius, 0f);
            for (int i = 1; i < density; i++)
                points[i] = VectorMath.RotatedNoTrig(points[i - 1], c, s);

            return points;
        }

        /// <param name="inner">Radius of end circle</param>
        /// <param name="outter">Radius of far circle</param>
        /// <param name="density">Number of points on ONE circle</param>
        public static Vector2[] Donut(float inner, float outter, int density = 10)
        {
            var points = new Vector2[density * 2];
            var secondCircle = density;

            var rad = Mathf.Tau / density;

            points[0] = new Vector2(0, -inner);
            points[0] = new Vector2(0, -inner);
            for (int i = 1; i < density; i++)
                points[i] = points[i - 1].Rotated(rad);


            return points;
        }


        /// <param name="h">Horizontal Size</param>
        /// <param name="v">Vertical Size</param>
        /// <param name="th">Thickness of the Horizontal Line</param>
        /// <param name="tv">Thickness of the Vertical Line</param>
        /// <param name="position01">position of interscetion in percentage (0f..1f)</param>
        public static Vector2[] HolyCross2D(float h, float v, float th, float tv, float position01 = 0.75f)
        {
            const float SAFE_THRESHOLD = 0.0001f;
            var c = (v - th - SAFE_THRESHOLD) * (2f * position01 - 1f);

            var ctp = c + th;
            var ctn = c - th;

            return new Vector2[12]
            {
                new Vector2(-h, ctn), //0
                new Vector2(-tv, ctn), //1
                new Vector2(-tv, -v), //2
                new Vector2(tv, -v), //3

                new Vector2(tv, ctn), //4
                new Vector2(h, ctn), //5
                new Vector2(h, ctp), //6
                new Vector2(tv, ctp), //7

                new Vector2(tv, v), //8
                new Vector2(-tv, v), //9
                new Vector2(-tv, ctp), //10
                new Vector2(-h, ctp), //11
            };
        }

        //public static Vector2[] DoubleArc2D(float radius, int density = 10)
        //{
        //    var points = new Vector2[density];

        //    return points;
        //}

        //public static Vector2[] Arc2D(float radius, int density = 10)
        //{
        //    var points = new Vector2[density];

        //    return points;
        //}

        ///// <summary>=D</summary>
        ///// <param name="radius"></param>
        //public static Vector2[] Bullet(float radius)
        //{
        //    var points = new Vector2[density];

        //    return points;
        //}


        #endregion



    }
}