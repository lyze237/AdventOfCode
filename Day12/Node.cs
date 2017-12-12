using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    public class Node
    {
        public int Id { get; }
        public List<Node> Path { get; } = new List<Node>();

        public Node(int id)
        {
            Id = id;
        }

        public bool FindChild(int id)
        {
            return FindChild(id, new List<Node>());
        }

        private bool FindChild(int id, List<Node> parents)
        {
            if (Id == id)
                return true;

            parents.Add(this);
            
            foreach (var node in Path)
            {
                if (parents.FirstOrDefault(parent => parent.Id == node.Id) != null)
                    return false;

                return node.FindChild(id, parents);
            }

            return false;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Path)}.Count: {Path.Count}";
        }
    }
}