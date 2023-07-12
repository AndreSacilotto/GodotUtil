namespace Util.Classes;

public class RandomInt : SystemRandomWithSeed<int>, IRandomInteger<int>
{
    public RandomInt() : base() { }
    public RandomInt(int seed) : base(seed) { }

    public override int Next() => rng.Next();
    public override int Next(int max) => rng.Next(max);
    public override int Next(int min, int max) => rng.Next(min, max);

    public int NextInclusive() => rng.Next(0, int.MaxValue);
    public int NextInclusive(int max) => rng.Next(0, max + 1);
    public int NextInclusive(int min, int max) => rng.Next(min, max + 1);
}

public class RandomLong : SystemRandomWithSeed<long>, IRandomInteger<long>
{
    public RandomLong() : base() { }
    public RandomLong(int seed) : base(seed) { }

    public override long Next() => rng.NextInt64();
    public override long Next(long max) => rng.NextInt64(max);
    public override long Next(long min, long max) => rng.NextInt64(min, max);

    public long NextInclusive() => rng.NextInt64(0L, long.MaxValue);
    public long NextInclusive(long max) => rng.NextInt64(0L, max + 1L);
    public long NextInclusive(long min, long max) => rng.NextInt64(min, max + 1L);
}
