using System;

namespace Util.Interpolation
{
	public class TweenerDelaySharp : TweenerSharpBase
	{
		public TweenerDelaySharp(TweenSharpBase owner) : base(owner) { }

		public override void Step(float delta)
		{
			if (complete)
				return;
			Accumulator += delta;
			TryEnd();
		}

		public override void Close() { }
	}

}
