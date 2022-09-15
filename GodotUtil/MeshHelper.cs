
using System;
using System.Linq;
using System.Collections.Generic;
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
                points[i] = b.Mult(points[i]);
                //points[i] = points[i].Rotated(axis, rotation);
        }

        //2D ROT

        public static void RotateVectors(Vector2[] points, float rotation)
        {
            var s = Mathf.Sin(rotation);
            var c = Mathf.Cos(rotation);
            for (int i = 0; i < points.Length; i++)
                points[i] = VectorMath.RotatedNoTrig(points[i], c, s);
        }
        public static void RotateVectors(Vector2[] points, Vector2 pivot, float rotation)
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

        /// <summary> https://math.stackexchange.com/a/1344707 </summary>
        /// <param name="radius">Radius/Size - Negative value flip the polygon</param>
        public static Vector2[] TriangleEquilateral(float radius)
        {
            var t = Mathf.Tan(UtilMath.TAU_30) * radius;
            return new Vector2[3] {
                new Vector2(0f, t * -2f),
                new Vector2(radius, t),
                new Vector2(-radius, t),
            };
        }

        /// <param name="size">Radius/Size/Diagonal</param>
        public static Vector2[] Square(float size) 
        {
            var rn = -size;
            return new Vector2[4] {
                new Vector2(rn, rn),
                new Vector2(size, rn),
                new Vector2(size, size),
                new Vector2(rn, size),
            };
        }

        public static Vector2[] Retangle(float width, float height)
        {
            return new Vector2[4] {
                new Vector2(-width, -height),
                new Vector2(width, -height),
                new Vector2(width, height),
                new Vector2(-width, height),
            };
        }

        public static Vector2[] Rhombus(float width, float height)
        {
            return new Vector2[4] {
                new Vector2(0f, -height),
                new Vector2(width, 0f),
                new Vector2(0f, height),
                new Vector2(-width, 0f),
            };
        }
        
        /// <param name="near">Near - bottom width</param>
        /// <param name="far">Far - top width</param>
        public static Vector2[] Trapezoid(float near, float far, float height)
        {
            return new Vector2[4] {
                new Vector2(-far, -height),
                new Vector2(far, -height),
                new Vector2(near, height),
                new Vector2(-near, height),
            };
        }

        /// <param name="radius">Radius/Size - Negative value flip the polygon</param>
        /// <param name="density">Number of sides of the primivite polygon</param>
        public static Vector2[] Circle(float radius, int density)
        {
            if (density < 2)
                return ErrorReturn2D();

            var points = new Vector2[density];

            var rad = Mathf.Tau / density;
            var s = Mathf.Sin(rad);
            var c = Mathf.Cos(rad);

            var v = Vector2.Left;
            for (int i = 0; i < density; i++)
            {
                points[i] = v * radius;
                v = VectorMath.RotatedNoTrig(v, c, s);
            }

            return points;
        }

        /// <summary> https://stackoverflow.com/a/34735255 </summary>
        /// <param name="width">Radius/Size</param>
        /// <param name="height">Radius/Size</param>
        /// <param name="density">Number of sides of the primivite polygon</param>
        public static Vector2[] Ellipse(float width, float height, int density = 10)
        {
            if (density < 2)
                return ErrorReturn2D();

            var points = new Vector2[density];

            var rad = Mathf.Tau / density;
            var s = Mathf.Sin(rad);
            var c = Mathf.Cos(rad);

            var v = Vector2.Left;
            for (int i = 0; i < density; i++) 
            {
                points[i] = new(v.x * width, v.y * height);
                v = VectorMath.RotatedNoTrig(v, c, s);
            }

            return points;
        }

        /// <param name="radius">Radius/Size - Negative value flip the polygon</param>
        /// <param name="sides">Number of points used to draw it</param>
        public static Vector2[] SemiCircle(float radius, int density = 10)
        {
            if (density < 2)
                return ErrorReturn2D();

            var points = new Vector2[density];

            var rad = UtilMath.TAU_180 / (density-1);
            var s = Mathf.Sin(rad);
            var c = Mathf.Cos(rad);

            var v = Vector2.Left;
            for (int i = 0; i < density; i++)
            {
                points[i] = v * radius;
                v = VectorMath.RotatedNoTrig(v, c, s);
            }

            return points;
        }

        /// <param name="height"></param>
        /// <param name="radius">How steep is the curve</param>
        /// <param name="arc">How much of tau the arc covers - 0..PI</param>
        /// <param name="density">Number of point on the arc</param>
        public static Vector2[] Arc(float width, float height, float radius, float arc, int density = 10)
        {
            var h = height - radius;

            if (height < 0 || radius < 0 || arc < 0)
                return ErrorReturn2D();

            var points = new Vector2[1 + density];

            var rad = arc / (density-1);
            var s = Mathf.Sin(rad);
            var c = Mathf.Cos(rad);

            var v = VectorMath.RadianToVector2(arc + UtilMath.TAU_90);
            for (int i = 0; i < density; i++)
            {
                points[i] = new(v.x * width, v.y * radius - h);
                v = VectorMath.RotatedNoTrig(v, c, s);
            }

            points[points.Length - 1] = new Vector2(0f, height);

            return points;
        }

        public static Vector2[] DoubleArc2D(float radius, int density = 10)
        {
            var points = new Vector2[density];

            return points;
        }


        /// <summary>=D</summary>
        /// <param name="width">Horizontal size</param>
        /// <param name="bodyHeight">Vertical size not including the arc</param>
        /// <param name="headRadius">Arc size</param>
        /// <param name="density">Number of points used  on the arc</param>
        public static Vector2[] Bullet(float width, float bodyHeight, float headRadius, int density = 10)
        {
            var h = bodyHeight - headRadius;

            if (width < 0 || bodyHeight < 0 || headRadius < 0 || h < 0)
                return ErrorReturn2D();

            var points = new Vector2[2+density];

            var rad = UtilMath.TAU_180 / (density-1);
            var s = Mathf.Sin(rad);
            var c = Mathf.Cos(rad);

            var v = Vector2.Left;
            for (int i = 0; i < density; i++)
            {
                points[i] = new(v.x * width, v.y * headRadius - h);
                v = VectorMath.RotatedNoTrig(v, c, s);
            }

            var len = points.Length - 1;
            points[len--] = new Vector2(-width, bodyHeight);
            points[len] = new Vector2(width, bodyHeight);

            return points;
        }

        public static Vector2[] Capsule(float width, float bodyHeight, float radius, int density = 10)
        {
            var h = bodyHeight - radius; 

            if (width < 0 || bodyHeight < 0 || radius < 0 || h < 0)
                return ErrorReturn2D();

            var points = new Vector2[density * 2];

            var rad = UtilMath.TAU_180 / (density - 1);
            var s = Mathf.Sin(rad);
            var c = Mathf.Cos(rad);

            var v = Vector2.Left;
            for (int i = 0, j = density; i < density; i++, j++)
            {
                var p = new Vector2(v.x * width, v.y * radius - h);
                points[i] = -p;
                points[j] = p;
                v = VectorMath.RotatedNoTrig(v, c, s);
            }
            return points;
        }

        #region 2D Non-Continuos

        private const float SAFE_THRESHOLD = 0.0001f;

        /// <param name="inner">Radius of end circle</param>
        /// <param name="outter">Radius of far circle</param>
        /// <param name="density">Number of points on ONE circle</param>
        public static Vector2[] Donut(float inner, float outter, int density = 10)
        {
            var points = new Vector2[density * 2];

            var rad = Mathf.Tau / (density - 1);
            var s = Mathf.Sin(rad);
            var c = Mathf.Cos(rad);

            var v1 = Vector2.Left;
            var v2 = Vector2.Left;
            for (int i = 0, j = density; i < density; i++, j++)
            {
                points[i] = v1 * inner;
                points[j] = v2 * outter;
                v1 = VectorMath.RotatedNoTrig(v1, c, s);
                v2 = VectorMath.RotatedNoTrig(v2, c, -s);
            }

            var safe = new Vector2(0f, SAFE_THRESHOLD);
            points[0] -= safe;
            points[points.Length - 1] -= safe;

            return points;
        }

        /// <param name="h">Horizontal Size</param>
        /// <param name="v">Vertical Size</param>
        /// <param name="th">Thickness of the Horizontal Line</param>
        /// <param name="tv">Thickness of the Vertical Line</param>
        /// <param name="position01">position of interscetion in percentage (0f..1f)</param>
        public static Vector2[] HolyCross2D(float h, float v, float th, float tv, float position01 = 0.75f)
        {
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


        #endregion


        #endregion



    }
}