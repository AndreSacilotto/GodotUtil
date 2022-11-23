using System;
using System.Runtime.CompilerServices;

using static Util.UtilShared;

namespace Util
{
	/// <summary> 
	/// System.Math float Wrapper, wrapping every and only funtions that doesnt accept float 
	/// This class is Obslote in .NET6 was MathF exist
	/// </summary>
	public static class MathF
	{
		public const float E = (float)Math.E;
		public const float PI = (float)Math.PI;
#pragma warning disable IDE1006 // Naming Styles
		public const float Tau = (float)(Math.PI * 2.0);
#pragma warning restore IDE1006 // Naming Styles

		[MethodImpl(INLINE)] public static float Acos(float value) => (float)Math.Acos(value);
		//[MethodImpl(INLINE)] public static float Acosh(float value) => (float)Math.Acosh(value); //.NET6
		[MethodImpl(INLINE)] public static float Asin(float value) => (float)Math.Asin(value);
		//[MethodImpl(INLINE)] public static float Asinh(float value) => (float)Math.Asinh(value); //.NET6
		[MethodImpl(INLINE)] public static float Atan(float value) => (float)Math.Atan(value);
		//[MethodImpl(INLINE)] public static float Atanh(float value) => (float)Math.Atanh(value); //.NET6
		[MethodImpl(INLINE)] public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);
		//[MethodImpl(INLINE)] public static float BitDecrement(float value) => (float)Math.BitDecrement(value); //.NET6
		//[MethodImpl(INLINE)] public static float BitIncrement(float value) => (float)Math.BitIncrement(value); //.NET6
		//[MethodImpl(INLINE)] public static float Cbrt(float value) => (float)Math.Cbrt(value); //.NET6
		[MethodImpl(INLINE)] public static float Ceiling(float value) => (float)Math.Ceiling(value);
		[MethodImpl(INLINE)] public static float Cos(float value) => (float)Math.Cos(value);
		[MethodImpl(INLINE)] public static float Cosh(float value) => (float)Math.Cosh(value);
		//[MethodImpl(INLINE)] public static float CopySign(float value) => (float)Math.CopySign(value); //.NET6
		[MethodImpl(INLINE)] public static float Exp(float value) => (float)Math.Exp(value);
		[MethodImpl(INLINE)] public static float Floor(float value) => (float)Math.Floor(value);
		//[MethodImpl(INLINE)] public static float FusedMultiplyAdd(float x, float y, float z) => (float)Math.FusedMultiplyAdd(x, y, z); //.NET6
		[MethodImpl(INLINE)] public static float IEEERemainder(float y, float x) => (float)Math.IEEERemainder(x, y);
		//[MethodImpl(INLINE)] public static float ILogB(float value) => (float)Math.ILogB(value); //.NET6
		[MethodImpl(INLINE)] public static float Log(float value) => (float)Math.Log(value);
		[MethodImpl(INLINE)] public static float Log(float value, float newBase) => (float)Math.Log(value, newBase);
		[MethodImpl(INLINE)] public static float Log10(float value) => (float)Math.Log10(value);
		//[MethodImpl(INLINE)] public static float Log2(float value) => (float)Math.Log2(value); //.NET6
		//[MethodImpl(INLINE)] public static float MaxMagnitude(float x, float y) => (float)Math.MaxMagnitude(x, y); //.NET6
		//[MethodImpl(INLINE)] public static float MinMagnitude(float x, float y) => (float)Math.MinMagnitude(x, y); //.NET6
		[MethodImpl(INLINE)] public static float Pow(float x, float y) => (float)Math.Pow(x, y);
		//[MethodImpl(INLINE)] public static float ReciprocalEstimate(float x, float y) => (float)Math.ReciprocalEstimate(x, y); //.NET6
		//[MethodImpl(INLINE)] public static float ReciprocalSqrtEstimate(float x, float y) => (float)Math.ReciprocalSqrtEstimate(x, y); //.NET6
		[MethodImpl(INLINE)] public static float Round(float value) => (float)Math.Round(value);
		[MethodImpl(INLINE)] public static float Round(float value, int digits) => (float)Math.Round(value, digits);
		[MethodImpl(INLINE)] public static float Round(float value, int digits, MidpointRounding mode) => (float)Math.Round(value, digits, mode);
		//[MethodImpl(INLINE)] public static float ScaleB(float x, float y) => (float)Math.ScaleB(x, y); //.NET6
		[MethodImpl(INLINE)] public static float Sin(float value) => (float)Math.Sin(value);
		//[MethodImpl(INLINE)] public static (float,float) SinCos(float x) => (float,float)Math.SinCos(x); //.NET6
		[MethodImpl(INLINE)] public static float Sinh(float value) => (float)Math.Sinh(value);
		[MethodImpl(INLINE)] public static float Sqrt(float value) => (float)Math.Sqrt(value);
		[MethodImpl(INLINE)] public static float Tan(float value) => (float)Math.Tan(value);
		[MethodImpl(INLINE)] public static float Tanh(float value) => (float)Math.Tanh(value);
		[MethodImpl(INLINE)] public static float Truncate(float value) => (float)Math.Truncate(value);


	}
}
