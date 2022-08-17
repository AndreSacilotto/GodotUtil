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

        public static RandomNumberGenerator GodotRNG(bool setup = false)
        {
            var rng = new RandomNumberGenerator();
            if(setup)
                rng.Randomize();
            return rng;
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

        #region Collections Array

        public static object[] GodotArray(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var arr = new object[len];
            for (int i = 0; i < len; i++)
                arr[i] = gdArray[i];
            return arr;
        }
        public static T[] GodotArray<T>(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var arr = new T[len];
            for (int i = 0; i < len; i++)
                arr[i] = (T)gdArray[i];
            return arr;
        }
        public static T[] GodotArray<T>(Collections.Array<T> gdArray)
        {
            var len = gdArray.Count;
            var arr = new T[len];
            for (int i = 0; i < len; i++)
                arr[i] = gdArray[i];
            return arr;
        }

        public static List<object> GodotArrayList(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var list = new List<object>(len);
            for (int i = 0; i < len; i++)
                list[i] = gdArray[i];
            return list;
        }
        public static List<T> GodotArrayList<T>(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var list = new List<T>(len);
            for (int i = 0; i < len; i++)
                list[i] = (T)gdArray[i];
            return list;
        }
        public static List<T> GodotArrayList<T>(Collections.Array<T> gdArray)
        {
            var len = gdArray.Count;
            var list = new List<T>(len);
            for (int i = 0; i < len; i++)
                list[i] = gdArray[i];
            return list;
        }

        #endregion

        #region Collections Dictionary

        public static Dictionary<object, object> GodotDictionary(Collections.Dictionary gdDict)
        {
            var len = gdDict.Count;
            var dic = new Dictionary<object, object>(len);
            foreach (KeyValuePair<object, object> item in gdDict)
                dic.Add(item.Key, item.Value);
            return dic;
        }
        public static Dictionary<K, V> GodotDictionary<K, V>(Collections.Dictionary gdDict)
        {
            var len = gdDict.Count;
            var dic = new Dictionary<K, V>(len);
            foreach (KeyValuePair<K, V> item in gdDict)
                dic.Add(item.Key, item.Value);
            return dic;
        }

        public static Dictionary<K, V> GodotDictionary<K, V>(Collections.Dictionary<K, V> gdDict)
        {
            var len = gdDict.Count;
            var dic = new Dictionary<K, V>(len);
            foreach (KeyValuePair<K, V> item in gdDict)
                dic.Add(item.Key, item.Value);
            return dic;
        }


        #endregion

    }
}
