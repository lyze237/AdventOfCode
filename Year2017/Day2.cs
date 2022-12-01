using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day2 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var sum = 0;
        foreach (var line in Input)
        {
            var inputs = line.Split('\t');
            var min = Int32.MaxValue;
            var max = Int32.MinValue;
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

    public override object ExecutePart2()
    {
        var sum = 0;
        foreach (var line in Input)
        {
            var inputs = line.Split('\t');
            foreach (var inputLeft in inputs)
            {
                var numberLeft = Convert.ToInt32(inputLeft);
                    
                foreach (var inputRight in inputs)
                {
                    var numberRight = Convert.ToInt32(inputRight);

                    var max = Math.Max(numberLeft, numberRight);
                    var min = Math.Min(numberLeft, numberRight);

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