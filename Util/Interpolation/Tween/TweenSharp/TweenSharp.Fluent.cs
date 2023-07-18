namespace Util.Interpolation;

partial class TweenSharp : IRequireGameLoop<float>
{
    public record struct TweenerBatch(int FirstIndex, int LastIndex)
    {
        public int Size => FirstIndex - LastIndex + 1;
    }

    private readonly List<TweenerSharpBase> tweeners = new();
    private readonly List<TweenerBatch> batches = new();

    private int conclusions = 0;
    private TweenerBatch current;
    private int currentIndex = 0;

    public int TotalSteps => batches.Count;
    public int TotalTweeners => tweeners.Count;

    public void Step(float delta)
    {
        if (IsPaused)
            return;
        timeAccumulator += delta;

        for (int i = current.FirstIndex; i <= current.LastIndex; i++)
            tweeners[i].Step(delta);
    }

    /// <summary>Time needed to all tweeners to end</summary>
    public float GetCompletationTime()
    {
        var total = 0f;
        foreach (var item in batches)
        {
            var bigger = 0f;
            for (int i = item.FirstIndex; i <= item.LastIndex; i++)
            {
                var duration = tweeners[i].Duration;
                if (duration > bigger)
                    bigger = duration;
            }
            total += bigger;
        }
        return total;
    }

    private void WhenTweenerEnd(TweenerSharpBase tweener)
    {
        conclusions++;
        if (current.Size == conclusions)
        {
            conclusions = 0;
            currentIndex++;
            if (currentIndex >= batches.Count)
            {
                finished = true;
                onTweenFinished?.Invoke();
            }
            else
                current = batches[currentIndex];
        }
    }

    public void Clear()
    {
        batches.Clear();

        foreach (var item in tweeners)
            item.OnTweenerEnd -= WhenTweenerEnd;
        tweeners.Clear();

        Reset();
    }

    public void AddTweener(TweenerSharpBase tweener)
    {
        tweeners.Add(tweener);
        tweener.OnTweenerEnd += WhenTweenerEnd;
        var len = tweeners.Count;
        batches.Add(new(len - 1, len));
    }
    public void AddTweeners(ICollection<TweenerSharpBase> tweeners)
    {
        if (tweeners.Count == 0)
            return;
        this.tweeners.AddRange(tweeners);
        foreach (var item in tweeners)
            item.OnTweenerEnd += WhenTweenerEnd;
        var len = tweeners.Count;
        batches.Add(new(len - 1, len));
    }
    public void AddTweenersParallel(ICollection<TweenerSharpBase> tweeners)
    {
        var len = tweeners.Count;
        if (len == 0)
            return;
        AddTweeners(tweeners);
        batches.Add(new(len - 1, tweeners.Count - 1));
    }

    public TweenerSharpDelay AddInterval(float duration)
    {
        var tweener = new TweenerSharpDelay(duration);
        AddTweener(tweener);
        return tweener;
    }


}
