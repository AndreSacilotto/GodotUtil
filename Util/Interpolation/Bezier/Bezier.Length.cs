using vec2 = System.Numerics.Vector2;
using vec3 = System.Numerics.Vector3;

namespace Util.Interpolation
{
	public static partial class Bezier
	{
		//length = sqrt(pow(x[1] - x[0], 2) + pow(y[1] - y[0], 2));

		public static float QuadBezierLengthApproximation(vec2 start, vec2 control, vec2 end)
		{
			var chord = (end - start).Length() +
				(start - control).Length() +
				(end - control).Length();
			return chord / 2;
		}
		public static float QuadBezierLengthApproximation(vec3 start, vec3 control, vec3 end)
		{
			var chord = (end - start).Length() +
				(start - control).Length() +
				(end - control).Length();
			return chord / 2;
		}

		public static float CubicBezierLengthApproximation(vec2 start, vec2 control0, vec2 control1, vec2 end)
		{
			var chord = (end - start).Length() +
				(start - control0).Length() +
				(control1 - control0).Length() +
				(end - control1).Length();
			return chord / 2;
		}
		public static float CubicBezierLengthApproximation(vec3 start, vec3 control0, vec3 control1, vec3 end)
		{
			var chord = (end - start).Length() +
				(start - control0).Length() +
				(control1 - control0).Length() +
				(end - control1).Length();
			return chord / 2;
		}


	}
}