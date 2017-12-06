using System;
using Utils;

namespace Day2
{
    class Day2Challenge1 : Challenge
    {
        public Day2Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            int sum = 0;
            foreach (var line in GetInputFilePerLine())
            {
                var inputs = line.Split('\t');
                int min = Int32.MaxValue;
                int max = Int32.MinValue;
                foreach (var input in inputs)
                {
                    var number = Convert.ToInt32(input);

                    if (number < min)
                        min = number;

                    if (number > max)
                        max = number;
                }
                sum += max - min;
            }

            return sum;
        }
    }
}