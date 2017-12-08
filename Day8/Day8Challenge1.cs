using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
 using Utils;

namespace Day8
{
    public class Day8Challenge1 : Challenge<int>
    {
        public Day8Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            List<Instruction> instructions = new List<Instruction>();
            Dictionary<string, int> registers = new Dictionary<string, int>();
            int maxValue = int.MinValue; 
            Regex regex = new Regex(@"(\w+) (dec|inc) (-?\d+) if (\w+) ([<=>!]{1,2}) (-?\d+)");
            foreach (string line in GetInputFilePerLine())
            {
                Match match = regex.Match(line);
                instructions.Add(new Instruction(match.Groups[1].Value, match.Groups[4].Value,
                    int.Parse(match.Groups[3].Value), match.Groups[2].Value.Equals("inc"),
                    match.Groups[5].Value, int.Parse(match.Groups[6].Value)));
                registers[match.Groups[1].Value] = 0;
            }

            foreach (Instruction instruction in instructions)
            {
                switch (instruction.Condition)
                {
                    case ">":
                        if (registers[instruction.OnWho] > instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                    case "<":
                        if (registers[instruction.OnWho] < instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                    case "<=":
                        if (registers[instruction.OnWho] <= instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                    case ">=":
                        if (registers[instruction.OnWho] >= instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                    case "==":
                        if (registers[instruction.OnWho] == instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                    case "!=":
                        if (registers[instruction.OnWho] != instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                }
                
            }
            return registers.Max(pair => pair.Value);
        }
    }
}