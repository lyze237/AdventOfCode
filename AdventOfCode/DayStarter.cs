using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeLibrary;
using AdventOfCodeLibrary.drawers;

namespace AdventOfCode
{
    public class DayStarter
    {
        private List<Day> days;
        private DrawerManager drawerManager;
        private int width;

        public DayStarter(List<Day> days, int width = 80)
        {
            this.days = days;
            this.width = 80;

            drawerManager = new DrawerManager();
        }

        public void Start(params int[] whichDays)
        {
            Console.WriteLine(days.Count);

            var ourDays = days;
            if (whichDays.Length > 0)
                ourDays.RemoveAll(day => !whichDays.Contains(day.DayNumber));

            for (int index = 0, y = 0; index < days.Count; index++, y++)
            {
                var day = days[index];

                if (day.Section == 0)
                {
                    var str = $"#    Day {day.DayNumber}    #";

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine(new string(' ', width / 2 - str.Length / 2) + new string('#', str.Length));
                    Console.WriteLine(new string(' ', width / 2 - str.Length / 2) + str);
                    Console.WriteLine(new string(' ', width / 2 - str.Length / 2) + new string('#', str.Length));
                    Console.WriteLine();

                    y += 7;
                }

                var sectionStr = $"Section {day.Section}: "; 
                Console.WriteLine(sectionStr);

                switch (day.Type)
                {
                    case DayType.Progress:
                        day.Drawer = new PercentProgressBar(sectionStr.Length + 2, y, width - sectionStr.Length);
                        break;
                    case DayType.Time:
                        day.Drawer = new TimeLeftBar(sectionStr.Length + 2, y, width - sectionStr.Length);
                        break;
                    case DayType.Spinner:
                        day.Drawer = new Spinner(sectionStr.Length + 2, y, width - sectionStr.Length);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                drawerManager.Add(day.Drawer);

                StartDay(day);
            }

            drawerManager.Start();
        }

        private void StartDay(Day day)
        {
            Task.Run(() =>
            {
                day.Run(day.Input);
            });
        }
    }
}
