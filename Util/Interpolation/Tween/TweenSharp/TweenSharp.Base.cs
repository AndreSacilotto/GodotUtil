namespace Util.Interpolation;

public partial class TweenSharp : IClosable, ITween<float>
{
    /// <summary>Only emmited after all loops</summary>
    public event Action? OnFinish;
    public event Action? OnBacthFinish;
    public event Action? OnLoopFinish;

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

    public void Start()
    {
        if (tweeners.Count == 0)
            return;
        if (finished)
            Reset();
        currentBatch = batches[currentIndex];
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
        paused = true;
        finished = true;
        OnFinish?.Invoke();
        Stop();
    }

    public void Reset()
    {
        finished = false;
        loopsLeft = loops;
        timeAccumulator = 0f;

        currentIndex = 0;
        conclusions = 0;
    }

    public void Close()
    {
        OnFinish = null;
        OnBacthFinish = null;
        OnLoopFinish = null;
    }

}
