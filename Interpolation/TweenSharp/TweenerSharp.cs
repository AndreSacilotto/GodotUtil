using System;

namespace Util.Interpolation
{
	public abstract partial class TweenSharp
	{

		public sealed class TweenerSharp : IDisposable
		{
			public delegate void InterpFunc(float interpolation);

			private readonly TweenSharp owner;

			private InterpFunc Interpolation;
			private Interpolation.EaseFunc EasingFunction;

			private float from, to, change;

			private bool complete;

			public float Duration { get; set; }
			public float Accumulator { get; set; }

			public TweenerSharp(TweenSharp owner) => this.owner = owner;

			#region Props Get-Only
			public TweenSharp Owner => owner;
			public bool Complete => complete;
			public float ChangeValue => change;
			#endregion

			#region Props Get-Set
			public float From
			{
				get => from;
				set {
					from = value;
					SetChange();
				}
			}
			public float To
			{
				get => to;
				set {
					to = value;
					SetChange();
				}
			}
			private void SetChange() => change = to - from;

			#endregion


			public void Setup(float from, float to, float duration, InterpFunc interpolation, Interpolation.EaseFunc easingFunction = null)
			{
				Setup(interpolation, easingFunction);
				Setup(from, to, duration);
			}

			public void Setup(InterpFunc interpolation, Interpolation.EaseFunc easingFunction)
			{
				Interpolation = interpolation ?? throw new ArgumentNullException(nameof(interpolation));
				EasingFunction = easingFunction ?? owner.DefaultEaseFunc;
			}

			public void Setup(float from, float to, float duration)
			{
				Duration = duration;
				this.from = from;
				this.to = to;
				SetChange();
			}

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
				Interpolation = null;
				EasingFunction = null;
			}

		}


	}

}
