using System.Text.RegularExpressions;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day7 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var disks = Input.Select(s => new Disk(s)).ToList();
        disks.ForEach(disk => disk.AddChildDisks(disks));

        return GetBaseDisk(disks).Name;
    }

    public override object ExecutePart2()
    {
        var disks = Input.Select(s => new Disk(s)).ToList();
        disks.ForEach(d => d.AddChildDisks(disks));

        var disk = GetBaseDisk(disks);
        var targetWeight = 0;

        while (!disk.IsBalanced())
            (disk, targetWeight) = disk.GetUnbalancedChild();

        var weightDiff = targetWeight - disk.GetTotalWeight();

        return disk.Weight + weightDiff;
    }

    private Disk GetBaseDisk(List<Disk> disks)
    {
        var disk = disks[0];
        while (disk.Parent != null)
            disk = disk.Parent;
        return disk;
    }

    public class Disk
    {
        public int Weight { get; set; }
        public string Name { get; set; }
        public List<string> ChildNames { get; set; }
        public List<Disk> ChildDisks { get; set; }
        public Disk? Parent { get; set; }

        private static readonly Regex InputRegex = new Regex(@"(\w*) \((\d*)\)( -> ([\w, ]*))?");

        public Disk(string input)
        {
            var words = InputRegex.Match(input);

            Name = words.Groups[1].Value;
            Weight = Convert.ToInt32(words.Groups[2].Value);

            ChildNames = words.Groups[4].Value.Split(',').Where(s => !string.IsNullOrEmpty(s)).Select(s => s.Trim())
                .ToList();
        }

        public void AddChildDisks(List<Disk> disks)
        {
            try
            {
                ChildDisks = ChildNames.Select(name => disks.First(disc => disc.Name == name)).ToList();
                ChildDisks.ForEach(child => child.Parent = this);
            }
            catch (Exception e)
            {
                Console.WriteLine("Breakpoint hype");
            }
        }

        public int GetTotalWeight()
        {
            return ChildDisks.Sum(disk => disk.GetTotalWeight()) + Weight;
        }

        public bool IsBalanced()
        {
            return ChildDisks.GroupBy(disk => disk.GetTotalWeight()).Count() == 1;
        }

        public (Disk disk, int targetWeight) GetUnbalancedChild()
        {
            var groups = ChildDisks.GroupBy(disk => disk.GetTotalWeight());
            var targetWeight = groups.First(disk => disk.Count() > 1).Key;
            var unbalancedChild = groups.First(disk => disk.Count() == 1).First();

            return (unbalancedChild, targetWeight);
        }
    }
}