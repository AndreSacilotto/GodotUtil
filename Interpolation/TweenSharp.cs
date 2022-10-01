using System;
using System.Collections.Generic;

namespace Util.Interpolation
{
    public partial class TweenSharp : IDisposable
    {
        public event Action TweenSharpEnd;
        public event Action<TweenerSharp> TweenerSharpEnd;

        #region Vars

        protected List<TweenerSharp> tweeners = new(1);
        protected float totalDuration;

        private TweenerSharp current;
        private int currentIndex;
        public bool Running { get; set; } = false;

        #endregion

        #region Props
        
        public TweenerSharp CurrentTweener => current;
        public int CurrentTweenerIndex => currentIndex;
        public int Count => tweeners.Count;

        /// <summary>Current time of animation between</summary>
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
            if (Running)
            {
                totalDuration += delta;
                current.Step(delta);
            }
        }

        public void Reset()
        {
            currentIndex = 0;
            totalDuration = 0;
            foreach (var item in tweeners)
                item.Reset();
            current = tweeners[0];
        }

        public void Clear()
        {
            currentIndex = -1;
            current = null;
            totalDuration = 0;
            DisposeTweeners();
        }

        private void TweenerEnd(TweenerSharp tweener)
        {
            current = tweeners[++currentIndex];
            TweenerSharpEnd?.Invoke(tweener);
            if (currentIndex == tweeners.Count)
                TweenSharpEnd?.Invoke();
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

            TweenSharpEnd = null;
            TweenerSharpEnd = null;
            DisposeTweeners();
            tweeners = null;

            _disposed = true;
        }

        #endregion


    }
}
