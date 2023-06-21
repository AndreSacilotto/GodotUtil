

namespace Godot;

public static partial class UtilGD
{
    #region Time

    public static void GodotTime(out int hour, out int minute, out int seconds)
    {
        var timeDict = Time.GetTimeDictFromSystem();
        hour = (int)timeDict["hour"];
        minute = (int)timeDict["minute"];
        seconds = (int)timeDict["second"];
    }

    #endregion

    #region Contructors

    public static Timer CreateTimerNode(Node ownerNode, bool oneshot = false, float waitTime = 1f, Timer.TimerProcessCallback processCallback = Timer.TimerProcessCallback.Idle)
    {
        var timer = new Timer
        {
            WaitTime = waitTime,
            OneShot = oneshot,
            ProcessCallback = processCallback,
            Autostart = false,
        };
        ownerNode?.AddChild(timer);
        return timer;
    }

    public static Line2D CreateLine2DBox(Node ownerNode, float width, float height, Color color, int girth = 10, int zindex = 0)
    {
        var line = new Line2D
        {
            Name = "LineBox2D",
            EndCapMode = Line2D.LineCapMode.Box,
            DefaultColor = color,
            Width = girth,
            Points = new Vector2[] {
                Vector2.Zero,
                new Vector2(0, height),
                new Vector2(width, height),
                new Vector2(width, 0),
                Vector2.Zero,
            },
            ZIndex = zindex,
        };
        ownerNode?.AddChild(ownerNode);
        return line;
    }

    #endregion
}
