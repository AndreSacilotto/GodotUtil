namespace Util.Interpolation;

partial class Easing
{
    /// <summary>Default easing function signature</summary>
    /// <param name="percent">current percent or time</param>
    /// <param name="initial">initial value</param>
    /// <param name="distance">the change between the initial and final value: (final - initial)</param>
    /// <param name="duration">duration of animation</param>
    public delegate float EaseFunc(float percent, float initial, float distance, float duration);

    public enum EaseEquation
    {
        Linear,
        ExpoIn, ExpoOut, ExpoInOut, ExpoOutIn,
        CircIn, CircOut, CircInOut, CircOutIn,
        QuadIn, QuadOut, QuadInOut, QuadOutIn,
        SineIn, SineOut, SineInOut, SineOutIn,
        CubicIn, CubicOut, CubicInOut, CubicOutIn,
        QuartIn, QuartOut, QuartInOut, QuartOutIn,
        QuintIn, QuintOut, QuintInOut, QuintOutIn,
        ElasticIn, ElasticOut, ElasticInOut, ElasticOutIn,
        BounceIn, BounceOut, BounceInOut, BounceOutIn,
        BackIn, BackOut, BackInOut, BackOutIn,
    };

    public static EaseFunc GetEaseEquation(EaseEquation equation) => equation switch
    {
        EaseEquation.ExpoInOut => ExpoInOut,
        EaseEquation.ExpoOut => ExpoOut,
        EaseEquation.ExpoIn => ExpoIn,
        EaseEquation.ExpoOutIn => ExpoOutIn,
        EaseEquation.CircOut => CircOut,
        EaseEquation.CircIn => CircIn,
        EaseEquation.CircInOut => CircInOut,
        EaseEquation.CircOutIn => CircOutIn,
        EaseEquation.QuadOut => QuadOut,
        EaseEquation.QuadIn => QuadIn,
        EaseEquation.QuadInOut => QuadInOut,
        EaseEquation.QuadOutIn => QuadOutIn,
        EaseEquation.SineOut => SineOut,
        EaseEquation.SineIn => SineIn,
        EaseEquation.SineInOut => SineInOut,
        EaseEquation.SineOutIn => SineOutIn,
        EaseEquation.CubicOut => CubicOut,
        EaseEquation.CubicIn => CubicIn,
        EaseEquation.CubicInOut => CubicInOut,
        EaseEquation.CubicOutIn => CubicOutIn,
        EaseEquation.QuartIn => QuartIn,
        EaseEquation.QuartOut => QuartOut,
        EaseEquation.QuartInOut => QuartInOut,
        EaseEquation.QuartOutIn => QuartOutIn,
        EaseEquation.QuintIn => QuintIn,
        EaseEquation.QuintOut => QuintOut,
        EaseEquation.QuintInOut => QuintInOut,
        EaseEquation.QuintOutIn => QuintOutIn,
        EaseEquation.ElasticIn => ElasticIn,
        EaseEquation.ElasticOut => ElasticOut,
        EaseEquation.ElasticInOut => ElasticInOut,
        EaseEquation.ElasticOutIn => ElasticOutIn,
        EaseEquation.BounceIn => BounceIn,
        EaseEquation.BounceOut => BounceOut,
        EaseEquation.BounceInOut => BounceInOut,
        EaseEquation.BounceOutIn => BounceOutIn,
        EaseEquation.BackIn => BackIn,
        EaseEquation.BackOut => BackOut,
        EaseEquation.BackInOut => BackInOut,
        EaseEquation.BackOutIn => BackOutIn,
        EaseEquation.Linear => LinearIn,
        _ => throw new ArgumentOutOfRangeException(nameof(equation), $"Easing function of name {equation} dont exist"),
    };
}
