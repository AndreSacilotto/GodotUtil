using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

using static Util.UtilShared;

namespace Util
{
	public static class UtilString
	{
		public const string FLOAT_FIXED_POINT = "0.############################################################################################################";

		public const string NUMBER_WITH_SIGN = "+#;-#;0";

		public const string NUMBERS = "0123456789";
		public const string CHAR_LOWER = "abcdefghijklmnopqrstuvwxyz";
		public const string CHAR_UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		#region Format

		[MethodImpl(INLINE)] public static string InvariantFormat(float value) => value.ToString(NumberFormatInfo.InvariantInfo);
		[MethodImpl(INLINE)] public static string InvariantFormat(double value) => value.ToString(NumberFormatInfo.InvariantInfo);

		#endregion

		#region Percent

		public static NumberFormatInfo PercentFormat => new() {
			PercentGroupSeparator = string.Empty,
			PercentPositivePattern = 1,
			PercentNegativePattern = 1,
			PercentDecimalSeparator = ".",
		};

		public static string LowPrecisePercentage(float value) =>
			value.ToString("0.##\\%", PercentFormat);

		public static string PrecisePercentage(float value, out decimal decimalValue)
		{
			decimalValue = Math.Floor((decimal)value * 100m) / 100m;
			if (Math.Floor(decimalValue) % 1 == 0)
				return decimalValue.ToString("P0", PercentFormat);
			return decimalValue.ToString("P1", PercentFormat);
		}

		#endregion

		#region Contains

		[MethodImpl(INLINE)] public static bool Contains(this string source, char value, int startIdx = 0) => source.IndexOf(value, startIdx) >= 0;

		[MethodImpl(INLINE)]
		public static bool Contains(this string source, string value, int startIdx = 0, StringComparison strCmp = StringComparison.InvariantCultureIgnoreCase) =>
			source.IndexOf(value, startIdx, strCmp) >= 0;
		[MethodImpl(INLINE)]
		public static bool Contains(this string source, string value, int startIdx, int length, StringComparison strCmp = StringComparison.InvariantCultureIgnoreCase) =>
			source.IndexOf(value, startIdx, length, strCmp) >= 0;

		#endregion

		#region Replace

		public static string Replace(this string text, char search, string replacement)
		{
			var sb = new StringBuilder(text.Length);
			for (int i = 0; i < text.Length; i++)
				sb.Append(text[i] == search ? replacement : text[i]);
			return sb.ToString();
		}

		public static string Replace(this string text, string replacement, params char[] search)
		{
			var sb = new StringBuilder(text.Length);
			for (int i = 0; i < text.Length; i++)
			{
				bool found = false;
				for (int j = 0; j < search.Length; j++)
				{
					if (text[i] == search[j])
					{
						sb.Append(replacement);
						found = true;
						break;
					}
				}

				if (!found)
					sb.Append(text[i]);
			}
			return sb.ToString();
		}

		public static string ReplaceAll(string text, string replacement, params string[] search)
		{
			var sb = new StringBuilder();

			for (int i = 0; i < text.Length; i++)
			{
				bool found = false;
				for (int j = 0; j < search.Length; j++)
				{
					var current = search[j];
					if (i - text.Length < current.Length)
						continue;

					bool same = true;
					int k;
					for (k = 0; k < current.Length; k++)
						same = text[i + k] == current[k];

					if (same)
					{
						sb.Append(replacement);
						i += k;
						found = true;
						break;
					}
				}

				if (!found)
					sb.Append(text[i]);
			}

			return sb.ToString();
		}

		public static string[] SplitAndReplace(string str, char separator = ' ')
		{
			if (str == null || str.Length == 0)
				return Array.Empty<string>();

			var len = str.Length;

			var isSplit = new bool[len];
			int foundSplit = 1;

			bool isSeparator, wasSeparator = false;
			for (int i = 1; i < len; i++)
			{
				isSeparator = str[i] == separator;
				if (isSeparator && !wasSeparator)
				{
					isSplit[i] = true;
					foundSplit++;
				}
				wasSeparator = isSeparator;
			}

			if (foundSplit == 1)
				return new string[1] { str.Substring(0, str.Length) };

			string strSeparator = "" + separator;
			var split = new string[foundSplit];
			int size = 1, index = 0, start = 0;
			for (int i = 1; i < len; i++, size++)
				if (isSplit[i])
				{
					split[index++] = str.Substring(start, size).Replace(strSeparator, string.Empty);
					start = i + 1;
					size = 0;
				}
			split[index] = str.Substring(start, --size).Replace(strSeparator, string.Empty);

			return split;
		}

		#endregion

		#region Others

		public static uint SumString(string str) 
		{
			uint sum = 0;
			for (int i = 0; i < str.Length; i++)
				sum += str[i];
			return sum;
		}

		public static string NicifyVariableName(string str)
		{
			if (str == null || str.Length == 0)
				return string.Empty;

			str = str.Trim();
			var sb = new StringBuilder();

			int i = 1;

			if (char.IsLetter(str[0]))
				sb.Append(char.ToUpper(str[0]));
			else if (str[0] == '_' && str.Length > 1)
				sb.Append(char.ToUpper(str[i++]));

			for (; i < str.Length; i++)
			{
				if (char.IsUpper(str[i]))
				{
					var i1 = i + 1;
					if (i1 < str.Length && char.IsLetterOrDigit(str[i1]) && !char.IsUpper(str[i1]))
						sb.Append(' ');
				}
				sb.Append(str[i]);
			}

			return sb.ToString();
		}

		public static string GenerateID(int lenght, bool allowNumbers, bool allowLowerCase, bool allowUpperCase)
		{
			var allowChars = string.Empty;
			if (allowNumbers)
				allowChars += NUMBERS;
			if (allowLowerCase)
				allowChars += CHAR_LOWER;
			if (allowUpperCase)
				allowChars += CHAR_UPPER;

			var rng = new Random();
			var sb = new StringBuilder(lenght);
			var len = allowChars.Length;
			for (int i = 0; i < lenght; i++)
				sb.Append(allowChars[rng.Next(len)]);
			return sb.ToString();
		}

		#endregion

		#region Collections to String

		public static string CollectionToString<TK, TV>(IEnumerable<KeyValuePair<TK, TV>> ie, Func<TK, TV, string> func, string separator = " ")
		{
			var sb = new StringBuilder();
			foreach (var item in ie)
				sb.Append(func(item.Key, item.Value) + separator);
			return sb.ToString(0, sb.Length - separator.Length);
		}

		public static string CollectionToString<T>(IEnumerable<T> ie, Func<T, string> func, string separator = " ")
		{
			var sb = new StringBuilder();
			foreach (var item in ie)
				sb.Append(func(item) + separator);
			return sb.ToString(0, sb.Length - separator.Length);
		}

		#endregion

	}

}