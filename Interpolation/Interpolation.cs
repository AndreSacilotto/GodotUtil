using System;
using System.Runtime.CompilerServices;

using static Util.UtilShared;

namespace Util.Interpolation
{
    public static class Interpolation
    {
        // https://en.wikipedia.org/wiki/Smoothstep
        // https://sol.gfxile.net/Interpolation/

        #region Smoothstep | https://iquilezles.org/articles/smoothsteps/
        // Inv = Inverse

        #region Polynomial Smoothstep

        [MethodImpl(INLINE)] public static float CubicSmoothstep(float x) => x * x * (3f - 2f * x);
        [MethodImpl(INLINE)] public static float CubicInvSmoothstep(float x) => (float)(0.5 - MathF.Sin(MathF.Asin(1f - 2f * x) / 3f));

        [MethodImpl(INLINE)] public static float QuarticSmoothstep(float x) => x * x * (2f - x * x);
        [MethodImpl(INLINE)] public static float QuarticInvSmoothstep(float x) => MathF.Sqrt(1f - MathF.Sqrt(1f - x));

        [MethodImpl(INLINE)] public static float QuinticSmoothstep(float x) => x * x * x * (x * (x * 6f - 15f) + 10f);

        #endregion

        #region Rational Smoothstep

        [MethodImpl(INLINE)] public static float RatQuadraticSmoothstep(float x) => x * x / (2f * x * x - 2f * x + 1f);
        [MethodImpl(INLINE)] public static float RatQuadraticInvSmoothstep(float x) => (x - MathF.Sqrt(x * (1f - x))) / (2f * x - 1f);

        [MethodImpl(INLINE)] public static float RatCubicSmoothstep(float x) => x * x * x / (3f * x * x - 3f * x + 1f);
        [MethodImpl(INLINE)]
        public static float RatCubicInvSmoothstep(float x)
        {
            var a = MathF.Pow(x, 1f / 3f);
            var b = MathF.Pow(1f - x, 1f / 3f);
            return a / (a + b);
        }

        [MethodImpl(INLINE)] public static float RationalSmoothstep(float x, float n) => MathF.Pow(x, n) / (MathF.Pow(x, n) + MathF.Pow(1f - x, n));
        [MethodImpl(INLINE)] public static float RationalInvSmoothstep(float x, float n) => RationalSmoothstep(x, 1f / n);

        #endregion

        #region Piecewise Smoothstep

        [MethodImpl(INLINE)] public static float PiecewiseQuadraticSmoothstep(float x) => (x < 0.5f) ? 2f * x * x : 2f * x * (2f - x) - 1f;
        [MethodImpl(INLINE)] public static float PiecewiseQuadraticInvSmoothstep(float x) => (x < 0.5f) ? MathF.Sqrt(0.5f * x) : 1f - MathF.Sqrt(0.5f - 0.5f * x);

        [MethodImpl(INLINE)] public static float PiecewisePolynomialSmoothstep(float x, float n) => (x < 0.5f) ? 0.5f * MathF.Pow(2f * x, n) : 1f - 0.5f * MathF.Pow(2f * (1f - x), n);
        [MethodImpl(INLINE)] public static float PiecewisePolynomialInvSmoothstep(float x, float n) => (x < 0.5f) ? 0.5f * MathF.Pow(2f * x, 1f / n) : 1f - 0.5f * MathF.Pow(2f * (1f - x), 1f / n);

        #endregion

        #region Trigonometric Smoothstep

        [MethodImpl(INLINE)] public static float TrigonometricSmoothstep(float x) => 0.5f - 0.5f * MathF.Cos(UtilMath.TAU_180 * x);
        [MethodImpl(INLINE)] public static float TrigonometricInvSmoothstep(float x) => MathF.Acos(1f - 2f * x) / UtilMath.TAU_180;

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

        public static float AlmostIdentity2(float x, float n) => MathF.Sqrt(x * x + n);

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
            return h * MathF.Exp(1f - h);
        }

        public static float QuaImpulse(float x, float k) => 2f * MathF.Sqrt(k) * x / (1f + k * x * x);
        public static float PolyImpulse(float k, float n, float x) => n / (n - 1f) * MathF.Pow((n - 1f) * k, 1f / n) * x / (1f + k * MathF.Pow(x, n));

        public static float ExpSustainedImpulse(float x, float f, float k)
        {
            float s = Math.Max(x - f, 0f);
            return Math.Min(x * x / (f * f), 1f + 2f / f * s * MathF.Exp(-k * s));
        }

        public static float CubicPulse(float c, float w, float x)
        {
            x = Math.Abs(x - c);
            if (x > w)
                return 0f;
            x /= w;
            return 1f - x * x * (3f - 2f * x);
        }

        public static float ExpStep(float x, float k, float n) => MathF.Exp(-k * MathF.Pow(x, n));

        public static float Gain(float x, float k)
        {
            float a = 0.5f * MathF.Pow(2f * ((x < 0.5) ? x : 1f - x), k);
            return (x < 0.5f) ? a : 1f - a;
        }

        public static float Parabola(float x, float k) => MathF.Pow(4f * x * (1f - x), k);

        public static float Pcurve(float x, float a, float b)
        {
            float k = MathF.Pow(a + b, a + b) / (MathF.Pow(a, a) * MathF.Pow(b, b));
            return k * MathF.Pow(x, a) * MathF.Pow(1f - x, b);
        }

        public static float Sinc(float x, float k)
        {
            float a = UtilMath.TAU_180 * (k * x - 1f);
            return MathF.Sin(a) / a;
        }

		#endregion

		#region Godot Easing Equations

		public static float LinearIn(float t, float b, float c, float d) => c * t / d + b;

		// Sine
		public static float SineIn(float t, float b, float c, float d)
		{
			return -c * MathF.Cos(t / d * UtilMath.TAU_90) + c + b;
		}

		public static float SineOut(float t, float b, float c, float d)
		{
			return c * MathF.Sin(t / d * UtilMath.TAU_90) + b;
		}

		public static float SineInOut(float t, float b, float c, float d)
		{
			return -c / 2f * (MathF.Cos(UtilMath.TAU_180 * t / d) - 1f) + b;
		}

		public static float SineOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
				return SineOut(t * 2f, b, c / 2f, d);
			return SineIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		// Quint
		public static float QuintIn(float t, float b, float c, float d)
		{
			return c * MathF.Pow(t / d, 5f) + b;
		}

		public static float QuintOut(float t, float b, float c, float d)
		{
			return c * (MathF.Pow(t / d - 1f, 5f) + 1f) + b;
		}

		public static float QuintInOut(float t, float b, float c, float d)
		{
			t = t / d * 2f;

			if (t < 1f)
				return c / 2f * MathF.Pow(t, 5f) + b;
			return c / 2f * (MathF.Pow(t - 2f, 5f) + 2f) + b;
		}

		public static float QuintOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
				return QuintOut(t * 2f, b, c / 2f, d);
			return QuintIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		// quart
		public static float QuartIn(float t, float b, float c, float d)
		{
			return c * MathF.Pow(t / d, 4f) + b;
		}

		public static float QuartOut(float t, float b, float c, float d)
		{
			return -c * (MathF.Pow(t / d - 1f, 4f) - 1f) + b;
		}

		public static float QuartInOut(float t, float b, float c, float d)
		{
			t = t / d * 2f;

			if (t < 1f)
				return c / 2f * MathF.Pow(t, 4f) + b;
			return -c / 2f * (MathF.Pow(t - 2f, 4f) - 2f) + b;
		}

		public static float QuartOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
				return QuartOut(t * 2f, b, c / 2f, d);
			return QuartIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		// quad
		public static float QuadIn(float t, float b, float c, float d)
		{
			return c * MathF.Pow(t / d, 2f) + b;
		}

		public static float QuadOut(float t, float b, float c, float d)
		{
			t /= d;
			return -c * t * (t - 2f) + b;
		}

		public static float QuadInOut(float t, float b, float c, float d)
		{
			t = t / d * 2f;

			if (t < 1f)
				return c / 2f * MathF.Pow(t, 2f) + b;
			return -c / 2f * ((t - 1f) * (t - 3f) - 1f) + b;
		}

		public static float QuadOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
				return QuadOut(t * 2f, b, c / 2f, d);
			return QuadIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		// expo
		public static float ExpoIn(float t, float b, float c, float d)
		{
			if (t == 0f)
				return b;
			return c * MathF.Pow(2f, 10 * (t / d - 1f)) + b - c * 0.001f;
		}

		public static float ExpoOut(float t, float b, float c, float d)
		{
			if (t == d)
				return b + c;
			return c * 1.001f * (-MathF.Pow(2f, -10 * t / d) + 1f) + b;
		}

		public static float ExpoInOut(float t, float b, float c, float d)
		{
			if (t == 0f)
				return b;

			if (t == d)
				return b + c;

			t = t / d * 2f;

			if (t < 1f)
			{
				return c / 2f * MathF.Pow(2f, 10 * (t - 1f)) + b - c * 0.0005f;
			}
			return c / 2f * 1.0005f * (-MathF.Pow(2f, -10 * (t - 1f)) + 2f) + b;
		}

		public static float ExpoOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
				return ExpoOut(t * 2f, b, c / 2f, d);
			return ExpoIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		// elastic
		public static float ElasticIn(float t, float b, float c, float d)
		{
			if (t == 0f)
				return b;

			t /= d;
			if (t == 1f)
				return b + c;

			t -= 1f;
			float p = d * 0.3f;
			float a = c * MathF.Pow(2f, 10 * t);
			float s = p / 4f;

			return -(a * MathF.Sin((t * d - s) * UtilMath.TAU / p)) + b;
		}

		public static float ElasticOut(float t, float b, float c, float d)
		{
			if (t == 0f)
				return b;

			t /= d;
			if (t == 1f)
				return b + c;

			float p = d * 0.3f;
			float s = p / 4f;

			return c * MathF.Pow(2f, -10 * t) * MathF.Sin((t * d - s) * UtilMath.TAU / p) + c + b;
		}

		public static float ElasticInOut(float t, float b, float c, float d)
		{
			if (t == 0f)
				return b;

			if ((t /= d / 2f) == 2f)
				return b + c;

			float p = d * (0.3f * 1.5f);
			float a = c;
			float s = p / 4f;

			if (t < 1f)
			{
				t -= 1f;
				a *= MathF.Pow(2f, 10 * t);
				return -0.5f * (a * MathF.Sin((t * d - s) * UtilMath.TAU / p)) + b;
			}

			t -= 1f;
			a *= MathF.Pow(2f, -10 * t);
			return a * MathF.Sin((t * d - s) * UtilMath.TAU / p) * 0.5f + c + b;
		}

		public static float ElasticOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
				return ElasticOut(t * 2f, b, c / 2f, d);
			return ElasticIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		// cubic
		public static float CubicIn(float t, float b, float c, float d)
		{
			t /= d;
			return c * t * t * t + b;
		}

		public static float CubicOut(float t, float b, float c, float d)
		{
			t = t / d - 1f;
			return c * (t * t * t + 1f) + b;
		}

		public static float CubicInOut(float t, float b, float c, float d)
		{
			t /= d / 2f;
			if (t < 1f)
				return c / 2f * t * t * t + b;
			t -= 2f;
			return c / 2f * (t * t * t + 2f) + b;
		}

		public static float CubicOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
				return CubicOut(t * 2f, b, c / 2f, d);
			return CubicIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		// circ
		public static float CircIn(float t, float b, float c, float d)
		{
			t /= d;
			return -c * (MathF.Sqrt(1f - t * t) - 1f) + b;
		}

		public static float CircOut(float t, float b, float c, float d)
		{
			t = t / d - 1f;
			return c * MathF.Sqrt(1f - t * t) + b;
		}

		public static float CircInOut(float t, float b, float c, float d)
		{
			t /= d / 2f;
			if (t < 1f)
				return -c / 2f * (MathF.Sqrt(1f - t * t) - 1f) + b;

			t -= 2f;
			return c / 2f * (MathF.Sqrt(1f - t * t) + 1f) + b;
		}

		public static float CircOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
				return CircOut(t * 2f, b, c / 2f, d);
			return CircIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		//bounce

		public static float BounceOut(float t, float b, float c, float d)
		{
			t /= d;

			if (t < (1f / 2.75f))
				return c * (7.5625f * t * t) + b;

			if (t < (2f / 2.75f))
			{
				t -= 1.5f / 2.75f;
				return c * (7.5625f * t * t + 0.75f) + b;
			}

			if (t < (2.5f / 2.75f))
			{
				t -= 2.25f / 2.75f;
				return c * (7.5625f * t * t + 0.9375f) + b;
			}

			t -= 2.625f / 2.75f;
			return c * (7.5625f * t * t + 0.984375f) + b;
		}


		public static float BounceIn(float t, float b, float c, float d)
		{
			return c - BounceOut(d - t, 0f, c, d) + b;
		}

		public static float BounceInOut(float t, float b, float c, float d)
		{
			if (t < d / 2f)
				return BounceIn(t * 2f, b, c / 2f, d);
			return BounceOut(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float BounceOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
				return BounceOut(t * 2f, b, c / 2f, d);
			return BounceIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		// back

		public static float BackIn(float t, float b, float c, float d)
		{
			const float s = 1.70158f;
			t /= d;

			return c * t * t * ((s + 1f) * t - s) + b;
		}

		public static float BackOut(float t, float b, float c, float d)
		{
			const float s = 1.70158f;
			t = t / d - 1f;

			return c * (t * t * ((s + 1f) * t + s) + 1f) + b;
		}

		public static float BackInOut(float t, float b, float c, float d)
		{
			const float s = 1.70158f * 1.525f;
			t /= d / 2f;

			if (t < 1f)
				return c / 2f * (t * t * ((s + 1f) * t - s)) + b;

			t -= 2f;
			return c / 2f * (t * t * ((s + 1f) * t + s) + 2f) + b;
		}

		public static float BackOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
				return BackOut(t * 2f, b, c / 2f, d);
			return BackIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		#endregion

	}
}
