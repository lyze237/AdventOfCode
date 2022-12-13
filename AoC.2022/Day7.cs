using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2022;

[TestFixture]
public class Day7 : Day<List<Day7.Dir>>
{
    public Day7() : base(2022, 7) { }

    protected override object DoPart1(List<Dir> input)
    {
        return input.Where(d => d.GetSize() <= 100000)
            .Select(d => d.GetSize())
            .Sum();
    }

    protected override object DoPart2(List<Dir> input)
    {
        const int diskSize = 70000000;
        var unused = diskSize - input.First(d => d.Name.Equals("/")).GetSize();
        var free = 30000000 - unused;

        return input
            .Select(d => d.GetSize())
            .Where(d => d > free)
            .Min();
    }

    protected override List<Dir> ParseInput(string input)
    {
        var rootDir = new Dir(null, "/");
        var currentDir = rootDir;

        var allDirectories = new List<Dir> { currentDir };

        // This whole thing is written to not safeguard against invalid directories.
        foreach (var line in input.Split("\n"))
        {
            if (line.StartsWith("$ cd"))
            {
                var name = line.Split(" ")[2];
                currentDir = name switch
                {
                    "/" => rootDir,
                    ".." => currentDir!.Up(),
                    _ => currentDir!.Down(name)
                };
            }
            else if (line.StartsWith("$ ls"))
            {
                // no need to do anything here as the only command which outputs something is ls
            }
            else if (line.StartsWith("dir "))
            {
                allDirectories.Add(new Dir(currentDir, line.Split(" ")[1]));
            }
            else
            {
                // assume that anything else is a file
                var (size, name, _) = line.Split(" ");
                currentDir!.AddFile(name!, Convert.ToInt32(size!));
            }
        }

        return allDirectories;
    }
    
    public class Dir
    {
        public string Name { get; }
        private Dir? Parent { get; }
        private List<Dir> Children { get; } = new();
        private List<(string name, int size)> Files { get; } = new();

        public Dir(Dir? parent, string name)
        {
            Name = name;
            Parent = parent;

            parent?.Children.Add(this);
        }
        
        public void AddFile(string name, int size) => 
            Files.Add((name, size));

        public Dir? Up() => Parent;

        public Dir Down(string name) => Children.First(d => d.Name.Equals(name));

        public int GetSize() =>
            Children.Select(c => c.GetSize()).Sum() + Files.Select(f => f.size).Sum();
    }
}