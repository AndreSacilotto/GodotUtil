using System;

namespace Util.Interpolation
{
    public partial class TweenSharp
    {
        public sealed class TweenerSharp : IDisposable
        {
            private TweenSharp Owner { get; init; }

            public event Action<float> Interpolation;
            public event Func<float, float> EasingFunction;
            public float duration;

            public float accumulator;
            public void Step(float time)
            {
                if (!Owner.Running)
                    return;
             
                accumulator += time;
                Interpolation(EasingFunction(accumulator / duration));
                if (accumulator > duration)
                {
                    Owner.TweenerEnd(this);
                    accumulator = 0f;
                }
            }

            public void Reset() => accumulator = 0f;

            public void Dispose()
            {
                Interpolation = null;
                EasingFunction = null;
            }

        }

    }
}
