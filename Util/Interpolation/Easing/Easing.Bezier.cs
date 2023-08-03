namespace Util.Interpolation;

using vec2 = System.Numerics.Vector2;

// https://forum.unity.com/threads/custom-easing-functions.293141/
partial class Easing
{
    public static EaseFunc QuadBezierCurve(vec2 control)
    {
        var bez = new QuadBezierCurve(control.X, control.Y);

        float Func(float percent, float initial, float delta, float duration)
        {
            var t = percent / duration;

            var r = bez.Sample(t);

            return delta * r + initial;
        }
        return Func;
    }

    public static EaseFunc CubicBezierCurve(vec2 control0, vec2 control1)
    {
        var bez = new CubicBezierCurve(control0.X, control0.Y, control1.X, control1.Y);

        float Func(float percent, float initial, float delta, float duration)
        {
            var t = percent / duration;

            var r = bez.Sample(t);

            return delta * r + initial;
        }
        return Func;
    }
}
