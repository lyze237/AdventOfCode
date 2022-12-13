using System.Text.RegularExpressions;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day12 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var regex = new Regex(@"(\d*) <-> ([\d, ]*)");

        var nodes = Input.Select(line => regex.Match(line))
            .Select(match => new Node(Convert.ToInt32(match.Groups[1].Value))).ToList();

        foreach (var line in Input)
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

    public override object ExecutePart2()
    {
        var regex = new Regex(@"(\d*) <-> ([\d, ]*)");

        var nodes = Input.Select(line => regex.Match(line))
            .Select(match => new Node(Convert.ToInt32(match.Groups[1].Value))).ToList();

        foreach (var line in Input)
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


        var cnt = 0;
        while (nodes.Count > 0)
        {
            var allNeighbours = nodes.First().FindAllNeighbours();
            nodes.RemoveAll(node => allNeighbours.Contains(node));
            cnt++;
        }

        return cnt;
    }

    public class Node
    {
        public int Id { get; }
        public List<Node> Path { get; } = new();

        public Node(int id)
        {
            Id = id;
        }

        #region stuff

        private bool Equals(Node other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Node)obj);
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
            foreach (var node in Path.Where(node => !alreadyVisited.Contains(node)))
            {
                node.FindAllNeighbours(alreadyVisited);
            }
        }
    }
}