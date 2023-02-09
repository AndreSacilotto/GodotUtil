using System.Runtime.CompilerServices;

using g_v2 = Godot.Vector2;
using g_v3 = Godot.Vector3;
using g_v4 = Godot.Vector4;
using g_q = Godot.Quat;
using g_t2 = Godot.Transform2D;
using g_t3 = Godot.Transform;

using s_v2 = System.Numerics.Vector2;
using s_v3 = System.Numerics.Vector3;
using s_v4 = System.Numerics.Vector4;
using s_q = System.Numerics.Quaternion;
using s_m3x2 = System.Numerics.Matrix3x2;
using s_m4x4 = System.Numerics.Matrix4x4;

using static Util.UtilShared;

namespace Util
{
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
		public static s_v2 Interop(this g_v2 vec) => new(vec.x, vec.y);
		[MethodImpl(INLINE)]
		public static s_v3 Interop(this g_v3 vec) => new(vec.x, vec.y, vec.z);
		[MethodImpl(INLINE)]
		public static s_v4 Interop(this g_v4 vec) => new(vec.x, vec.y, vec.z, vec.w);
		[MethodImpl(INLINE)]
		public static s_q Interop(this g_q quart) => new(quart.x, quart.y, quart.z, quart.w);

		[MethodImpl(INLINE)]
		public static s_m4x4 Interop4x4(this g_t2 m)
		{
			var c0 = m.x;
			var c1 = m.y;
			var c2 = m.origin;
			return new s_m4x4(c0.x, c1.x, c2.x, 0, 
							c0.y, c1.y, c2.y, 0, 
							   0,    0,    1, 0, 
							   0,    0,    0, 1);
		}
		[MethodImpl(INLINE)]
		public static s_m4x4 Interop(this g_t3 m)
		{
			var c0 = m.basis.Column0;
			var c1 = m.basis.Column1;
			var c2 = m.basis.Column2;
			var c3 = m.origin;
			return new s_m4x4(c0.x, c1.x, c2.x, c3.x, c0.y, c1.y, c2.y, c3.y, c0.z, c1.z, c2.z, c3.z, 0, 0, 0, 1);
		}


		[MethodImpl(INLINE)]
		public static s_m3x2 Interop(this g_t2 m)
		{
			// | xx	 yx |		| xx yx ox |
			// | xy  yy |	<=	| xy yy oy |
			// | ox  oy |
			return new s_m3x2(m.x.x, m.x.y, m.y.x, m.y.y, m.origin.x, m.origin.y);
		}

		[MethodImpl(INLINE)]
		public static s_m3x2 InteropTranspose(this g_t2 m)
		{
			// Transposed
			// | xx	 xy |		| xx yx ox |
			// | yx  yy |	<=	| xy yy oy |
			// | ox  oy |
			return new s_m3x2(m.x.x, m.y.x, m.x.y, m.y.y, m.origin.x, m.origin.y);
		}

		#endregion

	}
}
