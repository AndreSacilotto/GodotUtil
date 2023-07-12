using System.Numerics;
using System.Runtime.CompilerServices;

namespace Util;

public static class UtilBit
{
    #region https://www.Youtube.com/watch?v=ZRNO-ewsNcQ

    /// <summary>Find boolean value of (b)it in n</summary>
    [MethodImpl(INLINE)] public static bool IsBitSet(int n, int b) => ((n >> b) & 1) == 1;

    [MethodImpl(INLINE)] public static int SetBit(int value, int b) => (1 << b) | value;
    [MethodImpl(INLINE)] public static int ClearBit(int value, int b) => ~(1 << b) & value;
    [MethodImpl(INLINE)] public static int FlipBit(int value, int b) => (1 << b) ^ value;
    [MethodImpl(INLINE)] public static int GetLSB(int value) => value & (~value);

    public static int SwapBit(int value, int b1, int b2)
    {
        var p = (value >> b1) ^ (value >> b2) & 1;
        value ^= p << b1;
        value ^= p << b2;
        return value;
    }
    public static int BitCount(int value)
    {
        int count;
        for (count = 0; value != 0; count++)
            value &= value - 1;
        return count;
    }
    public static int CountBitIslands(int value) => (value & 1) + BitCount(value ^ (value >> 1)) / 2;

    #endregion

    [MethodImpl(INLINE)] public static int CombineBytes(byte b1, byte b2) => b1 << 8 | b2;

#if NET7_0_OR_GREATER
	[MethodImpl(INLINE)] public static T UnsetFlags<T>(T value, T flags) where T : IBinaryInteger<T> => value &= ~flags;
	[MethodImpl(INLINE)] public static T SetFlags<T>(T value, T flags) where T : IBinaryInteger<T> => value |= flags;
	[MethodImpl(INLINE)] public static T ToggleFlags<T>(T value, T flags) where T : IBinaryInteger<T> => value ^= flags;
	[MethodImpl(INLINE)] public static bool HasFlag<T>(T value, T flags) where T : IBinaryInteger<T> => (value & flags) == flags;
	[MethodImpl(INLINE)] public static bool HasAnyFlag<T>(T value, T flags) where T : IBinaryInteger<T> => (value & flags) != T.Zero;

	[MethodImpl(INLINE)] public static T GetHighOrderBit<T>(T value) where T : IBinaryInteger<T> => value & ~(T.AllBitsSet >>> 1);
	[MethodImpl(INLINE)] public static T GetLowOrderBit<T>(T value) where T : IBinaryInteger<T> => value & T.One;
#endif

}
