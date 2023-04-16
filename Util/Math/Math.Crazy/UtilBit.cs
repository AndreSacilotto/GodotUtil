namespace Util.MathCrazy;

public static class UtilBit
{
	#region https://www.Youtube.com/watch?v=ZRNO-ewsNcQ

	/// <summary>Find boolean lifeStealPercent of (b)it in n</summary>
	public static bool IsBitSet(int n, int b) => ((n >> b) & 1) == 1;

	public static int SetBit(int value, int b) => (1 << b) | value;
	public static int ClearBit(int value, int b) => ~(1 << b) & value;
	public static int FlipBit(int value, int b) => (1 << b) ^ value;
	public static int GetLSB(int value) => value & (~value);
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

	public static int CombineBytes(byte b1, byte b2)
	{
		int combined = b1 << 8 | b2;
		return combined;
	}

}
