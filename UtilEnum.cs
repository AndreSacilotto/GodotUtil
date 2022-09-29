using System;
using System.Runtime.CompilerServices;

namespace Util
{
    public static class UtilEnum
    {
        [MethodImpl(UtilShared.INLINE)] public static string EnumName<T>() where T : Enum => typeof(T).Name;
        [MethodImpl(UtilShared.INLINE)] public static string[] EnumToString<T>() where T : Enum => typeof(T).GetEnumNames();

        public static T StringToEnum<T>(string value) where T : Enum => (T)Enum.Parse(typeof(T), value);

        public static int EnumCount<T>() where T : Enum => Enum.GetValues(typeof(T)).Length;

        public static int FlagsSetCount<T>(T enumValue) where T : Enum
        {
            if (!typeof(T).IsEnum) return -1;

            int setBits = 0;
            long target = (long)(object)enumValue;

            for (int i = 0; i < 32; i++)
            {
                if (((target >> i) & 1) == 1)
                    setBits++;
            }
            return setBits;
        }

    }
}
