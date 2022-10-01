using System;

namespace Util.Interpolation
{
    public partial class TweenSharp
    {
        public sealed class TweenerSharp : IDisposable
        {

            private readonly TweenSharp owner;

            private Action<float> Interpolation;
            private Interpolation.EaseFunc EasingFunction;

            private float from, to, change;

            public float Duration { get; set; }
            public float Accumulator { get; set; }
            public float From { get; set; }

            public TweenerSharp(TweenSharp owner, float from, float to, float duration, Action<float> interpolation, Interpolation.EaseFunc easingFunction)
            {
                this.owner = owner;
                Interpolation = interpolation ?? throw new ArgumentNullException(nameof(interpolation));
                EasingFunction = easingFunction ?? throw new ArgumentNullException(nameof(easingFunction));
                Duration = duration;
                this.from = from;
                this.to = to;
                SetChange();
            }

            #region Props

            public TweenSharp Owner => owner;

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

            private void SetChange() => change = to - from;

            public void Step(float delta)
            {
                Accumulator += delta;
                Interpolation(EasingFunction(Accumulator, from, change, Duration));
                if (Accumulator > Duration)
                {
                    owner.TweenerEnd(this);
                    Reset();
                }
            }

            public void Reset() => Accumulator = 0f;

            public void Dispose()
            {
                Interpolation = null;
                EasingFunction = null;
            }

        }

    }
}
