using System;
using System.Collections.Generic;
using System.Drawing;

namespace Day3
{
    class Program
    {

        enum Direction
        {
            Left, Right, Up, Down
        }
        
        static void Main(string[] args)
        {
            Dictionary<Point,int> grid = new Dictionary<Point,int>();
            grid[new Point(0, 0)] = 1;
            
            int input = Convert.ToInt32(Console.ReadLine());



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
                    Console.WriteLine("Found val: " + val);
                    break;
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
        
        
        static void Challenge1(string[] args)
        {
            int input = Convert.ToInt32(Console.ReadLine());

            int ring = 0;
            for (int maxRingVal = 1; maxRingVal < input; maxRingVal += 8 * ++ring)
            {
                
            }
            Console.WriteLine("Ring: " + ring);
            int sequenceIndex = (input - 1) % (2 * ring);
            int distAlongEdge = Math.Abs(sequenceIndex - ring);
            int taxiDist = distAlongEdge + ring;

            Console.WriteLine("Output is: " + taxiDist);
        }
    }
}