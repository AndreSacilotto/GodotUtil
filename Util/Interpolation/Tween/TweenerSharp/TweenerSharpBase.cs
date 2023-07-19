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
    public bool Complete => complete;

    /// <summary>Reset the tweener to be able to play again</summary>
    public void Reset()
    {
        accumulator = 0f;
        complete = false;
    }

    public virtual void Step(float delta) 
    {
        if (complete)
            return;

        accumulator += delta;

        Step();

        if (accumulator >= Duration)
        {
            OnTweenerFinish?.Invoke();
            complete = true;
        }
    }

    protected abstract void Step();

    public virtual void Close()
    {
        OnTweenerFinish = null;
    }

}
