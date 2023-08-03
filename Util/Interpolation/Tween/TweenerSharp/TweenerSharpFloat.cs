namespace Util.Interpolation;

public class TweenerSharpFloat : TweenerSharpEasing
{
    public delegate void InterpFunc(float t, float current);

    protected InterpFunc interpolationFunc;

    private float from, to, distance;

    public TweenerSharpFloat(float from, float to, float duration, InterpFunc interpolationFunc, Easing.EaseFunc easingFunc) : base(duration, easingFunc)
    {
        this.interpolationFunc = interpolationFunc;
        Setup(from, to, duration);
    }

    protected override void EaseStep()
    {
        var ease = easingFunc(accumulator, from, distance, Duration);
        interpolationFunc(ease, from + ease * distance);
    }

    public InterpFunc InterpolationFunc
    {
        get => interpolationFunc;
        set => interpolationFunc = value;
    }

    public float Change => distance;

    public float From
    {
        get => from;
        set { from = value; SetDistance(); }
    }
    public float To
    {
        get => to;
        set { to = value; SetDistance(); }
    }

    public void Setup(float from, float to, float duration)
    {
        Duration = duration;
        this.from = from;
        this.to = to;
        SetDistance();
    }
    private void SetDistance() => distance = to - from;

    public void Invert() 
    {
        (to, from) = (from, to);
        SetDistance();
    }

}
