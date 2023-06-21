
using Godot;
using Util.Rng;

namespace GodotUtil;

public class GodotRandomInt : IRandom<int>, ISeed<ulong>
{
    private readonly RandomNumberGenerator rng = null!;

    public GodotRandomInt() => rng = new();
    public GodotRandomInt(ulong seed) => Seed = seed;

    public ulong Seed
    {
        get => rng.Seed;
        set => rng.Seed = value;
    }

    public int Next() => rng.RandiRange(0, int.MaxValue - 1);
    public int Next(int max) => rng.RandiRange(0, max);
    public int Next(int min, int max) => rng.RandiRange(min, max);
}