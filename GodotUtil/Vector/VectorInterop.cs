using System.Runtime.CompilerServices;

using g_q = Godot.Quaternion;
using g_t2 = Godot.Transform2D;
using g_t3 = Godot.Transform3D;
using g_v2 = Godot.Vector2;
using g_v3 = Godot.Vector3;
using g_v4 = Godot.Vector4;
using s_m3x2 = System.Numerics.Matrix3x2;
using s_m4x4 = System.Numerics.Matrix4x4;
using s_q = System.Numerics.Quaternion;
using s_v2 = System.Numerics.Vector2;
using s_v3 = System.Numerics.Vector3;
using s_v4 = System.Numerics.Vector4;

namespace Util;

public static class VectorInterop
{
	#region System -> Godot


	[MethodImpl(INLINE)]
	public static g_v2 Interop(this s_v2 vec) => new(vec.X, vec.Y);
	[MethodImpl(INLINE)]
	public static g_v3 Interop(this s_v3 vec) => new(vec.X, vec.Y, vec.Z);
	[MethodImpl(INLINE)]
	public static g_v4 Interop(this s_v4 vec) => new(vec.X, vec.Y, vec.Z, vec.W);
	[MethodImpl(INLINE)]
	public static g_q Interop(this s_q quart) => new(quart.X, quart.Y, quart.Z, quart.W);
	[MethodImpl(INLINE)]
	public static g_t3 Interop(this s_m4x4 m) => new(new(m.M11, m.M21, m.M31), new(m.M12, m.M22, m.M32), new(m.M13, m.M23, m.M33), new(m.M14, m.M24, m.M34));
	[MethodImpl(INLINE)]
	public static g_t2 Interop2(this s_m4x4 m) => new(new(m.M11, m.M21), new(m.M12, m.M22), new(m.M13, m.M23));

	[MethodImpl(INLINE)]
	public static g_t2 Interop(this s_m3x2 m)
	{
		// | 11	 12 |		| 11 12 31 |
		// | 21  22 |	=>	| 21 22 32 |
		// | 31  32 |
		return new g_t2(m.M11, m.M21, m.M12, m.M22, m.M31, m.M32);
	}

	[MethodImpl(INLINE)]
	public static g_t2 InteropTranspose(this s_m3x2 m)
	{
		//					Transposed
		// | 11	 12 |		| 11 21 31 |
		// | 21  22 |	=>	| 12 22 32 |
		// | 31  32 |
		return new g_t2(m.M11, m.M12, m.M21, m.M22, m.M31, m.M32);
	}

	#endregion


	#region Godot -> System

	[MethodImpl(INLINE)]
	public static s_v2 Interop(this g_v2 vec) => new(vec.X, vec.Y);
	[MethodImpl(INLINE)]
	public static s_v3 Interop(this g_v3 vec) => new(vec.X, vec.Y, vec.Z);
	[MethodImpl(INLINE)]
	public static s_v4 Interop(this g_v4 vec) => new(vec.X, vec.Y, vec.Z, vec.W);
	[MethodImpl(INLINE)]
	public static s_q Interop(this g_q quart) => new(quart.X, quart.Y, quart.Z, quart.W);

	[MethodImpl(INLINE)]
	public static s_m4x4 Interop4x4(this g_t2 m)
	{
		var c0 = m.X;
		var c1 = m.Y;
		var c2 = m.Origin;
		return new s_m4x4(	c0.X,	c1.X,	0,	c2.X,
							c0.Y,	c1.Y,	0,	c2.Y,
							0,		0,		1,	0,
							0,		0,		0,	1);
	}
	[MethodImpl(INLINE)]
	public static s_m4x4 Interop(this g_t3 m)
	{
		var c0 = m.Basis.Column0;
		var c1 = m.Basis.Column1;
		var c2 = m.Basis.Column2;
		var c3 = m.Origin;
		return new s_m4x4(c0.X, c1.X, c2.X, c3.X, c0.Y, c1.Y, c2.Y, c3.Y, c0.Z, c1.Z, c2.Z, c3.Z, 0, 0, 0, 1);
	}


	[MethodImpl(INLINE)]
	public static s_m3x2 Interop(this g_t2 m)
	{
		// | xx	 yx |		| xx yx ox |
		// | xy  yy |	<=	| xy yy oy |
		// | ox  oy |
		return new s_m3x2(m.X.X, m.X.Y, m.Y.X, m.Y.Y, m.Origin.X, m.Origin.Y);
	}

	[MethodImpl(INLINE)]
	public static s_m3x2 InteropTranspose(this g_t2 m)
	{
		// Transposed
		// | xx	 xy |		| xx yx ox |
		// | yx  yy |	<=	| xy yy oy |
		// | ox  oy |
		return new s_m3x2(m.X.X, m.Y.X, m.X.Y, m.Y.Y, m.Origin.X, m.Origin.Y);
	}

	#endregion

}
