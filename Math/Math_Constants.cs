using System.Runtime.CompilerServices;

namespace Util
{
    public static partial class UtilMath
    {

        #region PI

        /// <summary>Same as PI / 180, used to convert degree to radians</summary>
        public const float TAU_01 = TAU / 360f;

        /// <summary>Same as PI / 6</summary>
        public const float TAU_30 = TAU / 12f;

        /// <summary>Same as PI / 4</summary>
        public const float TAU_45 = TAU / 8f;

        /// <summary>Same as PI / 3</summary>
        public const float TAU_60 = TAU / 6f;

        /// <summary>Same as PI / 2</summary>
        public const float TAU_90 = TAU / 4f;

        /// <summary>Same as 2*PI/3</summary>
        public const float TAU_120 = TAU / 3f;

        /// <summary>Same as 3*PI/4</summary>
        public const float TAU_135 = 3f * TAU / 6f;

        /// <summary>Same as 5*PI/6</summary>
        public const float TAU_150 = 5f * TAU / 12f;

        /// <summary>Same as PI</summary>
        public const float TAU_180 = (float)System.Math.PI;

        /// <summary>Same as 3*PI/2</summary>
        public const float TAU_270 = TAU / 4f;

        /// <summary>Same as 2*PI</summary>
        public const float TAU = (float)(System.Math.PI * 2.0);

        #endregion

        public const float EPSILON_FLOAT = 1E-06F;
        public const float EPSILON_DOUBLE = 1E-12F;

    }
}