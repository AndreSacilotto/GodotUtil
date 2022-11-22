#if NETFRAMEWORK

using System;

namespace Godot
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ExportEnumAttribute : ExportAttribute
	{
		public ExportEnumAttribute(Type enumType) : base(
			PropertyHint.Enum,
			enumType.IsEnum ? string.Join(",", Enum.GetNames(enumType)) : "Invalid Enum Type"
		)
		{ }
	}
}

#endif