using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Util.Test
{
    public class BenchmarkNode
    {
        public string unit;
        public Benchmark.BenchmarkFunc func;
        public BenchmarkNode(string unit, Benchmark.BenchmarkFunc func)
        {
            this.unit = unit;
            this.func = func;
        }
    }

    public class Benchmark
    {
        public delegate void BenchmarkFunc(int i);
        public const int ITERATIONS = 10000;

        public int Iterations { get; set; }
        public Action<BenchmarkNode, TimeSpan> PrintFunc { get; set; }
        public Action<BenchmarkNode, TimeSpan, TimeSpan> PrintFuncBoth { get; set; }

        public List<BenchmarkNode> Funcs { get; } = new List<BenchmarkNode>();

        #region Constructors
        public Benchmark() => Iterations = ITERATIONS;

        #endregion

        #region List Related

        public void AddFunc(string unit, BenchmarkFunc func)
        {
            Funcs.Add(new BenchmarkNode(unit, func));
        }
        public void Clear() => Funcs.Clear();

        #endregion
        private void Initialize()
        {
            CollectGarbage();
            for (int i = 0; i < Funcs.Count; i++)
                Funcs[i].func.Invoke(i);
        }

        public TimeSpan[] Run(bool print = false)
        {
            Initialize();
            var arr = new TimeSpan[Funcs.Count];
            for (int f = 0; f < Funcs.Count; f++)
            {
                var func = Funcs[f].func;
                var sw = Stopwatch.StartNew();
                for (int i = 0; i <= Iterations; i++)
                    func.Invoke(i);
                sw.Stop();
                arr[f] = sw.Elapsed;
            }

            if (print && PrintFunc != null)
                for (int i = 0; i < arr.Length; i++)
                    PrintFunc(Funcs[i], arr[i]);
            return arr;
        }

        public TimeSpan[] RunInverse(bool print = false)
        {
            Initialize();
            var arr = new TimeSpan[Funcs.Count];
            for (int f = Funcs.Count - 1; f >= 0; f--)
            {
                var func = Funcs[f].func;
                var sw = Stopwatch.StartNew();
                for (int i = 0; i < Iterations; i++)
                    func.Invoke(i);
                sw.Stop();
                arr[f] = sw.Elapsed;
            }

            if (print && PrintFunc != null)
                for (int i = 0; i < arr.Length; i++)
                    PrintFunc(Funcs[i], arr[i]);
            return arr;
        }

        public (TimeSpan[] t1, TimeSpan[] t2) RunBoth(bool print = false)
        {
            var t1 = Run(false);
            var t2 = RunInverse(false);
            int len = Math.Max(t1.Length, t2.Length);
            if (print && PrintFunc != null)
                for (int i = 0; i < len; i++)
                    PrintFuncBoth(Funcs[i], t1[i], t2[i]);

            return (t1, t2);
        }


        public double ToSeconds(TimeSpan span) => AverageSeconds(span, Iterations);


        #region Static

        public static Benchmark GlobalBenchmark { get; } = new Benchmark();

        public static double AverageMiliSeconds(TimeSpan span, int iterations) => span.Milliseconds / iterations;
        public static double AverageSeconds(TimeSpan span, int iterations) => span.TotalSeconds / iterations;

        public static TimeSpan RunSingle(Action act, int iterations = 1)
        {
            CollectGarbage();
            act.Invoke();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                act.Invoke();
            sw.Stop();
            return sw.Elapsed;
        }

        private static void CollectGarbage()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        #endregion

    }
}
