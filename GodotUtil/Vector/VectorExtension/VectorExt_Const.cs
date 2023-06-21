#if REAL_T_IS_DOUBLE
using real_t = System.Double;
#else
#endif

namespace Godot;

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
    public static Vector2I TopLeft => new(LEFT, TOP);
    /// <summary>x: 1, y: -1</summary>
    public static Vector2I TopRight => new(RIGHT, TOP);
    /// <summary>x: 1, y: 1</summary>
    public static Vector2I BottomRight => new(RIGHT, BOTTOM);
    /// <summary>x: -1, y: 1</summary>
    public static Vector2I BottomLeft => new(LEFT, BOTTOM);

    #endregion

    #region 3D Int Diagonals

    /// <summary>x: 1, y: -1, z: 1</summary>
    public static Vector3I RightDownFoward => new(RIGHT, DOWN, FORWARD);
    /// <summary>x: -1, y: -1, z: 1</summary>
    public static Vector3I LeftDownFoward => new(LEFT, DOWN, FORWARD);
    /// <summary>x: 1, y: -1, z: -1</summary>
    public static Vector3I RightDownBackward => new(RIGHT, DOWN, BACKWARD);
    /// <summary>x: -1, y: -1, z: -1</summary>
    public static Vector3I LeftDownBackward => new(LEFT, DOWN, BACKWARD);
    /// <summary>x: 1, y: 1, z: 1</summary>
    public static Vector3I RightUpFoward => new(RIGHT, UP, FORWARD);
    /// <summary>x: -1, y: 1, z: 1</summary>
    public static Vector3I LeftUpFoward => new(LEFT, UP, FORWARD);
    /// <summary>x: 1, y: 1, z: -1</summary>
    public static Vector3I RightUpBackward => new(RIGHT, UP, BACKWARD);
    /// <summary>x: -1, y: 1, z: -1</summary>
    public static Vector3I LeftUpBackward => new(LEFT, UP, BACKWARD);

    #endregion


}
