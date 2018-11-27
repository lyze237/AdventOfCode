using System;
using System.Threading.Tasks;
using AdventOfCode.drawers;

namespace AdventOfCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            DrawerManager manager = new DrawerManager();

            var progressBar = new ProgressBar(1, 1, 100) {MinValue = 0, Value = 50, MaxValue = 100};
            manager.Add(progressBar);

            var spinner = new Spinner(3, 3, 10) {UpdatesAfterTicks = 2};
            manager.Add(spinner);

            var percentProgressBar = new PercentProgressBar(5, 5, 100) {MinValue = 0, Value = 50, MaxValue = 100};
            manager.Add(percentProgressBar);

            manager.Start();

            bool right = true;
            Task.Run(async () =>
            {
                while (true)
                {
                    if (right)
                    {
                        if (++percentProgressBar.Value > percentProgressBar.MaxValue) { 
                            percentProgressBar.Value = percentProgressBar.MaxValue;
                            right = false;
                        }
                    }
                    else
                    {
                        if (--percentProgressBar.Value < percentProgressBar.MinValue)
                        {
                            percentProgressBar.Value = percentProgressBar.MinValue;
                            right = true;
                        }
                    }

                    await Task.Delay(5);
                }
            });

            Task.Run(async () =>
            {
                while (true)
                {
                    if (right)
                    {
                        if (++progressBar.Value > progressBar.MaxValue) { 
                            progressBar.Value = progressBar.MaxValue;
                            right = false;
                        }
                    }
                    else
                    {
                        if (--progressBar.Value < progressBar.MinValue)
                        {
                            progressBar.Value = progressBar.MinValue;
                            right = true;
                        }
                    }

                    await Task.Delay(5);
                }
            });
            Console.ReadKey();
        }
    }
}
