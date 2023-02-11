namespace Util.Interpolation;

public class TweenerSharp : TweenerSharpBase
{
	public delegate void InterpFunc(float interpolation);

	public static Interpolation.EaseFunc DefaultEaseFunc { get; set; } = Util.Interpolation.Interpolation.LinearIn;

	private InterpFunc? Interpolation;
	private Interpolation.EaseFunc? EasingFunction;

	private float from, to, change;

	public float ChangeValue => change;

	public override void Step(float delta)
	{
		if (complete)
			return;
		Accumulator += delta;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
		Interpolation(EasingFunction(Accumulator, from, change, Duration));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
		TryEnd();
	}

	public override void Close()
	{
		Interpolation = null;
		EasingFunction = null;
		base.Close();
	}

	#region Setup

	public float From
	{
		get => from;
		set {
			from = value;
			SetChange();
		}
	}

	public float To
	{
		get => to;
		set {
			to = value;
			SetChange();
		}
	}

	private void SetChange() => change = to - from;

	public void Setup(float from, float to, float duration, InterpFunc interpolation, Interpolation.EaseFunc? easingFunction = null)
	{
		Setup(interpolation, easingFunction);
		Setup(from, to, duration);
	}

	public void Setup(InterpFunc interpolation, Interpolation.EaseFunc? easingFunction)
	{
		Interpolation = interpolation;
		EasingFunction = easingFunction ?? DefaultEaseFunc;
	}

	public void Setup(float from, float to, float duration)
	{
		Duration = duration;
		this.from = from;
		this.to = to;
		SetChange();
	}

	#endregion

}
