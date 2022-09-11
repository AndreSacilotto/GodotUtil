using System;
using Util;

namespace Godot
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ExportRangeAttribute : ExportAttribute
    {
        public ExportRangeAttribute(float min, float max, float step = 0f, bool allowLess = false, bool allowGreater = false) : 
            base(PropertyHint.Range, ExportRangeStringFloat(min, max, step, allowLess, allowGreater)) { }

        public ExportRangeAttribute(int min, int max, int step = 0, bool allowLess = false, bool allowGreater = false) :
            base(PropertyHint.Range, ExportRangeStringInt(min, max, step, allowLess, allowGreater))
        { }


        public static string ExportRangeStringInt(int min, int max, int step, bool allowLess, bool allowGreater)
        {
            var str = $"{min},{max}";

            if (step != 0)
                str += $",{step}";

            if (allowLess)
                str += ",or_lesser";

            if (allowGreater)
                str += ",or_greater";

            return str;
        }

        public static string ExportRangeStringFloat(float min, float max, float step, bool allowLess, bool allowGreater) 
        {
            var str = UtilString.InvariantFormat(min) + ',' + UtilString.InvariantFormat(max);

            if (step != 0)
                str += ',' + UtilString.InvariantFormat(step);

            if (allowLess)
                str += ",or_lesser";

            if (allowGreater)
                str += ",or_greater";
            
            GD.Print(str);
            return str;
        }


    }
}