using System;

namespace Util.Interpolation
{
	public sealed class TweenerSharp : TweenerSharpBase
	{
		public delegate void InterpFunc(float interpolation);

		private InterpFunc Interpolation;
		private Interpolation.EaseFunc EasingFunction;

		private float from, to, change;

		public TweenerSharp(TweenSharpBase owner) : base(owner) { }

		public float ChangeValue => change;

		public override void Step(float delta)
		{
			if (complete)
				return;
			Accumulator += delta;
			Interpolation(EasingFunction(Accumulator, from, change, Duration));
			TryEnd();
		}

		#region Setup

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

		#endregion

	}

}
