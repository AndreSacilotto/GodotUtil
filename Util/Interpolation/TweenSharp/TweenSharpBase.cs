namespace Util.Interpolation;

public abstract class TweenSharpBase : IPausable, IClosable, IRequireGameLoop<float>
{
    public event Action? OnTweenFinish;

    protected float totalDuration;
    protected TweenerSharpBase? current;

    public bool Paused { get; set; } = true;
    public bool Repeat { get; set; } = false;

    /// <summary>Total time that passed animating the tween</summary>
    public float Time => totalDuration;

    public TweenerSharpBase? Current => current;

    protected void CallOnTweenFinish() => OnTweenFinish?.Invoke();
    public void Step(float delta)
    {
        if (Paused || current == null)
            return;
        totalDuration += delta;
        current.Step(delta);
    }

    /// <summary>Time needed to all tweeners to end</summary>
    public abstract float GetCompletationTime();

    public abstract void Reset();
    public abstract void Clear();
    public abstract void TweenerEnd(TweenerSharpBase tweener);

    public virtual void Close()
    {
        Clear();
        OnTweenFinish = null;
    }

}
