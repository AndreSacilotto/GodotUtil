using System;

namespace Util
{
    public static class UtilEnum<T> where T : Enum
    {
        public static T StringToEnum(string value) => (T)Enum.Parse(typeof(T), value);

        public static int EnumCount() => Enum.GetValues(typeof(T)).Length;

        public static int FlagsSetCount(T enumValue)
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

        public static string EnumName() => typeof(T).Name;
        public static string[] EnumToString() => typeof(T).GetEnumNames();

    }
}
