namespace Util.Interpolation;

partial class TweenSharp : IRequireGameLoop<float>
{
    public record struct Batch(int FirstIndex, int LastIndex)
    {
        public int Size => FirstIndex - LastIndex + 1;
    }

    private readonly List<TweenerSharpBase> tweeners = new();
    private readonly List<Batch> batches = new();

    private int conclusions = 0;
    private Batch currentBatch;
    private int currentIndex = 0;

    public int TotalSteps => batches.Count;
    public int TotalTweeners => tweeners.Count;

    public void Step(float delta)
    {
        if (IsPaused)
            return;
        timeAccumulator += delta;

        for (int i = currentBatch.FirstIndex; i <= currentBatch.LastIndex; i++)
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

    private void WhenTweenerEnd()
    {
        conclusions++;
        if (currentBatch.Size == conclusions)
        {
            conclusions = 0;
            currentIndex++;
            OnBacthFinish?.Invoke();
            if (currentIndex >= batches.Count)
            {
                if (NextLoop())
                {
                    OnLoopFinish?.Invoke();
                    currentIndex = 0;
                    Reset();
                }
                else
                {
                    finished = true;
                    OnFinish?.Invoke();
                    Pause();
                    return;
                }
            }

            currentBatch = batches[currentIndex];
        }
    }

    public void Clear()
    {
        batches.Clear();

        foreach (var item in tweeners)
            item.OnTweenerFinish -= WhenTweenerEnd;
        tweeners.Clear();

        Reset();
    }

    public void AddTweener(TweenerSharpBase tweener)
    {
        tweeners.Add(tweener);
        tweener.OnTweenerFinish += WhenTweenerEnd;
        var len = tweeners.Count;
        batches.Add(new(len - 1, len));
    }
    public void AddTweeners(IEnumerable<TweenerSharpBase> tweeners)
    {
        foreach (var item in tweeners)
            AddTweener(item);
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
