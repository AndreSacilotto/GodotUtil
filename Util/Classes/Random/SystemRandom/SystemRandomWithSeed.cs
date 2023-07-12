using System.Numerics;

namespace Util.Classes;

public abstract class SystemRandomWithSeed<T> : IRandom<T>, ISeed<int> where T : INumber<T>
{
    protected Random rng;
    private int seed;

    public SystemRandomWithSeed() => rng = new();
    public SystemRandomWithSeed(int seed)
    {
        this.seed = seed;
        rng = new(seed);
    }

    public int Seed
    {
        get => seed; set
        {
            seed = value;
            rng = new(seed);
        }
    }

    public abstract T Next();
    public abstract T Next(T max);
    public abstract T Next(T min, T max);
}
