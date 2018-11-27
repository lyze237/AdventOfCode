using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AdventOfCodeLibrary;
using AdventOfCodeLibrary.drawers;

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
