using System;

namespace Util.Interpolation
{
    public partial class TweenSharp
    {
        public sealed class TweenerSharp : IDisposable
        {
            private TweenSharp owner;

            private Action<float> Interpolation;
            private Interpolation.EaseFunc EasingFunction;

            private float from, to, change;

            private bool complete;

            public float Duration { get; set; }
            public float Accumulator { get; set; }
            public float From { get; set; }

            public TweenerSharp(TweenSharp owner)
            {
                this.owner = owner;
            }

            #region Props

            public TweenSharp Owner => owner;

            public bool Complete => complete;
            public float ChangeValue => change;
            public float FromValue
            {
                get => from;
                set {
                    from = value;
                    SetChange();
                }
            }
            public float ToValue
            {
                get => From - to;
                set {
                    to = value;
                    SetChange();
                }
            }

            #endregion

            public void Setup(float from, float to, float duration, Action<float> interpolation, Interpolation.EaseFunc easingFunction = null)
            {
                Interpolation = interpolation ?? throw new ArgumentNullException(nameof(interpolation));
                EasingFunction = easingFunction ?? owner.DefaultEaseFunc;
                Duration = duration;
                this.from = from;
                this.to = to;
                SetChange();
            }

            private void SetChange() => change = to - from;

            public void Step(float delta)
            {
                if (complete)
                    return;
                Accumulator += delta;
                Interpolation(EasingFunction(Accumulator, from, change, Duration));
                if (Accumulator > Duration)
                {
                    owner.TweenerEnd(this);
                    complete = true;
                }
            }

            public void Reset()
            {
                Accumulator = 0f;
                complete = false;
            }

            public void Dispose()
            {
                owner = null;
                Interpolation = null;
                EasingFunction = null;
            }

        }

    }
}
