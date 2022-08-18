namespace Day15;

public struct Vector2Int
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2Int(float x, float y)
    {
        X = (int)x;
        Y = (int)y;
    }

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"X:{X} Y:{Y}";
    }
}