
namespace Util.Interpolation;

public static partial class Bezier
{
    /// <summary> It's the same thing as Lerp</summary>
    public static float LinearBezier(float start, float end, float t)
    {
        return start + (end - start) * t;
    }

    public static float QuadBezier(float start, float control, float end, float t)
    {
        var o = 1f - t;
        return o * o * start +
            o * t * 2f * control +
            t * t * end;
    }

    public static float QuadBezierDerivative(float start, float control, float end, float t)
    {
        return 2f * (1f - t) * (control - start) +
            2f * t * (end - control);
    }

    public static float CubicBezier(float start, float control0, float control1, float end, float t)
    {
        var o = 1f - t;
        var oo = o * o;
        var tt = t * t;

        return oo * o * start +
            3f * oo * t * control0 +
            3f * o * tt * control1 +
            tt * t * end;
    }
    public static float CubicBezierDerivative(float start, float control0, float control1, float end, float t)
    {
        var o = 1f - t;
        return (control0 - start) * 3f * o * o +
            (control1 - control0) * 6f * o * t +
            (end - control1) * 3f * t * t;
    }

}
