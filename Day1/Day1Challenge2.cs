using System;
using Utils;

namespace Day1
{
    public class Day1Challenge2 : Challenge<int>
    {
        public Day1Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            string input = GetInputFile();

            int sum = 0;
            var charArray = input.ToCharArray();
            for (int i = 0; i < charArray.Length / 2; i++)
            {
                int left = (int) Char.GetNumericValue(charArray[i]);
                int right = (int) Char.GetNumericValue(charArray[charArray.Length / 2 + i]);

                if (left == right) {
                    sum += left;   
                    Console.WriteLine(left + " = " + right);
                }

            }
            
            return sum * 2;
        }
    }
}