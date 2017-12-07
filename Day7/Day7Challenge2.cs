using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Day7
{
    public class Day7Challenge2 : Challenge<int>
    {
        public Day7Challenge2 () : base("input")
        {
        }

        public override int Run()
        {
            List<Disk> disks = GetInputFilePerLine().Select(s => new Disk(s)).ToList();
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
    }
}