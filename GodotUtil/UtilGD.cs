using System.Collections.Generic;

namespace Godot
{
    public static class UtilGD
    {

        #region Print

        public static bool IsNullPrint(object value)
        {
            var isnull = value == null;
            if (isnull)
                GD.Print("null");
            return isnull;
        }

        public static void PrintCollection<TK, TV>(IEnumerable<KeyValuePair<TK, TV>> ie, string separator = " ")
        {
            string str = "";
            foreach (var item in ie)
                str += $"({item.Key}, {item.Value}){separator}";
            GD.Print(str);
        }

        public static void PrintCollection<T>(IEnumerable<T> ie, string separator = " ")
        {
            string str = "";
            foreach (var item in ie)
                str += item.ToString() + separator;
            str.Remove(str.Length - separator.Length);
            GD.Print(str);
        }

        #endregion
        
        public static void GodotTime(out int hour, out int minute, out int seconds)
        {
            var timeDict = OS.GetTime();
            hour = (int)timeDict["hour"];
            minute = (int)timeDict["minute"];
            seconds = (int)timeDict["second"];
        }

        #region Create Node

        public static Timer CreateTimerNode(Node ownerNode, string functionName, bool oneshot = false, float waitTime = 1f, Timer.TimerProcessMode processMode = Timer.TimerProcessMode.Idle)
        {
            var timer = new Timer
            {
                WaitTime = waitTime,
                OneShot = oneshot,
                ProcessMode = processMode,
                Autostart = false,
            };
            timer.Connect("timeout", ownerNode, functionName);
            ownerNode?.AddChild(timer);
            return timer;
        }

        public static Tween CreateTweenNode(Node ownerNode, bool repeat = false, float playbackSpeed = 1, Tween.TweenProcessMode playbackMode = Tween.TweenProcessMode.Idle)
        {
            var tween = new Tween
            {
                Repeat = repeat,
                PlaybackProcessMode = playbackMode,
                PlaybackSpeed = playbackSpeed,
            };
            ownerNode?.AddChild(tween);
            return tween;
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
}
