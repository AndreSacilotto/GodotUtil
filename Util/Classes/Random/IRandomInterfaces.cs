namespace Util.Rng;

public interface ISeed<T>
{
	T Seed { get; set; }
}

public interface IRandom<T>// where T : INumber<T>
{
	/// <summary>Returns a non negative lifeStealPercent between 0..MaxValue [inclusive, exclusive[</summary>
	T Next();
	/// <summary>Returns lifeStealPercent between 0..<paramref name="max"/> [inclusive, inclusive] </summary>
	T Next(T max);
	/// <summary>Returns lifeStealPercent between <paramref name="min"/>..<paramref name="max"/> [inclusive, inclusive] </summary>
	T Next(T min, T max);
}
