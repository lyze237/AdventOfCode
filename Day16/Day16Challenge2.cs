using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using Utils;

namespace Day16
{
    public class Day16Challenge2 : Challenge<string>
    {
        public Day16Challenge2() : base("input")
        {
        }

        public override string Run()
        {
            string initialPrograms = "abcdefghijklmnop";
            string programs = "abcdefghijklmnop";

            var commands = GetInputFile().Split(',').ToList();
           
            for (int i = 0; i < 1_000_000_000%60; i++)
            {
                if (programs == initialPrograms)
                    Console.WriteLine(i); // repeats itself each 60 times -> mod
                
                foreach (var task in commands)
                {
                    var parameter = task.Substring(1);
                    switch (task[0])
                    {
                        case 's':
                            int amount = Convert.ToInt32(parameter);

                            var temp = programs.Substring(programs.Length - amount);
                            programs = programs.Remove(programs.Length - amount);

                            programs = temp + programs;
                            break;
                        case 'x':
                            var strings = parameter.Split('/');

                            var indexA = Convert.ToInt32(strings[0]);
                            var indexB = Convert.ToInt32(strings[1]);

                            var programsArray = programs.ToCharArray();
                            programsArray[indexA] = programs[indexB];
                            programsArray[indexB] = programs[indexA];

                            programs = string.Join(string.Empty, programsArray);
                            break;
                        case 'p':
                            strings = parameter.Split('/');

                            var programA = strings[0];
                            var programB = strings[1];

                            indexA = programs.IndexOf(programA, StringComparison.Ordinal);
                            indexB = programs.IndexOf(programB, StringComparison.Ordinal);

                            programsArray = programs.ToCharArray();
                            programsArray[indexA] = programs[indexB];
                            programsArray[indexB] = programs[indexA];

                            programs = string.Join(string.Empty, programsArray);
                            break;
                    }
                }
            }
            return programs;
        }
    }
}