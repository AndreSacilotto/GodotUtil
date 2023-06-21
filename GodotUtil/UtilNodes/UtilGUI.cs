using Godot;

namespace GodotUtil;

public static class UtilGUI
{
    #region Rect Math

    public static Vector2 GetSizeDelta(Control control) =>
        control.GetViewportRect().Size * new Vector2(control.AnchorRight - control.AnchorLeft, control.AnchorBottom - control.AnchorTop);

    public static Vector2 KeepInsideArea(Vector2 position, Vector2 size, Vector2 area)
    {
        if (position.Y < 0)
            position.Y = 0;
        else if (position.Y + size.Y > area.Y)
            position.Y -= position.Y + size.Y - area.Y;

        if (position.X < 0)
            position.X = 0;
        else if (position.X + size.X > area.X)
            position.X -= position.X + size.X - area.X;

        return position;
    }

    public static Vector2 GetCenterPosition(Control control) => control.Position + control.Size * 0.5f;
    public static Vector2 GetCenterGlobalPosition(Control control) => control.GlobalPosition + control.Size * 0.5f;

    #endregion

    #region Control

    // Margins in 3.X
    public static void SetAnchorOffset(this Control control, float left, float top, float right, float bottom)
    {
        control.OffsetTop = top;
        control.OffsetLeft = left;
        control.OffsetBottom = bottom;
        control.OffsetRight = right;
    }

    public static void SetAnchorPoints(this Control control, float left, float top, float right, float bottom)
    {
        control.AnchorTop = top;
        control.AnchorLeft = left;
        control.AnchorBottom = bottom;
        control.AnchorRight = right;
    }

    public static void SetSizeFlags(this Control control, Control.SizeFlags flag)
    {
        control.SizeFlagsHorizontal = flag;
        control.SizeFlagsVertical = flag;
    }

    #endregion
}