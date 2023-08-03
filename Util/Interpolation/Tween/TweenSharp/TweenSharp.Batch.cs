namespace Util.Interpolation;

partial class TweenSharp : IRequireGameLoop<float>
{
    private readonly List<ITweenerBatch> tweenerBatchs = new();

    private int conclusions = 0;
    private int currentIndex = 0;

    public int TotalBatchs => tweenerBatchs.Count;

    public int GetTotalTweeners() 
    {
        var sum = 0;
        foreach (var batch in tweenerBatchs)
            sum += batch.Size;
        return sum;
    }

    public void Step(float delta)
    {
        if (paused)
            return;

        timeAccumulator += delta;

        tweenerBatchs[currentIndex].Step(delta);
    }

    private void WhenTweenerEnd()
    {
        var currentBatch = tweenerBatchs[currentIndex];
        conclusions++;
        if (currentBatch.Size == conclusions)
        {
            var overshot = currentBatch.GetOvershot();

            conclusions = 0;
            currentIndex++;
            OnBatchFinish?.Invoke();
            if (currentIndex >= tweenerBatchs.Count)
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
            Step(overshot);
        }
    }

    #region Collection

    public void AddTweener(TweenerSharpDelay tweener)
    {
        tweener.OnTweenerFinish += WhenTweenerEnd;
        tweenerBatchs.Add(new TweenerBatchSingle(tweener));
    }

    public void AddParallelTweeners(params TweenerSharpDelay[] tweeners)
    {
        if (tweeners.Length == 0)
            return;
        foreach (var tweener in tweeners)
            tweener.OnTweenerFinish += WhenTweenerEnd;
        tweenerBatchs.Add(new TweenerBatchMultiple(tweeners));
    }

    public void Clear()
    {
        foreach (var batch in tweenerBatchs)
            foreach (var tweener in batch)
                tweener.OnTweenerFinish -= WhenTweenerEnd;
        tweenerBatchs.Clear();
        Reset();
    }

    public void ReverseTweenersOrder() 
    {
        Stop();
        tweenerBatchs.Reverse();
    }

    #endregion

    public float GetCompletationTime()
    {
        if (loops < 0)
            return -1f;

        var loopTime = 0f;
        foreach (var item in tweenerBatchs)
            loopTime += item.GetLongestTweener().Duration;
        return loopTime;
    }
    public float GetTimeLeft()
    {
        if (loops < 0)
            return -1f;
        return GetCompletationTime() - timeAccumulator;
    }

}
