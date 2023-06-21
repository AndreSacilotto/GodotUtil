using Godot;
using Util;

namespace GodotUtil;

public static class UtilPopup
{
    public static void PopupMenuAddEnum<T>(this PopupMenu popup, string separator = "separator") where T : Enum
    {
        var items = UtilEnum.EnumToString<T>();
        for (int i = 0; i < items.Length; i++)
        {
            if (UtilString.Contains(items[i], separator))
                popup.AddSeparator();
            else
                popup.AddItem(UtilString.NicifyVariableName(items[i]).Replace('_', ' '));
        }
    }
}