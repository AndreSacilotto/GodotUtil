using System;

namespace Godot
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ExportRangeAttribute : ExportAttribute
    {
        public ExportRangeAttribute(float min, float max, float step = 1, bool allowLess = false, bool allowGreater = false) : base(
            PropertyHint.Range,
            $"{min},{max},{step:#.######}{ (allowLess ? ",or_lesser" : "") }{ (allowGreater ? ",or_greater" : "") }"
        )
        { }
    }
}