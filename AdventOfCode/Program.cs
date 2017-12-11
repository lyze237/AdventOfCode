using System;
using Day10;
using Day11;
using Day9;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var challenge = new Day11Challenge2();

            Console.WriteLine($"Starting challenge {challenge.GetType().Name}");

            Console.WriteLine($"Challenge finished with result {challenge.Run()}");
        }
    }
}