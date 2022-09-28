using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using static Util.UtilShared;

namespace Util.MathC
{
    public static class Interpolation
    {
        // https://en.wikipedia.org/wiki/Smoothstep
        // https://sol.gfxile.net/interpolation/

        #region Smoothstep | https://iquilezles.org/articles/smoothsteps/
        // Inv = Inverse

        #region Polynomial Smoothstep

        [MethodImpl(INLINE)] public static float CubicSmoothstep(float x) => x * x * (3f - 2f * x);
        [MethodImpl(INLINE)] public static float CubicInvSmoothstep(float x) => (float)(0.5 - Math.Sin(Math.Asin(1.0 - 2.0 * x) / 3.0));

        [MethodImpl(INLINE)] public static float QuarticSmoothstep(float x) => x * x * (2f - x * x);
        [MethodImpl(INLINE)] public static float QuarticInvSmoothstep(float x) => (float)Math.Sqrt(1.0 - Math.Sqrt(1.0 - x));

        [MethodImpl(INLINE)] public static float QuinticSmoothstep(float x) => x * x * x * (x * (x * 6f - 15f) + 10f);

        #endregion

        #region Rational Smoothstep

        [MethodImpl(INLINE)] public static float RatQuadraticSmoothstep(float x) => x * x / (2f * x * x - 2f * x + 1f);
        [MethodImpl(INLINE)] public static float RatQuadraticInvSmoothstep(float x) => (x - (float)Math.Sqrt(x * (1.0 - x))) / (2f * x - 1f);

        [MethodImpl(INLINE)] public static float RatCubicSmoothstep(float x) => x * x * x / (3f * x * x - 3f * x + 1f);
        [MethodImpl(INLINE)]
        public static float RatCubicInvSmoothstep(float x)
        {
            var a = (float)Math.Pow(x, 1.0 / 3.0);
            var b = (float)Math.Pow(1.0 - x, 1.0 / 3.0);
            return a / (a + b);
        }

        [MethodImpl(INLINE)] public static float RationalSmoothstep(float x, float n) => (float)(Math.Pow(x, n) / (Math.Pow(x, n) + Math.Pow(1.0 - x, n)));
        [MethodImpl(INLINE)] public static float RationalInvSmoothstep(float x, float n) => RationalSmoothstep(x, 1f / n);

        #endregion

        #region Piecewise Smoothstep

        [MethodImpl(INLINE)] public static float PiecewiseQuadraticSmoothstep(float x) => (x < 0.5f) ? 2f * x * x : 2f * x * (2f - x) - 1f;
        [MethodImpl(INLINE)] public static float PiecewiseQuadraticInvSmoothstep(float x) => (x < 0.5f) ? (float)Math.Sqrt(0.5 * x) : 1f - (float)Math.Sqrt(0.5 - 0.5 * x);

        [MethodImpl(INLINE)] public static float PiecewisePolynomialSmoothstep(float x, float n) => (x < 0.5f) ? 0.5f * (float)Math.Pow(2.0 * x, n) : 1.0f - 0.5f * (float)Math.Pow(2.0 * (1.0 - x), n);
        [MethodImpl(INLINE)] public static float PiecewisePolynomialInvSmoothstep(float x, float n) => (x < 0.5f) ? 0.5f * (float)Math.Pow(2.0 * x, 1.0 / n) : 1.0f - 0.5f * (float)Math.Pow(2.0 * (1.0 - x), 1.0 / n);

        #endregion

        #region Trigonometric Smoothstep

        [MethodImpl(INLINE)] public static float TrigonometricSmoothstep(float x) => 0.5f - 0.5f * (float)Math.Cos(Math.PI * x);
        [MethodImpl(INLINE)] public static float TrigonometricInvSmoothstep(float x) => (float)(Math.Acos(1.0 - 2.0 * x) / Math.PI);

        #endregion

        #endregion

        #region IQ Funcs | https://iquilezles.org/articles/functions/

        public static float AlmostIdentity(float x, float m, float n)
        {
            if (x > m)
                return x;
            float a = 2f * n - m;
            float b = 2f * m - 3f * n;
            float t = x / m;
            return (a * t + b) * t * t + n;
        }

        public static float AlmostIdentity2(float x, float n) => (float)Math.Sqrt(x * x + n);

        public static float AlmostUnitIdentity(float x) => x * x * (2f - x);
        public static float IntegralSmoothstep(float x, float t)
        {
            if (x > t)
                return x - t / 2f;
            return x * x * x * (1f - x * 0.5f / t) / t / t;
        }

        public static float ExpImpulse(float x, float k)
        {
            float h = k * x;
            return h * (float)Math.Exp(1.0 - h);
        }

        public static float QuaImpulse(float x, float k) => 2f * (float)Math.Sqrt(k) * x / (1f + k * x * x);
        public static float PolyImpulse(float k, float n, float x) => n / (n - 1f) * (float)Math.Pow((n - 1.0) * k, 1.0 / n) * x / (1f + k * (float)Math.Pow(x, n));

        public static float ExpSustainedImpulse(float x, float f, float k)
        {
            float s = Math.Max(x - f, 0f);
            return Math.Min(x * x / (f * f), 1 + 2f / f * s * (float)Math.Exp(-k * s));
        }

        public static float CubicPulse(float c, float w, float x)
        {
            x = Math.Abs(x - c);
            if (x > w)
                return 0f;
            x /= w;
            return 1f - x * x * (3f - 2f * x);
        }

        public static float ExpStep(float x, float k, float n) => (float)Math.Exp(-k * (float)Math.Pow(x, n));

        public static float Gain(float x, float k)
        {
            float a = 0.5f * (float)Math.Pow(2.0 * ((x < 0.5) ? x : 1.0 - x), k);
            return (x < 0.5f) ? a : 1f - a;
        }

        public static float Parabola(float x, float k) => (float)Math.Pow(4.0 * x * (1.0 - x), k);

        public static float Pcurve(float x, float a, float b)
        {
            float k = (float)Math.Pow(a + b, a + b) / ((float)Math.Pow(a, a) * (float)Math.Pow(b, b));
            return k * (float)Math.Pow(x, a) * (float)Math.Pow(1.0 - x, b);
        }

        public static float Sinc(float x, float k)
        {
            float a = (float)Math.PI * (k * x - 1f);
            return (float)Math.Sin(a) / a;
        }

        #endregion

    }
}
