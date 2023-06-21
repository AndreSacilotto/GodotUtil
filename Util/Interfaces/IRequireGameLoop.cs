namespace Util;

/// <summary>Used by the game loop invocation</summary>
public interface IRequireGameLoop<T> where T : struct
{
    /// <summary>Funtion to be called on Game Loop Update</summary>
    void Step(T delta);
}
