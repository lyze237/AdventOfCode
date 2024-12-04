using AoC.Framework;

namespace AoC._2024;

public class Day4() : Day<char[][]>(2024, 4)
{
    private readonly List<(int, int)> directions =
    [
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
        (1, 1),
        (1, -1),
        (-1, -1),
        (-1, 1)
    ];

    protected override object DoPart1(char[][] input)
    {
        const string pattern = "XMAS";
        var totalCount = 0;

        for (var row = 0; row < input.Length; row++)
            for (var col = 0; col < input[row].Length; col++)
                totalCount += directions.Count(direction => IsValid(input, row, col, pattern, direction));

        return totalCount;
    }

    private static bool IsValid(char[][] input, int startRow, int startCol, string pattern, (int dX, int dY) direction)
    {
        int i;
        for (i = 0; i < pattern.Length; i++)
        {
            var newRow = startRow + i * direction.dX;
            var newCol = startCol + i * direction.dY;

            if (newRow < 0 || newRow >= input.Length || newCol < 0 || newCol >= input[startRow].Length)
                break;
            if (input[newRow][newCol] != pattern[i])
                break;
        }

        return i == pattern.Length;
    }

    protected override object DoPart2(char[][] input)
    {
        string[] patterns = ["MAS", "SAM"];
        var totalCount = 0;

        for (var row = 1; row < input.Length - 1; row++)
        {
            for (var col = 1; col < input[row].Length - 1; col++)
            {
                // check any pattern combination
                foreach (var pattern1 in patterns)
                {
                    foreach (var pattern2 in patterns)
                    {
                        var isValidLeftToRight = IsDiagonal(input, row - 1, col - 1, pattern1, (1, 1));
                        var isValidRightToLeft = IsDiagonal(input, row + 1, col - 1, pattern2, (-1, 1));
                        if (isValidLeftToRight && isValidRightToLeft)
                            totalCount++;
                    }
                }
            }
        }

        return totalCount;
    }

    private static bool IsDiagonal(char[][] input, int startRow, int startCol, string pattern, (int dx, int dy) direction)
    {
        for (var i = 0; i < pattern.Length; i++)
        {
            var newRow = startRow + i * direction.dx;
            var newCol = startCol + i * direction.dy;

            if (newRow < 0 || newRow >= input.Length || newCol < 0 || newCol >= input[0].Length)
                return false;

            if (input[newRow][newCol] != pattern[i])
                return false;
        }

        return true;
    }

    protected override char[][] ParseInput(string input) =>
        input.Split("\n").Select(line => line.ToCharArray()).ToArray();
}