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

using System;
using System.Collections.Generic;
using System.Drawing;
using Utils;

namespace Day3
{
    public class Day3Challenge2 : Challenge<int>
    {
        enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        public Day3Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            Dictionary<Point, int> grid = new Dictionary<Point, int>();
            grid[new Point(0, 0)] = 1;

            int input = Convert.ToInt32(GetInputFile());


            Direction direction = Direction.Right;
            int x = 0;
            int y = 0;

            bool init = false;
            while (true)
            {
                var point = new Point(x, y);


                int val = 0;

                if (!init)
                    init = true;
                else
                    switch (direction)
                    {
                        case Direction.Right:
                            if (!grid.ContainsKey(new Point(x, y + 1)))
                            {
                                direction = Direction.Up;
                            }
                            break;
                        case Direction.Left:
                            if (!grid.ContainsKey(new Point(x, y - 1)))
                            {
                                direction = Direction.Down;
                            }
                            break;
                        case Direction.Up:
                            if (!grid.ContainsKey(new Point(x - 1, y)))
                            {
                                direction = Direction.Left;
                            }
                            break;
                        case Direction.Down:
                            if (!grid.ContainsKey(new Point(x + 1, y)))
                            {
                                direction = Direction.Right;
                            }
                            break;
                    }


                if (grid.ContainsKey(new Point(x - 1, y - 1)))
                {
                    val += grid[new Point(x - 1, y - 1)];
                }

                if (grid.ContainsKey(new Point(x, y - 1)))
                {
                    val += grid[new Point(x, y - 1)];
                }

                if (grid.ContainsKey(new Point(x + 1, y - 1)))
                {
                    val += grid[new Point(x + 1, y - 1)];
                }


                if (grid.ContainsKey(new Point(x - 1, y)))
                {
                    val += grid[new Point(x - 1, y)];
                }

                if (grid.ContainsKey(new Point(x, y)))
                {
                    val += grid[new Point(x, y)];
                }

                if (grid.ContainsKey(new Point(x + 1, y)))
                {
                    val += grid[new Point(x + 1, y)];
                }


                if (grid.ContainsKey(new Point(x - 1, y + 1)))
                {
                    val += grid[new Point(x - 1, y + 1)];
                }

                if (grid.ContainsKey(new Point(x, y + 1)))
                {
                    val += grid[new Point(x, y + 1)];
                }

                if (grid.ContainsKey(new Point(x + 1, y + 1)))
                {
                    val += grid[new Point(x + 1, y + 1)];
                }

                grid[point] = val;
                Console.WriteLine(point + ": " + val + " (" + grid.Count + ")");

                if (val > input)
                {
                    return val;
                }


                switch (direction)
                {
                    case Direction.Right:
                        x++;
                        break;
                    case Direction.Left:
                        x--;
                        break;
                    case Direction.Up:
                        y++;
                        break;
                    case Direction.Down:
                        y--;
                        break;
                }
            }
        }
    }
}
