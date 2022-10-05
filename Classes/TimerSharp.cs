using System;
using System.Collections.Generic;

namespace Util
{
    public class TimerSharp : IDisposable, IRequireGameLoop
    {
        #region Event

        private Action onTimeout;
        public event Action OnTimeout
        {
            add { 
                onTimeout += value;
                refCount++;
            } 
            remove {
                onTimeout -= value;
                if (--refCount <= 0) { 
                    refCount = 0;
                    Paused = true;
                    Reset();
                }
            }
        }

        #endregion

        private int refCount;

        private float accumulator;
        public float TimeoutTime { get; set; } = 1f;
        public bool Paused { get; set; } = true;

        public float Time => accumulator;

        public void Step(float delta)
        {
            if (Paused)
                return;
            accumulator += delta;
            if (accumulator > TimeoutTime)
            {
                onTimeout?.Invoke();
                Reset();
            }
        }

        public void Reset() => accumulator = 0f;

        public void Start(float delay) {
            Reset();
            TimeoutTime = delay;
            Paused = false;
        }

        #region Dispose

        ~TimerSharp() => DisposeInternal();
        public void Dispose()
        {
            DisposeInternal();
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        private void DisposeInternal()
        {
            if (_disposed)
                return;
            Disposing();
            _disposed = true;
        }

        protected virtual void Disposing()
        {
            onTimeout = null;
        }

        #endregion


    }
}
