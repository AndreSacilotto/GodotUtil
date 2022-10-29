using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Util.Test
{
	public class Benchmark
	{
		public delegate void BenchmarkFunc(int i);
		public const int ITERATIONS = 10000;

		public class BenchmarkNode
		{
			private readonly string unit;
			private readonly BenchmarkFunc func;
			public string UnitName => unit;
			public BenchmarkFunc Funtion => func;
			public BenchmarkNode(string unit, BenchmarkFunc func)
			{
				this.unit = unit;
				this.func = func;
			}
		}

		public int Iterations { get; set; } = ITERATIONS;

		public List<BenchmarkNode> Funtions { get; } = new List<BenchmarkNode>();

		public void AddFunc(string unit, BenchmarkFunc func) => Funtions.Add(new BenchmarkNode(unit, func));

		public void Clear() => Funtions.Clear();

		private void Initialize()
		{
			UtilGC.CollectEverthing();
			for (int i = 0; i < Funtions.Count; i++)
				Funtions[i].Funtion.Invoke(i);
		}

		public TimeSpan[] Run(Action<BenchmarkNode, TimeSpan> printFunc = null)
		{
			Initialize();
			var arr = new TimeSpan[Funtions.Count];
			for (int f = 0; f < Funtions.Count; f++)
			{
				var func = Funtions[f].Funtion;
				var sw = Stopwatch.StartNew();
				for (int i = 0; i <= Iterations; i++)
					func.Invoke(i);
				sw.Stop();
				arr[f] = sw.Elapsed;
			}

			if (printFunc != null)
				for (int i = 0; i < arr.Length; i++)
					printFunc(Funtions[i], arr[i]);
			return arr;
		}

		public TimeSpan[] RunInverse(Action<BenchmarkNode, TimeSpan> printFunc = null)
		{
			Initialize();
			var arr = new TimeSpan[Funtions.Count];
			for (int f = Funtions.Count - 1; f >= 0; f--)
			{
				var func = Funtions[f].Funtion;
				var sw = Stopwatch.StartNew();
				for (int i = 0; i < Iterations; i++)
					func.Invoke(i);
				sw.Stop();
				arr[f] = sw.Elapsed;
			}

			if (printFunc != null)
				for (int i = 0; i < arr.Length; i++)
					printFunc(Funtions[i], arr[i]);
			return arr;
		}

		public double ToMiliSeconds(TimeSpan span) => AverageMiliSeconds(span, Iterations);
		public double ToSeconds(TimeSpan span) => AverageSeconds(span, Iterations);

		#region Static

		public static double AverageMiliSeconds(TimeSpan span, int iterations) => span.Milliseconds / iterations;
		public static double AverageSeconds(TimeSpan span, int iterations) => span.TotalSeconds / iterations;

		public static TimeSpan RunSingle(Action act, int iterations = 1)
		{
			UtilGC.CollectEverthing();
			act.Invoke();
			var sw = Stopwatch.StartNew();
			for (int i = 0; i < iterations; i++)
				act.Invoke();
			sw.Stop();
			return sw.Elapsed;
		}

		#endregion

	}
}
