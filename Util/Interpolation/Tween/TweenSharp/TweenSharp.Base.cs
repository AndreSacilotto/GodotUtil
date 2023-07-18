namespace Util.Interpolation;

public partial class TweenSharp : IClosable, ITween<float>
{
    protected Action? onTweenFinished;
    public event Action? OnTweenFinished { add => onTweenFinished += value; remove => onTweenFinished -= value; }

    private bool paused, finished;
    private int loops, loopsLeft;
    private float timeAccumulator;

    protected TweenSharp(bool autostart = true)
    {
        if (autostart)
            Start();
    }

    public float Time => timeAccumulator;

    public int LoopsLeft => loopsLeft;

    public bool HasFinish => finished;
    public bool IsPaused => paused;

    public int Loops
    {
        get => loops;
        set
        {
            loops = value;
            loopsLeft = Math.Min(loops, loopsLeft);
        }
    }

    protected bool NextLoop()
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

    public virtual void Start()
    {
        if (finished)
            Reset();
        paused = false;
    }

    public virtual void Pause() => paused = true;

    public void Stop()
    {
        paused = true;
        Reset();
    }

    public virtual void End()
    {
        finished = true;
        onTweenFinished?.Invoke();
        Stop();
    }

    public virtual void Reset()
    {
        finished = false;
        loopsLeft = loops;
        timeAccumulator = 0f;

        current = default;
        currentIndex = 0;
        conclusions = 0;
    }

    public virtual void Close()
    {
        onTweenFinished = null;
    }

}
