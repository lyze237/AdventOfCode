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
using Utils;

namespace Day24
{
    public class Day24Challenge2 : Challenge<int>
    {
        private List<Component> components;
        private Dictionary<int, int> lengthSize = new Dictionary<int, int>();

        public Day24Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            components = GetInputFilePerLine().Select(line => line.Split('/')).Select(strings =>
                new Component(Convert.ToInt32(strings[0]), Convert.ToInt32(strings[1]))).ToList();

            var root = new Component(0, 0);

            var tree = Build(null, root, components);
            FindSizes(tree, 0, 0);
            var maxLength = int.MinValue;
            foreach (var lengthSizeKey in lengthSize.Keys)
            {
                if (maxLength < lengthSizeKey)
                    maxLength = lengthSizeKey;
            }
            return lengthSize[maxLength];
        }


        private void FindSizes(Node node, int startingSize, int length)
        {
            length++;
            if (node.Childs.Count == 0)
            {
                var nodeSize = node.Component.Left + node.Component.Right;
                if (!lengthSize.ContainsKey(length))
                {
                    lengthSize.Add(length, startingSize + nodeSize);
                }
                else if (lengthSize[length] < startingSize + nodeSize)
                {
                    lengthSize[length] = startingSize + nodeSize;
                }
            }
            else
            {
                node.Childs.ForEach(child => FindSizes(child, startingSize + node.Component.Left + node.Component.Right, length));   
            }
        }

        private static Node Build(Node parent, Component component, IEnumerable<Component> inventory)
        {
            List<Component> localInventory = new List<Component>(inventory);
            localInventory.Remove(component);
            var node = new Node(component, parent);
            parent?.Childs.Add(node);
            List<Component> children = localInventory.FindAll(c => c.Left == node.GetChildSide() || c.Right == node.GetChildSide()).ToList();
            children.ForEach(child => Build(node, child, localInventory));
            return node;
        }
    }
}
