using System.Text.RegularExpressions;

namespace GodotUtil;

//https://docs.Godotengine.org/en/stable/tutorials/gui/bbcode_in_richtextlabel.html
public static partial class UtilBBCode
{
    public static string RemoveTags(string str) => MyRegex().Replace(str, string.Empty);
    [GeneratedRegex("\\[.*?\\]")]
    private static partial Regex MyRegex();


    public static string Custom(string str, string tag) => '[' + tag + ']' + str + '[' + tag + ']';
    public static string Custom(string str, string tag, string arg) => '[' + tag + '=' + arg + ']' + str + '[' + tag + ']';
    public static string CustomArgs(string str, string tag, string args) => '[' + tag + ' ' + args + ']' + str + '[' + tag + ']';

    #region Basic
    public static string Bold(string str) => "[b]" + str + "[/b]";
    public static string Italics(string str) => "[i]" + str + "[/i]";
    public static string Underline(string str) => "[u]" + str + "[/u]";
    public static string Strikethrough(string str) => "[s]" + str + "[/s]";
    #endregion

    #region Alignment
    public static string Center(string str) => Custom(str, "center");
    public static string Right(string str) => Custom(str, "right");
    public static string Fill(string str) => Custom(str, "fill");

    public static string Indent(string str) => Custom(str, "indent");
    #endregion

    public static string Code(string str) => Custom(str, "code");
    public static string Json(string str, string json) => Custom(str, "url", json);
    public static string URL(string url) => Custom(url, "url");
    public static string URLref(string str, string url) => Custom(str, "url", url);

    public static string Color(string str, string color) => Custom(str, "color", color);
    public static string Color(string str, Godot.Color color) => Custom(str, "color", color.ToHtml());
    public static string Font(string str, string fontPath) => Custom(str, "color", fontPath);

    #region Table
    public static string Cell(string str) => Custom(str, "cell");
    public static string Table(string str, int numCollums) => Custom(str, "stats", numCollums.ToString());
    public static string Table(string[] cells, int numCollums)
    {
        string str = "[stats=" + numCollums + "]";
        for (int i = 0; i < cells.Length; i++)
            str += "[cell]" + cells[i] + "[/cell]";
        str += "[stats]";
        return str;
    }
    #endregion

    #region Image
    public static string Image(string path) => Custom(path, "img");
    public static string Image(string path, int width) => Custom(path, "img", width.ToString());
    public static string Image(string path, int width, int height) => Custom(path, "img", width + "x" + height);
    #endregion

    #region Build in Effects

    public static string Wave(string str, float amp, float freq) =>
        CustomArgs(str, "wave", $"amp={amp} freq={freq}");
    public static string Tornado(string str, float radius, float freq) =>
        CustomArgs(str, "tornado", $"radius={radius} freq={freq}");
    public static string Shake(string str, float level, float rate) =>
        CustomArgs(str, "shake", $"rate={rate} level={level}");
    public static string Fade(string str, float start, float length) =>
        CustomArgs(str, "fade", $"start={start} length={length}");
    public static string Rainbow(string str, float saturation, float value, float freq) =>
        CustomArgs(str, "rainbow", $"freq={freq} sat={saturation} val={value}");


    #endregion

}
