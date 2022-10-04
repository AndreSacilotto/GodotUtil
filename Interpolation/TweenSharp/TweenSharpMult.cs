using System;
using System.Collections.Generic;
using System.Linq;

namespace Util.Interpolation
{
	public class TweenSharpMult : TweenSharpBase
	{
		public event Action<TweenerSharpBase> OnTweenerEnd;

		protected List<TweenerSharpBase> tweeners = new(1);

		private int currentIndex;

		public int Count => tweeners.Count;
		public bool HasTweeners => tweeners.Count > 0;

		public TweenerSharp CreateTweener()
		{
			var tweener = new TweenerSharp(this);
			AddTweenerInternal(tweener);
			return tweener;
		}
		public TweenerDelaySharp CreateTweenerDelay()
		{
			var tweener = new TweenerDelaySharp(this);
			AddTweenerInternal(tweener);
			return tweener;
		}

		public TweenerSharpBase AddTweenerInternal(TweenerSharpBase tweener)
		{
			if (current == null)
			{
				currentIndex = 0;
				current = tweener;
			}
			tweeners.Add(tweener);
			return tweener;
		}

		/// <summary>Remove the an tweener and reset the animation if exist</summary>
		public void RemoveTweener(TweenerSharpBase tweener, bool dispose = false)
		{
			if (tweeners.Remove(tweener))
			{
				Reset();
				if (dispose)
					tweener.Dispose();
			}
		}

		public override void Reset()
		{
			currentIndex = 0;
			totalDuration = 0f;
			if (HasTweeners)
			{
				foreach (var item in tweeners)
					item.Reset();
				current = tweeners[0];
			}
			else
				current = null;
			Paused = true;
		}

		public override void Clear()
		{
			totalDuration = 0f;
			currentIndex = -1;
			DisposeTweeners();
			Paused = true;
		}

		public override float GetCompletationTime() => tweeners.Sum(x => x.Duration);

		public override void TweenerEnd(TweenerSharpBase tweener)
		{
			currentIndex++;
			if (currentIndex == tweeners.Count)
			{
				CallOnTweenFinish();
				Reset();
				Paused = !Repeat;
			}
			else
			{
				current = tweeners[currentIndex];
				current.Reset();
			}
			OnTweenerEnd?.Invoke(tweener);
		}

		#region Dispose

		private void DisposeTweeners()
		{
			foreach (var item in tweeners)
				item.Dispose();
			tweeners.Clear();
			current = null;
		}

		protected override void Disposing()
		{
			base.Disposing();
			OnTweenerEnd = null;
			DisposeTweeners();
			tweeners = null;
		}

		#endregion


	}
}
