using System.Numerics;

namespace Day05;

public static class Extension
{
    public static Vector2 NormalizedClamp(this Vector2 vector)
    {
        var normalized = Vector2.Normalize(vector);
        var normalizedClamp = new Vector2(
            normalized.X < 0 ? -1 : normalized.X > 0 ? 1 : 0,
            normalized.Y < 0 ? -1 : normalized.Y > 0 ? 1 : 0);

        return normalizedClamp;
    }
}