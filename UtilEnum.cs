using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Util
{
	public static class UtilEnum
	{
		[MethodImpl(UtilShared.INLINE)] public static string EnumTitle<T>() where T : Enum => typeof(T).Name;
		[MethodImpl(UtilShared.INLINE)] public static string[] EnumToString<T>() where T : Enum => typeof(T).GetEnumNames();

		public static T StringToEnum<T>(string value) where T : Enum => (T)Enum.Parse(typeof(T), value);
		public static T StringToEnum<T>(string value, bool ignoreCase) where T : Enum => (T)Enum.Parse(typeof(T), value, ignoreCase);
		public static string EnumToString<T>(T value) where T : Enum => Enum.GetName(typeof(T), value);

		public static int EnumCount<T>() where T : Enum => Enum.GetValues(typeof(T)).Length;
		public static int FlagsSetCount<T>(T enumValue) where T : Enum
		{
			//if (!typeof(T).IsEnum) return -1;
			int settedBits = 0;
			var target = (long)(object)enumValue;

			for (int i = 0; i < 32; i++)
				if (((target >> i) & 1) == 1)
					settedBits++;
			return settedBits;
		}

		public static T[] EnumToArray<T>() where T : Enum => (T[])Enum.GetValues(typeof(T));
		public static Dictionary<TEnum, TValue> EnumToDict<TEnum, TValue>(Func<TValue> newFunc, bool skipFirst = false, bool skipLast = false) where TEnum : Enum
		{
			var arr = EnumToArray<TEnum>();
			var len = arr.Length;
			int start = skipFirst ? 1 : 0;
			int end = skipLast ? len - 1 : len;
			var dict = new Dictionary<TEnum, TValue>(len);
			for (int i = start; i < end; i++)
				dict.Add(arr[i], newFunc());
			return dict;
		}


	}
}
