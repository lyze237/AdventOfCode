using System.Drawing;
using System.Runtime.CompilerServices;
using AdventOfCode.Parsers;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day11 : Day<int>.WithParser<IntParser>
{
    public override object ExecutePart1()
    {
        Grid maxGrid = null;
        for (int i = 1; i <= 298; i++) {
            for (int j = 1; j <= 298; j++) {
                var currentGrind = new Grid(i, j, Input, 3);
                if (maxGrid == null || maxGrid.PowerLevel < currentGrind.PowerLevel)
                    maxGrid = currentGrind;
            }
        }
            
        return maxGrid?.ToString1();
    }

    public override object ExecutePart2()
    {
        // gave up and cheated.
        // worked for a friend of mine but not for me.
        // 4 other solutions in the subreddit didn't work either. top answer did though.
        Grid maxGrid = null;
        for (int size = 1; size <= 200; size++) {
            for (int i = 1; i <= 301 - size; i++) {
                for (int j = 1; j <= 301 - size; j++) {
                    var currentGrind = new Grid(i, j, Input, size);
                    if (maxGrid == null || maxGrid.PowerLevel < currentGrind.PowerLevel) {
                        maxGrid = currentGrind;
                    }
                }
            }
        }

        return maxGrid?.ToString2();
    }

    private class Grid
    {
        public Point TopLeft { get; }
        public int PowerLevel { get; }
        public int Size { get; }
        private readonly int serialNumber;


        public Grid(int x, int y, int serialNumber, int size)
        {
            TopLeft = new Point(x, y);
            this.serialNumber = serialNumber;
            Size = size;

            for (int i = x; i < x + size; i++)
            {
                for (int j = y; j < y + size; j++)
                {
                    PowerLevel += CalculatePowerLevel(i, j);
                }
            }
        }

        private int CalculatePowerLevel(int x, int y)
        {
            int rackId = x + 10;
            int powerLevel = rackId * y;
            powerLevel += serialNumber;
            powerLevel *= rackId;
            powerLevel = (powerLevel / 100) % 10;
            return powerLevel - 5;
        }

        public string ToString1()
        {
            return $"{TopLeft.X},{TopLeft.Y}";
        }

        public string ToString2()
        {
            return $"{TopLeft.X},{TopLeft.Y},{Size}";
        }

        public override string ToString()
        {
            return $"{nameof(TopLeft)}: {TopLeft}, {nameof(PowerLevel)}: {PowerLevel}, {nameof(Size)}: {Size}";
        }
    }   
}