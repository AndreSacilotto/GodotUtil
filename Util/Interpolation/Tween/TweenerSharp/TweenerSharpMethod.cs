namespace Util.Interpolation;

public class TweenerSharpMethod : TweenerSharpDelay
{
    public delegate void InterpFunc(float interpolation);

    protected InterpFunc interpolationFunc;
    protected Easing.EaseFunc easingFunc;

    public TweenerSharpMethod(float duration, InterpFunc interpolationFunc, Easing.EaseFunc easingFunc) : base(duration)
    {
        this.interpolationFunc = interpolationFunc;
        this.easingFunc = easingFunc;
    }

    protected override void EaseStep()
    {
        var ease = easingFunc(accumulator, 0f, 1f, Duration);
        interpolationFunc(ease);
    }

    #region Delegate Funcs

    public InterpFunc InterpolationFunc 
    {
        get => interpolationFunc;
        set => interpolationFunc = value;
    }
    public Easing.EaseFunc EasingFunc
    {
        get => easingFunc;
        set => easingFunc = value;
    }
    public void SetEasingFunc(Easing.EaseEquation easing) => easingFunc = Easing.GetEaseEquation(easing);

    #endregion

}
