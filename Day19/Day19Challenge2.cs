//-----------------------------------------------------------------------
// <copyright>
// MIT License
//
// Copyright (c) 2018 Michael Weinberger lyze@owl.sh
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

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
