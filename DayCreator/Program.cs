using System;
using System.Diagnostics;
using System.IO;
using owl.sh.owlutils.extensions;

namespace DayCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo("..");
            var mainDir = dir.GetSubDirectory("AdventOfCode");
            var libDir = dir.GetSubDirectory("AdventOfCodeLibrary");

            var daysDir = dir.GetSubDirectory("days");
            var templateDir = daysDir.GetSubDirectory("Template");

            Console.Write("Day to generate: ");
            var day = Convert.ToInt32(Console.ReadLine());

            var dayDir = daysDir.GetSubDirectory($"Day{day}");
            dayDir.Create();

            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "dotnet",
                ArgumentList = { "new", "classlib", "-f", "netcoreapp2.1" },
                WorkingDirectory = dayDir.FullName
            });
            process.WaitForExit();

            process = Process.Start(new ProcessStartInfo
            {
                FileName = "dotnet",
                ArgumentList = {"sln", "add", dayDir.FullName},
                WorkingDirectory = dir.FullName
            });
            process.WaitForExit();

            process = Process.Start(new ProcessStartInfo
            {
                FileName = "dotnet",
                ArgumentList = {"add", "reference", libDir.FullName},
                WorkingDirectory = dayDir.FullName
            });
            process.WaitForExit();

            process = Process.Start(new ProcessStartInfo
            {
                FileName = "dotnet",
                ArgumentList = {"add", "reference", dayDir.FullName},
                WorkingDirectory = mainDir.FullName
            });
            process.WaitForExit();

            for (int i = 1; i <= 2; i++)
                DoSectionStuff(dayDir, templateDir, day, i);
        }

        private static void DoSectionStuff(DirectoryInfo dayDir, DirectoryInfo templateDir, int day, int section)
        {
            dayDir.GetFileInDirectory("Class1.cs").Delete();
            var templateSection1 = templateDir.GetFileInDirectory($"DayXSection{section}.cs");
            var section1 = dayDir.GetFileInDirectory($"Day{day}Section{section}.cs");
            templateSection1.CopyTo(section1.FullName);

            section1.WriteAllText(templateSection1.ReadAllText().Replace("/*DayX*/-1", "" + day).Replace("DayX", $"Day{day}"));
        }
    }
}
