using System.Linq;
using System;
using System.Drawing;
using Utils;

namespace Day19
{
    public class Day19Challenge2 : Challenge<int>
    {
        public Day19Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            string[] input = GetInputFilePerLine();

            // find starting position

            Direction direction = Direction.DOWN;
            Point position = new Point(input[0].IndexOf('|'), 0);

            for (int steps = 0;; steps++)
            {
                char c = input[position.Y][position.X];
                switch (c)
                {
                    case '+':
                        if (input[position.Y - 1][position.X] != ' ' && direction != Direction.DOWN)
                        {
                            position.Y -= 1;
                            direction = Direction.UP;
                        }
                        else if (input[position.Y + 1][position.X] != ' ' && direction != Direction.UP)
                        {
                            position.Y += 1;
                            direction = Direction.DOWN;
                        }
                        else if (input[position.Y][position.X - 1] != ' ' && direction != Direction.RIGHT)
                        {
                            position.X -= 1;
                            direction = Direction.LEFT;
                        }
                        else if (input[position.Y][position.X + 1] != ' ' && direction != Direction.LEFT)
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
    }
}