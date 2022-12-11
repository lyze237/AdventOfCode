using System.Text.RegularExpressions;
using AdventOfCode.Year2022.Extensions;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public partial class Day11 : Day<string[][]>
{
    public override string[][] ParseInput(string rawInput) =>
        rawInput.Split("\n\n").Select(s => s.Split("\n")).ToArray();

    public override object ExecutePart1() => 
        Calculate(20, true);

    public override object ExecutePart2() => 
        Calculate(10000, false);

    private object Calculate(int iterations, bool decreaseWorry)
    {
        var monkeys = Input.Select(monkeyInput => new Monkey(monkeyInput)).ToList();

        for (var i = 0; i < iterations; i++)
            monkeys.ForEach(m => m.Process(monkeys, decreaseWorry));

        return monkeys
            .OrderByDescending(m => m.Inspections)
            .Take(2)
            .Aggregate(1L, (i, monkey) => i * monkey.Inspections);
    }

    public record Operator(string Left, string Type, string Right)
    {
        public long Calculate(long old)
        {
            var right = Right == "old" ? old : Right.ToLong();
            
            return Type switch
            {
                "*" => old * right,
                "+" => old + right,
                _ => throw new ArgumentOutOfRangeException(nameof(Type), "Unknown operator")
            };
        }
    }

    private partial class Monkey
    {
        [GeneratedRegex("Monkey (?<id>\\d+):")]
        private static partial Regex IdRegex();

        [GeneratedRegex("Starting items: (?<items>[\\d, ]+)")]
        private static partial Regex StartingItemsRegex();

        [GeneratedRegex("Operation: new = (?<left>[\\w\\d]+) (?<operator>[\\+\\-\\*]) (?<right>[\\w\\d]+)")]
        private static partial Regex OperationsRegex();

        [GeneratedRegex("Test: (?<operator>divisible) by (?<number>\\d+)")]
        private static partial Regex TestRegex();

        [GeneratedRegex("If (?<bool>true|false): throw to monkey (?<number>\\d+)")]
        private static partial Regex IfBoolRegex();

        private int Id { get; }
        private List<long> Items { get; }

        private Operator Operator { get; }
        private int TestNumber { get; }

        private int IfTrueTarget { get; }
        private int IfFalseTarget { get; }

        public long Inspections { get; private set; }

        public Monkey(IReadOnlyList<string> input)
        {
            Id = IdRegex().Match(input[0]).Groups["id"].Value.ToInt();

            Items = StartingItemsRegex().Match(input[1]).Groups["items"].Value.Split(", ").Select(i => i.ToLong()).ToList();

            var operationsMatch = OperationsRegex().Match(input[2]);
            Operator = new Operator(operationsMatch.Groups["left"].Value, operationsMatch.Groups["operator"].Value, operationsMatch.Groups["right"].Value);

            var testMatch = TestRegex().Match(input[3]);
            TestNumber = testMatch.Groups["number"].Value.ToInt();

            IfTrueTarget = IfBoolRegex().Match(input[4]).Groups["number"].Value.ToInt();
            IfFalseTarget = IfBoolRegex().Match(input[5]).Groups["number"].Value.ToInt();
        }

        public void Process(IReadOnlyCollection<Monkey> monkeys, bool decreaseWorry)
        {
            var multiplier = monkeys.Aggregate(1L, (i, monkey) => i * monkey.TestNumber);

            for (var i = Items.Count - 1; i >= 0; i--, Inspections++)
            {
                Items[i] = Operator.Calculate(Items[i]);
                
                if (decreaseWorry)
                    Items[i] = (long)Math.Floor(Items[i] / 3f);
                
                Items[i] %= multiplier;

                monkeys.First(m => m.Id == (Items[i] % TestNumber == 0 ? IfTrueTarget : IfFalseTarget)).Items.Add(Items[i]);
                Items.RemoveAt(i);
            }
        }
    }
}