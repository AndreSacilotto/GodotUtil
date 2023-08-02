using System.Collections;

namespace Util.Interpolation;

partial class TweenSharp : IRequireGameLoop<float>
{
    internal interface ITweenerBatch : IRequireGameLoop<float>, IEnumerable<TweenerSharpDelay>
    {
        int Size { get; }
        TweenerSharpDelay GetLongestTweener();
        float GetOvershot();
    }

    internal class TweenerBatchSingle : ITweenerBatch
    {
        private readonly TweenerSharpDelay tweener;
        public TweenerBatchSingle(TweenerSharpDelay tweener) => this.tweener = tweener;

        public int Size => 1;
        public TweenerSharpDelay GetLongestTweener() => tweener;
        public float GetOvershot() => tweener.Accumulator - tweener.Duration;
        public void Step(float delta) => tweener.Step(delta);

        private IEnumerable<TweenerSharpDelay> EnumerableTween()
        {
            yield return tweener;
        }
        public IEnumerator<TweenerSharpDelay> GetEnumerator() => EnumerableTween().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => EnumerableTween().GetEnumerator();
    }

    internal class TweenerBatchMultiple : ITweenerBatch
    {
        private readonly TweenerSharpDelay[] tweeners;
        public int Size => tweeners.Length;
        public TweenerBatchMultiple(TweenerSharpDelay[] tweeners) => this.tweeners = tweeners;
        public TweenerSharpDelay GetLongestTweener()
        {
            var bigger = tweeners[0];
            for (int i = 1; i < tweeners.Length; i++)
                if (tweeners[i].Duration > bigger.Duration)
                    bigger = tweeners[i];
            return bigger;
        }
        public float GetOvershot()
        {
            var bigger = GetLongestTweener();
            return bigger.Accumulator - bigger.Duration;
        }
        public void Step(float delta)
        {
            for (int i = 0; i < tweeners.Length; i++)
                tweeners[i].Step(delta);
        }

        public IEnumerator<TweenerSharpDelay> GetEnumerator() => ((IEnumerable<TweenerSharpDelay>)tweeners).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => tweeners.GetEnumerator();
    }

}
