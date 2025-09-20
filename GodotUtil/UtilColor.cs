using System.Text.RegularExpressions;

using Color = Godot.Color;

namespace GodotUtil;

public static partial class UtilColor
{
    public enum ColorIndex : int
    {
        R,
        G,
        B,
        A
    }

    private static readonly Random colorRng = new();


    [GeneratedRegex("#(?:[0-9A-Fa-f]{3}){1,2}")]
    public static partial Regex HexColorRegex();

    public static bool HexStringIsValidColor(string value) => HexColorRegex().IsMatch(value);

    public static Color RandomColor(float alpha = 1f) => new(colorRng.NextSingle(), colorRng.NextSingle(), colorRng.NextSingle(), alpha);

    public static Color PseudoRandomColor(string colorStr, float alpha = 1)
    {
        long r = 0, g = 0, b = 0;
        for (int i = 0; i < colorStr.Length; i++)
        {
            int charValue = colorStr[i];
            r += charValue;
            g += charValue + charValue;
            b += charValue + charValue + charValue;
        }
        return new(r % 255 / 255f, g % 255 / 255f, b % 255 / 255f, alpha);
    }

    #region Text Color

    //https://stackoverflow.com/q/3942878
    public static Color TextColorFromColor(Color color, Color lightColor, Color darkColor) =>
        color.R * 0.299f + color.G * 0.587f + color.B * 0.114f > 0.729f ? darkColor : lightColor;

    public static Color TextColorFromColorLerp(Color color, Color lightColor, Color darkColor) =>
       lightColor.Lerp(darkColor, color.R * 0.299f + color.G * 0.587f + color.B * 0.114f);

    public static Color TextColorFromColorAdvanced(Color color, Color lightColor, Color darkColor)
    {
        float[] mult = { 0.2126f, 0.7152f, 0.0722f };
        float L = 0f;
        for (int i = 0; i < 3; i++)
        {
            var c = color[i] < 0.03928f ? color[i] / 12.92f : MathF.Pow((color[i] + 0.055f) / 1.055f, 2.4f);
            L += c * mult[i];
        }
        return L > 0.179f ? darkColor : lightColor;
    }

    #endregion

    #region From


    #endregion
}
