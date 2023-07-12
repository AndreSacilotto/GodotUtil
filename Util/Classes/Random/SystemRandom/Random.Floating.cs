namespace Util.Classes;

public class RandomDouble : SystemRandomWithSeed<double>, IRandomFloating<double>
{
    public RandomDouble() : base() { }
    public RandomDouble(int seed) : base(seed) { }

    public double Next01() => rng.NextDouble();

    public override double Next() => rng.NextDouble() * double.MaxValue;
    public override double Next(double max) => rng.NextDouble() * max;
    public override double Next(double min, double max) => rng.NextDouble() * (max - min) + min;
}

public class RandomFloat : SystemRandomWithSeed<float>, IRandomFloating<float>
{
    public RandomFloat() : base() { }
    public RandomFloat(int seed) : base(seed) { }

    public float Next01() => rng.Next();

    public override float Next() => rng.NextSingle() * float.MaxValue;
    public override float Next(float max) => rng.NextSingle() * max;
    public override float Next(float min, float max) => rng.NextSingle() * (max - min) + min;
}
