using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day1 : Day
{
    public override object ExecutePart1()
    {
        var sum = 0;
        var charArray = Input.ToCharArray();
        for (var i = 1; i < charArray.Length; i++)
        {
            var left = (int) char.GetNumericValue(charArray[i - 1]);
            var right = (int) char.GetNumericValue(charArray[i]);

            if (left == right) {
                sum += left;   
                Console.WriteLine(left + " = " + right);
            }

        }

        var left2 = (int) char.GetNumericValue(charArray[0]);
        var right2 = (int) char.GetNumericValue(charArray[^1]);

        if (left2 == right2) {
            sum += left2;
        }

        return sum;
    }

    public override object ExecutePart2()
    {
        var sum = 0;
        var charArray = Input.ToCharArray();
        for (var i = 0; i < charArray.Length / 2; i++)
        {
            var left = (int) char.GetNumericValue(charArray[i]);
            var right = (int) char.GetNumericValue(charArray[charArray.Length / 2 + i]);

            if (left == right) {
                sum += left;   
                Console.WriteLine(left + " = " + right);
            }
        }
            
        return sum * 2;
    }
}