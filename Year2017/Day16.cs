using AdventOfCode.Parsers;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day16 : Day<string[]>.WithParser<CommaParser>
{
    public override object ExecutePart1()
    {
        string programs = "abcdefghijklmnop";

        foreach (var task in Input)
        {
            var parameter = task[1..];
            switch (task[0])
            {
                case 's':
                    var amount = Convert.ToInt32(parameter);

                    var temp = programs[^amount..];
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

    public override object ExecutePart2()
    {
        const string initialPrograms = "abcdefghijklmnop";
        string programs = "abcdefghijklmnop";

        for (var i = 0; i < 1_000_000_000 % 60; i++)
        {
            if (programs == initialPrograms)
                Console.WriteLine(i); // repeats itself each 60 times -> mod

            foreach (var task in Input)
            {
                var parameter = task[1..];
                switch (task[0])
                {
                    case 's':
                        var amount = Convert.ToInt32(parameter);

                        var temp = programs[^amount..];
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