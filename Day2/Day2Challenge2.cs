using System;
using Utils;

namespace Day2
{
    public class Day2Challenge2 : Challenge
    {
        public Day2Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            int sum = 0;
            foreach (var line in GetInputFilePerLine())
            {
                var inputs = line.Split('\t');
                foreach (var inputLeft in inputs)
                {
                    var numberLeft = Convert.ToInt32(inputLeft);
                    
                    foreach (var inputRight in inputs)
                    {
                        var numberRight = Convert.ToInt32(inputRight);

                        int max = Math.Max(numberLeft, numberRight);
                        int min = Math.Min(numberLeft, numberRight);

                        if (max % min == 0 && max != min)
                        {
                            Console.WriteLine(line + $": max {max} / min {min}");
                            sum += max / min;
                        }
                    }
                }
            }

            return sum / 2;
        }
    }
}