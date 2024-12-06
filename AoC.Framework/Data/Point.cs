namespace AoC.Framework.Data;

public record Point(long X, long Y)
{
    public double Length => Math.Sqrt(Length2);
    public double Length2 => X * X + Y * Y;

    public long ManhattanDistance(Point other) => Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
    public long Dot(Point other) => X * other.X + Y * other.Y;

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, Point b) => new(a.X * b.X, a.Y * b.Y);

    public static Point operator -(Point a) => new(-a.X, -a.Y);

    public static Point operator +(Point a, long b) => new(a.X + b, a.Y + b);
    public static Point operator -(Point a, long b) => new(a.X - b, a.Y - b);
    public static Point operator *(Point a, long b) => new(a.X * b, a.Y * b);

    public static Point operator ++(Point a) => a + 1;
    public static Point operator --(Point a) => a - 1;

    public Point Move(Direction direction) => this + direction.ToPoint();
    public Point Move(Direction direction, long amount) => this + (direction.ToPoint() * amount);
    public Point Move(Point direction) => this + direction;

    public bool InRectangle<T>(T[][] rectangle) => X >= 0 && Y >= 0 && X < rectangle[0].Length && Y < rectangle.Length;

    public T Get<T>(T[][] arr) => arr[Y][X];
}