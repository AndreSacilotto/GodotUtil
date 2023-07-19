using System.Numerics;

namespace Util.Interpolation;

public interface ITween<T> where T : INumber<T>
{
    /// <summary>
    /// If -1 or less it loops indefinitely<br/>
    /// If 0 it dont loop<br/>
    /// If 1 or more it loops for that amount
    /// </summary>
    int Loops { get; set; }
    /// <summary>How many loops will happen</summary>
    int LoopsLeft { get; }

    /// <summary>If true the tween is paused and is not processing</summary>
    bool IsPaused { get; }

    /// <summary>The total accumalted time of the tween</summary>
    T Time { get; }

    /// <summary>Reset the tween to it initial state</summary>
    void Reset();
    /// <summary>Start the tween or resume it</summary>
    void Start();
    /// <summary>Pause the tween, can be resumed with Start</summary>
    void Pause();
    /// <summary>Stop and reset the tween to its initial state</summary>
    void Stop() 
    {
        Pause();
        Reset();
    }
    /// <summary>Abruptly plays the entire tween</summary>
    void Complete();
}
