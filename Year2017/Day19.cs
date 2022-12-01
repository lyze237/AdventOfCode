using System.Drawing;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day19 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        // find starting position

        var direction = Direction.DOWN;
        var position = new Point(Input[0].IndexOf('|'), 0);

        var visited = "";

        try
        {
            while (true)
            {
                var c = Input[position.Y][position.X];
                switch (c)
                {
                    case '+':
                        if (Input[position.Y - 1][position.X] != ' ' && direction != Direction.DOWN)
                        {
                            position.Y -= 1;
                            direction = Direction.UP;
                        }
                        else if (Input[position.Y + 1][position.X] != ' ' && direction != Direction.UP)
                        {
                            position.Y += 1;
                            direction = Direction.DOWN;
                        }
                        else if (Input[position.Y][position.X - 1] != ' ' && direction != Direction.RIGHT)
                        {
                            position.X -= 1;
                            direction = Direction.LEFT;
                        }
                        else if (Input[position.Y][position.X + 1] != ' ' && direction != Direction.LEFT)
                        {
                            position.X += 1;
                            direction = Direction.RIGHT;
                        }

                        break;
                    case ' ':
                        Console.WriteLine($"Found end at {position}, stopping");
                        return visited;
                    default:
                        if (c >= 'A' && c <= 'Z')
                        {
                            visited += c;
                        }

                        Console.WriteLine($"Moving {direction}");
                        switch (direction)
                        {
                            case Direction.DOWN:
                                position.Y += 1;
                                break;
                            case Direction.UP:
                                position.Y -= 1;
                                break;
                            case Direction.LEFT:
                                position.X -= 1;
                                break;
                            case Direction.RIGHT:
                                position.X += 1;
                                break;
                        }

                        break;
                }
            }
        }
        catch (IndexOutOfRangeException)
        {
            return visited;
        }
    }

    public override object ExecutePart2()
    {
        var direction = Direction.DOWN;
        var position = new Point(Input[0].IndexOf('|'), 0);

        for (var steps = 0;; steps++)
        {
            var c = Input[position.Y][position.X];
            switch (c)
            {
                case '+':
                    if (Input[position.Y - 1][position.X] != ' ' && direction != Direction.DOWN)
                    {
                        position.Y -= 1;
                        direction = Direction.UP;
                    }
                    else if (Input[position.Y + 1][position.X] != ' ' && direction != Direction.UP)
                    {
                        position.Y += 1;
                        direction = Direction.DOWN;
                    }
                    else if (Input[position.Y][position.X - 1] != ' ' && direction != Direction.RIGHT)
                    {
                        position.X -= 1;
                        direction = Direction.LEFT;
                    }
                    else if (Input[position.Y][position.X + 1] != ' ' && direction != Direction.LEFT)
                    {
                        position.X += 1;
                        direction = Direction.RIGHT;
                    }

                    break;
                case ' ':
                    Console.WriteLine($"Found end at {position}, moved {steps} steps, stopping");
                    return steps;
                default:
                    Console.WriteLine($"Moving {direction}");
                    switch (direction)
                    {
                        case Direction.DOWN:
                            position.Y += 1;
                            break;
                        case Direction.UP:
                            position.Y -= 1;
                            break;
                        case Direction.LEFT:
                            position.X -= 1;
                            break;
                        case Direction.RIGHT:
                            position.X += 1;
                            break;
                    }

                    break;
            }
        }
    }

    private enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
}