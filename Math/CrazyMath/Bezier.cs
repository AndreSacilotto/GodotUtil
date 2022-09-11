using System;
using System.Collections.Generic;

using Vector2 = Godot.Vector2;
using Vector3 = Godot.Vector3;

namespace Util.MathC
{
    public static class Bezier
    {
        public static Vector2 LinearBezier(Vector2 start, Vector2 end, float t) => (1f - t) * start + t * end;
        public static Vector3 LinearBezier(Vector3 start, Vector3 end, float t) => (1f - t) * start + t * end;

        public static Vector2 QuadraticBezier(Vector2 start, Vector2 pivot, Vector2 end, float t)
        {
            var o = 1f - t;

            return o * o * start +
                    o * t * pivot +
                    t * t * end;
        }
        public static Vector3 QuadraticBezier(Vector3 start, Vector3 pivot, Vector3 end, float t)
        {
            var o = 1f - t;

            return o * o * start +
                    o * t * pivot +
                    t * t * end;
        }

        public static Vector2 CubicBezier(Vector2 start, Vector2 pivot0, Vector2 pivot1, Vector2 end, float t)
        {
            var o = 1f - t;
            var oo = o * o;
            var tt = t * t;

            return oo * o * start +
                    oo * t * 3 * pivot0 +
                    tt * t * pivot1 +
                    tt * o * 3 * end;
        }
        public static Vector3 CubicBezier(Vector3 start, Vector3 pivot0, Vector3 pivot1, Vector3 end, float t)
        {
            var o = 1f - t;
            var oo = o * o;
            var tt = t * t;

            return oo * o * start +
                    oo * t * 3 * pivot0 +
                    tt * t * pivot1 +
                    tt * o * 3 * end;
        }

    }
}
