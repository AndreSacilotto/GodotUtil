using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Util
{
    public static partial class MathUtil
    {

        #region Trinary

        [MethodImpl(INLINE)]
        public static T ZeroTrinary<T>(int value, T less, T equal, T more)
        {
            //return value == 0 ? equal : (value < 0 ? less : more);
            if (value == 0)
                return equal;
            else if (value < 0)
                return less;
            else
                return more;
        }

        [MethodImpl(INLINE)]
        public static T TrinaryCmp<K, T>(K v1, K v2, T less, T equal, T more) where K : IComparable<K> =>
            ZeroTrinary(v1.CompareTo(v2), less, equal, more);

        #endregion

        #region Bool to Value
        [MethodImpl(INLINE)] public static int BoolValue01(bool value) => value ? 0 : 1;

        [MethodImpl(INLINE)] public static int BoolValue10(bool value) => value ? 1 : 0;

        [MethodImpl(INLINE)] public static int BoolValue11(bool value) => value ? 1 : -1;
        #endregion

        #region Min Max
        [MethodImpl(INLINE)]
        public static void MinMax<T>(T v1, T v2, out T min, out T max) where T : IComparable<T>
        {
            if (v1.CompareTo(v2) > 0)
            {
                max = v1;
                min = v2;
            }
            else
            {
                max = v2;
                min = v1;
            }
        }
        [MethodImpl(INLINE)]
        public static void MinMax<T>(T v1, T v2, IComparer<T> comparer, out T min, out T max)
        {
            if (comparer.Compare(v1, v2) > 0)
            {
                max = v1;
                min = v2;
            }
            else
            {
                max = v2;
                min = v1;
            }
        }

        public static T Min<T>(params T[] values) where T : IComparable<T>
        {
            int len = values.Length;
            if (len == 0)
                return default;
            var smaller = values[0];
            for (int i = 1; i < len; i++)
                if (values[i].CompareTo(smaller) < 0)
                    smaller = values[i];
            return smaller;
        }
        public static T Max<T>(params T[] values) where T : IComparable<T>
        {
            int len = values.Length;
            if (len == 0)
                return default;
            var biggest = values[0];
            for (int i = 1; i < len; i++)
                if (values[i].CompareTo(biggest) > 0)
                    biggest = values[i];
            return biggest;
        }

        #endregion

        #region Even Odd
        [MethodImpl(INLINE)] public static bool IsEven(int value) => value % 2 == 0;
        [MethodImpl(INLINE)] public static bool IsOdd(int value) => value % 2 != 0;

        // Do not use any of the below, trust in the compiler (He is better than you)
        //public static bool IsEvenBitwise(int value) => (value & 1) == 0;
        //public static bool IsEvenBitShifting(int value) => ((value >> 1) << 1) == value;

        #endregion

        #region Swap

        [MethodImpl(INLINE)]
        public static void Swap<T>(ref T a, ref T b)
        {
            var temp = b;
            b = a;
            a = temp;
        }

        #endregion

        #region Precision Float

        [MethodImpl(INLINE)] public static bool MoreThanZero(this float value) => value >  EPSILON_FLOAT;
        [MethodImpl(INLINE)] public static bool LessThanZero(this float value) => value < -EPSILON_FLOAT; 

        #endregion

    }
}