using System;
using System.Collections.Generic;

namespace Util.Interpolation
{
	public class TweenSharpSingle : TweenSharp
	{
		public override TweenerSharp CreateTweener()
		{
			var tweener = new TweenerSharp(this);
			if (current != null)
				current.Dispose();
			return current = tweener;
		}

		public override void Reset(bool pause = false)
		{
			totalDuration = 0f;
			current.Reset();
			Paused = pause;
		}

		public override void Clear()
		{
			totalDuration = 0f;
			if (current != null)
			{
				current.Dispose();
				current = null;
			}
			Paused = true;
		}

		public override float GetCompletationTime() => current.Duration;

		protected override void TweenerEnd(TweenerSharp tweener)
		{
			CallOnTweenFinish();
			Reset();
			Paused = !Repeat;
		}

	}
}
