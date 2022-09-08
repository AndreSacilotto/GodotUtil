using Godot;
using System;
using System.Collections.Generic;

namespace Util
{
    public static class UtilGUI
    {
        #region Rect Math

        public static Vector2 GetSizeDelta(Control control) =>
            control.GetViewportRect().Size * new Vector2(control.AnchorRight - control.AnchorLeft, control.AnchorBottom - control.AnchorTop);

        public static Vector2 KeepInsideArea(Vector2 position, Vector2 size, Vector2 area)
        {
            if (position.y < 0)
                position.y = 0;
            else if (position.y + size.y > area.y)
                position.y -= position.y + size.y - area.y;

            if (position.x < 0)
                position.x = 0;
            else if (position.x + size.x > area.x)
                position.x -= position.x + size.x - area.x;

            return position;
        }

        public static Vector2 GetCenterRectPosition(Control control) => control.RectPosition + control.RectSize * 0.5f;
        public static Vector2 GetCenterRectGlobalPosition(Control control) => control.RectGlobalPosition + control.RectSize * 0.5f;

        #endregion

        #region Control
        public static void SetMargin(this Control control, float left, float top, float right, float bottom)
        {
            control.MarginTop = top;
            control.MarginLeft = left;
            control.MarginBottom = bottom;
            control.MarginRight = right;
        }
        public static void SetAnchor(this Control control, float left, float top, float right, float bottom)
        {
            control.AnchorTop = top;
            control.AnchorLeft = left;
            control.AnchorBottom = bottom;
            control.AnchorRight = right;
        }
        public static void SetSizeFlags(this Control control, Control.SizeFlags flag)
        {
            control.SizeFlagsHorizontal = (int)flag;
            control.SizeFlagsVertical = (int)flag;
        }

        public static List<Control> GetVisibleControlChildren(Control control, int startIdx = 0)
        {
            var len = control.GetChildCount();
            var list = new List<Control>();
            for (int i = startIdx; i < len; i++)
            {
                if (control.GetChild(i) is not Control c || !c.Visible)
                    continue;
                list.Add(c);
            }
            return list;
        }

        #endregion

        #region Tree

        public static IEnumerable<TreeItem> GetEnumerator(this TreeItem parent, bool deep = false)
        {
            var next = parent.GetChildren();
            while (next != null)
            {
                yield return next;
                if (deep)
                {
                    foreach (var b in GetEnumerator(next, deep))
                        yield return b;
                }
                next = next.GetNext();
            }
        }
        public static TreeItem ClearAndRoot(this Tree tree)
        {
            tree.Clear();
            return tree.CreateItem();
        }

        #endregion

        #region Popup

        public static void ShowPopup(this Popup popup, Vector2 position)
        {
            popup.Popup_();
            popup.RectPosition = position;
        }

        public static void ShowPopup(this Popup popup, Vector2 position, Vector2 size)
        {
            popup.Popup_(new Rect2(position, size));
        }

        public static void PopupMenuAddEnum<T>(this PopupMenu popup, string separator = "separator") where T : Enum
        {
            var items = UtilEnum<T>.EnumToString();
            for (int i = 0; i < items.Length; i++)
            {
                if (UtilString.Contains(items[i], separator))
                    popup.AddSeparator();
                else
                    popup.AddItem(UtilString.NicifyVariableName(items[i]).Replace('_', ' '));
            }
        }

        #endregion

        #region StyleBox

        public static void StyleBoxSetContentMargin(this StyleBox box, float left, float top, float right, float bottom)
        {
            box.ContentMarginTop = top;
            box.ContentMarginRight = right;
            box.ContentMarginBottom = bottom;
            box.ContentMarginLeft = left;
        }
        public static void StyleBoxSetContentMargin(this StyleBox box, float vertical, float horizontal)
        {
            box.ContentMarginTop = box.ContentMarginBottom = vertical;
            box.ContentMarginRight = box.ContentMarginLeft = horizontal;
        }
        public static void StyleBoxSetContentMargin(this StyleBox box, float value) =>
            box.ContentMarginTop = box.ContentMarginBottom = box.ContentMarginRight = box.ContentMarginLeft = value;

        #endregion

        public static IEnumerable<T> GetEnumeratorControlChildren<T>(this Control node) where T : Control
        {
            foreach (var item in node.GetChildren())
            {
                if (item is not Control c || !c.Visible)
                    continue;
                yield return (T)c;
            }
        }

    }
}