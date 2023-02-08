
using System.Collections.Generic;

namespace Util
{
	public interface IPriority
	{
		int Priority { get; }
	}

	public class PriorityComparerLowerFirst : IComparer<IPriority>
	{
		public static PriorityComparerLowerFirst Default { get; } = Create();
		public static PriorityComparerLowerFirst Create() => new();
		public int Compare(IPriority x, IPriority y) => x.Priority.CompareTo(y.Priority);
	}

	public class PriorityComparerHigherFirst : IComparer<IPriority>
	{
		public static PriorityComparerHigherFirst Default { get; } = Create();
		public static PriorityComparerHigherFirst Create() => new();
		public int Compare(IPriority x, IPriority y) => y.Priority.CompareTo(x.Priority);
	}

}