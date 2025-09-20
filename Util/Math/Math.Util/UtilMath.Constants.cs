namespace Util;

public static partial class UtilMath
{
    #region Constansts

    public const float SQTR_2 = 1.41421356237f;

    #endregion

    #region PI

    /// <summary>Same as PI / 180, used to convert degree to radians</summary>
    public const float TAU_01 = TAU_360 / 360f;

    /// <summary>Same as PI / 6</summary>
    public const float TAU_30 = TAU_360 / 12f;

    /// <summary>Same as PI / 4</summary>
    public const float TAU_45 = TAU_360 / 8f;

    /// <summary>Same as PI / 3</summary>
    public const float TAU_60 = TAU_360 / 6f;

    /// <summary>Same as PI / 2</summary>
    public const float TAU_90 = TAU_360 / 4f;

    /// <summary>Same as 2*PI/3</summary>
    public const float TAU_120 = TAU_360 / 3f;

    /// <summary>Same as 3*PI/4</summary>
    public const float TAU_135 = 3f * TAU_360 / 8f;

    /// <summary>Same as 5*PI/6</summary>
    public const float TAU_150 = 5f * TAU_360 / 12f;

    /// <summary>Same as PI</summary>
    public const float TAU_180 = MathF.PI;

    /// <summary>Same as 3*PI/2</summary>
    public const float TAU_270 = 3f * TAU_360 / 4f;

    /// <summary>Same as 2*PI</summary>
    public const float TAU_360 = MathF.PI * 2f;

    #endregion

    public const float EPSILON_FLOAT = 1E-06f;
    public const double EPSILON_DOUBLE = 1E-12f;

}