using System.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using Utils;

namespace Day22
{
    public class Day22Challenge1 : Challenge<int>
    {
        public Day22Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            bool[][] inputArray = GetInputFilePerLine().Select(s => s.Select(c => c == '#').ToArray()).ToArray();
            Dictionary<Point, bool> input = new Dictionary<Point, bool>();
            
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
            for (int generaLIndex = 0; generaLIndex < 10_000; generaLIndex++)
            {
                if (!input.ContainsKey(position))
                {
                    input.Add(new Point(position.X, position.Y), false);
                }

                // if is infected
                if (input[position])
                {
                    if ((int) ++direction >= directionsArray.Length)
                    {
                        direction = 0;
                    }
                }
                else
                {
                    if ((int) --direction < 0)
                    {
                        direction = (Direction) directionsArray.GetValue(directionsArray.Length - 1);
                    }
                    
                    // we'll infect one -> count that!
                    infected++;
                }

                // infect or clean
                input[position] = !input[position];
                
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