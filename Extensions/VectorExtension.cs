using CounterStrikeSharp.API.Modules.Utils;

namespace MBKLibrary.Extensions;

public static class VectorExtension
{
    /*public static (float X, float Y, float Z) ParseFromString(string input)
    {
        // Split the input string into components
        var parts = input.Split(' ');
        if (parts.Length != 3)
            throw new FormatException("Input string must contain exactly three space-separated values.");

        // Convert each part back to a float
        float x = float.Parse(parts[0], System.Globalization.NumberStyles.Float);
        float y = float.Parse(parts[1], System.Globalization.NumberStyles.Float);
        float z = float.Parse(parts[2], System.Globalization.NumberStyles.Float);

        return (x, y, z);
    }*/

    public static Vector ParseFromString(string input)
    {
        // Split the input string into components
        var parts = input.Split(' ');
        if (parts.Length != 3)
            throw new FormatException("Input string must contain exactly three space-separated values.");

        // Convert each part back to a float
        float x = float.Parse(parts[0], System.Globalization.NumberStyles.Float);
        float y = float.Parse(parts[1], System.Globalization.NumberStyles.Float);
        float z = float.Parse(parts[2], System.Globalization.NumberStyles.Float);

        return new Vector(x, y, z);
    }
}
