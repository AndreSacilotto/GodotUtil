using System;

namespace Util.Interpolation
{
	public abstract partial class TweenSharp : IRequireGameLoop, IDisposable
	{
		public event Action OnTweenFinish;
		public Interpolation.EaseFunc DefaultEaseFunc { get; set; }

		protected float totalDuration;
		protected TweenerSharp current;

		public bool Paused { get; set; } = true;
		public bool Repeat { get; set; } = false;

		/// <summary>Total time that passed animating the tween</summary>
		public float Time => totalDuration;

		public TweenerSharp CurrentTweener => current;

		protected void CallOnTweenFinish() => OnTweenFinish?.Invoke();
		public void PauseOrResume() => Paused = !Paused;
		public void Step(float delta)
		{
			if (Paused)
				return;
			totalDuration += delta;
			current.Step(delta);
		}

		/// <summary>Time needed to all tweeners to end</summary>
		public abstract float GetCompletationTime();

		public abstract TweenerSharp CreateTweener();
		protected abstract void TweenerEnd(TweenerSharp tweener);

		public abstract void Reset(bool pause);
		public abstract void Clear();

		#region Dispose

		~TweenSharp() 
		{
			if (_disposed)
				return;
			Disposing();
			_disposed = true;
		} 

		private bool _disposed;
		public void Dispose()
		{
			if (_disposed)
				return;
			Disposing();
			_disposed = true;
			GC.SuppressFinalize(this);
		}

		protected virtual void Disposing()
		{
			OnTweenFinish = null;
			if (current != null)
			{
				current.Dispose();
				current = null;
			}
		}


		#endregion

	}
}
