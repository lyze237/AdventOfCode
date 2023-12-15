using System.Text.RegularExpressions;
using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day15 : Day<string[]>
{
    public record Lens
    {
        public string Label { get; }
        public string Operation { get; }
        public int? FocalLength { get; }

        public long Hash { get; }

        public Lens(string input)
        {
            var match = Regex.Match(input, @"(?<label>\w+)(?<operation>[=-])(?<number>\d+)?");

            Label = match.Groups["label"].Value;
            Operation = match.Groups["operation"].Value;
            FocalLength = match.Groups["number"].Success ? match.Groups["number"].Value.ToInt() : null;
            Hash = CalculateHash(Label);
        }

        public void AddToBox(List<Lens> box)
        {
            var index = box.FindIndex(l => l.Label == Label);
            if (index >= 0)
                box[index] = this;
            else
                box.Add(this);
        }

        public void RemoveFromBox(List<Lens> box) =>
            box.RemoveAll(b => b.Label == Label);
    }

    public Day15() : base(2023, 15)
    {
    }

    protected override object DoPart1(string[] input) =>
        input.Sum(CalculateHash);

    protected override object DoPart2(string[] input)
    {
        var boxes = new List<Lens>[256];
        for (var i = 0; i < boxes.Length; i++)
            boxes[i] = new List<Lens>();

        foreach (var lens in input.Select(line => new Lens(line)))
        {
            switch (lens.Operation)
            {
                case "=":
                {
                    lens.AddToBox(boxes[lens.Hash]);
                    break;
                }
                case "-":
                {
                    lens.RemoveFromBox(boxes[lens.Hash]);
                    break;
                }
            }
        }

        return boxes.SelectMany((lenses, boxIndex) =>
            lenses.Select((lens, lensIndex) => (boxIndex + 1) * (lensIndex + 1) * lens.FocalLength)).Sum()!;
    }

    private static long CalculateHash(string str)
    {
        var value = 0;

        foreach (var c in str)
        {
            value += c;
            value *= 17;
            value %= 256;
        }

        return value;
    }

    protected override string[] ParseInput(string input) =>
        input.Split(",");
}