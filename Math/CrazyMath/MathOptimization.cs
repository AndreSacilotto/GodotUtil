using System.Runtime.CompilerServices;

namespace Util.MathC
{
    public static class MathOptimization
    {
        #region Division
        /// <summary> Make a division using bitwise operator </summary>
        /// <param name="divisor">Needs to be power of two</param>
        [MethodImpl(UtilShared.INLINE)]
        public static int PowerOf2Division(int value, Power2 divisor) => value >> divisor.GetExponent();

        #endregion

        #region Power

        [MethodImpl(UtilShared.INLINE)]
        public static int ExponentOf2(int exponent) => 1 << exponent;

        /// <summary>IsPowerOfTwo, but it also return true when value is 0</summary>
        [MethodImpl(UtilShared.INLINE)]
        public static bool IsPowerOfTwoFast(int value) => (value & (value - 1)) == 0;

        [MethodImpl(UtilShared.INLINE)]
        public static bool IsPowerOfTwo(int value) => (value != 0) && IsPowerOfTwoFast(value);

        #endregion

        #region Log

        /// <summary>Floor(Log2(value)). Return wrong result if value eql or less than 0 </summary>
        public static int IntLog2(int value)
        {
            if (value <= 0)
                return -1;

            int result = 0xFFFF - value >> 31 & 0x10;
            value >>= result;
            int shift = 0xFF - value >> 31 & 0x8;
            value >>= shift;
            result |= shift;
            shift = 0xF - value >> 31 & 0x4;
            value >>= shift;
            result |= shift;
            shift = 0x3 - value >> 31 & 0x2;
            value >>= shift;
            result |= shift;
            result |= value >> 1;
            return result;
        }


        #endregion
    }

}
