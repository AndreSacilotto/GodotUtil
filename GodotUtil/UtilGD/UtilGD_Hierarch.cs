using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UtilShared = Util.UtilShared;

namespace Godot
{
	public static partial class UtilGD
	{
		[MethodImpl(UtilShared.INLINE)] public static SceneTree GetSceneTree() => (SceneTree)Engine.GetMainLoop();

		#region Add Generic Cast

		[MethodImpl(UtilShared.INLINE)] public static T GetShaderParam<T>(this ShaderMaterial sm, string uniform) => (T)sm.GetShaderParam(uniform);
		[MethodImpl(UtilShared.INLINE)] public static T GetShaderParam<T>(this Material sm, string uniform) => (T)((ShaderMaterial)sm).GetShaderParam(uniform);
		[MethodImpl(UtilShared.INLINE)] public static void SetShaderParam(this Material sm, string uniform, object value) => ((ShaderMaterial)sm).SetShaderParam(uniform, value);

		[MethodImpl(UtilShared.INLINE)] public static T Duplicate<T>(this Resource res, bool subResources = false) where T : Resource => (T)res.Duplicate(subResources);
		[MethodImpl(UtilShared.INLINE)] public static T Duplicate<T>(this Node node, Node.DuplicateFlags flags = (Node.DuplicateFlags)15) where T : Node => (T)node.Duplicate((int)flags);


		#endregion

		#region Instanciate->Add

		public static Node Instanciate(this PackedScene scene, Node parent, PackedScene.GenEditState editState = PackedScene.GenEditState.Disabled)
		{
			var instance = scene.Instance(editState);
			parent.AddChild(instance);
			return instance;
		}
		public static T Instanciate<T>(this PackedScene scene, Node parent, PackedScene.GenEditState editState = PackedScene.GenEditState.Disabled) where T : Node
		{
			var instance = scene.Instance<T>(editState);
			parent.AddChild(instance);
			return instance;
		}

		#endregion

		#region Node Add

		[MethodImpl(UtilShared.INLINE)]
		public static void AddChildren(this Node parent, params Node[] children)
		{
			for (int i = 0; i < children.Length; i++)
				parent.AddChild(children[i]);
		}

		[MethodImpl(UtilShared.INLINE)]
		public static T AddChild<T>(this Node parent, T node) where T : Node
		{
			parent.AddChild(node);
			return node;
		}

		[MethodImpl(UtilShared.INLINE)]
		public static T AddChildAt<T>(this Node parent, T node, int index) where T : Node
		{
			parent.AddChild(node);
			parent.MoveChild(node, index);
			return node;
		}

		[MethodImpl(UtilShared.INLINE)]
		public static T NewChild<T>(this Node parent) where T : Node, new()
		{
			var nd = new T();
			parent.AddChild(nd);
			return nd;
		}

		#endregion

		#region Node Create

		[MethodImpl(UtilShared.INLINE)]
		public static T CreateAndAdd<T>(this Node parent) where T : Node, new()
		{
			var newNode = new T();
			parent.AddChild(newNode);
			return newNode;
		}

		[MethodImpl(UtilShared.INLINE)]
		public static T CreateAndAdd<T>(this Node parent, string name) where T : Node, new()
		{
			var newNode = new T() { Name = name };
			parent.AddChild(newNode);
			return newNode;
		}

		#endregion

		#region Delete/Remove

		public static void QueueFreeAllChildrens(this Node parent)
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

		public static T? GetFirstChild<T>(this Node node) where T : Node
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

		#region Node Hierarch

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

		#endregion

	}
}
