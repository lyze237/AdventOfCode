using System.Drawing;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day22 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var inputArray = Input.Select(s => s.Select(c => c == '#').ToArray()).ToArray();
        var input = new Dictionary<Point, bool>();

        // fill dictionary
        for (var y = 0; y < inputArray.Length; y++)
        {
            for (var x = 0; x < inputArray[y].Length; x++)
            {
                input.Add(new Point(x, y), inputArray[y][x]);
            }
        }

        var directionsArray = Enum.GetValues(typeof(Direction));
        var direction = Direction.UP;
        var position = new Point(inputArray[0].Length / 2, inputArray.Length / 2);

        var infected = 0;
        for (var generaLIndex = 0; generaLIndex < 10_000; generaLIndex++)
        {
            if (!input.ContainsKey(position))
            {
                input.Add(new Point(position.X, position.Y), false);
            }

            // if is infected
            if (input[position])
            {
                if ((int)++direction >= directionsArray.Length)
                {
                    direction = 0;
                }
            }
            else
            {
                if ((int)--direction < 0)
                {
                    direction = (Direction)directionsArray.GetValue(directionsArray.Length - 1);
                }

                // we'll infect one -> count that!
                infected++;
            }

            // infect or clean
            input[position] = !input[position];

            switch (direction)
            {
                case Direction.DOWN:
                    position.Y += 1;
                    break;
                case Direction.UP:
                    position.Y -= 1;
                    break;
                case Direction.LEFT:
                    position.X -= 1;
                    break;
                case Direction.RIGHT:
                    position.X += 1;
                    break;
            }
        }

        return infected;
    }

    public override object ExecutePart2()
    {
        var inputArray = Input
            .Select(s => s.Select(c => c == '#' ? NodeState.INFECTED : NodeState.CLEAN).ToArray()).ToArray();
        var input = new Dictionary<Point, NodeState>();

        // fill dictionary
        for (var y = 0; y < inputArray.Length; y++)
        {
            for (var x = 0; x < inputArray[y].Length; x++)
            {
                input.Add(new Point(x, y), inputArray[y][x]);
            }
        }

        var directionsArray = Enum.GetValues(typeof(Direction));
        var direction = Direction.UP;
        var position = new Point(inputArray[0].Length / 2, inputArray.Length / 2);

        var infected = 0;
        for (var generaLIndex = 0; generaLIndex < 10_000_000; generaLIndex++)
        {
            if (!input.ContainsKey(position))
            {
                input.Add(new Point(position.X, position.Y), NodeState.CLEAN);
            }

            var nextState = NodeState.CLEAN;
            // if is infected
            switch (input[position])
            {
                case NodeState.FLAGGED:
                    nextState = NodeState.CLEAN;
                    if ((int)(direction += 2) >= directionsArray.Length)
                    {
                        direction -= directionsArray.Length;
                    }

                    break;
                case NodeState.INFECTED:
                    nextState = NodeState.FLAGGED;
                    if ((int)++direction >= directionsArray.Length)
                    {
                        direction = 0;
                    }

                    break;
                case NodeState.WEAKENED:
                    nextState = NodeState.INFECTED;
                    // continues moving forward
                    // we'll infect one -> count that!
                    infected++;
                    break;
                case NodeState.CLEAN:
                    nextState = NodeState.WEAKENED;
                    if ((int)--direction < 0)
                    {
                        direction = (Direction)directionsArray.GetValue(directionsArray.Length - 1);
                    }

                    break;
            }

            // infect or clean
            input[position] = nextState;

            switch (direction)
            {
                case Direction.DOWN:
                    position.Y += 1;
                    break;
                case Direction.UP:
                    position.Y -= 1;
                    break;
                case Direction.LEFT:
                    position.X -= 1;
                    break;
                case Direction.RIGHT:
                    position.X += 1;
                    break;
            }
        }

        return infected;
    }

    private enum Direction
    {
        UP = 0,
        RIGHT = 1,
        DOWN = 2,
        LEFT = 3
    }

    private enum NodeState
    {
        CLEAN = 0,
        WEAKENED = 1,
        INFECTED = 2,
        FLAGGED = 3
    }
}