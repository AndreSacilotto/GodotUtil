using System.Numerics;

namespace Util.Interpolation;

public class TimerSharp<TNumber> : ITween<TNumber>, IRequireGameLoop<TNumber>, IClosable where TNumber : unmanaged, IFloatingPoint<TNumber>
{
    #region Event

    private Action? onTimeout;

    /// <summary>Timeout is invoked every time the timer ends or a new loop starts</summary>
    public event Action OnTimeout
    {
        add
        {
            onTimeout += value;
        }
        remove
        {
            onTimeout -= value;
            if (onTimeout == null)
                Stop();
        }
    }

    #endregion

    private int loops, loopsLeft;

    private bool paused = true;
    private bool finished = false;

    private TNumber timeAccumulator, delay;

    public TimerSharp(TNumber delay) => this.delay = delay;

    public bool IsFinished => paused;
    public bool IsPaused => paused;
    public TNumber Time => timeAccumulator;
    public int LoopsLeft => loopsLeft;

    public int Loops
    {
        get => loops;
        set
        {
            loops = value;
            loopsLeft = Math.Min(loops, loopsLeft);
        }
    }

    public TNumber Delay
    {
        get => delay;
        set
        {
            delay = value;
            timeAccumulator = TNumber.Min(timeAccumulator, delay);
        }
    }

    public TNumber LoopPercentage => timeAccumulator / delay;
    public TNumber TotalPercentage => timeAccumulator / GetCompletationTime();

    public void Step(TNumber delta)
    {
        if (paused)
            return;
        timeAccumulator += delta;
        if (timeAccumulator >= delay)
        {
            if (NextLoop()) 
            {
                onTimeout?.Invoke();
                var overshot = timeAccumulator - delay;
                Reset();
                Step(overshot);
            }
            else
                End();
        }
    }
    
    private void End() 
    {
        finished = true;
        paused = true;
        onTimeout?.Invoke();
    }

    private bool NextLoop()
    {
        if (loopsLeft < 0)
            return true;
        if (loopsLeft > 0)
        {
            loopsLeft--;
            return true;
        }
        return false;
    }

    public void Reset()
    {
        timeAccumulator = TNumber.Zero;
        loopsLeft = loops;
    }

    public void Start()
    {
        if (finished)
            Reset();
        paused = false;
    }

    public void Pause() => paused = true;

    public void Stop()
    {
        Pause();
        Reset();
    }

    public void Complete() 
    {
        if (loops < 0)
            return;
        End();
    }

    public void Close()
    {
        onTimeout = null;
    }

    public TNumber GetCompletationTime()
    {
        if (loops < 0)
            return TNumber.NegativeOne;
        return delay * (TNumber.One + TNumber.CreateChecked(loops));
    }

    public TNumber GetTimeLeft()
    {
        if (loops < 0)
            return TNumber.NegativeOne;
        return GetCompletationTime() - timeAccumulator;
    }

}
