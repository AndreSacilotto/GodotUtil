using System;

using Vector2 = Godot.Vector2;

namespace Util.MathCrazy
{
	public static class Heuristics
	{
		public enum Heuristic
		{
			Manhattan,
			Chebyshev,
			Euclidean,
			EuclideanSquared,
			CosineSimilarity,
			Minkowski,
		}

		/// <summary> 
		/// Cells distance number in a grid <br/> 
		/// Use when diagonal move is not allowed 
		/// </summary>
		public static int Manhattan(int x0, int x1, int y0, int y1)
		{
			return Math.Abs(x0 - x1) + Math.Abs(y0 - y1);
		}

		/// <summary> 
		/// Cells distance number in a grid <br/> 
		/// Use when diagonal move is allowed 
		/// </summary>
		public static int Chebyshev(int x0, int x1, int y0, int y1)
		{
			int diffX = Math.Abs(x1 - x0);
			int diffY = Math.Abs(y1 - y0);
			return Math.Max(diffX, diffY);
		}

		/// <summary> 
		/// Cells distance number in a grid <br/> 
		/// Use when diagonal move is allowed 
		/// </summary>
		public static float Octile(int x0, int x1, int y0, int y1)
		{
			int diffX = Math.Abs(x1 - x0);
			int diffY = Math.Abs(y1 - y0);

			return Math.Max(diffX, diffY) + Godot.Mathf.Sqrt2 * Math.Min(diffX, diffY);
		}

		/// <summary> 
		/// Generic funtion of Chebyshev and Octile <br/> 
		/// if sc = dc = 1 Chebyshev <br/> 
		/// if sc = 1 e dc = sqtr2 Octile 
		/// </summary>
		public static float DiagonalDistance(int x0, int x1, int y0, int y1, int straightCost, int diagonalCost)
		{
			int diffX = Math.Abs(x1 - x0);
			int diffY = Math.Abs(y1 - y0);

			return straightCost * Math.Max(diffX, diffY) + (diagonalCost - straightCost) * Math.Min(diffX, diffY);
		}


		#region Euclidean
		/// <summary> Absolute distance in a straight line </summary>
		public static float Euclidean(int x0, int x1, int y0, int y1)
		{
			int diffX = Math.Abs(x1 - x0);
			int diffY = Math.Abs(y1 - y0);
			return MathF.Sqrt(diffX * diffX + diffY * diffY);
		}


		/// <summary> 
		/// Absolute distance in a straight line - 
		/// Only can be used with comparasion with itself 
		/// </summary>
		public static int EuclideanSquared(int x0, int x1, int y0, int y1)
		{
			int diffX = Math.Abs(x1 - x0);
			int diffY = Math.Abs(y1 - y0);
			return diffX * diffX + diffY * diffY;
		}

		#endregion

		/// <summary>Using cosine to find distance between two points </summary>
		public static float CosineSimilarity(int x0, int x1, int y0, int y1)
		{
			var v0 = new Vector2(x0, y0);
			var v1 = new Vector2(x1, y1);
			return v0.Dot(v1) / (v0.Length() * v1.Length());
		}

		/// <summary> 
		/// Dont use inefficient <br/> 
		/// if p=1 Euclidian <br/> 
		/// if p=2 Manhattan 
		/// </summary>
		public static float Minkowski(int x0, int x1, int y0, int y1, int p)
		{
			int diffX = Math.Abs(x1 - x0);
			int diffY = Math.Abs(y1 - y0);
			return MathF.Pow(MathF.Pow(diffX, p) + MathF.Pow(diffY, p), 1f / p);
		}

	}

}
