using System.Text.RegularExpressions;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day8 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var instructions = new List<Instruction>();
        var registers = new Dictionary<string, int>();
        var regex = new Regex(@"(\w+) (dec|inc) (-?\d+) if (\w+) ([<=>!]{1,2}) (-?\d+)");
        foreach (var line in Input)
        {
            var match = regex.Match(line);
            instructions.Add(new Instruction(match.Groups[1].Value, match.Groups[4].Value,
                int.Parse(match.Groups[3].Value), match.Groups[2].Value.Equals("inc"),
                match.Groups[5].Value, int.Parse(match.Groups[6].Value)));
            registers[match.Groups[1].Value] = 0;
        }

        foreach (var instruction in instructions)
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

    public override object ExecutePart2()
    {
        var instructions = new List<Instruction>();
        var registers = new Dictionary<string, int>();
        var maxValue = int.MinValue;
        var regex = new Regex(@"(\w+) (dec|inc) (-?\d+) if (\w+) ([<=>!]{1,2}) (-?\d+)");
        foreach (var line in Input)
        {
            var match = regex.Match(line);
            instructions.Add(new Instruction(match.Groups[1].Value, match.Groups[4].Value,
                int.Parse(match.Groups[3].Value), match.Groups[2].Value.Equals("inc"),
                match.Groups[5].Value, int.Parse(match.Groups[6].Value)));
            registers[match.Groups[1].Value] = 0;
        }

        foreach (var instruction in instructions)
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

            if (maxValue < registers[instruction.Name])
                maxValue = registers[instruction.Name];
        }

        return maxValue;
    }

    public class Instruction
    {
        public string Name { get; set; }

        public string OnWho { get; set; }
        public int ChangeAmount { get; set; }
        public bool IsInc { get; set; }
        public string Condition { get; set; }
        public int Amount { get; set; }

        public Instruction(string name, string onWho, int changeAmount, bool isInc, string condition, int amount)
        {
            Name = name;
            OnWho = onWho;
            ChangeAmount = changeAmount;
            IsInc = isInc;
            Condition = condition;
            Amount = amount;
        }
    }
}