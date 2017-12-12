using System.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Utils;

namespace Day12
{
    public class Day12Challenge1 : Challenge<int>
    {
        public Day12Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            Regex regex = new Regex(@"(\d*) <-> ([\d, ]*)");

            List<Node> nodes = new List<Node>();
            
            foreach (var line in GetInputFilePerLine())
            {
                var match = regex.Match(line);
                nodes.Add(new Node(Convert.ToInt32(match.Groups[1].Value)));
            }
            
            foreach (var line in GetInputFilePerLine())
            {
                var match = regex.Match(line);
                var neighbours = match.Groups[2].Value
                    .Split(',')
                    .Select(s => nodes
                        .First(node => node.Id == Convert.ToInt32(s.Trim()))
                    ).ToArray();

                nodes.First(node => node.Id == Convert.ToInt32(match.Groups[1].Value)).Path
                    .AddRange(neighbours);
            }

            return nodes.First(node => node.Id == 0)
                .FindAllNeighbours().Count;
        }
    }
}