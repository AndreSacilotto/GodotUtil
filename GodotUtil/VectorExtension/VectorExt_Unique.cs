﻿using System.Runtime.CompilerServices;
using Util;

namespace Godot
{
    public static partial class VectorExt
    {

        #region From Godot 4.0

        /// <summary>
        /// https://github.com/godotengine/godot/blob/2d9583fa3bf2426dabbdd9e9d9b8fd9725b9436c/modules/mono/glue/GodotSharp/GodotSharp/Core/Basis.cs#L861
        /// </summary>
        public static Vector3 Mult(this Basis basis, Vector3 vector)
        {
            return new Vector3
            (
                basis.Row0[0] * vector.x + basis.Row1[0] * vector.y + basis.Row2[0] * vector.z,
                basis.Row0[1] * vector.x + basis.Row1[1] * vector.y + basis.Row2[1] * vector.z,
                basis.Row0[2] * vector.x + basis.Row1[2] * vector.y + basis.Row2[2] * vector.z
            );
        }

        #endregion

        #region Rounding

        public static Vector2i Round(float x, float y) => new(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
        public static Vector2i Floor(float x, float y) => new(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
        public static Vector2i Ceil(float x, float y) => new(Mathf.CeilToInt(x), Mathf.CeilToInt(y));

        public static Vector3i Round(float x, float y, float z) => new(Mathf.RoundToInt(x), Mathf.RoundToInt(y), Mathf.RoundToInt(z));
        public static Vector3i Floor(float x, float y, float z) => new(Mathf.FloorToInt(x), Mathf.FloorToInt(y), Mathf.FloorToInt(z));
        public static Vector3i Ceil(float x, float y, float z) => new(Mathf.CeilToInt(x), Mathf.CeilToInt(y), Mathf.FloorToInt(z));

        #endregion

        #region Invert
        /// <summary>Returns new Vector(y, x)</summary>
        public static Vector2 Invert(this Vector2 v) => new(v.y, v.x);
        /// <summary>Returns new Vector(y, x)</summary>
        public static Vector2i Invert(this Vector2i v) => new(v.y, v.x);
        #endregion

    }
}