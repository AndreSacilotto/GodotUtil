using System;
using System.Linq;

namespace Godot
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ExportEnumFlagsAttribute : ExportAttribute
	{
		public ExportEnumFlagsAttribute(Type enumType) :
			base(PropertyHint.Flags, enumType.IsEnum ? string.Join(",", Enum.GetValues(enumType).Cast<int>()) : "Invalid Enum Type")
		{ }
	}
}
