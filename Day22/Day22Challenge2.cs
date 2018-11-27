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
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Utils;

namespace Day22
{
    public class Day22Challenge2 : Challenge<int>
    {
        public Day22Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            NodeState[][] inputArray = GetInputFilePerLine()
                .Select(s => s.Select(c => c == '#' ? NodeState.INFECTED : NodeState.CLEAN).ToArray()).ToArray();
            Dictionary<Point, NodeState> input = new Dictionary<Point, NodeState>();

            // fill dictionary
            for (var y = 0; y < inputArray.Length; y++)
            {
                for (var x = 0; x < inputArray[y].Length; x++)
                {
                    input.Add(new Point(x, y), inputArray[y][x]);
                }
            }

            Array directionsArray = Enum.GetValues(typeof(Direction));
            Direction direction = Direction.UP;
            Point position = new Point(inputArray[0].Length / 2, inputArray.Length / 2);

            int infected = 0;
            for (int generaLIndex = 0; generaLIndex < 10_000_000; generaLIndex++)
            {
                if (!input.ContainsKey(position))
                {
                    input.Add(new Point(position.X, position.Y), NodeState.CLEAN);
                }

                NodeState nextState = NodeState.CLEAN;
                // if is infected
                switch (input[position])
                {
                    case NodeState.FLAGGED:
                        nextState = NodeState.CLEAN;
                        if ((int) (direction += 2) >= directionsArray.Length)
                        {
                            direction -= directionsArray.Length;
                        }
                        break;
                    case NodeState.INFECTED:
                        nextState = NodeState.FLAGGED;
                        if ((int) ++direction >= directionsArray.Length)
                        {
                            direction = 0;
                        }
                        break;
                    case NodeState.WEAKENED:
                        nextState = NodeState.INFECTED;
                        // continues moving forward
                        // we'll infect one -> count that!
                        infected++;
                        break;
                    case NodeState.CLEAN:
                        nextState = NodeState.WEAKENED;
                        if ((int) --direction < 0)
                        {
                            direction = (Direction) directionsArray.GetValue(directionsArray.Length - 1);
                        }

                        break;
                }

                // infect or clean
                input[position] = nextState;

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
            }


            return infected;
        }
    }
}
