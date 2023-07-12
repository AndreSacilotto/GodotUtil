using System.Numerics;

namespace Util.Classes;

public interface ISeed<T>
{
    T Seed { get; set; }
}

public interface IRandom<T>
{
    /// <summary>Returns a non negative value between 0..(<typeparamref name="T"/>.MaxValue) [inclusive, exclusive[</summary>
    T Next();
    /// <summary>
    /// Returns value between 0..<paramref name="max"/> [inclusive, exclusive[<br/> 
    /// Integer: [inclusive, inclusive] and using <typeparamref name="T"/>.MaxValue will cause a error. <br/> 
    /// </summary>
    T Next(T max);
    /// <summary>
    /// Returns value between <paramref name="min"/>..<paramref name="max"/> [inclusive, exclusive[<br/> 
    /// Integer: [inclusive, inclusive] and using <typeparamref name="T"/>.MaxValue will cause a error.
    /// </summary>
    T Next(T min, T max);
}

public interface IRandomFloating<T> where T : IFloatingPoint<T>
{
    /// <summary>Returns a non negative value between 0..1 [inclusive, exclusive[</summary>
    T Next01();
}

public interface IRandomInteger<T> where T : IBinaryInteger<T>
{
    /// <summary>Returns a non negative value between 0..(<typeparamref name="T"/>.MaxValue) [inclusive, inclusive]</summary>
    T NextInclusive();
    /// <summary>
    /// Returns value between 0..<paramref name="max"/> [inclusive, inclusive]<br/> 
    /// Using <typeparamref name="T"/>.MaxValue will cause a operation overflow<br/> 
    /// </summary>
    T NextInclusive(T max);
    /// <summary>
    /// Returns value between <paramref name="min"/>..<paramref name="max"/> [inclusive, inclusive]<br/> 
    /// Using <typeparamref name="T"/>.MaxValue will cause a operation overflow
    /// </summary>
    T NextInclusive(T min, T max);
}