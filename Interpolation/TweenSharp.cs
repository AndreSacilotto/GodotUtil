using System;
using System.Collections.Generic;
using System.Linq;

namespace Util.Interpolation
{
    public partial class TweenSharp : IDisposable, IRequireGameLoop
    {
        public event Action OnTweenFinish;
        public event Action<TweenerSharp> OnTweenerEnd;

        #region Vars

        protected List<TweenerSharp> tweeners = new(1);
        protected float totalDuration;

        private TweenerSharp current;
        private int currentIndex;
        public bool Paused { get; set; } = true;
        public bool Repeat { get; set; } = false;

        public Interpolation.EaseFunc DefaultEaseFunc { get; set; } = Interpolation.LinearIn;

        #endregion

        #region Props

        public TweenerSharp CurrentTweener => current;
        public int Count => tweeners.Count;
        public bool HasTweeners => tweeners.Count > 0;

        /// <summary>Total time that passed animating the tween</summary>
        public float Time => totalDuration;

        #endregion

        public void Step(float delta)
        {
            if (Paused)
                return;
            totalDuration += delta;
            current.Step(delta);
        }

        public TweenerSharp CreateTweener()
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
            if (tweeners.Remove(tweener)) { 
                Reset();
                if(dispose)
                    tweener.Dispose();
            }
        }

        public void Reset()
        {
            currentIndex = 0;
            totalDuration = 0f;
            foreach (var item in tweeners)
                item.Reset();
            if (tweeners.Count > 0)
                current = tweeners[0];
        }

        public void Clear()
        {
            currentIndex = -1;
            current = null;
            totalDuration = 0;
            DisposeTweeners();
            Paused = true;
        }

        /// <summary>Time needed to all tweeners to end</summary>
        public float GetCompletationTime() => tweeners.Sum(x => x.Duration);

        private void TweenerEnd(TweenerSharp tweener)
        {
            currentIndex++;
            if (currentIndex == tweeners.Count) { 
                OnTweenFinish?.Invoke();
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

        ~TweenSharp() => Dispose(false);

        protected void DisposeTweeners()
        {
            foreach (var item in tweeners)
                item.Dispose();
            tweeners.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            OnTweenFinish = null;
            OnTweenerEnd = null;
            DisposeTweeners();
            tweeners = null;

            _disposed = true;
        }

        #endregion


    }
}
