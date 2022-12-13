using System.Text;
using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2022;

[TestFixture]
public class Day10 : Day<List<(string, int?)>>
{
    public Day10() : base(2022, 10, true) { }
    
    protected override object DoPart1(List<(string, int?)> input)
    {
        var requiredCycles = new[] { 20, 60, 100, 140, 180, 220 };
        var processor = new Processor();

        foreach (var (cmd, val) in input)
            commands[cmd].Invoke(processor, val);

        return processor.SignalStrengths.Where(s => requiredCycles.Contains(s.Key)).Sum(s => s.Value);

    }

    protected override object DoPart2(List<(string, int?)> input)
    {
        var output = new StringBuilder();
        var processor = new Processor(output);

        foreach (var (cmd, val) in input)
            commands[cmd].Invoke(processor, val);

        Console.WriteLine($"\n{output}\n");

        return output.ToString().Ocr();
    }

    protected override List<(string, int?)> ParseInput(string input) =>
        input.Split("\n").Select(s =>
        {
            var (cmd, val, _) = s.Split(" ");
            return (cmd!, val?.ToInt());
        }).ToList();
    
    private class Sprite
    {
        private readonly StringBuilder? output;
        private int[] positions = null!;
        
        public Sprite(StringBuilder? output)
        {
            this.output = output;
            SetPosition(1);
        }

        public void SetPosition(int value) => 
            positions = new[] { value - 1, value, value + 1 };

        public void Draw(int marker)
        {
            if (marker % 40 == 0 && marker != 0)
                output?.Append('\n');

            output?.Append(positions.Contains(marker % 40) ? '#' : '.');
        }
    }
    
    private class Processor
    {
        public Dictionary<int, int> SignalStrengths { get; } = new();
        private Sprite Sprite { get; }

        private int Cycle { get; set; }
        private int X { get; set; } = 1;
        
        public Processor(StringBuilder? output = null) => 
            Sprite = new Sprite(output);

        public void Tick(int? add)
        {
            Sprite.Draw(Cycle);
            
            SignalStrengths.Add(++Cycle, Cycle * X);

            if (add == null) 
                return;
            
            Sprite.SetPosition(X += (int) add);
        }
    }

    private readonly Dictionary<string, Action<Processor, int?>> commands = new()
    {
        { "noop", (processor, _) => processor.Tick(null) },
        { "addx", (processor, add) => { processor.Tick(null); processor.Tick(add); } }
    };
}