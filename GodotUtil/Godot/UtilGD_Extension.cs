using System.Runtime.CompilerServices;

namespace Godot;

public static partial class UtilGD
{
    [MethodImpl(INLINE)] public static ShaderMaterial AsShader(this Material material) => (ShaderMaterial)material;

    [MethodImpl(INLINE)] public static T New<T>(this CSharpScript sharp) where T : GodotObject => (T)sharp.New();
    [MethodImpl(INLINE)] public static T New<[MustBeVariant] T>(this CSharpScript sharp, params Variant[] args) => sharp.New(args).As<T>();

    [MethodImpl(INLINE)] public static T GetShaderParameter<[MustBeVariant] T>(this ShaderMaterial sm, string uniform) => sm.GetShaderParameter(uniform).As<T>();

    [MethodImpl(INLINE)] public static T Duplicate<T>(this Resource res, bool subResources = false) where T : Resource => (T)res.Duplicate(subResources);
    [MethodImpl(INLINE)] public static T Duplicate<T>(this Node node, Node.DuplicateFlags flags = (Node.DuplicateFlags)15) where T : Node => (T)node.Duplicate((int)flags);


    public static void DisposeInputEventImmediately(this InputEvent inputEvent)
    {
        inputEvent.Dispose();
        GC.Collect(GC.MaxGeneration);
        GC.WaitForPendingFinalizers();
    }
}
