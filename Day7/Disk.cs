//-----------------------------------------------------------------------
// <copyright>
// MIT License
//
// Copyright (c) 2018 Michael Weinberger lyze@owl.sh
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Day7
{
    public class Disk
    {
        public int Weight { get; set; }
        public string Name { get; set; }
        public List<string> ChildNames { get; set; }
        public List<Disk> ChildDisks { get; set; }
        public Disk Parent { get; set; }

        private static Regex inputRegex = new Regex(@"(\w*) \((\d*)\)( -> ([\w, ]*))?");
        
        public Disk(string input)
        {
            Match words = inputRegex.Match(input);

            Name = words.Groups[1].Value;
            Weight = Convert.ToInt32(words.Groups[2].Value);

            ChildNames = words.Groups[4].Value.Split(',').Where(s => !string.IsNullOrEmpty(s)).Select(s => s.Trim()).ToList();
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
