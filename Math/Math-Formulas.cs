using System.Runtime.CompilerServices;

namespace Util
{

    public static partial class MathUtil
    {

        #region Percent
        [MethodImpl(Util.UtilShared.INLINE)]
        public static float ValuePercent(float value, float max) => max != 0f ? value / max : 0f;
        [MethodImpl(Util.UtilShared.INLINE)]
        public static float ValuePercent(float value, float max, float min)
        {
            float diff = max - min;
            return diff != 0f ? (value - min) / diff : -1f;
        }

        #endregion

        #region Interpolation & Remapping

        [MethodImpl(Util.UtilShared.INLINE)]
        public static float Lerp(float from, float to, float weight) => from + ((to - from) * weight);
        [MethodImpl(Util.UtilShared.INLINE)]
        public static float InverseLerp(float from, float to, float weight) => (weight - from) / (to - from);
        [MethodImpl(Util.UtilShared.INLINE)]
        public static float InverseLerpClamped(float from, float to, float value) => Clamp01((value - from) / (to - from));
        [MethodImpl(Util.UtilShared.INLINE)]
        public static float Remap(float fromMin, float fromMax, float toMin, float toMax, float value)
        {
            float t = InverseLerp(fromMin, fromMax, value);
            return Lerp(toMin, toMax, t);
        }
        [MethodImpl(Util.UtilShared.INLINE)]
        public static float RemapClamped(float fromMin, float fromMax, float toMin, float toMax, float value)
        {
            float t = InverseLerpClamped(fromMin, fromMax, value);
            return Lerp(toMin, toMax, t);
        }

        #endregion

        #region Average
        // https://math.stackexchange.com/questions/22348/how-to-add-and-subtract-values-from-an-average
        // size = number of elements that already exist

        public static float AddToAverage(float average, float newValue, int newSize) =>
            average + (newValue - average) / newSize;

        public static float SubtractFromAverage(float average, float newValue, int newSize) =>
            average + (newValue - average) / newSize;

        public static float ReplaceInAverage(float average, int size, float oldValue, float newValue) =>
            (size * average - oldValue + newValue) / size;

        public static float MergeAverages(float average1, int size1, float average2, int size2) =>
            (size1 * average1 + size2 * average2) / (size1 + size2);

        #endregion

    }
}