
using System;
using Godot;
using Util.Vector;

namespace Util
{
	public static class PolygonHelper
	{
		#region Mesh Util

		public static void Generate(this SurfaceTool st, bool generateNormals, bool generateTangents, bool generateIndex)
		{
			if (generateNormals)
				st.GenerateNormals();
			if (generateTangents)
				st.GenerateTangents();
			if (generateIndex)
				st.Index();
		}

		#endregion

		#region Internal

		private const float SAFE_THRESHOLD = 0.0001f;

		private static Vector2[] ErrorReturn2D()
		{
			return Array.Empty<Vector2>();
			//return null;
		}

		//private static Vector3[] ErrorReturn3D()
		//{
		//    return Array.Empty<Vector3>();
		//    //return null;
		//}

		#endregion

		/// <summary>GODOT uses CLOCKWISE for Geometry and Rotations</summary>
		public const bool IS_CLOCKWISE_CW = true;
		public const bool IS_COUNTER_CLOCKWISE_CC = false;

		public const int DEFAULT_DENSITY = 10;

		public static Vector2[] AppendLast(Vector2[] points)
		{
			var arr = new Vector2[points.Length + 1];
			points.CopyTo(arr, 0);
			arr[points.Length] = points[0];
			return arr;
		}

		#region Create 2D - Linear

		/// <summary> https://math.stackexchange.com/a/1344707 </summary>
		/// <param name="radius">Radius/Size - Negative value flip the polygon</param>
		public static Vector2[] TriangleEquilateral(float radius)
		{
			var t = MathF.Tan(UtilMath.TAU_30) * radius;
			return new Vector2[3] {
				new Vector2(0f, t * -2f),
				new Vector2(radius, t),
				new Vector2(-radius, t),
			};
		}

		public static Vector2[] Square(float size)
		{
			var rn = -size;
			return new Vector2[4] {
				new Vector2(rn, rn),
				new Vector2(size, rn),
				new Vector2(size, size),
				new Vector2(rn, size),
			};
		}

		public static Vector2[] Retangle(float width, float height)
		{
			return new Vector2[4] {
				new Vector2(-width, -height),
				new Vector2(width, -height),
				new Vector2(width, height),
				new Vector2(-width, height),
			};
		}

		public static Vector2[] Rhombus(float width, float height)
		{
			return new Vector2[4] {
				new Vector2(0f, -height),
				new Vector2(width, 0f),
				new Vector2(0f, height),
				new Vector2(-width, 0f),
			};
		}

		/// <param name="near">Near - bottom width</param>
		/// <param name="far">Far - top width</param>
		public static Vector2[] Trapezoid(float near, float far, float height)
		{
			return new Vector2[4] {
				new Vector2(-far, -height),
				new Vector2(far, -height),
				new Vector2(near, height),
				new Vector2(-near, height),
			};
		}

		public static Vector2[] Hexagon(float size, bool rotated = false)
		{
			if (rotated)
			{
				var vec = Vector2.Left.Rotated(UtilMath.TAU_60) * size;
				return new Vector2[6] {
					new Vector2(-size, 0f),
					vec,
					vec.NegX(),
					new Vector2(size, 0f),
					-vec,
					vec.NegY(),
				};
			}
			else
			{
				var vec = Vector2.Up.Rotated(UtilMath.TAU_60) * size;
				return new Vector2[6] {
					vec,
					new Vector2(0f, -size),
					vec.NegX(),
					-vec,
					new Vector2(0f, size),
					vec.NegY(),
				};
			}
		}

		/// <param name="hSize">Horizontal Size</param>
		/// <param name="vSize">Vertical Size</param>
		/// <param name="hThickness">Thickness of the Horizontal Line</param>
		/// <param name="vThickness">Thickness of the Vertical Line</param>
		/// <param name="crossPos01">Position of the interscetion in percentage (0f..1f)</param>
		public static Vector2[] HolyCross2D(float hSize, float vSize, float hThickness, float vThickness, float crossPos01 = 0.75f)
		{
			var c = (vSize - hThickness - SAFE_THRESHOLD) * (2f * crossPos01 - 1f);

			var ctp = c + hThickness;
			var ctn = c - hThickness;

			return new Vector2[12]
			{
				new Vector2(-hSize, ctn), //0
                new Vector2(-vThickness, ctn), //1
                new Vector2(-vThickness, -vSize), //2
                new Vector2(vThickness, -vSize), //3

                new Vector2(vThickness, ctn), //4
                new Vector2(hSize, ctn), //5
                new Vector2(hSize, ctp), //6
                new Vector2(vThickness, ctp), //7

                new Vector2(vThickness, vSize), //8
                new Vector2(-vThickness, vSize), //9
                new Vector2(-vThickness, ctp), //10
                new Vector2(-hSize, ctp), //11
            };
		}


		#endregion

		#region Create 2D - Circle

		public static Vector2[] Polygon2D(float size, int sides = 3) => Circle(size, sides);

		/// <param name="radius">Radius/Size - Negative value flip the polygon</param>
		/// <param name="density">Number of sides of the primivite polygon</param>
		public static Vector2[] Circle(float radius, int density = DEFAULT_DENSITY)
		{
			if (density < 2)
				return ErrorReturn2D();

			var points = new Vector2[density];

			var rad = MathF.Tau / density;
			var s = MathF.Sin(rad);
			var c = MathF.Cos(rad);

			var v = Vector2.Left;
			for (int i = 0; i < density; i++)
			{
				points[i] = v * radius;
				v = VectorMath.RotatedNoTrigCW(v, c, s);
			}

			return points;
		}

		/// <summary> https://stackoverflow.com/a/34735255 </summary>
		/// <param name="width">Radius/Size</param>
		/// <param name="height">Radius/Size</param>
		/// <param name="density">Number of sides of the primivite polygon</param>
		public static Vector2[] Ellipse(float width, float height, int density = DEFAULT_DENSITY)
		{
			if (density < 2)
				return ErrorReturn2D();

			var points = new Vector2[density];

			var rad = MathF.Tau / density;
			var s = MathF.Sin(rad);
			var c = MathF.Cos(rad);

			var v = Vector2.Left;
			for (int i = 0; i < density; i++)
			{
				points[i] = new(v.x * width, v.y * height);
				v = VectorMath.RotatedNoTrigCW(v, c, s);
			}

			return points;
		}

		/// <param name="radius">Radius/Size - Negative value flip the polygon</param>
		/// <param name="sides">Number of points used to draw it</param>
		public static Vector2[] SemiCircle(float radius, int density = DEFAULT_DENSITY)
		{
			if (density < 2)
				return ErrorReturn2D();

			var points = new Vector2[density];

			var rad = UtilMath.TAU_180 / (density - 1);
			var s = MathF.Sin(rad);
			var c = MathF.Cos(rad);

			var v = Vector2.Left;
			for (int i = 0; i < density; i++)
			{
				points[i] = new(v.x * radius, (v.y + 0.5f) * radius);
				v = VectorMath.RotatedNoTrigCW(v, c, s);
			}

			return points;
		}

		/// <param name="inner">Radius of end circle</param>
		/// <param name="outter">Radius of far circle</param>
		/// <param name="density">Number of points on ONE circle</param>
		public static Vector2[] Donut(float inner, float outter, int density = DEFAULT_DENSITY)
		{
			if (inner <= 0f || inner >= outter)
				return ErrorReturn2D();

			var points = new Vector2[density * 2];

			var rad = MathF.Tau / (density - 1);
			var s = MathF.Sin(rad);
			var c = MathF.Cos(rad);

			var v = Vector2.Left;
			for (int i = 0, j = density; i < density; i++, j++)
			{
				points[i] = v * inner;
				points[j] = v.NegY() * outter;
				v = VectorMath.RotatedNoTrigCW(v, c, s);
			}

			var safe = new Vector2(0f, SAFE_THRESHOLD);
			points[0] -= safe;
			points[points.Length - 1] -= safe;

			return points;
		}

		#endregion

		#region Create 2D - Arc

		/// <param name="arc">How much of tau the cone covers - 0..TAU</param>
		/// <param name="density">Number of point on the arc</param>
		public static Vector2[] Cone(float width, float height, float arc, int density = DEFAULT_DENSITY)
		{
			if (width < 0f || height < 0f || arc < 0f)
				return ErrorReturn2D();

			var points = new Vector2[1 + density];

			var rad = arc / (density - 1);
			var s = MathF.Sin(rad);
			var c = MathF.Cos(rad);

			var v = Vector2.Up.RotatedCC(arc * 0.5f);
			for (int i = 0; i < density; i++)
			{
				points[i] = new Vector2(v.x * width, v.y * height);
				v = VectorMath.RotatedNoTrigCW(v, c, s);
			}

			points[points.Length - 1] = new Vector2(0f, height);

			return points;
		}

		/// <param name="curvature">How much of the height the curve covers - 0..1</param>
		/// <param name="density">Number of points used  on the arc</param>
		public static Vector2[] Bullet(float width, float height, float curvature, int density = DEFAULT_DENSITY)
		{
			if (width < 0f || height < 0f)
				return ErrorReturn2D();
			curvature = Mathf.Clamp(curvature, 0f, 1f);

			var points = new Vector2[2 + density];

			var rad = UtilMath.TAU_180 / (density - 1);
			var s = MathF.Sin(rad);
			var c = MathF.Cos(rad);

			var h1 = height * curvature;
			var h2 = height * (1f - curvature);

			var v = Vector2.Left;
			for (int i = 0; i < density; i++)
			{
				points[i] = new(v.x * width, v.y * h1 - h2);
				v = VectorMath.RotatedNoTrigCW(v, c, s);
			}

			points[points.Length - 2] = new(width, height);
			points[points.Length - 1] = new(-width, height);

			return points;
		}

		/// <param name="curvature">How much of the height the curve covers - 0..1</param>
		public static Vector2[] Capsule(float width, float height, float curvature, int density = DEFAULT_DENSITY)
		{
			if (width < 0f || height < 0f)
				return ErrorReturn2D();
			curvature = Mathf.Clamp(curvature, 0f, 1f - SAFE_THRESHOLD);

			var points = new Vector2[density * 2];

			var rad = UtilMath.TAU_180 / (density - 1);
			var s = MathF.Sin(rad);
			var c = MathF.Cos(rad);

			var h1 = height * curvature;
			var h2 = height * (1f - curvature);

			var v = Vector2.Left;
			for (int i = 0, j = density; i < density; i++, j++)
			{
				var p = new Vector2(v.x * width, v.y * h1 - h2);
				points[i] = -p;
				points[j] = p;
				v = VectorMath.RotatedNoTrigCW(v, c, s);
			}
			return points;
		}

		/// <param name="curvature">How much of the height the curve covers - 0..1</param>
		/// <param name="density">Number of point on the arc</param>
		public static Vector2[] Curve2D(float width, float height, float curvature, int density = DEFAULT_DENSITY) => DoubleArc(width, height, curvature, curvature, density);

		/// <param name="upperCur">How much of the height the upper curve covers - 0..1</param>
		/// <param name="lowerCur">How much of the height the lower curve covers - 0..1</param>
		public static Vector2[] DoubleArc(float width, float height, float upperCur, float lowerCur, int density = DEFAULT_DENSITY)
		{
			if (width < 0f || height < 0f)
				return ErrorReturn2D();
			upperCur = Mathf.Clamp(upperCur, 0f, 1f - SAFE_THRESHOLD);
			lowerCur = Mathf.Clamp(lowerCur, 0f, 1f - SAFE_THRESHOLD);

			var points = new Vector2[density * 2];

			var rad = UtilMath.TAU_180 / (density - 1);
			var s = MathF.Sin(rad);
			var c = MathF.Cos(rad);

			var hU1 = height * upperCur;
			var hU2 = height * (1f - upperCur);

			var hL1 = height * lowerCur;
			//TODO

			var v = Vector2.Left;
			for (int i = 0, j = density; i < density; i++, j++)
			{
				var w = v.x * width;
				points[i] = new(w, v.y * hU1 - hU2);
				points[j] = new(-w, v.y * hL1 + height);
				v = VectorMath.RotatedNoTrigCW(v, c, s);
			}

			return points;
		}

		#endregion

		#region 3D

		#endregion


	}
}