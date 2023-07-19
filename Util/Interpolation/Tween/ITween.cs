using System.Numerics;

namespace Util.Interpolation;

public interface ITween<T> where T : IFloatingPoint<T>
{
    /// <summary>
    /// If -1 or less it loops indefinitely<br/>
    /// If 0 it dont loop<br/>
    /// If 1 or more it loops for that amount
    /// </summary>
    int Loops { get; set; }
    /// <summary>How many loops will still happen</summary>
    int LoopsLeft { get; }

    /// <summary>If true the tween has ended</summary>
    bool IsFinished { get; }

    /// <summary>If true the tween is paused and is not processing</summary>
    bool IsPaused { get; }

    /// <summary>The total accumulated time on tweening</summary>
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
    /// <summary>
    /// Plays the remaining tween instantly, completing it<br/>
    /// If loops is negative the method does nothing
    /// </summary>
    void Complete();

    /// <summary>Total time required to play the entire tween</summary>
    /// <returns>-1 if loops is negative</returns>
    public T GetCompletationTime();
    /// <summary>Time that is still play rest of the tween</summary>
    /// <returns>-1 if loops is negative</returns>
    public T GetTimeLeft();
}
