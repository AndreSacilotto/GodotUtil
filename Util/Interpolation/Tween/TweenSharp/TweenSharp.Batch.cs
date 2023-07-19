namespace Util.Interpolation;

partial class TweenSharp : IRequireGameLoop<float>
{
    public readonly record struct TweenerBatch(int FirstIndex, int LastIndex)
    {
        public int Size => FirstIndex - LastIndex + 1;
        public TweenerSharpBase GetLongestTweener(List<TweenerSharpBase> tweeners) 
        {
            var bigger = tweeners[FirstIndex];
            for (int i = FirstIndex + 1; i <= LastIndex; i++)
            {
                if (tweeners[i].Duration > bigger.Duration)
                    bigger = tweeners[i];
            }
            return bigger;
        }
        public float GetOvershot(List<TweenerSharpBase> tweeners)
        {
            var bigger = GetLongestTweener(tweeners);
            return bigger.Accumulator - bigger.Duration;
        }

    }

    private readonly List<TweenerSharpBase> tweeners = new();
    private readonly List<TweenerBatch> batches = new();

    private int conclusions = 0;
    private TweenerBatch currentBatch;
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

    private void WhenTweenerEnd()
    {
        conclusions++;
        if (currentBatch.Size == conclusions)
        {
            var overshot = currentBatch.GetOvershot(tweeners);

            conclusions = 0;
            currentIndex++;
            OnBatchFinish?.Invoke();
            if (currentIndex >= batches.Count)
            {
                if (NextLoop())
                {
                    OnLoopFinish?.Invoke();
                    Reset();
                }
                else
                {
                    End();
                    return;
                }
            }

            currentBatch = batches[currentIndex];
            Step(overshot);
        }
    }

    #region Collection

    public void Clear()
    {
        batches.Clear();

        foreach (var item in tweeners)
            item.OnTweenerFinish -= WhenTweenerEnd;
        tweeners.Clear();

        Reset();
    }

    private void AddTweenerNoBatch(TweenerSharpBase tweener)
    {
        tweeners.Add(tweener);
        tweener.OnTweenerFinish += WhenTweenerEnd;
    }
    public void AddTweener(TweenerSharpBase tweener)
    {
        AddTweenerNoBatch(tweener);
        var len = tweeners.Count;
        batches.Add(new(len - 1, len));
    }
    public void AddTweenersParallel(ICollection<TweenerSharpBase> tweeners)
    {
        var len = tweeners.Count;
        if (len == 0)
            return;
        foreach (var item in tweeners)
            AddTweenerNoBatch(item);
        batches.Add(new(len - 1, tweeners.Count - 1));
    }
    public TweenerSharpDelay AddInterval(float duration)
    {
        var tweener = new TweenerSharpDelay(duration);
        AddTweener(tweener);
        return tweener;
    }

    #endregion

    public float GetCompletationTime()
    {
        if (loops < 0)
            return -1f;

        var loopTime = 0f;
        foreach (var item in batches)
            loopTime += item.GetLongestTweener(tweeners).Duration;
        return loopTime;
    }
    public float GetTimeLeft()
    {
        if (loops < 0)
            return -1f;
        return GetCompletationTime() - timeAccumulator;
    }

}
