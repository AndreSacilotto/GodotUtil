using System.Runtime.CompilerServices;

namespace Util.Interpolation;

//https://probablymarcus.com/blocks/2015/02/26/using-bezier-curves-as-easing-functions.html
//https://searchfox.org/mozilla-central/source/dom/smil/SMILKeySpline.cpp
//https://github.com/gre/bezier-easing/blob/master/src/index.js
public class BezierCurveMozilla : IBezierCurve<float>
{
    private const int NEWTON_ITERATIONS = 4;
    private const float NEWTON_MIN_SLOPE = 0.02f;
    private const float SUBDIVISION_PRECISION = 0.0000001f;
    private const int SUBDIVISION_MAX_ITERATIONS = 10;

    private const int SPLINE_TABLE_SIZE = 11;
    private const float SAMPLE_STEP_SIZE = 1f / (SPLINE_TABLE_SIZE - 1f);

    private readonly float x1;
    private readonly float y1;
    private readonly float x2;
    private readonly float y2;
    private readonly float[]? sampledValues;

    public float X1 => x1;
    public float X2 => x2;
    public float Y1 => y1;
    public float Y2 => y2;

    public bool IsLinear => sampledValues == null;

    public BezierCurveMozilla(float x1, float y1, float x2, float y2)
    {
        this.x1 = x1;
        this.y1 = y1;
        this.x2 = x2;
        this.y2 = y2;

        if (x1 == y1 && x2 == y2)
            sampledValues = null;
        else
        {
            sampledValues = new float[SPLINE_TABLE_SIZE];
            for (int i = 0; i < SPLINE_TABLE_SIZE; i++)
                sampledValues[i] = CalcBezier(x1, x2, i * SAMPLE_STEP_SIZE);
        }
    }

    //[MethodImpl(INLINE)] private static float A(float a1, float a2) => 1f - 3f * a2 + 3f * a1;
    //[MethodImpl(INLINE)] private static float B(float a1, float a2) => 3f * a2 - 6f * a1;
    //[MethodImpl(INLINE)] private static float C(float a1) => 3f * a1;

    // Use Horner's scheme to evaluate the Bezier polynomial
    [MethodImpl(INLINE)]
    private static float CalcBezier(float a1, float a2, float t)
    {
        //return ((A(a1, a2) * t + B(a1, a2)) * t + C(a1)) * t;
        return (((1f - 3f * a2 + 3f * a1) * t + (3f * a2 - 6f * a1)) * t + (3f * a1)) * t;
    }

    [MethodImpl(INLINE)]
    private static float GetSlope(float a1, float a2, float t)
    {
        //return 3f * A(a1, a2) * t * t + 2f * B(a1, a2) * t + C(a1);
        return 3f * (1f - 3f * a2 + 3f * a1) * t * t +
                2f * (3f * a2 - 6f * a1) * t +
                3f * a1;
    }

    // Original name GetSplineValue
    public float Sample(float t)
    {
        if (x1 == y1 && x2 == y2) // check if is a linear curve
            return t;
        return CalcBezier(y1, y2, GetTForX(t));
    }

    private float GetTForX(float t)
    {
        // Early return when aX == 1f to avoid floating-point inaccuracies.
        if (t == 1f)
            return 1f;

        if(sampledValues == null) // if null means the curve is straight
            return t;

        // Find interval where t lies
        float intervalStart = 0f;
        int currentSample = 1;
        int lastSample = SPLINE_TABLE_SIZE - 1;

        for (; currentSample != lastSample && currentSample <= t; currentSample++)
        {
            intervalStart += SAMPLE_STEP_SIZE;
        }
        currentSample--;  // t now lies between *currentSample and *currentSample+1

        // Interpolate to provide an initial guess for t
        float dist = (t - sampledValues[currentSample]) / (sampledValues[currentSample + 1] - sampledValues[currentSample]);

        float guessForT = intervalStart + dist * SAMPLE_STEP_SIZE;

        // Check the slope to see what strategy to use. If the slope is too small
        // Newton-Raphson iteration won't converge on a root so we use bisection instead.
        float initialSlope = GetSlope(x1, x2, guessForT);

        if (initialSlope >= NEWTON_MIN_SLOPE)
            return NewtonRaphsonIterate(t, guessForT, x1, x2);

        if (initialSlope == 0f)
            return guessForT;

        return BinarySubdivide(t, intervalStart, intervalStart + SAMPLE_STEP_SIZE, x1, x2);
    }

    public static float NewtonRaphsonIterate(float aX, float guessT, float a1, float a2)
    {
        // Refine guess with Newton-Raphson iteration
        for (int i = 0; i < NEWTON_ITERATIONS; i++)
        {
            // We're trying to find where f(t) = aX,
            // so we're actually looking for a root for: CalcBezier(t) - aX
            float currentX = CalcBezier(a1, a2, guessT) - aX;
            float currentSlope = GetSlope(a1, a2, guessT);

            if (currentSlope == 0f)
                return guessT;

            guessT -= currentX / currentSlope;
        }

        return guessT;
    }

    public static float BinarySubdivide(float aX, float aA, float aB, float a1, float a2)
    {
        float currentX;
        float currentT;
        int i = 0;

        do
        {
            currentT = aA + (aB - aA) / 2f;
            currentX = CalcBezier(a1, a2, currentT) - aX;

            if (currentX > 0f)
                aB = currentT;
            else
                aA = currentT;

        } while (MathF.Abs(currentX) > SUBDIVISION_PRECISION && ++i < SUBDIVISION_MAX_ITERATIONS);

        return currentT;
    }
}
