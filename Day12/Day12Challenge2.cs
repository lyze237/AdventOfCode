using System.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Utils;

namespace Day12
{
    public class Day12Challenge2 : Challenge<int>
    {
        public Day12Challenge2() : base("input")
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


            int cnt = 0;
            while (nodes.Count > 0)
            {
                var allNeighbours = nodes.First().FindAllNeighbours();
                nodes.RemoveAll(node => allNeighbours.Contains(node));
                cnt++;
            }
            return cnt;
        }
    }
}