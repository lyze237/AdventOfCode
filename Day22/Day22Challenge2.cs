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