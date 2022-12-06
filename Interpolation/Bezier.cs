
using vec2 = Godot.Vector2;
using vec3 = Godot.Vector3;

namespace Util.Interpolation
{
	public static class Bezier
	{
		#region Linear (Same as Lerp)
		public static float LinearBezier(float start, float end, float t) => start + (end - start) * t;
		public static vec2 LinearBezier(vec2 start, vec2 end, float t) => start + (end - start) * t;
		public static vec3 LinearBezier(vec3 start, vec3 end, float t) => start + (end - start) * t;
		#endregion

		#region Quadratic
		public static float QuadBezier(float start, float control, float end, float t)
		{
			var o = 1f - t;
			return o * o * start + o * t * 2f * control + t * t * end;
		}
		public static vec2 QuadBezier(vec2 start, vec2 control, vec2 end, float t)
		{
			return new(
				QuadBezier(start.x, control.x, end.x, t),
				QuadBezier(start.y, control.y, end.y, t)
			);
		}
		public static vec3 QuadBezier(vec3 start, vec3 control, vec3 end, float t)
		{
			return new(
				QuadBezier(start.x, control.x, end.x, t),
				QuadBezier(start.y, control.y, end.y, t),
				QuadBezier(start.z, control.z, end.z, t)
			);
		}
		#endregion

		#region Quadratic Derivative
		public static float QuadBezierDerivative(float start, float control, float end, float t)
		{
			return 2f * (1f - t) * (control - start) + 2f * t * (end - control);
		}
		public static vec2 QuadBezierDerivative(vec2 start, vec2 control, vec2 end, float t)
		{
			return new(
				QuadBezier(start.x, control.x, end.x, t),
				QuadBezier(start.y, control.y, end.y, t)
			);
		}
		public static vec3 QuadBezierDerivative(vec3 start, vec3 control, vec3 end, float t)
		{
			return new(
				QuadBezier(start.x, control.x, end.x, t),
				QuadBezier(start.y, control.y, end.y, t),
				QuadBezier(start.z, control.z, end.z, t)
			);
		}
		#endregion

		#region Cubic
		public static float CubicBezier(float start, float control0, float control1, float end, float t)
		{
			var o = 1f - t;
			var oo = o * o;
			var tt = t * t;

			return oo * o * start +
				3f * oo * t * control0 +
				3f * o * tt * control1 +
				tt * t * end;
		}
		public static vec2 CubicBezier(vec2 start, vec2 control0, vec2 control1, vec2 end, float t)
		{
			return new(
				CubicBezier(start.x, control0.x, control1.x, end.x, t),
				CubicBezier(start.y, control0.y, control1.y, end.y, t)
			);
		}
		public static vec3 CubicBezier(vec3 start, vec3 control0, vec3 control1, vec3 end, float t)
		{
			return new(
				CubicBezier(start.x, control0.x, control1.x, end.x, t),
				CubicBezier(start.y, control0.y, control1.y, end.y, t),
				CubicBezier(start.z, control0.z, control1.z, end.z, t)
			);
		}
		#endregion

		#region Cubic Derivative
		public static float CubicBezierDerivative(float start, float control0, float control1, float end, float t)
		{
			var o = 1f - t;
			return (control1 - start) * 3f * o * o + 
				(control1 - control0) * 6f * o * t + 
				(end - control1) * 3f * t * t;
		}
		public static vec2 CubicBezierDerivative(vec2 start, vec2 control0, vec2 control1, vec2 end, float t)
		{
			return new(
				CubicBezierDerivative(start.x, control0.x, control1.x, end.x, t),
				CubicBezierDerivative(start.y, control0.y, control1.y, end.y, t)
			);
		}
		public static vec3 CubicBezierDerivative(vec3 start, vec3 control0, vec3 control1, vec3 end, float t)
		{
			return new(
				CubicBezierDerivative(start.x, control0.x, control1.x, end.x, t),
				CubicBezierDerivative(start.y, control0.y, control1.y, end.y, t),
				CubicBezierDerivative(start.z, control0.z, control1.z, end.z, t)
			);
		}
		#endregion

	}
}
