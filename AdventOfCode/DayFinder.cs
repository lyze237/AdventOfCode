using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AdventOfCodeLibrary;

namespace AdventOfCode
{
    public static class DayFinder
    {
        public static List<Day> Find()
        {
            var days = new List<Day>();

            var assemblyLocation = new FileInfo(Assembly.GetExecutingAssembly().Location);
            foreach (var dll in assemblyLocation.Directory.GetFiles("*.dll"))
            {
                var assembly = Assembly.LoadFile(dll.FullName);

                foreach (var type in assembly.GetTypes().Where(type => type.BaseType == typeof(Day)))
                {
                    if (!(Activator.CreateInstance(type) is Day day))
                    {
                        Console.WriteLine($"Couldn't instantiate {type.FullName}");
                        continue;
                    }

                    days.Add(day);
                }
            }

            days = days.OrderBy(day => day.DayNumber).ThenBy(day => day.Section).ToList();

            return days;
        }
    }
}
