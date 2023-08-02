using System.Runtime.CompilerServices;

namespace Util.Interpolation;

// https://chromium.googlesource.com/chromium/blink/+/master/Source/platform/animation/UnitBezier.h
public readonly struct BezierCurveChromium : IBezierCurve<float>
{
    private const int NEWTON_ITERATIONS = 8;

    private readonly float ax, ay;
    private readonly float bx, by;
    private readonly float cx, cy;
    private readonly float startGradient;
    private readonly float endGradient;

    public BezierCurveChromium(float x1, float y1, float x2, float y2)
    {
        // Calculate the polynomial coefficients, first and last control points are (0,0) and (1,1)
        // for X:
        cx = 3f * x1;
        bx = 3f * (x2 - x1) - cx;
        ax = 1f - cx - bx;
        // for Y:
        cy = 3f * y1;
        by = 3f * (y2 - y1) - cy;
        ay = 1f - cy - by;

        // End-point gradients are used to calculate timing function results outside the range [0, 1]:
        //
        // There are three possibilities for the gradient at each end:
        // (1) the closest control point is not horizontally coincident with regard to
        //     (0, 0) or (1, 1). In this case the line between the end point and
        //     the control point is tangent to the bezier at the end point;
        // (2) the closest control point is coincident with the end point. In
        //     this case the line between the end point and the far control
        //     point is tangent to the bezier at the end point;
        // (3) the closest control point is horizontally coincident with the end
        //     point, but vertically distinct. In this case the gradient at the
        //     end point is Infinite. However, this causes issues when
        //     interpolating. As a result, we break down to a simple case of
        //     0 gradient under these conditions;
        if (x1 > 0f)
            startGradient = y1 / x1;
        else if (y1 == 0f && x2 > 0f)
            startGradient = y2 / x2;
        else
            startGradient = 0;

        if (x2 < 1f)
            endGradient = (y2 - 1f) / (x2 - 1f);
        else if (x2 == 1f && x1 < 1f)
            endGradient = (y1 - 1f) / (x1 - 1f);
        else
            endGradient = 0f;
    }

    public float Sample(float t)
    {
        if (t < 0f)
            return 0f + startGradient * t;
        if (t > 1f)
            return 1f + endGradient * (t - 1f);
        return SampleCurve(ay, by, cy, SolveCurve(ax, bx, cx, t));
    }

    // `ax t^3 + bx t^2 + cx t' expanded using Horner's rule
    [MethodImpl(INLINE)] private static float SampleCurve(float a, float b, float c, float t) => ((a * t + b) * t + c) * t;
    [MethodImpl(INLINE)] private static float SampleCurveDerivative(float a, float b, float c, float t) => (3f * a * t + 2f * b) * t + c;

    // Given an x value, find a parametric value it came from
    private static float SolveCurve(float a, float b, float c, float t)
    {
        if (t < 0f || t > 1f)
            throw new Exception();

        // First try a few iterations of Newton's method -- normally very fast
        float t2 = t;
        for (int i = 0; i < NEWTON_ITERATIONS; i++)
        {
            float x2 = SampleCurve(a, b, c, t2) - t;
            if (MathF.Abs(x2) < UtilMath.EPSILON_FLOAT)
                return t2;
            float d2 = SampleCurveDerivative(a, b, c, t2);
            if (MathF.Abs(d2) < UtilMath.EPSILON_FLOAT)
                break;
            t2 -= x2 / d2;
        }
        // Fall back to the bisection method for reliability
        float t0 = 0f;
        float t1 = 1f;
        t2 = t;
        while (t0 < t1)
        {
            float x2 = SampleCurve(a, b, c, t2);
            if (MathF.Abs(x2 - t) < UtilMath.EPSILON_FLOAT)
                return t2;
            if (t > x2)
                t0 = t2;
            else
                t1 = t2;
            t2 = (t1 - t0) * 0.5f + t0;
        }

        // Return something if both fail
        return t2;
    }


}

/*
 * Copyright (C) 2008 Apple Inc. All Rights Reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY APPLE INC. ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL APPLE INC. OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY
 * OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
 */