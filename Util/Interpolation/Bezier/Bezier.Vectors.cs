﻿using System.Runtime.CompilerServices;

using vec2 = System.Numerics.Vector2;
using vec3 = System.Numerics.Vector3;

namespace Util.Interpolation;

public static partial class Bezier
{
    #region Linear
    [MethodImpl(INLINE)]
    public static vec2 LinearBezier(vec2 start, vec2 end, float t) =>
        start + (end - start) * t;
    [MethodImpl(INLINE)]
    public static vec3 LinearBezier(vec3 start, vec3 end, float t) =>
        start + (end - start) * t;
    #endregion

    #region Quad
    public static vec2 QuadBezier(vec2 start, vec2 control, vec2 end, float t) => new(
            QuadBezier(start.X, control.X, end.X, t),
            QuadBezier(start.Y, control.Y, end.Y, t)
        );

    public static vec3 QuadBezier(vec3 start, vec3 control, vec3 end, float t) => new(
            QuadBezier(start.X, control.X, end.X, t),
            QuadBezier(start.Y, control.Y, end.Y, t),
            QuadBezier(start.Z, control.Z, end.Z, t)
        );
    public static vec2 QuadBezierFirstDerivative(vec2 start, vec2 control, vec2 end, float t) => new(
            QuadBezierFirstDerivative(start.X, control.X, end.X, t),
            QuadBezierFirstDerivative(start.Y, control.Y, end.Y, t)
        );
    public static vec3 QuadBezierFirstDerivative(vec3 start, vec3 control, vec3 end, float t) => new(
            QuadBezierFirstDerivative(start.X, control.X, end.X, t),
            QuadBezierFirstDerivative(start.Y, control.Y, end.Y, t),
            QuadBezierFirstDerivative(start.Z, control.Z, end.Z, t)
        );
    #endregion

    #region Cubic
    public static vec2 CubicBezier(vec2 start, vec2 control0, vec2 control1, vec2 end, float t) => new(
            CubicBezier(start.X, control0.X, control1.X, end.X, t),
            CubicBezier(start.Y, control0.Y, control1.Y, end.Y, t)
        );
    public static vec3 CubicBezier(vec3 start, vec3 control0, vec3 control1, vec3 end, float t) => new(
            CubicBezier(start.X, control0.X, control1.X, end.X, t),
            CubicBezier(start.Y, control0.Y, control1.Y, end.Y, t),
            CubicBezier(start.Z, control0.Z, control1.Z, end.Z, t)
        );
    public static vec2 CubicBezierFirstDerivative(vec2 start, vec2 control0, vec2 control1, vec2 end, float t) => new(
            CubicBezierFirstDerivative(start.X, control0.X, control1.X, end.X, t),
            CubicBezierFirstDerivative(start.Y, control0.Y, control1.Y, end.Y, t)
        );
    public static vec3 CubicBezierFirstDerivative(vec3 start, vec3 control0, vec3 control1, vec3 end, float t) => new(
            CubicBezierFirstDerivative(start.X, control0.X, control1.X, end.X, t),
            CubicBezierFirstDerivative(start.Y, control0.Y, control1.Y, end.Y, t),
            CubicBezierFirstDerivative(start.Z, control0.Z, control1.Z, end.Z, t)
        );
    #endregion
}