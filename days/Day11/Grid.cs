using System.Drawing;

namespace Day11
{
    public class Grid
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