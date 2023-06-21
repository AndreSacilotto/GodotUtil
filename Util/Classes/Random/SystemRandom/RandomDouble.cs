namespace Util.Rng;

public class RandomDouble : IRandom<double>, ISeed<int>
{
    private Random rng = null!;
    private int seed;

    public RandomDouble() => rng = new();
    public RandomDouble(int seed) => Seed = seed;

    public int Seed
    {
        get => seed; set
        {
            seed = value;
            rng = new(seed);
        }
    }

    public double Next() => rng.Next();
    public double Next(double max) => rng.NextDouble() * max + 1.0;
    public double Next(double min, double max) => rng.NextDouble() * max + min;
}
