using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Godot
{
    public static class UtilNode
    {
        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

        #region Add

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

        #endregion

        #region Delete/Remove

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

        public static void DeleteAllChildrens(this Node parent)
        {
            foreach (Node item in parent.GetChildren())
                item.QueueFree();
        }

        public static bool RemoveSelf(this Node self)
        {
            var parent = self.GetParent();
            if (parent == null)
                return false;
            parent.RemoveChild(self);
            return true;
        }

        #endregion

        #region Child
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

        #region Add Generic Cast

        [MethodImpl(INLINE)] public static T GetShaderParam<T>(this ShaderMaterial sm, string uniform) => (T)sm.GetShaderParam(uniform);
        
        [MethodImpl(INLINE)] public static T Duplicate<T>(this Resource res, bool subResources = false) where T : Resource => (T)res.Duplicate(subResources);
        [MethodImpl(INLINE)] public static T Duplicate<T>(this Node res, Node.DuplicateFlags flags = (Node.DuplicateFlags)15) where T : Node => (T)res.Duplicate((int)flags);
        
        #endregion
    }
}
