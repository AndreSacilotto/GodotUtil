namespace Util.Interpolation;

public class TweenerSharp : TweenerSharpMethod
{
    private float from, distance;

    public TweenerSharp(float from, float to, float duration, InterpFunc interpolationFunc, Easing.EaseFunc easingFunc) : base(duration, interpolationFunc, easingFunc)
    {
        Setup(from, to, duration);
    }

    protected override void EaseStep()
    {
        var ease = easingFunc(accumulator, from, distance, Duration);
        interpolationFunc(ease);
    }

    public float Change => distance;

    public float From
    {
        get => from;
        set
        {
            distance += value - from;
            from = value;
        }
    }

    public float To
    {
        get => from + distance;
        set => distance = value - from;
    }

    public void Setup(float from, float to, float duration)
    {
        Duration = duration;
        this.from = from;
        To = to;
    }

}
