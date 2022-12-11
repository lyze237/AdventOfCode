using System.Text;
using AdventOfCode.Year2022.Extensions;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public class Day10 : Day<List<(string, int?)>>
{
    private static readonly StringBuilder Output = new();
    
    private class Sprite
    {
        private int[] positions = null!;
        
        public Sprite() => 
            SetPosition(1);

        public void SetPosition(int value) => 
            positions = new[] { value - 1, value, value + 1 };

        public void Draw(int marker)
        {
            if (marker % 40 == 0 && marker != 0)
                Output.Append('\n');

            Output.Append(positions.Contains(marker % 40) ? '#' : '.');
        }
    }
    
    private class Processor
    {
        public Dictionary<int, int> SignalStrengths { get; } = new();
        private Sprite Sprite { get; } = new();

        private int Cycle { get; set; }
        private int X { get; set; } = 1;
        
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

    public override object ExecutePart1()
    {
        var requiredCycles = new[] { 20, 60, 100, 140, 180, 220 };
        var processor = new Processor();

        foreach (var (cmd, val) in Input)
            commands[cmd].Invoke(processor, val);

        return processor.SignalStrengths.Where(s => requiredCycles.Contains(s.Key)).Sum(s => s.Value);
    }
    
    public override object ExecutePart2()
    {
        var processor = new Processor();

        foreach (var (cmd, val) in Input)
            commands[cmd].Invoke(processor, val);

        Console.WriteLine($"\n{Output}\n");

        return Output.ToString().Ocr();
    }
    
    public override List<(string, int?)> ParseInput(string rawInput) =>
        rawInput.Split("\n").Select(s =>
        {
            var (cmd, val, _) = s.Split(" ");
            return (cmd!, val?.ToInt());
        }).ToList();
}











