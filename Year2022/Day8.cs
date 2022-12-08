using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public class Day8 : Day<char[][]>
{
    public override char[][] ParseInput(string rawInput) =>
        rawInput.Split("\n").Select(arr => arr.ToCharArray()).ToArray();

    public override object ExecutePart1()
    {
        var forest = new Forest(Input);

        return forest.Trees.Count(tree => Directions.Any(d => forest.IsTallest(tree, d)));
    }

    public override object ExecutePart2()
    {
        var forest = new Forest(Input);

        return forest.Trees.Select(tree => Directions.Aggregate(1, (left, right) => left * forest.TreeViewDistance(tree, right))).Max();
    }
    
    private static readonly (int x, int y)[] Directions =
    {
        (-1, 0), (1, 0), (0, -1), (0, 1)
    };

    private record Tree(int Height, int Y, int X);

    private record Forest(char[][] Input, int Y, int X)
    {
        public readonly List<Tree> Trees = new();

        public Forest(char[][] input) : this(input, input.Length, input[0].Length)
        {
            for (var y = 0; y < Y; y++)
                for (var x = 0; x < X; x++)
                    Trees.Add(new Tree(Input[y][x], y, x));
        }

        public int TreeViewDistance(Tree tree, (int x, int y) dir) =>
            IsTallest(tree, dir) ? TreesInDirection(tree, dir).Count() : FindSmallerTrees(tree, dir).Count() + 1;

        public bool IsTallest(Tree tree, (int x, int y) dir) =>
            TreesInDirection(tree, dir).All(t => t.Height < tree.Height);

        private IEnumerable<Tree> FindSmallerTrees(Tree tree, (int x, int y) dir) =>
            TreesInDirection(tree, dir).TakeWhile(t => t.Height < tree.Height);
        
        private IEnumerable<Tree> TreesInDirection(Tree tree, (int x, int y) dir)
        {
            var trees = new List<Tree>();
            
            for (int y = tree.Y + dir.y, x = tree.X + dir.x; y < Y && y >= 0 && x < X && x >= 0; y += dir.y, x += dir.x)
                trees.Add(new Tree(Input[y][x], y, x));

            return trees;
        }
    }
}