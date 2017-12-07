using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Utils;

namespace Day7
{
    public class Day7Challenge1 : Challenge<string>
    {
        public Day7Challenge1() : base("input")
        {
        }

        public override string Run()
        {
            List<Disk> disks = GetInputFilePerLine().Select(s => new Disk(s)).ToList();
            disks.ForEach(disk => disk.AddChildDisks(disks));
            
            return GetBaseDisk(disks).Name;
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