using System;

namespace Util.Interpolation
{
	public sealed class TweenerDelaySharp : TweenerSharpBase
	{
		public TweenerDelaySharp(TweenSharpBase owner) : base(owner) { }

		public override void Step(float delta)
		{
			if (complete)
				return;
			Accumulator += delta;
			TryEnd();
		}

		protected override void Disposing() { }

	}

}
