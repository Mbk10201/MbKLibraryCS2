using System.Numerics;

namespace MBKLibrary.Extensions;

public static partial class MathExtension
{
    public static float GetDistance(CounterStrikeSharp.API.Modules.Utils.Vector v1, CounterStrikeSharp.API.Modules.Utils.Vector v2)
    {
        return Vector3.Distance(new(v1.X, v1.Y, v1.Z), new(v2.X, v2.Y, v2.Z));
    }
}
