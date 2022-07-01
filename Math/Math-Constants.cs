using System.Runtime.CompilerServices;

namespace Util
{

    public static partial class MathUtil
    {
        public const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

        #region PI

        /// <summary>Same as PI / 4</summary>
        public const float TAU_45 = 0.785398f;

        /// <summary>Same as PI / 2</summary>
        public const float TAU_90 = 1.570796f;

        public const float TAU_01 = 0.017453f;
        public const float TAU_270 = 4.712388f;

        #endregion

        public const float EPSILON_FLOAT = 1E-06F;
        public const float EPSILON_DOUBLE = 1E-12F;

    }
}