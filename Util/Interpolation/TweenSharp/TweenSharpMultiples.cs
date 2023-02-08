using System;
using System.Collections.Generic;
using System.Linq;

namespace Util.Interpolation
{
	public class TweenSharpMultiples : TweenSharpBase
	{
		public event Action<TweenerSharpBase>? OnTweenerEnd;

		protected List<TweenerSharpBase> tweeners = new(2);

		private int currentIndex;

		public int Count => tweeners.Count;
		public bool HasTweeners => tweeners.Count > 0;

		public TweenerSharpBase AddTweener(TweenerSharpBase tweener)
		{
			if (current == null)
			{
				currentIndex = 0;
				current = tweener;
			}
			tweeners.Add(tweener);
			tweener.OnTweenerEnd += TweenerEnd;
			return tweener;
		}

		/// <summary>Remove the an tweener and reset the animation if exist</summary>
		public void RemoveTweener(TweenerSharpBase tweener)
		{
			if (tweeners.Remove(tweener))
				Reset();
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
			Paused = true;
			totalDuration = 0f;
			currentIndex = -1;
			current = null;
			foreach (var item in tweeners)
				item.Close();
			tweeners.Clear();
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

	}
}
