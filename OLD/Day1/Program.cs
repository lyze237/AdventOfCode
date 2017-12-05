using System;
using System.IO;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo file = new FileInfo("input2");
            string input = File.ReadAllText(file.FullName);

            int sum = 0;
            var charArray = input.ToCharArray();
            for (int i = 0; i < charArray.Length / 2; i++)
            {
                int left = (int) Char.GetNumericValue(charArray[i]);
                int right = (int) Char.GetNumericValue(charArray[charArray.Length / 2 + i]);

                if (left == right) {
                    sum += left;   
                    System.Console.WriteLine(left + " = " + right);
                }

            }
            
            System.Console.WriteLine("Result: " + (sum * 2));
        }

        static void Challenge1(string[] args)
        {
            FileInfo file = new FileInfo("input");
            string input = File.ReadAllText(file.FullName);

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

            System.Console.WriteLine("Result: " + sum);
        }
    }
}