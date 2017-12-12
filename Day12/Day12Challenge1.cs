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
                int nodeId = Convert.ToInt32(match.Groups[1].Value);
                
                nodes.Add(new Node(nodeId));
            }
            
            foreach (var line in GetInputFilePerLine())
            {
                var match = regex.Match(line);
                int nodeId = Convert.ToInt32(match.Groups[1].Value);
                var nodePath = match.Groups[2].Value
                    .Split(',')
                    .Select(s => Convert.ToInt32(s.Trim()))
                    .Select(id => nodes.First(node => node.Id == id));

                nodes.First(node => node.Id == nodeId).Path.AddRange(nodePath.ToArray());
            }

            int cnt = 0;
            var t = new Thread(() =>
            {
                foreach (var node in nodes)
                {
                    if (node.FindChild(0))
                        cnt++;
                    else
                        Console.WriteLine("Couldn't find target node 0 in node: " + node.Id);
                }
            }, 1024 * 1024 * 1024);
            
            t.Start();
            t.Join();
            
            return cnt;
        }
    }
}