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
using System.ComponentModel.Design;
using Utils;

namespace Day24
{
    public class Day24Challenge1 : Challenge<int>
    {
        private List<Component> components;
        
        public Day24Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            components = GetInputFilePerLine().Select(line => line.Split('/')).Select(strings =>
                new Component(Convert.ToInt32(strings[0]), Convert.ToInt32(strings[1]))).ToList();

            var root = new Component(0, 0);

            var tree = Build(null, root, components);
            return GetMaxSize(tree);
        }

        
        private static int GetMaxSize(Node tree, int startingSize = 0)
        {
            if (tree.Childs.Count == 0)
                return startingSize;
            return tree.Childs.Max(node => GetMaxSize(node, node.Component.Left + node.Component.Right)) + startingSize;
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
