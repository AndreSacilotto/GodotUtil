using System.Numerics;

namespace Util.Interpolation;

public class TimerSharp<TNumber> : ITween<TNumber>, IRequireGameLoop<TNumber>, IClosable where TNumber : unmanaged, INumber<TNumber>
{
    #region Event

    private Action? onTimeout;
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

    private bool paused;

    private TNumber accumulator, delay;

    public TimerSharp(TNumber delay, bool autostart = true)
    {
        this.delay = delay;
        if (autostart)
            Start();
    }

    public bool IsPaused => paused;
    public TNumber Time => accumulator;
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
            accumulator = TNumber.Min(accumulator, delay);
        }
    }

    public TNumber Percentage => accumulator / delay;
    public TNumber TimeLeft => delay - accumulator;

    public void Step(TNumber delta)
    {
        if (paused)
            return;
        accumulator += delta;
        if (accumulator > delay)
        {
            onTimeout?.Invoke();
            if (NextLoop())
                Reset();
            else
                Pause();
        }
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
        accumulator = TNumber.Zero;
        loopsLeft = loops;
    }

    public void Start()
    {
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
        accumulator = delay;
        paused = true;
        onTimeout?.Invoke();
    }

    public void Close()
    {
        Pause();
        onTimeout = null;
    }

}
