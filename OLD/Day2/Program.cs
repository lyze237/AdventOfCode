using System;
using System.IO;

namespace Day2
{
    class Program
    {
        static void Challenge1(string[] args)
        {
            int sum = 0;
            FileInfo file = new FileInfo("input");
            foreach (var line in File.ReadAllLines(file.FullName))
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

            Console.WriteLine("The Sum is: " + sum);
        }
        
        static void Main(string[] args)
        {
            int sum = 0;
            FileInfo file = new FileInfo("input2");
            foreach (var line in File.ReadAllLines(file.FullName))
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

            Console.WriteLine("The Sum is: " + sum / 2);
        }
    }
}