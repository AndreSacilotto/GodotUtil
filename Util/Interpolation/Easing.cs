using System.Numerics;

/**
* Author: 5Spectra
* Credit: Godot Engine 3.5 and Robert Penner
*/
namespace Util.Interpolation;

/// <summary>
/// Robert Penner's Easing Functions<br/><br/>
/// For explanation go to:<br/>
/// <see href="http://robertpenner.com/easing/"/><br/>
/// <see href="https://gist.Github.com/xanathar/735e17ac129a72a277ee"/>
/// </summary>
public static partial class Easing
{
    // t || percent = current percent; (Alias time)
    // b || initial = starting/initial value of the property. (Alias beginning)
    // c || delta = the change between the beginning and destination value of the property. (Alias delta)
    // d || duration = duration of animation;
    // t & d need to use the same measure of percent which could be frames, seconds, milliseconds or whatever.

    public static float LinearIn(float percent, float initial, float delta, float duration) => 
        delta * percent / duration + initial;

    #region Sine or Sinusoidal
    public static float SineIn(float percent, float initial, float delta, float duration) => 
        delta * (1f + MathF.Cos(percent / duration * UtilMath.TAU_90) * -1f) + initial;

    public static float SineOut(float percent, float initial, float delta, float duration) => 
        delta * MathF.Sin(percent / duration * UtilMath.TAU_90) + initial;

    public static float SineInOut(float percent, float initial, float delta, float duration) =>
        -0.5f * delta * (MathF.Cos(UtilMath.TAU_180 * percent / duration) - 1f) + initial;

    public static float SineOutIn(float percent, float initial, float delta, float duration)
    {
        var halfDelta = delta * 0.5f;
        if (percent < duration * 0.5f)
            return SineOut(percent * 2f, initial, halfDelta, duration);
        return SineIn(percent * 2f - duration, initial + halfDelta, halfDelta, duration);
    }

    #endregion

    #region Quint
    public static float QuintIn(float percent, float initial, float delta, float duration) => 
        delta * UtilMath.PowQuintic(percent / duration) + initial;

    public static float QuintOut(float percent, float initial, float delta, float duration) => 
        delta * (UtilMath.PowQuintic(percent / duration - 1f) + 1f) + initial;

    public static float QuintInOut(float percent, float initial, float delta, float duration)
    {
        percent = 2f * percent / duration;
        if (percent < 1f)
            return 0.5f * delta * UtilMath.PowQuintic(percent) + initial;
        return 0.5f * delta * (UtilMath.PowQuintic(percent - 2f) + 2f) + initial;
    }

    public static float QuintOutIn(float percent, float initial, float delta, float duration)
    {
        var halfDelta = delta * 0.5f;
        if (percent < duration * 0.5f)
            return QuintOut(percent * 2f, initial, halfDelta, duration);
        return QuintIn(percent * 2f - duration, initial + halfDelta, halfDelta, duration);
    }

    #endregion

    #region Quart
    public static float QuartIn(float percent, float initial, float delta, float duration) => 
        delta * UtilMath.PowQuartic(percent / duration) + initial;

    public static float QuartOut(float percent, float initial, float delta, float duration) => 
        -delta * (UtilMath.PowQuartic(percent / duration - 1f) - 1f) + initial;

    public static float QuartInOut(float percent, float initial, float delta, float duration)
    {
        percent = 2f * percent / duration;
        if (percent < 1f)
            return 0.5f * delta * UtilMath.PowQuartic(percent) + initial;
        return -0.5f * delta * (UtilMath.PowQuartic(percent - 2f) - 2f) + initial;
    }

    public static float QuartOutIn(float percent, float initial, float delta, float duration)
    {
        var halfDelta = delta * 0.5f;
        if (percent < duration * 0.5f)
            return QuartOut(percent * 2f, initial, halfDelta, duration);
        return QuartIn(percent * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region (Quad)dratic
    public static float QuadIn(float percent, float initial, float delta, float duration) => 
        delta * UtilMath.PowSquare(percent / duration) + initial;

    public static float QuadOut(float percent, float initial, float delta, float duration)
    {
        percent /= duration;
        return -delta * percent * (percent - 2f) + initial;
    }

    public static float QuadInOut(float percent, float initial, float delta, float duration)
    {
        percent = 2f * percent / duration;
        if (percent < 1f)
            return 0.5f * delta * UtilMath.PowSquare(percent) + initial;
        return -0.5f * delta * ((percent - 1f) * (percent - 3f) - 1f) + initial;
    }

    public static float QuadOutIn(float percent, float initial, float delta, float duration)
    {
        var halfDelta = delta * .5f;
        if (percent < duration * 0.5f)
            return QuadOut(percent * 2f, initial, halfDelta, duration);
        return QuadIn(percent * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region (Expo)nential
    public static float ExpoIn(float percent, float initial, float delta, float duration)
    {
        if (percent == 0f)
            return initial;
        return delta * (MathF.Pow(2f, 10f * (percent / duration - 1f)) - 0.001f) + initial;
    }

    public static float ExpoOut(float percent, float initial, float delta, float duration)
    {
        if (percent == duration)
            return initial + delta;
        return delta * 1.001f * (-MathF.Pow(2f, -10f * percent / duration) + 1f) + initial;
    }

    public static float ExpoInOut(float percent, float initial, float delta, float duration)
    {
        if (percent == 0f)
            return initial;

        if (percent == duration)
            return initial + delta;

        percent = 2f * percent / duration;

        if (percent < 1f)
            return delta * (0.5f * MathF.Pow(2f, 10 * (percent - 1f)) - 0.0005f) + initial;

        return 0.5f * delta * 1.0005f * (-MathF.Pow(2f, -10 * (percent - 1f)) + 2f) + initial;
    }

    public static float ExpoOutIn(float percent, float initial, float delta, float duration)
    {
        var halfDelta = delta * 0.5f;
        if (percent < duration * 0.5f)
            return ExpoOut(percent * 2f, initial, halfDelta, duration);
        return ExpoIn(percent * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region Elastic
    public static float ElasticIn(float percent, float initial, float delta, float duration)
    {
        if (percent == 0f)
            return initial;

        percent /= duration;
        if (percent == 1f)
            return initial + delta;

        percent -= 1f;
        float p = duration * 0.3f;

        return -(delta * MathF.Pow(2f, 10 * percent) * MathF.Sin((percent * duration - p * 0.25f) * UtilMath.TAU_360 / p)) + initial;
    }

    public static float ElasticOut(float percent, float initial, float delta, float duration)
    {
        if (percent == 0f)
            return initial;

        percent /= duration;
        if (percent == 1f)
            return initial + delta;

        float p = duration * 0.3f;
        return delta * (MathF.Pow(2f, -10 * percent) * MathF.Sin((percent * duration - p * 0.25f) * UtilMath.TAU_360 / p) + 1f) + initial;
    }

    public static float ElasticInOut(float percent, float initial, float delta, float duration)
    {
        if (percent == 0f)
            return initial;

        percent = 2f * percent / duration;
        if (percent == 2f)
            return initial + delta;

        float p = duration * (0.3f * 1.5f);
        float s = p * 0.25f;

        percent -= 1f;
        if (percent < 0f)
            return -0.5f * delta * MathF.Pow(2f, 10 * percent) * MathF.Sin((percent * duration - s) * UtilMath.TAU_360 / p) + initial;

        return delta * (0.5f * MathF.Pow(2f, -10 * percent) * MathF.Sin((percent * duration - s) * UtilMath.TAU_360 / p) + 1f) + initial;
    }

    public static float ElasticOutIn(float percent, float initial, float delta, float duration)
    {
        var halfDelta = delta * 0.5f;
        if (percent < duration * 0.5f)
            return ElasticOut(percent * 2f, initial, halfDelta, duration);
        return ElasticIn(percent * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region Cubic
    public static float CubicIn(float percent, float initial, float delta, float duration)
    {
        percent /= duration;
        return delta * UtilMath.PowCubic(percent) + initial;
    }

    public static float CubicOut(float percent, float initial, float delta, float duration)
    {
        percent /= duration - 1f;
        return delta * (UtilMath.PowCubic(percent) + 1f) + initial;
    }

    public static float CubicInOut(float percent, float initial, float delta, float duration)
    {
        percent = 2f * percent / duration;
        if (percent < 1f)
            return 0.5f * delta * UtilMath.PowCubic(percent) + initial;
        percent -= 2f;
        return 0.5f * delta * (UtilMath.PowCubic(percent) + 2f) + initial;
    }

    public static float CubicOutIn(float percent, float initial, float delta, float duration)
    {
        var halfDelta = delta * 0.5f;
        if (percent < duration * 0.5f)
            return CubicOut(percent * 2f, initial, halfDelta, duration);
        return CubicIn(percent * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region (Circ)ular
    public static float CircIn(float percent, float initial, float delta, float duration)
    {
        percent /= duration;
        return -delta * (MathF.Sqrt(1f - UtilMath.PowSquare(percent)) - 1f) + initial;
    }

    public static float CircOut(float percent, float initial, float delta, float duration)
    {
        percent /= duration - 1f;
        return delta * MathF.Sqrt(1f - UtilMath.PowSquare(percent)) + initial;
    }

    public static float CircInOut(float percent, float initial, float delta, float duration)
    {
        percent = 2f * percent / duration;
        if (percent < 1f)
            return -0.5f * delta * (MathF.Sqrt(1f - UtilMath.PowSquare(percent)) - 1f) + initial;
        percent -= 2f;
        return 0.5f * delta * (MathF.Sqrt(1f - UtilMath.PowSquare(percent)) + 1f) + initial;
    }

    public static float CircOutIn(float percent, float initial, float delta, float duration)
    {
        var halfDelta = delta * .5f;
        if (percent < duration * 0.5f)
            return CircOut(percent * 2f, initial, halfDelta, duration);
        return CircIn(percent * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region Bounce
    public static float BounceOut(float percent, float initial, float delta, float duration)
    {
        const float elevenByFour = 2.75f;

        percent /= duration;

        if (percent < 1f / elevenByFour)
            return delta * (7.5625f * percent * percent) + initial;

        if (percent < 2f / elevenByFour)
        {
            percent -= 1.5f / elevenByFour;
            return delta * (7.5625f * percent * percent + 0.75f) + initial;
        }

        if (percent < 2.5f / elevenByFour)
        {
            percent -= 2.25f / elevenByFour;
            return delta * (7.5625f * percent * percent + 0.9375f) + initial;
        }

        percent -= 2.625f / elevenByFour;
        return delta * (7.5625f * percent * percent + 0.984375f) + initial;
    }

    public static float BounceIn(float percent, float initial, float delta, float duration) => 
        delta - BounceOut(duration - percent, 0f, delta, duration) + initial;

    public static float BounceInOut(float percent, float initial, float delta, float duration)
    {
        var halfDelta = delta * 0.5f;
        if (percent < duration * 0.5f)
            return BounceIn(percent * 2f, initial, halfDelta, duration);
        return BounceOut(percent * 2f - duration, initial + halfDelta, halfDelta, duration);
    }

    public static float BounceOutIn(float percent, float initial, float delta, float duration)
    {
        var halfDelta = delta * 0.5f;
        if (percent < duration * 0.5f)
            return BounceOut(percent * 2f, initial, halfDelta, duration);
        return BounceIn(percent * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region Back
    public static float BackIn(float percent, float initial, float delta, float duration)
    {
        const float s = 1.70158f;
        percent /= duration;

        return delta * percent * percent * ((s + 1f) * percent - s) + initial;
    }

    public static float BackOut(float percent, float initial, float delta, float duration)
    {
        const float s = 1.70158f;
        percent /= duration - 1f;

        return delta * (percent * percent * ((s + 1f) * percent + s) + 1f) + initial;
    }

    public static float BackInOut(float percent, float initial, float delta, float duration)
    {
        const float s = 1.70158f * 1.525f;
        percent = 2f * percent / duration;

        if (percent < 1f)
            return 0.5f * delta * (percent * percent * ((s + 1f) * percent - s)) + initial;

        percent -= 2f;
        return 0.5f * delta * (percent * percent * ((s + 1f) * percent + s) + 2f) + initial;
    }

    public static float BackOutIn(float percent, float initial, float delta, float duration)
    {
        var halfDelta = delta * .5f;
        if (percent < duration * 0.5f)
            return BackOut(percent * 2f, initial, halfDelta, duration);
        return BackIn(percent * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

}
