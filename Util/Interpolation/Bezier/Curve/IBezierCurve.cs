
using System.Numerics;

namespace Util.Interpolation;

internal interface IBezierCurve<T> where T : INumber<T>
{
    T Sample(T t);
}
