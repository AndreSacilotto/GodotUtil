namespace Util.Interpolation;

public class TweenerDelaySharp : TweenerSharpBase
{
	public override void Step(float delta)
	{
		if (complete)
			return;
		Accumulator += delta;
		TryEnd();
	}
}
