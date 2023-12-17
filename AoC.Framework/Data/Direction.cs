namespace AoC.Framework.Data;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public static class DirectionExtensions
{
    public static Point ToPoint(this Direction direction) =>
        direction switch
        {
            Direction.Up => new Point(0, -1),
            Direction.Down => new Point(0, 1),
            Direction.Left => new Point(-1, 0),
            Direction.Right => new Point(1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

    public static Direction RotateRight90(this Direction direction) =>
        direction switch
        {
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            Direction.Up => Direction.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

    public static Direction RotateLeft90(this Direction direction) =>
        direction switch
        {
            Direction.Right => Direction.Up,
            Direction.Up => Direction.Left,
            Direction.Left => Direction.Down,
            Direction.Down => Direction.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

    public static Direction Turn(this Direction direction, DirectionInstruction instruction) =>
        instruction switch
        {
            DirectionInstruction.TurnLeft => RotateLeft90(direction),
            DirectionInstruction.TurnRight => RotateRight90(direction),
            DirectionInstruction.Forward => direction,
            DirectionInstruction.Backwards when direction == Direction.Up => Direction.Down,
            DirectionInstruction.Backwards when direction == Direction.Down => Direction.Up,
            DirectionInstruction.Backwards when direction == Direction.Left => Direction.Right,
            DirectionInstruction.Backwards when direction == Direction.Right => Direction.Left,
            _ => throw new ArgumentOutOfRangeException(nameof(instruction), instruction, null)
        };
}