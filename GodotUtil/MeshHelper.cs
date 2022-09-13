
using Godot;

namespace Util
{
    public static class MeshHelper
    {
        public static void Generate(this SurfaceTool st, bool generateNormals, bool generateTangents, bool generateIndex)
        {
            if (generateNormals)
                st.GenerateNormals();
            if (generateTangents)
                st.GenerateTangents();
            if (generateIndex)
                st.Index();
        }

        #region Rotation

        public static void RotateVectors(Vector3[] points, float rotation, Vector3 axis)
        {
            for (int i = 0; i < points.Length; i++)
                points[i] = points[i].Rotated(axis, rotation);
        }

        public static void RotateVectors(Vector2[] points, float rotation)
        {
            for (int i = 0; i < points.Length; i++)
                points[i] = points[i].Rotated(rotation);
        }

        #endregion

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

        /// <summary> 
        /// https://math.stackexchange.com/questions/1344690/is-it-possible-to-find-the-vertices-of-an-equilateral-triangle-given-its-center
        /// </summary>
        /// <param name="r">Radius/Size</param>
        public static Vector2[] TriangleEquilateral(float r)
        {
            var t = Mathf.Tan(MathUtil.TAU_30) * r;

            return new Vector2[3] {
                new Vector2(0f, t * -2f),
                new Vector2(r, t),
                new Vector2(-r, t),
            };
        }

        /// <param name="radius"></param>
        /// <param name="sides">Number of sides of the primivite polygon</param>
        public static Vector2[] SimplePolygon2D(float radius, int sides)
        {
            if (sides < 3)
                //return System.Array.Empty<Vector2>();
                return null;

            var points = new Vector2[sides];

            var rad = Mathf.Tau / sides;

            points[0] = new Vector2(0, -radius);
            for (int i = 1; i < sides; i++)
                points[i] = points[i - 1].Rotated(rad);

            return points;
        }


        #endregion



    }
}