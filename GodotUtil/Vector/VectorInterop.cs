using gv2 = Godot.Vector2;
using gv3 = Godot.Vector3;
using gv4 = Godot.Vector4;
using gq = Godot.Quat;
using gt2 = Godot.Transform2D;
using gt3 = Godot.Transform;

using sv2 = System.Numerics.Vector2;
using sv3 = System.Numerics.Vector3;
using sv4 = System.Numerics.Vector4;
using sq = System.Numerics.Quaternion;
using sm44 = System.Numerics.Matrix4x4;

namespace Util
{
	public static class VectorInterop
	{
		public static gv2 Interop(this sv2 vec) => new(vec.X, vec.Y);
		public static gv3 Interop(this sv3 vec) => new(vec.X, vec.Y, vec.Z);
		public static gv4 Interop(this sv4 vec) => new(vec.X, vec.Y, vec.Z, vec.W);
		public static gq Interop(this sq quart) => new(quart.X, quart.Y, quart.Z, quart.W);

		public static gt3 Interop(this sm44 m) => new(new(m.M11, m.M21, m.M31), new(m.M12, m.M22, m.M32), new(m.M13, m.M23, m.M33), new(m.M14, m.M24, m.M34));
		public static gt2 Interop2(this sm44 m) => new(new(m.M11, m.M21), new(m.M12, m.M22), new(m.M13, m.M23));


		public static sv2 Interop(this gv2 vec) => new(vec.x, vec.y);
		public static sv3 Interop(this gv3 vec) => new(vec.x, vec.y, vec.z);
		public static sv4 Interop(this gv4 vec) => new(vec.x, vec.y, vec.z, vec.w);
		public static sq Interop(this gq quart) => new(quart.x, quart.y, quart.z, quart.w);

		public static sm44 Interop(this gt2 m)
		{
			var c0 = m.x;
			var c1 = m.y;
			var c2 = m.origin;
			return new sm44(c0.x, c1.x, c2.x, 0, 
							c0.y, c1.y, c2.y, 0, 
							   0,    0,    1, 0, 
							   0,    0,    0, 1);
		}
		public static sm44 Interop(this gt3 m)
		{
			var c0 = m.basis.Column0;
			var c1 = m.basis.Column1;
			var c2 = m.basis.Column2;
			var c3 = m.origin;
			return new sm44(c0.x, c1.x, c2.x, c3.x, c0.y, c1.y, c2.y, c3.y, c0.z, c1.z, c2.z, c3.z, 0, 0, 0, 1);
		}
	}
}
