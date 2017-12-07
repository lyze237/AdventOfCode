using System;
using Utils;

namespace Day3
{
    public class Day3Challenge1 : Challenge<int>
    {
        public Day3Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            int input = Convert.ToInt32(GetInputFile());

            int ring = 0;
            for (int maxRingVal = 1; maxRingVal < input; maxRingVal += 8 * ++ring)
            {
            }
            Console.WriteLine("Ring: " + ring);
            int sequenceIndex = (input - 1) % (2 * ring);
            int distAlongEdge = Math.Abs(sequenceIndex - ring);
            int taxiDist = distAlongEdge + ring;

            return taxiDist;
        }
    }
}