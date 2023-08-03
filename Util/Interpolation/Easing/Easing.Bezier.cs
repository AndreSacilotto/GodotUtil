namespace Util.Interpolation;

using vec2 = System.Numerics.Vector2;

// https://forum.unity.com/threads/custom-easing-functions.293141/
partial class Easing
{
    public static EaseFunc QuadBezierCurve(vec2 control)
    {
        var bez = new QuadBezierCurve(control.X, control.Y);

        float Func(float time, float initial, float change, float duration)
        {
            var t = time / duration;

            var s = bez.Sample(t);

            return initial + s * change;
        }
        return Func;
    }

    public static EaseFunc CubicBezierCurve(vec2 control0, vec2 control1)
    {
        var bez = new CubicBezierCurve(control0.X, control0.Y, control1.X, control1.Y);

        float Func(float time, float initial, float change, float duration)
        {
            var t = time / duration;

            var s = bez.Sample(t);

            return initial + s * change;
        }
        return Func;
    }
}
