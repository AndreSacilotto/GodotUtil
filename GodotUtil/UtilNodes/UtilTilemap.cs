using Godot;

namespace Util.GDNode
{
	public static class UtilTilemap
	{
		public static Rect2 CalculateBounds(this TileMap tilemap)
		{
			var cellBounds = tilemap.GetUsedRect();
			var cellToPixel = new Transform2D(
				new Vector2(tilemap.CellSize.x * tilemap.Scale.x, 0),
				new Vector2(0, tilemap.CellSize.y * tilemap.Scale.y),
				Vector2.Zero
			);
			return new Rect2(cellToPixel * cellBounds.Position, cellToPixel * cellBounds.Size);
		}

		public static T[,] TilemapToArray<T>(this TileMap tilemap)
		{
			var rect = tilemap.GetUsedRect();
			int x = UtilMath.FloorToInt(rect.Size.x);
			int y = UtilMath.FloorToInt(rect.Size.y);
			return new T[y, x];
		}

		public static int[,] TilemapToGetCellsArray(this TileMap tilemap)
		{
			var rect = tilemap.GetUsedRect();
			var pos = (Vector2i)rect.Position;
			var size = (Vector2i)rect.Size;

			var arr = new int[size.y, size.x];
			for (int r = 0; r < size.y; r++)
				for (int c = 0; c < size.x; c++)
					arr[r, c] = tilemap.GetCell(c + pos.y, r + pos.x);
			return arr;
		}

		public static Rect2 GetTileRect(this TileMap tilemap, int tileId) =>
			tilemap.TileSet.TileGetRegion(tileId);
		public static Shape2D GetTileShape(this TileMap tilemap, int tileId) =>
			tilemap.TileSet.TileGetShape(tileId, 0);

		public static bool TileHasCollider(this TileMap tilemap, int tileId) =>
			tilemap.TileSet.TileGetShapeCount(tileId) > 0;

		public static void SetCollider(this TileSet ts, int id, bool oneWay = false, float oneWayf = 1f)
		{
			ts.TileSetShapeOneWay(id, 0, oneWay);
			ts.TileSetShapeOneWayMargin(id, 0, oneWayf);
		}

		public static void SetTileOffset(this TileSet ts, int id, Vector2 offset)
		{
			ts.TileSetTextureOffset(id, offset);
			ts.TileSetShapeOffset(id, 0, offset);
			ts.TileSetOccluderOffset(id, offset);
		}

	}
}
