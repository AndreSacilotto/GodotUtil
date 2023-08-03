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
    // t || time = current time; (Alias position)
    // b || initial = starting/initial value of the property. (Alias beginning)
    // c || change = the change/difference between the beginning and final value of the property (intitial - final). (Alias distance, delta)
    // d || duration = duration of animation; (Alias total_time)
    // t & d need to use the same measure of time which could be frames, seconds, milliseconds or whatever.

    // { c * (t / d) + b } or { change * (time / duration) + initial }
    // The parenthesized portion is the percentage of completion

    public static float LinearIn(float time, float initial, float change, float duration) => change * time / duration + initial;

    #region Sine or Sinusoidal
    public static float SineIn(float time, float initial, float change, float duration)
    {
        return change * (1f + MathF.Cos(time / duration * UtilMath.TAU_90) * -1f) + initial;
    }

    public static float SineOut(float time, float initial, float change, float duration)
    {
        return change * MathF.Sin(time / duration * UtilMath.TAU_90) + initial;
    }

    public static float SineInOut(float time, float initial, float change, float duration)
    {
        return -0.5f * change * (MathF.Cos(UtilMath.TAU_180 * time / duration) - 1f) + initial;
    }

    public static float SineOutIn(float time, float initial, float change, float duration)
    {
        var halfDelta = change * 0.5f;
        if (time < duration * 0.5f)
            return SineOut(time * 2f, initial, halfDelta, duration);
        return SineIn(time * 2f - duration, initial + halfDelta, halfDelta, duration);
    }

    #endregion

    #region Quint
    public static float QuintIn(float time, float initial, float change, float duration)
    {
        return change * UtilMath.PowQuintic(time / duration) + initial;
    }

    public static float QuintOut(float time, float initial, float change, float duration)
    {
        return change * (UtilMath.PowQuintic(time / duration - 1f) + 1f) + initial;
    }

    public static float QuintInOut(float time, float initial, float change, float duration)
    {
        time = 2f * time / duration;
        if (time < 1f)
            return 0.5f * change * UtilMath.PowQuintic(time) + initial;
        return 0.5f * change * (UtilMath.PowQuintic(time - 2f) + 2f) + initial;
    }

    public static float QuintOutIn(float time, float initial, float change, float duration)
    {
        var halfDelta = change * 0.5f;
        if (time < duration * 0.5f)
            return QuintOut(time * 2f, initial, halfDelta, duration);
        return QuintIn(time * 2f - duration, initial + halfDelta, halfDelta, duration);
    }

    #endregion

    #region Quart
    public static float QuartIn(float time, float initial, float change, float duration)
    {
        return change * UtilMath.PowQuartic(time / duration) + initial;
    }

    public static float QuartOut(float time, float initial, float change, float duration)
    {
        return -change * (UtilMath.PowQuartic(time / duration - 1f) - 1f) + initial;
    }

    public static float QuartInOut(float time, float initial, float change, float duration)
    {
        time = 2f * time / duration;
        if (time < 1f)
            return 0.5f * change * UtilMath.PowQuartic(time) + initial;
        return -0.5f * change * (UtilMath.PowQuartic(time - 2f) - 2f) + initial;
    }

    public static float QuartOutIn(float time, float initial, float change, float duration)
    {
        var halfDelta = change * 0.5f;
        if (time < duration * 0.5f)
            return QuartOut(time * 2f, initial, halfDelta, duration);
        return QuartIn(time * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region (Quad)dratic
    public static float QuadIn(float time, float initial, float change, float duration)
    {
        return change * UtilMath.PowSquare(time / duration) + initial;
    }

    public static float QuadOut(float time, float initial, float change, float duration)
    {
        time /= duration;
        return -change * time * (time - 2f) + initial;
    }

    public static float QuadInOut(float time, float initial, float change, float duration)
    {
        time = 2f * time / duration;
        if (time < 1f)
            return 0.5f * change * UtilMath.PowSquare(time) + initial;
        return -0.5f * change * ((time - 1f) * (time - 3f) - 1f) + initial;
    }

    public static float QuadOutIn(float time, float initial, float change, float duration)
    {
        var halfDelta = change * .5f;
        if (time < duration * 0.5f)
            return QuadOut(time * 2f, initial, halfDelta, duration);
        return QuadIn(time * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region (Expo)nential
    public static float ExpoIn(float time, float initial, float change, float duration)
    {
        if (time == 0f)
            return initial;
        return change * (MathF.Pow(2f, 10f * (time / duration - 1f)) - 0.001f) + initial;
    }

    public static float ExpoOut(float time, float initial, float change, float duration)
    {
        if (time == duration)
            return initial + change;
        return change * 1.001f * (-MathF.Pow(2f, -10f * time / duration) + 1f) + initial;
    }

    public static float ExpoInOut(float time, float initial, float change, float duration)
    {
        if (time == 0f)
            return initial;

        if (time == duration)
            return initial + change;

        time = 2f * time / duration;

        if (time < 1f)
            return change * (0.5f * MathF.Pow(2f, 10 * (time - 1f)) - 0.0005f) + initial;

        return 0.5f * change * 1.0005f * (-MathF.Pow(2f, -10 * (time - 1f)) + 2f) + initial;
    }

    public static float ExpoOutIn(float time, float initial, float change, float duration)
    {
        var halfDelta = change * 0.5f;
        if (time < duration * 0.5f)
            return ExpoOut(time * 2f, initial, halfDelta, duration);
        return ExpoIn(time * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region Elastic
    public static float ElasticIn(float time, float initial, float change, float duration)
    {
        if (time == 0f)
            return initial;

        time /= duration;
        if (time == 1f)
            return initial + change;

        time -= 1f;
        float p = duration * 0.3f;

        return -(change * MathF.Pow(2f, 10 * time) * MathF.Sin((time * duration - p * 0.25f) * UtilMath.TAU_360 / p)) + initial;
    }

    public static float ElasticOut(float time, float initial, float change, float duration)
    {
        if (time == 0f)
            return initial;

        time /= duration;
        if (time == 1f)
            return initial + change;

        float p = duration * 0.3f;
        return change * (MathF.Pow(2f, -10 * time) * MathF.Sin((time * duration - p * 0.25f) * UtilMath.TAU_360 / p) + 1f) + initial;
    }

    public static float ElasticInOut(float time, float initial, float change, float duration)
    {
        if (time == 0f)
            return initial;

        time = 2f * time / duration;
        if (time == 2f)
            return initial + change;

        float p = duration * (0.3f * 1.5f);
        float s = p * 0.25f;

        time -= 1f;
        if (time < 0f)
            return -0.5f * change * MathF.Pow(2f, 10 * time) * MathF.Sin((time * duration - s) * UtilMath.TAU_360 / p) + initial;

        return change * (0.5f * MathF.Pow(2f, -10 * time) * MathF.Sin((time * duration - s) * UtilMath.TAU_360 / p) + 1f) + initial;
    }

    public static float ElasticOutIn(float time, float initial, float change, float duration)
    {
        var halfDelta = change * 0.5f;
        if (time < duration * 0.5f)
            return ElasticOut(time * 2f, initial, halfDelta, duration);
        return ElasticIn(time * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region Cubic
    public static float CubicIn(float time, float initial, float change, float duration)
    {
        time /= duration;
        return change * UtilMath.PowCubic(time) + initial;
    }

    public static float CubicOut(float time, float initial, float change, float duration)
    {
        time /= duration - 1f;
        return change * (UtilMath.PowCubic(time) + 1f) + initial;
    }

    public static float CubicInOut(float time, float initial, float change, float duration)
    {
        time = 2f * time / duration;
        if (time < 1f)
            return 0.5f * change * UtilMath.PowCubic(time) + initial;
        time -= 2f;
        return 0.5f * change * (UtilMath.PowCubic(time) + 2f) + initial;
    }

    public static float CubicOutIn(float time, float initial, float change, float duration)
    {
        var halfDelta = change * 0.5f;
        if (time < duration * 0.5f)
            return CubicOut(time * 2f, initial, halfDelta, duration);
        return CubicIn(time * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region (Circ)ular
    public static float CircIn(float time, float initial, float change, float duration)
    {
        time /= duration;
        return -change * (MathF.Sqrt(1f - UtilMath.PowSquare(time)) - 1f) + initial;
    }

    public static float CircOut(float time, float initial, float change, float duration)
    {
        time /= duration - 1f;
        return change * MathF.Sqrt(1f - UtilMath.PowSquare(time)) + initial;
    }

    public static float CircInOut(float time, float initial, float change, float duration)
    {
        time = 2f * time / duration;
        if (time < 1f)
            return -0.5f * change * (MathF.Sqrt(1f - UtilMath.PowSquare(time)) - 1f) + initial;
        time -= 2f;
        return 0.5f * change * (MathF.Sqrt(1f - UtilMath.PowSquare(time)) + 1f) + initial;
    }

    public static float CircOutIn(float time, float initial, float change, float duration)
    {
        var halfDelta = change * .5f;
        if (time < duration * 0.5f)
            return CircOut(time * 2f, initial, halfDelta, duration);
        return CircIn(time * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region Bounce
    public static float BounceOut(float time, float initial, float change, float duration)
    {
        const float elevenByFour = 11f / 4f;

        time /= duration;

        if (time < 1f / elevenByFour)
            return change * (7.5625f * time * time) + initial;

        if (time < 2f / elevenByFour)
        {
            time -= 1.5f / elevenByFour;
            return change * (7.5625f * time * time + 0.75f) + initial;
        }

        if (time < 2.5f / elevenByFour)
        {
            time -= 2.25f / elevenByFour;
            return change * (7.5625f * time * time + 0.9375f) + initial;
        }

        time -= 2.625f / elevenByFour;
        return change * (7.5625f * time * time + 0.984375f) + initial;
    }

    public static float BounceIn(float time, float initial, float change, float duration)
    {
        return change - BounceOut(duration - time, 0f, change, duration) + initial;
    }

    public static float BounceInOut(float time, float initial, float change, float duration)
    {
        var halfDelta = change * 0.5f;
        if (time < duration * 0.5f)
            return BounceIn(time * 2f, initial, halfDelta, duration);
        return BounceOut(time * 2f - duration, initial + halfDelta, halfDelta, duration);
    }

    public static float BounceOutIn(float time, float initial, float change, float duration)
    {
        var halfDelta = change * 0.5f;
        if (time < duration * 0.5f)
            return BounceOut(time * 2f, initial, halfDelta, duration);
        return BounceIn(time * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

    #region Back
    public static float BackIn(float time, float initial, float change, float duration)
    {
        const float s = 1.70158f;
        time /= duration;

        return change * time * time * ((s + 1f) * time - s) + initial;
    }

    public static float BackOut(float time, float initial, float change, float duration)
    {
        const float s = 1.70158f;
        time /= duration - 1f;

        return change * (time * time * ((s + 1f) * time + s) + 1f) + initial;
    }

    public static float BackInOut(float time, float initial, float change, float duration)
    {
        const float s = 1.70158f * 1.525f;
        time = 2f * time / duration;

        if (time < 1f)
            return 0.5f * change * (time * time * ((s + 1f) * time - s)) + initial;

        time -= 2f;
        return 0.5f * change * (time * time * ((s + 1f) * time + s) + 2f) + initial;
    }

    public static float BackOutIn(float time, float initial, float change, float duration)
    {
        var halfDelta = change * .5f;
        if (time < duration * 0.5f)
            return BackOut(time * 2f, initial, halfDelta, duration);
        return BackIn(time * 2f - duration, initial + halfDelta, halfDelta, duration);
    }
    #endregion

}
