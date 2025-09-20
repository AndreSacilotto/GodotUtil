namespace Util.Classes;

public readonly struct Power2 : IFormattable, IConvertible, IComparable<Power2>
{
    public static Power2 NewPower2(int exponent) => new((uint)(1 << exponent));

    private readonly uint value;

    private Power2(uint v)
    {
        value = v;
    }
    private Power2(int v)
    {
        v--;
        v |= v >> 1;
        v |= v >> 2;
        v |= v >> 4;
        v |= v >> 8;
        v |= v >> 16;
        v++;
        value = (uint)v;
    }

    public int IntValue => (int)value;

    public int GetExponent() => MathExtra.MathOptimization.IntLog2(IntValue);

    public readonly bool IsZero => value == 0;

    public readonly int CompareTo(Power2 other) => value.CompareTo(other.value);
    public readonly bool Equals(Power2 other) => value.Equals(other.value);

    public override readonly string ToString() => value.ToString();
    public readonly string ToString(IFormatProvider? provider) => value.ToString(provider);
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => value.ToString(format, formatProvider);

    public readonly TypeCode GetTypeCode() => value.GetTypeCode();

    #region IConvertible

    public bool ToBoolean(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToBoolean(provider);
    }

    public byte ToByte(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToByte(provider);
    }

    public char ToChar(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToChar(provider);
    }

    public DateTime ToDateTime(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToDateTime(provider);
    }

    public decimal ToDecimal(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToDecimal(provider);
    }

    public double ToDouble(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToDouble(provider);
    }

    public short ToInt16(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToInt16(provider);
    }

    public int ToInt32(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToInt32(provider);
    }

    public long ToInt64(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToInt64(provider);
    }

    public sbyte ToSByte(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToSByte(provider);
    }

    public float ToSingle(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToSingle(provider);
    }

    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        return ((IConvertible)value).ToType(conversionType, provider);
    }

    public ushort ToUInt16(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToUInt16(provider);
    }

    public uint ToUInt32(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToUInt32(provider);
    }

    public ulong ToUInt64(IFormatProvider? provider)
    {
        return ((IConvertible)value).ToUInt64(provider);
    }

    #endregion

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }


    public static explicit operator Power2(int v) => new(v);
    public static explicit operator Power2(uint v) => new(v);

    public static implicit operator int(Power2 p) => p.IntValue;
    public static implicit operator uint(Power2 p) => p.value;

    public static bool operator <(Power2 left, Power2 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(Power2 left, Power2 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(Power2 left, Power2 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(Power2 left, Power2 right)
    {
        return left.CompareTo(right) >= 0;
    }

}
