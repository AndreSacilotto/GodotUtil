namespace Util.Interpolation
{
	public class TweenSharpSingle : TweenSharpBase
	{
		public TweenerSharp CreateTweener()
		{
			var tweener = new TweenerSharp(this);
			current = tweener;
			return tweener;
		}

		public override void Reset()
		{
			totalDuration = 0f;
			current.Reset();
		}

		public override void Clear()
		{
			totalDuration = 0f;
			current = null;
			Paused = true;
		}

		public override float GetCompletationTime() => current.Duration;

		public override void TweenerEnd(TweenerSharpBase tweener)
		{
			CallOnTweenFinish();
			Reset();
			Paused = !Repeat;
		}

	}
}
