using System;
using System.Collections.Generic;
using System.Linq;

namespace Util.Interpolation
{
    public class TweenSharpMult : TweenSharp
    {
        public event Action<TweenerSharp> OnTweenerEnd;

        protected List<TweenerSharp> tweeners = new(1);

        private int currentIndex;

        public int Count => tweeners.Count;
        public bool HasTweeners => tweeners.Count > 0;

        public override TweenerSharp CreateTweener()
        {
            var tweener = new TweenerSharp(this);
            if (current == null)
            {
                currentIndex = 0;
                current = tweener;
            }
            tweeners.Add(tweener);
            return tweener;
        }

        /// <summary>Remove the an tweener and reset the animation</summary>
        public void RemoveTweener(TweenerSharp tweener, bool dispose = false)
        {
            if (tweeners.Remove(tweener)) 
            { 
                Reset();
                if(dispose)
                    tweener.Dispose();
            }
        }

        public override void Reset(bool pause = false)
        {
            currentIndex = 0;
            totalDuration = 0f;
            foreach (var item in tweeners)
                item.Reset();
            if (tweeners.Count > 0)
                current = tweeners[0];
            Paused = pause;
        }

        public override void Clear()
        {
            totalDuration = 0f;
            currentIndex = -1;
            current = null;
            ReleaseTweeners();
            Paused = true;
        }

        public override float GetCompletationTime() => tweeners.Sum(x => x.Duration);

        protected override void TweenerEnd(TweenerSharp tweener)
        {
            currentIndex++;
            if (currentIndex == tweeners.Count) {
                CallOnTweenFinish();
                Reset();
                Paused = !Repeat;
            }
            else
            {
                current = tweeners[currentIndex];
                current.Reset();
            }
            OnTweenerEnd?.Invoke(tweener);
        }

        #region Dispose

        protected void ReleaseTweeners()
        {
            foreach (var item in tweeners)
                item.Dispose();
            tweeners.Clear();
        }

        protected override void Disposing()
        {
            base.Disposing();
            OnTweenerEnd = null;
            ReleaseTweeners();
            tweeners = null;
        }

        #endregion


    }
}
