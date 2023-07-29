namespace Util;

public static class UtilWeb
{
    public static Dictionary<string, string> HeaderToDict(string[] header)
    {
        var dict = new Dictionary<string, string>(header.Length);
        for (int i = 0; i < header.Length; i++)
        {
            var temp = header[i].ToLowerInvariant().Split(": ");
            dict.Add(temp[0], temp[1]);
        }
        return dict;
    }

    public static bool ValidUrl(string uri) =>
        Uri.TryCreate(uri, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

    public static bool SucessStatusCode(int statusCode) => 
        statusCode >= 200 && statusCode <= 299;

    public static bool IsPNG(byte[] buffer)
    {
        if (buffer.Length < 8)
            return false;

        return buffer[0] == 0x89 &&
            buffer[1] == 0x50 &&
            buffer[2] == 0x4E &&
            buffer[3] == 0x47 &&
            buffer[4] == 0x0D &&
            buffer[5] == 0x0A &&
            buffer[6] == 0x1A &&
            buffer[7] == 0x0A;
    }

    public static bool IsJPG(byte[] buffer)
    {
        if (buffer.Length < 4)
            return false;

        return buffer[0] == 0xFF && buffer[1] == 0xD8 &&
            buffer[buffer.Length - 2] == 0xFF &&
            buffer[buffer.Length - 1] == 0xD9;
    }

}