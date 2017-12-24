using System;
using System.Collections.Generic;

namespace Day24
{
    public class Node
    {
        public Component Component { get; set; }

        public Node Parent { get; set; }
        public List<Node> Childs { get; set; }

        public Node(Component component, Node parent)
        {
            Component = component;
            Childs = new List<Node>();
            Parent = parent;

            if (GetParentSide() == -1)
                throw new ArgumentException("Can't connect parent with child");
        }

        public int GetChildSide()
        {
            var parentSide = GetParentSide();
            
            return parentSide == Component.Left ? Component.Right : Component.Left;
        }

        public int GetParentSide()
        {
            if (Parent == null)
                return 0;
            
            if (Component.Left == Parent.Component.Left)
            {
                return Component.Left;
            }
            if (Component.Left == Parent.Component.Right)
            {
                return Component.Left;
            }
            if (Component.Right == Parent.Component.Left)
            {
                return Component.Right;
            }
            if (Component.Right == Parent.Component.Right)
            {
                return Component.Right;
            }
            return -1;
        }
    }
}