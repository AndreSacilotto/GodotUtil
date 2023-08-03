using System.Runtime.CompilerServices;

namespace Util.Interpolation;

public class QuadBezierCurve : IBezierCurve<float>
{
    private readonly float ax, ay;
    private readonly float bx, by;

    private readonly float[]? sampledSpline;
    private readonly float sampledStep;

    /// <param name="x">control point X</param>
    /// <param name="y">control point Y</param>
    public QuadBezierCurve(float x, float y, int tableSize = CubicBezierCurve.SPLINE_TABLE_SIZE)
    {
        QuadBezierCoefficients(x, out ax, out bx);
        QuadBezierCoefficients(y, out ay, out by);

        //Sample Spline
        if (x == y) // check for linearity
        {
            sampledSpline = null;
            sampledStep = 0;
        }
        else
        {
            sampledSpline = new float[tableSize];
            sampledStep = 1f / (tableSize - 1);
            for (int i = 0; i < tableSize; i++)
                sampledSpline[i] = HornerMethodQuad(ax, bx, 0f, i * sampledStep); // get the curve
        }
    }

    public void GetControlPoint(out float x, out float y)
    {
        x = bx / 2f;
        y = by / 2f;
        // or (1 - ax)/2f
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

        float guessedSlope = HornerMethodQuadFirstDerivative(ax, bx, guessedT);

        float xT;

        //If the slope is too small NewtonRaphson iteration wont'x converge on a root
        if (guessedSlope >= CubicBezierCurve.NEWTON_MIN_SLOPE)
        {
            xT = NewtonRaphsonQuad(ax, bx, 0f, t, guessedT);
        }
        //If NewtonRaphson fail and the slope is 0, bisection will not work
        else if (MathF.Abs(guessedSlope) < UtilMath.EPSILON_FLOAT)
            xT = guessedT;
        else
            xT = BisectionMethodQuad(ax, bx, 0f, t, interval, interval + sampledStep);

        return HornerMethodQuad(ay, by, 0f, xT);
    }

    #region Horner Method

    /// <summary>
    /// Calculate polynomial coefficients, where first and last control points are (0,0) and (1,1)<br/>
    /// So [p0 = 0] and [p3 = 1] the quad bezier expanded equation becomes:<br/>
    /// "2ct - 2ct^2 + t^2"<br/>
    /// By applying Horner’s method it becomes:<br/> 
    /// "((1 - 2c) * t + 2c) * t"<br/>
    /// <br/>Note: p1 = c
    /// </summary>
    /// <param name="a">(1 - 2c) = 1 - 2 * p1</param>
    /// <param name="b">(2c) = 2 * p1</param>
    public static void QuadBezierCoefficients(float p1, out float a, out float b)
    {
        // d = 0; //since start is 0
        b = 2 * p1;
        a = 1 - b;
    }

    /// <summary> quadratic function (a * x^2 + b * x + c) expanded by Horner's rule</summary>
    [MethodImpl(INLINE)] public static float HornerMethodQuad(float a, float b, float c, float x) => ((a * x) + b) * x + c;

    /// <summary> quadratic first derivative function (2 * a * x + 1 * b + 0 * c) expanded by Horner's rule</summary>
    [MethodImpl(INLINE)] public static float HornerMethodQuadFirstDerivative(float a, float b, float x) => 2f * a * x + b;

    #endregion

    // https://en.wikipedia.org/wiki/Newton%27s_method
    public static float NewtonRaphsonQuad(float a, float b, float c, float t, float t2, int iterations = CubicBezierCurve.NEWTON_ITERATIONS)
    {
        for (int i = 0; i < iterations; i++)
        {
            // Trying to find [f(x) = t] not [f(x) = t2], so [t2 - t == 0]
            float currentT = HornerMethodQuad(a, b, c, t2) - t;
            if (MathF.Abs(currentT) < UtilMath.EPSILON_FLOAT)
                return t2; // success
            float currentSlope = HornerMethodQuadFirstDerivative(a, b, t2);
            if (MathF.Abs(currentSlope) < UtilMath.EPSILON_FLOAT) // if slope == 0, means it's not a valid function
                break; // fail
            t2 -= currentT / currentSlope; //f(x)/f'(x)
        }
        return t2; // fail
    }

    //https://en.wikipedia.org/wiki/Bisection_method
    public static float BisectionMethodQuad(float a, float b, float c, float t, float startT = 0f, float endT = 1f)
    {
        float t2 = t;

        while (startT < endT)
        {
            float currentT = HornerMethodQuad(a, b, c, t2);

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
