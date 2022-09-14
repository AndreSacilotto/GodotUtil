using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Godot
{
    public static partial class UtilGD
    {

        #region Add Generic Cast

        [MethodImpl(Util.UtilShared.INLINE)] public static T GetShaderParam<T>(this ShaderMaterial sm, string uniform) => (T)sm.GetShaderParam(uniform);

        [MethodImpl(Util.UtilShared.INLINE)] public static T Duplicate<T>(this Resource res, bool subResources = false) where T : Resource => (T)res.Duplicate(subResources);
        [MethodImpl(Util.UtilShared.INLINE)] public static T Duplicate<T>(this Node res, Node.DuplicateFlags flags = (Node.DuplicateFlags)15) where T : Node => (T)res.Duplicate((int)flags);


        #endregion

        #region Node Add/Create

        [MethodImpl(Util.UtilShared.INLINE)]
        public static T CreateAndAdd<T>(this Node parent) where T : Node, new()
        {
            var newNode = new T();
            parent.AddChild(newNode);
            return newNode;
        }

        [MethodImpl(Util.UtilShared.INLINE)]
        public static T CreateAndAdd<T>(this Node parent, string name) where T : Node, new()
        {
            var newNode = new T() { Name = name };
            parent.AddChild(newNode);
            return newNode;
        }

        [MethodImpl(Util.UtilShared.INLINE)]
        public static T AddChild<T>(this Node parent, T node) where T : Node
        {
            parent.AddChild(node);
            return node;
        }

        #endregion

        #region Delete/Remove

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

        public static bool RemoveFromParent(this Node self)
        {
            var parent = self.GetParent();
            if (parent == null)
                return false;
            parent.RemoveChild(self);
            return true;
        }

        #endregion
        
        #region Node Move/Find
        
        public static void MoveNodeTo(this Node self, Node newParent)
        {
            var parent = self.GetParent();
            if (parent != null)
                parent.RemoveChild(self);
            newParent.AddChild(self);
        }

        public static List<T> GetChildrenOfType<T>(this Node node) where T : Node
        {
            var children = node.GetChildren();
            var col = new List<T>();
            for (int i = 0; i < children.Count; i++)
                if (children[i] is T t)
                    col.Add(t);
            return col;
        }

        public static T GetFirstChild<T>(this Node node) where T : Node
        {
            var count = node.GetChildCount();
            if (count == 0)
                return null;
            return node.GetChild<T>(0);
        }
        public static T GetLastChild<T>(this Node node) where T : Node
        {
            var count = node.GetChildCount();
            if (count == 0) 
                return null;
            return node.GetChild<T>(count - 1);
        }

        public static T GetFirstChildOfType<T>(this Node node) where T : Node
        {
            var children = node.GetChildren();
            for (int i = 0; i < children.Count; i++)
                if (children[i] is T t)
                    return t;
            return null;
        }
        public static T GetLastChildOfType<T>(this Node node) where T : Node
        {
            var children = node.GetChildren();
            for (int i = children.Count - 1; i >= 0; i--)
                if (children[i] is T t)
                    return t;
            return null;
        }


        #endregion

        #region Node Hierarch

        [MethodImpl(Util.UtilShared.INLINE)] public static bool HasAnyChild(this Node node) => node.GetChildCount() > 0;
        [MethodImpl(Util.UtilShared.INLINE)] public static bool HasNoChild(this Node node) => node.GetChildCount() == 0;

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
    }
}
