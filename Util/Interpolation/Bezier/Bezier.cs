
using System.Runtime.CompilerServices;

namespace Util.Interpolation;

public static partial class Bezier
{
    /// <summary> It's the same thing as Lerp</summary>
    [MethodImpl(INLINE)]
    public static float LinearBezier(float start, float end, float t) => start + (end - start) * t;

    public static float QuadBezier(float start, float control, float end, float t)
    {
        var o = 1f - t;
        return o * o * start +
            o * t * 2f * control +
            t * t * end;
    }
    /// <summary>Get the slope/tangent</summary>
    public static float QuadBezierFirstDerivative(float start, float control, float end, float t)
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
    /// <summary>Get the slope/tangent</summary>
    public static float CubicBezierFirstDerivative(float start, float control0, float control1, float end, float t)
    {
        var o = 1f - t;
        return (control0 - start) * 3f * o * o +
            (control1 - control0) * 6f * o * t +
            (end - control1) * 3f * t * t;
    }

    public static float CubicHermiteSpline(float start, float tangent0, float tangent1, float end, float t)
    {
        float tt = t * t;
        float ttt = t * t * t;

        return (2f * ttt - 3f * tt + 1f) * start +
            (ttt - 2f * tt + t) * tangent0 +
            (-2f * ttt + 3f * tt) * end +
            (ttt - tt) * tangent1;
    }


    /// <summary> https://solhsa.com/interpolation/index.html Spline</summary>
    public static float Catmullrom(float p0, float p1, float p2, float p3, float t)
    {
        return 0.5f * ((2f * p1) + (-p0 + p2) * t + 
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t + 
            (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t);
    }

}
