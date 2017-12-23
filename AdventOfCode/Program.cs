using System;
using Day23;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var challenge = new Day23Challenge1();

            Console.WriteLine($"Starting challenge {challenge.GetType().Name}");

            Console.WriteLine($"Challenge finished with result {challenge.Run()}");
        }
    }
}