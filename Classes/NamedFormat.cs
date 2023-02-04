using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Util
{
	public class NamedFormat
	{
		private static readonly Regex parseRegex = new(@"{([\w_]+)}", RegexOptions.Compiled);

		private readonly string finalString;

		private readonly string[] NamedParams;
		private readonly string[] NamedParamValues;

		public NamedFormat(string source) : this(new StringBuilder(source)) { }
		public NamedFormat(StringBuilder sb)
		{
			var namedParams = new List<string>();
			var mc = parseRegex.Matches(sb.ToString());

			for (int i = 0; i < mc.Count; i++)
			{
				var key = mc[i].Groups[1].Value;
				if (!namedParams.Contains(key))
					namedParams.Add(key);
			}

			for (int i = mc.Count - 1; i >= 0; i--)
			{
				var m = mc[i];
				sb.Replace(m.Index, m.Length, "{" + namedParams.IndexOf(m.Groups[1].Value) + "}");
			}

			finalString = sb.ToString();

			var len = namedParams.Count;
			NamedParams = new string[len];
			NamedParamValues = new string[len];
			for (int i = 0; i < len; i++)
			{
				NamedParams[i] = namedParams[i];
				NamedParamValues[i] = string.Empty;
			}
		}

		public string FinalString => finalString;

		public bool HasParam(string param) => Array.IndexOf(NamedParams, param) > 0;
		public void SetParam(string param, string value)
		{
			var index = Array.IndexOf(NamedParams, param);
			if (index > 0)
				NamedParamValues[index] = value;
		}
		public void SetParam(string param, object value)
		{
			var index = Array.IndexOf(NamedParams, param);
			if (index > 0)
				NamedParamValues[index] = value.ToString() ?? string.Empty;
		}
		public void SetParam<T>(string param, T value) where T : notnull
		{
			var index = Array.IndexOf(NamedParams, param);
			if (index > 0)
				NamedParamValues[index] = value.ToString() ?? string.Empty;
		}

		public void SetParamsByReflection(string param, object model)
		{
			var props = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

			var prop = props.FirstOrDefault(info => HasParam(info.Name));
			if (prop != null)
			{
				var value = prop.GetValue(model);
				if (value != null)
				{
					SetParam(prop.Name, value);
					return;
				}
			}

			var fields = model.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
			var field = fields.FirstOrDefault(info => HasParam(info.Name));
			if (field != null)
			{
				var value = field.GetValue(model);
				if (value != null)
				{
					SetParam(field.Name, value);
					return;
				}
			}
		}

		public override string ToString() => string.Format(FinalString, NamedParamValues);
		public string ToString(params object[] values) => string.Format(FinalString, values);
	}

}
