namespace Util.Interpolation;

public class TweenerSharp : TweenerSharpBase
{
    public static Easing.EaseFunc DefaultEaseFunc { get; set; } = Easing.LinearIn;

    public delegate void InterpFunc(float interpolation);

    private InterpFunc interpolationFunc;
    private Easing.EaseFunc easingFunc;

    private float from, to;
    private float deltaStep;

    public TweenerSharp(float from, float to, InterpFunc interpolationFunc, Easing.EaseFunc? easingFunc = null)
    {
        this.from = from;
        this.to = to;
        this.interpolationFunc = interpolationFunc;
        this.easingFunc = easingFunc ?? DefaultEaseFunc;
    }

    protected override void StepInternal()
    {
        var ease = easingFunc(accumulator, from, deltaStep, Duration);
        interpolationFunc(ease);
    }

    #region To, From, DeltaStep

    public float DeltaStep => deltaStep;

    public float From
    {
        get => from;
        set
        {
            from = value;
            SetDeltaStep();
        }
    }

    public float To
    {
        get => to;
        set
        {
            to = value;
            SetDeltaStep();
        }
    }

    private void SetDeltaStep() => deltaStep = to - from;

    public void Setup(float from, float to, float duration)
    {
        Duration = duration;
        this.from = from;
        this.to = to;
        SetDeltaStep();
    }

    #endregion

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
