using System;
using System.Text;

namespace Godot
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ExportRangeAttribute : ExportAttribute
    {
        public ExportRangeAttribute(float min, float max, float? step = null, bool allowLess = false, bool allowGreater = false) : 
            base(PropertyHint.Range, ExportRangeString(min, max, step, allowLess, allowGreater)) { }

        public static string ExportRangeString(float min, float max, float? step, bool allowLess, bool allowGreater) 
        {
            var sb = new StringBuilder($"{min},{max}");

            if(step != null)
                sb.AppendFormat("{0.######}", step);

            if (allowLess)
                sb.Append(",or_lesser");

            if (allowGreater)
                sb.Append(",or_greater");

            return sb.ToString();
        }


    }
}