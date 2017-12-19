using System.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using Utils;

namespace Day19
{
    public class Day19Challenge1 : Challenge<string>
    {
        public Day19Challenge1() : base("input")
        {
        }

        public override string Run()
        {
            string[] input = GetInputFilePerLine();

            // find starting position

            Direction direction = Direction.DOWN;
            Point position = new Point(input[0].IndexOf('|'), 0);

            string visited = "";

            try
            {
                while (true)
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
    }
}