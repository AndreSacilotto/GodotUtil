using System.Runtime.CompilerServices;

namespace Godot;

public static partial class UtilGD
{
    [MethodImpl(INLINE)] public static SceneTree GetSceneTree() => (SceneTree)Engine.GetMainLoop();


    #region PackedScene->Add
    public static Node Instantiate(this PackedScene scene, Node parent, PackedScene.GenEditState editState = PackedScene.GenEditState.Disabled)
    {
        var instance = scene.Instantiate(editState);
        parent.AddChild(instance);
        return instance;
    }
    public static T Instantiate<T>(this PackedScene scene, Node parent, PackedScene.GenEditState editState = PackedScene.GenEditState.Disabled) where T : Node
    {
        var instance = scene.Instantiate<T>(editState);
        parent.AddChild(instance);
        return instance;
    }
    #endregion

    #region Node Add

    [MethodImpl(INLINE)]
    public static void AddChildren(this Node parent, params Node[] children)
    {
        for (int i = 0; i < children.Length; i++)
            parent.AddChild(children[i]);
    }

    [MethodImpl(INLINE)]
    public static T AddChild<T>(this Node parent, T node) where T : Node
    {
        parent.AddChild(node);
        return node;
    }

    [MethodImpl(INLINE)]
    public static T AddChildAt<T>(this Node parent, T node, int index) where T : Node
    {
        parent.AddChild(node);
        parent.MoveChild(node, index);
        return node;
    }

    #endregion

    #region Node Create

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

    #endregion

    #region Remove/Move

    public static bool RemoveFromParent(this Node self)
    {
        var parent = self.GetParent();
        if (parent == null)
            return false;
        parent.RemoveChild(self);
        return true;
    }

    public static void MoveNodeTo(this Node self, Node newParent)
    {
        self.GetParent()?.RemoveChild(self);
        newParent.AddChild(self);
    }

    #endregion

    #region Get Child

    public static T? GetFirstChildAs<T>(this Node node) where T : Node
    {
        return node.GetChildCount() == 0 ? null : node.GetChild<T>(0);
    }

    public static T? GetLastChild<T>(this Node node) where T : Node
    {
        var count = node.GetChildCount();
        return count == 0 ? null : node.GetChild<T>(count - 1);
    }

    public static T? GetFirstChildOfType<T>(this Node node) where T : Node
    {
        var children = node.GetChildren();
        for (int i = 0; i < children.Count; i++)
            if (children[i] is T t)
                return t;
        return null;
    }
    public static T? GetLastChildOfType<T>(this Node node) where T : Node
    {
        var children = node.GetChildren();
        for (int i = children.Count - 1; i >= 0; i--)
            if (children[i] is T t)
                return t;
        return null;
    }

    #endregion

    #region Children Iteration Util

    public static void QueueFreeAllChildrens(this Node parent)
    {
        foreach (Node item in parent.GetChildren())
            item.QueueFree();
    }

    public static Node[] RemoveAllChildrens(this Node parent)
    {
        int i = 0;
        var arr = new Node[parent.GetChildCount()];
        foreach (var item in parent.GetChildren())
        {
            arr[i] = item;
            item.RemoveChild(item);
        }
        return arr;
    }

    #endregion

    #region Children Iteration

    public static IEnumerable<T> GetEnumeratorChildrenOfType<T>(this Node node) where T : Node
    {
        var children = node.GetChildren();
        for (int i = 0; i < children.Count; i++)
            if (children[i] is T t)
                yield return t;
    }

    public static IEnumerable<T> GetEnumeratorChildren<T>(this Node node) where T : Node
    {
        foreach (var item in node.GetChildren())
            yield return (T)item;
    }

    public static IEnumerable<T> GetEnumeratorVisibleChildren<T>(this Node node, bool visible = true) where T : CanvasItem
    {
        foreach (var item in node.GetChildren())
            if (item is T c && c.Visible == visible)
                yield return c;
    }

    public static IEnumerable<T> GetEnumeratorVisibleChildren3D<T>(this Node node, bool visible = true) where T : Node3D
    {
        foreach (var item in node.GetChildren())
            if (item is T c && c.Visible == visible)
                yield return c;
    }

    #endregion

}
