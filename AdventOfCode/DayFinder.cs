using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AdventOfCodeLibrary;
using AdventOfCodeLibrary.days;
using owl.sh.owlutils.extensions;

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

                foreach (var type in assembly.GetTypes().Where(type => type.BaseType.In(typeof(ProgressDay), typeof(SpinnerDay), typeof(TimeDay))))
                {
                    if (!(Activator.CreateInstance(type) is Day day))
                    {
                        LockConsole.WriteLineAt($"Couldn't instantiate {type.FullName}", 0, 0);
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
