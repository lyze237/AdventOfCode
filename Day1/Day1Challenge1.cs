using System;
using Utils;

namespace Day1
{
    class Day1Challenge1 : Challenge<int>
    {
        public Day1Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            string input = GetInputFile();

            int sum = 0;
            var charArray = input.ToCharArray();
            for (int i = 1; i < charArray.Length; i++)
            {
                int left = (int) Char.GetNumericValue(charArray[i - 1]);
                int right = (int) Char.GetNumericValue(charArray[i]);

                if (left == right) {
                    sum += left;   
                    System.Console.WriteLine(left + " = " + right);
                }

            }

            int left2 = (int) Char.GetNumericValue(charArray[0]);
            int right2 = (int) Char.GetNumericValue(charArray[charArray.Length - 1]);

            if (left2 == right2) {
                sum += left2;
            }

            return sum;
        }
    }
}