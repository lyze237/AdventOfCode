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