
using System;

namespace Util
{
	public class RandomInt : IRandom<int>, ISeed<int>
	{
		private Random rng = null!;
		private int seed;

		public RandomInt() => rng = new();
		public RandomInt(int seed) => Seed = seed;

		public int Seed
		{
			get => seed; set {
				seed = value;
				rng = new(seed);
			}
		}

		public int Next() => rng.Next();
		public int Next(int max) => rng.Next(max) + 1;
		public int Next(int min, int max) => rng.Next(min, max + 1);
	}
}