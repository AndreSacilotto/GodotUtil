namespace Util.Interpolation;

public class TweenerSharpMethod : TweenerSharpEasing
{
    public delegate void InterpFunc(float interpolation);

    protected InterpFunc interpolationFunc;

    public TweenerSharpMethod(float duration, InterpFunc interpolationFunc, Easing.EaseFunc easingFunc) : base(duration, easingFunc)
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
    #endregion

}
