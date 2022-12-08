#if REAL_T_IS_DOUBLE
using real_t = System.Double;
#else
using real_t = System.Single;
#endif

namespace Godot
{
	public static partial class VectorExt
	{
		#region 2D Const

		public const int TOP = -1;
		public const int BOTTOM = 1;
		public const int LEFT = -1;
		public const int RIGHT = 1;

		#endregion

		#region 3D Const

		public const int NORTH = 1;
		public const int SOUTH = -1;

		public const int WEST = 1;
		public const int EAST = -1;

		#endregion


		/// <summary>x: 0, y: 0</summary>
		public static Vector2i Zero => new(0, 0);

		/// <summary>x: 0, y: -1</summary>
		public static Vector2i Top => new(0, TOP);
		/// <summary>x: 1, y: 0</summary>
		public static Vector2i Right => new(RIGHT, 0);
		/// <summary>x: 0, y: 1</summary>
		public static Vector2i Bottom => new(0, BOTTOM);
		/// <summary>x: -1, y: 0</summary>
		public static Vector2i Left => new(LEFT, 0);

		/// <summary>x: -1, y: 1</summary>
		public static Vector2i BottomLeft => new(LEFT, BOTTOM);
		/// <summary>x: 1, y: 1</summary>
		public static Vector2i BottomRight => new(RIGHT, BOTTOM);
		/// <summary>x: -1, y: -1</summary>
		public static Vector2i TopLeft => new(LEFT, TOP);
		/// <summary>x: 1, y: -1</summary>
		public static Vector2i TopRight => new(RIGHT, TOP);

	}
}
