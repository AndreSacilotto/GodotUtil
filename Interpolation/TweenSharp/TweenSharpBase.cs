using System;

namespace Util.Interpolation
{
	public abstract partial class TweenSharpBase : IRequireGameLoop
	{
		public event Action OnTweenFinish;
		public Interpolation.EaseFunc DefaultEaseFunc { get; set; }

		protected float totalDuration;
		protected TweenerSharpBase current;

		public bool Paused { get; set; } = true;
		public bool Repeat { get; set; } = false;

		/// <summary>Total time that passed animating the tween</summary>
		public float Time => totalDuration;

		public TweenerSharpBase Current => current;
		public T GetCurrent<T>() where T : TweenerSharpBase => (T)current;

		protected void CallOnTweenFinish() => OnTweenFinish?.Invoke();
		public void Step(float delta)
		{
			if (Paused)
				return;
			totalDuration += delta;
			current.Step(delta);
		}

		/// <summary>Time needed to all tweeners to end</summary>
		public abstract float GetCompletationTime();

		public abstract void Reset();
		public abstract void Clear();
		public abstract void TweenerEnd(TweenerSharpBase tweener);

	}
}
