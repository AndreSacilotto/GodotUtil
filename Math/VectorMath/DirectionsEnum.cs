namespace Util.Vector
{
	[System.Flags]
	public enum Directions : byte
	{
		None = 0,
		Top = 1,
		Right = 2,
		Bottom = 4,
		Left = 8,
		TopLeft = Top | Left,
		TopRight = Top | Right,
		BottomLeft = Bottom | Left,
		BottomRight = Bottom | Right,
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

	public enum StraightDirection : byte
	{
		None = Directions.None,
		Top = Directions.Top,
		Right = Directions.Right,
		Bottom = Directions.Bottom,
		Left = Directions.Left,
	}

	public enum DiagonalDirection : byte
	{
		None = Directions.None,
		TopLeft = Directions.TopLeft,
		TopRight = Directions.TopRight,
		BottomRight = Directions.BottomRight,
		BottomLeft = Directions.BottomLeft,
	}

}