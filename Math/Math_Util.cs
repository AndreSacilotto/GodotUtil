﻿using System;
using System.Runtime.CompilerServices;

namespace Util
{

    public static partial class UtilMath
    {

        #region Pow

        [MethodImpl(UtilShared.INLINE)] public static int Pow2(int value) => value * value;
        [MethodImpl(UtilShared.INLINE)] public static float Pow2(float value) => value * value;
        [MethodImpl(UtilShared.INLINE)] public static double Pow2(double value) => value * value;

        [MethodImpl(UtilShared.INLINE)] public static int Pow3(int value) => value * value * value;
        [MethodImpl(UtilShared.INLINE)] public static float Pow3(float value) => value * value * value;
        [MethodImpl(UtilShared.INLINE)] public static double Pow3(double value) => value * value * value;

        #endregion

        #region Precision

        [MethodImpl(UtilShared.INLINE)] public static float CorrectPrecision(float value) => (float)Math.Round(value, 6);

        #endregion

        #region Rounding

        [MethodImpl(UtilShared.INLINE)] public static float RoundToNearestHalf(float value) => (float)Math.Round(value * 2d) / 2f;
        [MethodImpl(UtilShared.INLINE)] public static float RoundToNearestFloat(float value, double divisorOfOne = 2d) => (float)(Math.Round(value * divisorOfOne) / divisorOfOne);

        [MethodImpl(UtilShared.INLINE)] public static int EvenCeil(int value) => value % 2 != 0 ? ++value : value;
        [MethodImpl(UtilShared.INLINE)] public static int EvenFloor(int value) => value % 2 != 0 ? --value : value;
        [MethodImpl(UtilShared.INLINE)] public static int OddCeil(int value) => value % 2 == 0 ? ++value : value;
        [MethodImpl(UtilShared.INLINE)] public static int OddFloor(int value) => value % 2 == 0 ? --value : value;

        [MethodImpl(UtilShared.INLINE)] public static int FloorToInt(float value) => (int)Math.Floor(value);
        [MethodImpl(UtilShared.INLINE)] public static int FloorToInt(double value) => (int)Math.Floor(value);
        [MethodImpl(UtilShared.INLINE)] public static int RoundToInt(float value) => (int)Math.Round(value);
        [MethodImpl(UtilShared.INLINE)] public static int RoundToInt(double value) => (int)Math.Round(value);
        [MethodImpl(UtilShared.INLINE)] public static int CeilToInt(float value) => (int)Math.Ceiling(value);
        [MethodImpl(UtilShared.INLINE)] public static int CeilToInt(double value) => (int)Math.Ceiling(value);

        #endregion

        #region Clamping

        [MethodImpl(UtilShared.INLINE)]
        public static int Clamp(int value, int min, int max)
        {
            if (value > max)
                return max;
            else if (value < min)
                return min;
            return value;
        }

        [MethodImpl(UtilShared.INLINE)]
        public static float Clamp(float value, float min, float max)
        {
            if (value > max)
                return max;
            else if (value < min)
                return min;
            return value;
        }
        [MethodImpl(UtilShared.INLINE)]
        public static float Clamp01(float value)
        {
            if (value > 1f)
                return 1f;
            else if (value < 0f)
                return 0f;
            return value;
        }

        #endregion

        #region Digit
        
        public static int GetDigit(int value, int power, int radix) => (int)(value / Math.Pow(radix, power)) % radix;
        
        #endregion

    }

}