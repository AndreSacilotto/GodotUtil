using System.Runtime.CompilerServices;
using Util;

namespace Godot
{
	public static partial class UtilGD
	{
		[MethodImpl(UtilShared.INLINE)] public static ShaderMaterial AsShader(this Material material) => (ShaderMaterial)material;


		#region Add Generic Cast

		[MethodImpl(UtilShared.INLINE)] public static T GetShaderParam<T>(this ShaderMaterial sm, string uniform) => (T)sm.GetShaderParam(uniform);
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


	}
}
