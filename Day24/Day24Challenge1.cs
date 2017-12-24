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