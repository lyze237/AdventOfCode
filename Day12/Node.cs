using System.Collections.Generic;

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

        #region stuff

        protected bool Equals(Node other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Path)}.Count: {Path.Count}";
        }

        #endregion

        public List<Node> FindAllNeighbours()
        {
            var neighbours = new List<Node>();
            FindAllNeighbours(neighbours);
            return neighbours;
        }

        private void FindAllNeighbours(List<Node> alreadyVisited)
        {
            alreadyVisited.Add(this);
            foreach (var node in Path)
            {
                if (alreadyVisited.Contains(node))
                    continue;
                
                node.FindAllNeighbours(alreadyVisited);
            }
        }
    }
}
