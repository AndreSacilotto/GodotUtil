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

		/// <summary>2D Y</summary>
		public const int TOP = -1;
		/// <summary>2D Y</summary>
		public const int BOTTOM = 1;
		/// <summary>2D/3D X</summary>
		public const int LEFT = -1;
		/// <summary>2D/3D X</summary>
		public const int RIGHT = 1;

		#endregion

		#region 3D Const

		/// <summary>3D Y (FRONT)</summary>
		public const int UP = 1;
		/// <summary>3D Y (FRONT)</summary>
		public const int DOWN = -1;

		//public const int WEST = -1;
		//public const int EAST = 1;

		/// <summary>3D Z (FRONT)</summary>
		public const int FORWARD = 1;
		/// <summary>3D Z (REAR)</summary>
		public const int BACKWARD = -1;

		#endregion

		#region 2D Int Diagonals

		/// <summary>x: -1, y: -1</summary>
		public static Vector2i TopLeft => new(LEFT, TOP);
		/// <summary>x: 1, y: -1</summary>
		public static Vector2i TopRight => new(RIGHT, TOP);
		/// <summary>x: 1, y: 1</summary>
		public static Vector2i BottomRight => new(RIGHT, BOTTOM);
		/// <summary>x: -1, y: 1</summary>
		public static Vector2i BottomLeft => new(LEFT, BOTTOM);

		#endregion

		#region 3D Int Diagonals

		/// <summary>x: 1, y: -1, z: 1</summary>
		public static Vector3i RightDownFoward => new(RIGHT, DOWN, FORWARD);
		/// <summary>x: -1, y: -1, z: 1</summary>
		public static Vector3i LeftDownFoward => new(LEFT, DOWN, FORWARD);
		/// <summary>x: 1, y: -1, z: -1</summary>
		public static Vector3i RightDownBackward => new(RIGHT, DOWN, BACKWARD);
		/// <summary>x: -1, y: -1, z: -1</summary>
		public static Vector3i LeftDownBackward => new(LEFT, DOWN, BACKWARD);
		/// <summary>x: 1, y: 1, z: 1</summary>
		public static Vector3i RightUpFoward => new(RIGHT, UP, FORWARD);
		/// <summary>x: -1, y: 1, z: 1</summary>
		public static Vector3i LeftUpFoward => new(LEFT, UP, FORWARD);
		/// <summary>x: 1, y: 1, z: -1</summary>
		public static Vector3i RightUpBackward => new(RIGHT, UP, BACKWARD);
		/// <summary>x: -1, y: 1, z: -1</summary>
		public static Vector3i LeftUpBackward => new(LEFT, UP, BACKWARD);

		#endregion


	}
}
