
using Godot;
using System.Runtime.CompilerServices;

namespace GodotUtil;

public static class UtilNodeXD
{
    [MethodImpl(INLINE)] public static Vector2 FowardDirection(Node2D node) => node.GlobalTransform.X;
    [MethodImpl(INLINE)] public static Vector3 FowardDirection(Node3D node) => -node.GlobalTransform.Basis.Z;

}
