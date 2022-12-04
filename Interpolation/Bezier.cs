
using vec2 = Godot.Vector2;
using vec3 = Godot.Vector3;

namespace Util.Interpolation
{
	public static class Bezier
	{
		#region Linear
		public static float LinearBezier(float start, float end, float t) => start + (end - start) * t;
		public static vec2 LinearBezier(vec2 start, vec2 end, float t) => start + (end - start) * t;
		public static vec3 LinearBezier(vec3 start, vec3 end, float t) => start + (end - start) * t;
		#endregion

		#region Quad
		public static float QuadraticBezier(float start, float control, float end, float t)
		{
			var o = 1f - t;
			return o * o * start + o * t * 2f * control + t * t * end;
		}
		public static vec2 QuadraticBezier(vec2 start, vec2 control, vec2 end, float t)
		{
			var o = 1f - t;
			return o * o * start + o * t * 2f * control + t * t * end;
		}
		public static vec3 QuadraticBezier(vec3 start, vec3 control, vec3 end, float t)
		{
			var o = 1f - t;
			return o * o * start + o * t * 2f * control + t * t * end;
		}
		#endregion

		#region Cubic
		public static float CubicBezier(float start, float control0, float control1, float end, float t)
		{
			var o = 1f - t;
			var oo = o * o;
			var tt = t * t;

			return oo * o * start +
					oo * t * 3f * control0 +
					tt * t * control1 +
					tt * o * 3f * end;
		}
		public static vec2 CubicBezier(vec2 start, vec2 control0, vec2 control1, vec2 end, float t)
		{
			var o = 1f - t;
			var oo = o * o;
			var tt = t * t;

			return oo * o * start +
					oo * t * 3f * control0 +
					tt * t * control1 +
					tt * o * 3f * end;
		}
		public static vec3 CubicBezier(vec3 start, vec3 control0, vec3 control1, vec3 end, float t)
		{
			var o = 1f - t;
			var oo = o * o;
			var tt = t * t;

			return oo * o * start +
					oo * t * 3f * control0 +
					tt * t * control1 +
					tt * o * 3f * end;
		}
		#endregion

	}
}
