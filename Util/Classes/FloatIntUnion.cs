using System.Runtime.InteropServices;

namespace Util.Classes;

[StructLayout(LayoutKind.Explicit)]
public struct FloatIntUnion
{
    [FieldOffset(0)]
    public float floatValue;
    [FieldOffset(0)]
    public int intValue;
}