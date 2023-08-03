namespace Util.Interpolation;

public abstract class TweenerSharpEasing : TweenerSharpDelay
{
    protected Easing.EaseFunc easingFunc;

    public TweenerSharpEasing(float duration, Easing.EaseFunc easingFunc) : base(duration) => this.easingFunc = easingFunc;

    public Easing.EaseFunc EasingFunc
    {
        get => easingFunc;
        set => easingFunc = value;
    }
    public void SetEasingFunc(Easing.EaseEquation easing) => easingFunc = Easing.GetEaseEquation(easing);
}
