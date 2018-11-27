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
