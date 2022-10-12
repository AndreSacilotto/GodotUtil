using System;

namespace Util.Interpolation
{
	public abstract class TweenerSharpBase : IRequireGameLoop
	{
		protected readonly TweenSharpBase owner;

		/// <summary>Time of the tweener animation</summary>
		public float Duration { get; set; } = 1f;

		/// <summary>Time spent doing the tweener animation</summary>
		public float Accumulator { get; set; } = 0f;

		protected bool complete;

		public TweenerSharpBase(TweenSharpBase owner) => this.owner = owner;

		/// <summary>How created and is using the tweener</summary>
		public TweenSharpBase Owner => owner;

		/// <summary>True if the tweener has completed his animation, false otherwise</summary>
		public bool Complete => complete;

		/// <summary>Reset the tweener to be able to play again</summary>
		public virtual void Reset()
		{
			Accumulator = 0f;
			complete = false;
		}

		protected void TryEnd() 
		{
			if (Accumulator > Duration)
			{
				owner.TweenerEnd(this);
				complete = true;
			}
		}

		public abstract void Step(float delta);

	}

}
