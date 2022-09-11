using System.Collections.Generic;

namespace Godot
{
    public static partial class UtilGD
    {
        public static bool IsNullPrint(object value)
        {
            var isnull = value == null;
            if (isnull)
                GD.Print("null");
            return isnull;
        }

        public static void PrintCollection<TK, TV>(IEnumerable<KeyValuePair<TK, TV>> ie, string separator = " ")
        {
            string str = "";
            foreach (var item in ie)
                str += $"({item.Key}, {item.Value}){separator}";
            GD.Print(str);
        }

        public static void PrintCollection<T>(IEnumerable<T> ie, string separator = " ")
        {
            string str = "";
            foreach (var item in ie)
                str += item.ToString() + separator;
            str.Remove(str.Length - separator.Length);
            GD.Print(str);
        }

        public static void PrintTree(SceneTree tree, bool pretty = false) 
        {
            if (pretty)
                tree.Root.PrintTree();
            else
                tree.Root.PrintTreePretty();
        }

    }
}
