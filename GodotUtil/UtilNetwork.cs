using Godot;

namespace GodotUtil;

public static class UtilNetwork
{
    public const int MAX_PORT = 65535;
    public const int SERVER_ID = 1;

    public static void SeparateFullAddress(string fullAddress, out string address, out int port, char separator = ':')
    {
        var s = fullAddress.Trim().Split(separator);
        address = s[1];
        port = s[2].ToInt();
    }

    public static string MakeFullAddress(string address, int port, char separator = ':')
    {
        return address + separator + port;
    }

    public static void OpenTerminals(int count, string scene = "")
    {
        var godotPath = OS.GetExecutablePath();
        var projectPath = ProjectSettings.GlobalizePath("res://");
        if (string.IsNullOrWhiteSpace(scene))
            scene = (string)ProjectSettings.GetSetting("application/run/main_scene");
        OpenTerminals(count, godotPath, projectPath, scene);
    }

    public static string GenerateObjectName(string prefix, int id) => prefix + '_' + id;
    public static string GenerateObjectName(string prefix, int id, int number) => prefix + '_' + id + '_' + number;

    public static void OpenTerminals(int count, string godot, string project, string scene)
    {
        var args = new string[] { "--path", project, scene };
        for (int i = 0; i < count; i++)
            OS.Execute(godot, args, null, false, false);
    }

}

