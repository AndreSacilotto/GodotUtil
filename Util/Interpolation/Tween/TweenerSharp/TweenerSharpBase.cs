namespace Util.Interpolation;

public abstract class TweenerSharpBase : IClosable, IRequireGameLoop<float>
{
    public event Action? OnTweenerFinish;

    protected float accumulator = 0f;
    protected bool complete = false;

    /// <summary>Total Time of the tweener animation</summary>
    public float Duration { get; set; } = 1f;

    /// <summary>Time spent doing the tweener animation</summary>
    public float Accumulator { get => accumulator; set => float.Max(accumulator, Duration); }

    /// <summary>True if the tweener has completed his animation, false otherwise</summary>
    public bool HasFinish => complete;

    public void Complete() 
    {
        accumulator = Duration;
        complete = true;
        OnTweenerFinish?.Invoke();
    }

    /// <summary>Reset the tweener to be able to play again</summary>
    public void Reset()
    {
        accumulator = 0f;
        complete = false;
    }

    public void Step(float delta)
    {
        if (complete)
            return;

        accumulator += delta;
        
        StepInternal();

        if (accumulator >= Duration)
            Complete();
    }

    protected abstract void StepInternal();

    public virtual void Close() => OnTweenerFinish = null;
}
