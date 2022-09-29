using System;
using System.Runtime.CompilerServices;

namespace Util
{

    public static partial class UtilMath
    {

        #region Percent

        [MethodImpl(UtilShared.INLINE)] public static float ValuePercent(float value, float max) => max != 0f ? value / max : 0f;
        [MethodImpl(UtilShared.INLINE)] public static float ValuePercent(float value, float max, float min) => max != min ? (value - min) / (max - min) : 0f;

        #endregion

        #region Interpolation & Remapping

        [MethodImpl(UtilShared.INLINE)] public static float Lerp(float from, float to, float weight) => from + ((to - from) * weight);
        [MethodImpl(UtilShared.INLINE)] public static float LerpClamped(float from, float to, float weight) => Clamp01(from + ((to - from) * weight));

        [MethodImpl(UtilShared.INLINE)] public static float InverseLerp(float from, float to, float weight) => (weight - from) / (to - from);
        [MethodImpl(UtilShared.INLINE)] public static float InverseLerpClamped(float from, float to, float value) => Clamp01((value - from) / (to - from));
        [MethodImpl(UtilShared.INLINE)] public static float Remap(float fromMin, float fromMax, float toMin, float toMax, float value) => Lerp(toMin, toMax, InverseLerp(fromMin, fromMax, value));
        [MethodImpl(UtilShared.INLINE)] public static float RemapClamped(float fromMin, float fromMax, float toMin, float toMax, float value) => Lerp(toMin, toMax, InverseLerpClamped(fromMin, fromMax, value));

        public static float Smoothstep(float min, float max, float value)
        {
            if (value < min) return 0f;
            if (value >= max) return 1f;
            float t = (value - min) / (max - min);
            return t * t * (3f - 2f * t);
        }
        public static float Smootherstep(float min, float max, float x)
        {
            x = Clamp01((x - min) / (max - min));
            return x * x * x * (x * (x * 6f - 15f) + 10f);
        }

        #endregion

        #region Average
        // https://math.stackexchange.com/questions/22348/how-to-add-and-subtract-values-from-an-average
        // size = number of elements that already exist

        [MethodImpl(UtilShared.INLINE)] public static float AddToAverage(float average, float newValue, int newSize) => average + (newValue - average) / newSize;

        [MethodImpl(UtilShared.INLINE)] public static float SubtractFromAverage(float average, float newValue, int newSize) => average + (newValue - average) / newSize;

        [MethodImpl(UtilShared.INLINE)] public static float ReplaceInAverage(float average, int size, float oldValue, float newValue) => (size * average - oldValue + newValue) / size;

        [MethodImpl(UtilShared.INLINE)] public static float MergeAverages(float average1, int size1, float average2, int size2) => (size1 * average1 + size2 * average2) / (size1 + size2);

        #endregion

    }
}