namespace Util.Directions;

[System.Flags]
public enum Directions : int
{
	None = 0,
	Top = 1 << 1,
	Right = 1 << 2,
	Bottom = 1 << 3,
	Left = 1 << 4,
	TopLeft = Top | Left,
	TopRight = Top | Right,
	BottomLeft = Bottom | Left,
	BottomRight = Bottom | Right,
	All = ~(-1 << 5),
}

public enum Direction : byte
{
	TopLeft,
	Top,
	TopRight,
	Right,
	BottomRight,
	Bottom,
	BottomLeft,
	Left,
}

public enum DirectionStraight : byte
{
	Top = Direction.Top,
	Right = Direction.Right,
	Bottom = Direction.Bottom,
	Left = Direction.Left,
}

public enum DirectionDiagonal : byte
{
	TopLeft = Direction.TopLeft,
	TopRight = Direction.TopRight,
	BottomRight = Direction.BottomRight,
	BottomLeft = Direction.BottomLeft,
}