﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Godot
{
    public static class UtilGD
    {
        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

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

        #region Time
        
        public static void GodotTime(out int hour, out int minute, out int seconds)
        {
            var timeDict = OS.GetTime();
            hour = (int)timeDict["hour"];
            minute = (int)timeDict["minute"];
            seconds = (int)timeDict["second"];
        }

        #endregion

        #region RNG

        public static RandomNumberGenerator GodotRNG(bool setup = false)
        {
            var rng = new RandomNumberGenerator();
            if(setup)
                rng.Randomize();
            return rng;
        }

        #endregion

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

        #region Node Create/Add Remove/Delete

        [MethodImpl(INLINE)]
        public static T CreateAndAdd<T>(this Node parent) where T : Node, new()
        {
            var newNode = new T();
            parent.AddChild(newNode);
            return newNode;
        }

        [MethodImpl(INLINE)]
        public static T CreateAndAdd<T>(this Node parent, string name) where T : Node, new()
        {
            var newNode = new T() { Name = name };
            parent.AddChild(newNode);
            return newNode;
        }

        [MethodImpl(INLINE)]
        public static T AddChild<T>(this Node parent, T node) where T : Node
        {
            parent.AddChild(node);
            return node;
        }

        public static void DeleteAllChildrens(this Node parent)
        {
            foreach (Node item in parent.GetChildren())
                item.QueueFree();
        }

        public static T[] RemoveAllChildrens<T>(this Node parent) where T : Node
        {
            int i = 0;
            var arr = new T[parent.GetChildCount()];
            foreach (T item in parent.GetChildren())
            {
                arr[i] = item;
                item.RemoveChild(item);
            }
            return arr;
        }

        public static bool RemoveSelfFromParent(this Node self)
        {
            var parent = self.GetParent();
            if (parent == null)
                return false;
            parent.RemoveChild(self);
            return true;
        }

        public static void MoveNodeTo(this Node self, Node newParent)
        {
            var parent = self.GetParent();
            if (parent != null)
                parent.RemoveChild(self);
            newParent.AddChild(self);
        }

        #endregion

        #region Node Hierarch
        [MethodImpl(INLINE)] public static void GetNode<T>(this Node node, NodePath np, out T item) where T : Node => item = node.GetNode<T>(np);
        [MethodImpl(INLINE)] public static void GetChild<T>(this Node node, int index, out T item) where T : Node => item = node.GetChild<T>(index);

        [MethodImpl(INLINE)] public static bool HasChild(this Node node) => node.GetChildCount() > 0;
        [MethodImpl(INLINE)] public static bool HasNoChild(this Node node) => node.GetChildCount() <= 0;

        public static IEnumerable<T> GetEnumeratorNodeChildren<T>(this Node node) where T : Node
        {
            foreach (var item in node.GetChildren())
                yield return (T)item;
        }

        #endregion

        #region Node Others

        public static void SetBothProcess(Node node, bool enable)
        {
            node.SetProcess(enable);
            node.SetPhysicsProcess(enable);
        }

        #endregion

        #region Add Generic Cast

        [MethodImpl(INLINE)] public static T GetShaderParam<T>(this ShaderMaterial sm, string uniform) => (T)sm.GetShaderParam(uniform);

        [MethodImpl(INLINE)] public static T Duplicate<T>(this Resource res, bool subResources = false) where T : Resource => (T)res.Duplicate(subResources);
        [MethodImpl(INLINE)] public static T Duplicate<T>(this Node res, Node.DuplicateFlags flags = (Node.DuplicateFlags)15) where T : Node => (T)res.Duplicate((int)flags);

        #endregion

    }
}