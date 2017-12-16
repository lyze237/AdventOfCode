using System.Linq;
using System;
using System.IO.IsolatedStorage;
using System.Text;
using Utils;

namespace Day16
{
    public class Day16Challenge1 : Challenge<string>
    {
        public Day16Challenge1() : base("input")
        {
        }

        public override string Run()
        {
            string programs = "abcdefghijklmnop";
            
            foreach (var task in GetInputFile().Split(','))
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

            return programs;
        }
    }
}