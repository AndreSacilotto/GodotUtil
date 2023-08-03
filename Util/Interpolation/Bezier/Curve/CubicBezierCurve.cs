using System.Runtime.CompilerServices;

namespace Util.Interpolation;

// https://probablymarcus.com/blocks/2015/02/26/using-bezier-curves-as-easing-functions.html
public class CubicBezierCurve : IBezierCurve<float>
{
    internal const int NEWTON_ITERATIONS = 8;
    internal const float NEWTON_MIN_SLOPE = 0.01f; //1%

    internal const int SPLINE_TABLE_SIZE = 11;
    //internal const float SAMPLE_STEP_SIZE = 1f / (SPLINE_TABLE_SIZE - 1f);

    private readonly float ax, ay;
    private readonly float bx, by;
    private readonly float cx, cy;

    private readonly float[]? sampledSpline;
    private readonly float sampledStep;

    public bool IsLinear => sampledSpline == null;

    /// https://cubic-bezier.com/
    /// <param name="x1">control point 1 X</param>
    /// <param name="y1">control point 1 Y</param>
    /// <param name="x2">control point 2 X</param>
    /// <param name="y2">control point 2 Y</param>
    public CubicBezierCurve(float x1, float y1, float x2, float y2, int tableSize = SPLINE_TABLE_SIZE)
    {
        CubicBezierCoefficients(x1, x2, out ax, out bx, out cx);
        CubicBezierCoefficients(y1, y2, out ay, out by, out cy);

        //Sample Spline
        if (x1 == y1 && x2 == y2) // check for linearity
        {
            sampledSpline = null;
            sampledStep = 0;
        }
        else
        {
            sampledSpline = new float[tableSize];
            sampledStep = 1f / (tableSize - 1);
            for (int i = 0; i < tableSize; i++)
                sampledSpline[i] = HornerMethodCubic(ax, bx, cx, 0f, i * sampledStep); // get the curve
        }
    }

    public void GetControlPoint1(out float x, out float y)
    {
        x = cx / 3f;
        y = cy / 3f;
    }
    public void GetControlPoint2(out float x, out float y)
    {
        x = (bx + 6f * ax) / 3f;
        y = (by + 6f * ay) / 3f;
    }

    public float Sample(float t)
    {
        if (t <= 0f)
            return 0f;

        if (t >= 1f)
            return 1f;

        if (sampledSpline == null) // means it is linear
            return t;

        int sampleIndex;
        for (sampleIndex = 1; sampleIndex < sampledSpline.Length; sampleIndex++)
            if (t <= sampledSpline[sampleIndex])
                break;

        int previusIndex = sampleIndex - 1;
        float interpolate = (t - sampledSpline[previusIndex]) / (sampledSpline[sampleIndex] - sampledSpline[previusIndex]);
        float interval = previusIndex * sampledStep;
        float guessedT = interval + interpolate * sampledStep;

        float guessedSlope = HornerMethodCubicFirstDerivative(ax, bx, cx, guessedT);

        float xT;

        //If the slope is too small NewtonRaphson iteration wont'x converge on a root
        if (guessedSlope >= NEWTON_MIN_SLOPE)
        {
            xT = NewtonRaphsonCubic(ax, bx, cx, 0f, t, guessedT);
        }
        //If NewtonRaphson fail and the slope is 0, bisection will not work
        else if (MathF.Abs(guessedSlope) < UtilMath.EPSILON_FLOAT)
            xT = guessedT;
        else
            xT = BisectionMethodCubic(ax, bx, cx, 0f, t, interval, interval + sampledStep);

        return HornerMethodCubic(ay, by, cy, 0f, xT);
    }

    #region Horner Method

    /// <summary>
    /// Calculate polynomial coefficients, where first and last control points are (0,0) and (1,1)<br/>
    /// So [p0 = 0] and [p3 = 1] the cubic bezier expanded equation becomes:<br/>
    /// "3at - 6at^2 + 3at^3 + 3bt^2 - 3bt^3 + t^3"<br/>
    /// By applying Horner’s method it becomes:<br/> 
    /// "(((1 - 3b + 3a) * t - 6a + 3b) * t + 3a) * t"<br/>
    /// <br/>Note: p1 = a, p2 = b
    /// </summary>
    /// <param name="a">(1 - 3b + 3a) = 1 - 3 * p2 + 3 * p1</param>
    /// <param name="b">(6a + 3b) = 3 * p2 - 6 * p1</param>
    /// <param name="c">(3a) = 3 * p1</param>
    public static void CubicBezierCoefficients(float p1, float p2, out float a, out float b, out float c)
    {
        // d = 0; //since start is 0
        c = 3f * p1;
        b = 3f * (p2 - p1) - c;
        a = 1f - c - b;
    }

    /// <summary> cubic function (a * x^3 + b * x^2 + c * x + d) expanded by Horner's rule/method</summary>
    [MethodImpl(INLINE)] public static float HornerMethodCubic(float a, float b, float c, float d, float x) => (((a * x + b) * x) + c) * x + d;

    /// <summary> cubic first derivative function (3 * a * x^2 + 2 * b * x + 1 * c + 0 * d) expanded by Horner's rule/method</summary>
    [MethodImpl(INLINE)] public static float HornerMethodCubicFirstDerivative(float a, float b, float c, float x) => (3f * a * x + 2f * b) * x + c;

    #endregion

    // https://en.wikipedia.org/wiki/Newton%27s_method
    public static float NewtonRaphsonCubic(float a, float b, float c, float d, float t, float t2, int iterations = NEWTON_ITERATIONS)
    {
        for (int i = 0; i < iterations; i++)
        {
            // Trying to find [f(x) = t] not [f(x) = t2], so [t2 - t == 0]
            float currentT = HornerMethodCubic(a, b, c, d, t2) - t;
            if (MathF.Abs(currentT) < UtilMath.EPSILON_FLOAT)
                return t2; // success
            float currentSlope = HornerMethodCubicFirstDerivative(a, b, c, t2);
            if (MathF.Abs(currentSlope) < UtilMath.EPSILON_FLOAT) // if slope == 0, means it's not a valid function
                break; // fail
            t2 -= currentT / currentSlope; //f(x)/f'(x)
        }
        return t2; // fail
    }

    //https://en.wikipedia.org/wiki/Bisection_method
    public static float BisectionMethodCubic(float a, float b, float c, float d, float t, float startT = 0f, float endT = 1f)
    {
        float t2 = t;

        while (startT < endT)
        {
            float currentT = HornerMethodCubic(a, b, c, d, t2);

            // check if middle point is root
            if (MathF.Abs(currentT - t) < UtilMath.EPSILON_FLOAT)
                return t2; //success

            // decide the side to repeat the steps
            if (t > currentT)
                startT = t2; // left
            else
                endT = t2; // right

            t2 = (endT + startT) * 0.5f; // middle point same as [start + (end - start) * 0.5]
        }

        return t2; // fail
    }



}
