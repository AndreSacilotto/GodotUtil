using Godot;
using Util;

namespace GodotUtil;

public static class UtilTilemap
{
    //public static Rect2 CalculateBounds(this TileMap tilemap)
    //{
    //	var cellBounds = tilemap.GetUsedRect();
    //	var cellToPixel = new Transform2D(
    //		new Vector2(tilemap.CellSize.X * tilemap.Scale.X, 0),
    //		new Vector2(0, tilemap.CellSize.Y * tilemap.Scale.Y),
    //		Vector2.Zero
    //	);
    //	return new Rect2(cellToPixel * cellBounds.Position, cellToPixel * cellBounds.Size);
    //}

    public static T[,] TilemapToArray<T>(this TileMap tilemap)
    {
        var rect = tilemap.GetUsedRect();
        int x = UtilMath.FloorToInt(rect.Size.X);
        int y = UtilMath.FloorToInt(rect.Size.Y);
        return new T[y, x];
    }


}
