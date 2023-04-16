using System.Runtime.CompilerServices;

namespace Util;

public static class UtilEnum
{
	[MethodImpl(INLINE)] public static string EnumTitle<T>() where T : Enum => typeof(T).Name;
	[MethodImpl(INLINE)] public static string[] EnumToString<T>() where T : Enum => typeof(T).GetEnumNames();

	public static T StringToEnum<T>(string value) where T : Enum => (T)Enum.Parse(typeof(T), value);
	public static T StringToEnum<T>(string value, bool ignoreCase) where T : Enum => (T)Enum.Parse(typeof(T), value, ignoreCase);

	public static string EnumToName<T>(T enumValue) where T : Enum
	{
		var str = enumValue.ToString();
		var sb = new System.Text.StringBuilder(str.Length);

		if (char.IsLetterOrDigit(str[0]))
			sb.Append(str[0]);
		for (int i = 1; i < str.Length; i++)
		{
			var c = str[i];
			if (c == '_')
				sb.Append(' ');
			else if (char.IsUpper(c) || char.IsNumber(c))
				sb.Append(' ' + c);
			else
				sb.Append(c);
		}

		return sb.ToString();
	}

	public static int FlagsSetCount<T>(T enumValue) where T : Enum
	{
		int settedBits = 0;
		var target = (long)(object)enumValue;

		for (int i = 0; i < 32; i++)
			if (((target >> i) & 1) == 1)
				settedBits++;
		return settedBits;
	}

	#region Collections
	public static int EnumCount<T>() where T : Enum => Enum.GetValues(typeof(T)).Length;

	public static T[] EnumToArray<T>() where T : Enum => (T[])typeof(T).GetEnumValues();
	public static Dictionary<TEnum, TValue> EnumToDictionary<TEnum, TValue>(Func<TValue> newFunc, params TEnum[] skip) where TEnum : Enum
	{
		var arr = EnumToArray<TEnum>();
		var len = arr.Length;
		var dict = new Dictionary<TEnum, TValue>(len - skip.Length);
		for (int i = 0, j; i < len; i++)
		{
			for (j = 0; j < skip.Length; j++)
			{
				if (skip[i].Equals(arr[i]))
					break;
			}
			if (j == skip.Length)
				dict.Add(arr[i], newFunc());
		}
		return dict;
	}

    #endregion

    public static bool HasFlagsAttribute<T>() where T : Enum => typeof(T).IsDefined(typeof(FlagsAttribute), false);

	public static IEnumerable<T> ComposingFlags<T>(T value, int enumSize = -1) where T : Enum
    {
		if (enumSize < 0)
			enumSize = Enum.GetValues(typeof(T)).Length;

		var v = (int)(object)value;
        for (int i = 0; i < enumSize; i++)
        {
            var shift = 1 << i;
			if ((shift & v) == shift)
				yield return (T)(object)shift;
        }
    }

}
