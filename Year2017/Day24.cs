using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day24 : Day.NewLineSplitParsed<string>
{
    private List<Component> components;
    private Dictionary<int, int> lengthSize = new();
        
    public override object ExecutePart1()
    {
        components = Input.Select(line => line.Split('/')).Select(strings =>
            new Component(Convert.ToInt32(strings[0]), Convert.ToInt32(strings[1]))).ToList();

        var root = new Component(0, 0);

        var tree = Build(null, root, components);
        return GetMaxSize(tree);
    }

    public override object ExecutePart2()
    {
        components = Input.Select(line => line.Split('/')).Select(strings =>
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

    private class Component
    {
        public int Left { get; set; }
        public int Right { get; set; }

        private Guid guid;

        public Component(int left, int right)
        {
            Left = left;
            Right = right;

            guid = Guid.NewGuid();
        }

        protected bool Equals(Component other)
        {
            return guid.Equals(other.guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Component) obj);
        }

        public override int GetHashCode()
        {
            return guid.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(Left)}: {Left}, {nameof(Right)}: {Right}";
        }
    }
    
    private class Node
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