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

        #endregion

        #region Props
        
        public TweenerSharp CurrentTweener => current;
        public int CurrentTweenerIndex => currentIndex;
        public int Count => tweeners.Count;

        /// <summary>Total time that passed animatiing the tween</summary>
        public float Time => totalDuration;
        
        #endregion

        public void AddTweener(TweenerSharp tweener)
        {
            if (current == null)
            {
                currentIndex = 0;
                current = tweener;
            }
            tweeners.Add(tweener);
        }

        public void Step(float delta)
        {
            if (Paused)
                return;
            totalDuration += delta;
            current.Step(delta);
        }

        public void Reset()
        {
            currentIndex = 0;
            totalDuration = 0f;
            foreach (var item in tweeners)
                item.Reset();
            if(tweeners.Count > 0)
                current = tweeners[0];
        }

        public void Clear()
        {
            currentIndex = -1;
            current = null;
            totalDuration = 0;
            DisposeTweeners();
        }

        /// <summary>Time needed to all tweeners to end</summary>
        public float GetCompletationTime() => tweeners.Sum(x => x.Duration);

        private void TweenerEnd(TweenerSharp tweener)
        {
            current = tweeners[++currentIndex];
            OnTweenerEnd?.Invoke(tweener);
            if (currentIndex == tweeners.Count)
                OnTweenFinish?.Invoke();
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
