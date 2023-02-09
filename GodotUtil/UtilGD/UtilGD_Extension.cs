using System.Runtime.CompilerServices;
using Util;

namespace Godot
{
	public static partial class UtilGD
	{
		[MethodImpl(UtilShared.INLINE)] public static ShaderMaterial AsShader(this Material material) => (ShaderMaterial)material;
		
		[MethodImpl(UtilShared.INLINE)] public static T New<T>(this CSharpScript sharp) where T : Godot.Object => (T)sharp.New();
		[MethodImpl(UtilShared.INLINE)] public static T New<T>(this CSharpScript sharp, params object[] args) => (T)sharp.New(args);


		[MethodImpl(UtilShared.INLINE)] public static T GetShaderParam<T>(this ShaderMaterial sm, string uniform) => (T)sm.GetShaderParam(uniform);

		[MethodImpl(UtilShared.INLINE)] public static T Duplicate<T>(this Resource res, bool subResources = false) where T : Resource => (T)res.Duplicate(subResources);
		[MethodImpl(UtilShared.INLINE)] public static T Duplicate<T>(this Node node, Node.DuplicateFlags flags = (Node.DuplicateFlags)15) where T : Node => (T)node.Duplicate((int)flags);

	}
}
