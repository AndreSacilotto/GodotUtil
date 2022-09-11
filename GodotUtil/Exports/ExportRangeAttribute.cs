using System;
using System.Text;

namespace Godot
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ExportRangeAttribute : ExportAttribute
    {
        public ExportRangeAttribute(float min, float max, float step = 0f, bool allowLess = false, bool allowGreater = false) : 
            base(PropertyHint.Range, ExportRangeString(min, max, step, allowLess, allowGreater)) { }

        public ExportRangeAttribute(int min, int max, int step = 0, bool allowLess = false, bool allowGreater = false) :
            base(PropertyHint.Range, ExportRangeString(min, max, step, allowLess, allowGreater))
        { }


        public static string ExportRangeString(int min, int max, int step, bool allowLess, bool allowGreater)
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

        public static string ExportRangeString(float min, float max, float step, bool allowLess, bool allowGreater) 
        {
            var str = $"{min},{max}";

            if (step != 0)
                str += $",{step:0.####}";

            if (allowLess)
                str += ",or_lesser";

            if (allowGreater)
                str += ",or_greater";

            return str;
        }


    }
}