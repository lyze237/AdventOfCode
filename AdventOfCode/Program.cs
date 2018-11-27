using System;

namespace AdventOfCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.ReadKey();

            var days = DayFinder.Find();
            var dayStarter = new DayStarter(days);
            dayStarter.Start();

            Console.ReadKey();
        }

        private void StartDay()
        {

        }
    }
}
