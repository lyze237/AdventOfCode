using System;
using Day6;
using Day7;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var challenge = new Day7Challenge2();

            Console.WriteLine($"Starting challenge {challenge.GetType().Name}");

            Console.WriteLine($"Challenge finished with result {challenge.Run()}");
        }
    }
}