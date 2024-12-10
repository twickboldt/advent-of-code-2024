using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day10 : Day
{
    public override long SolvePuzzle1(string[] input)
    {
        return GetNumberOfPaths(input);
    }

    public override long SolvePuzzle2(string[] input)
    {
        return GetNumberOfPaths(input, false);
    }

    private static long GetNumberOfPaths(string[] input, bool distinct = true)
    {
        var map = input.Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

        var startingPositions = GetPositionsWithValue(map, 0);

        var numberOfPaths = 0;
        foreach (var startingPosition in startingPositions)
        {
            var valueToBeFound = 1;
            var hikerPositions = new[] { startingPosition };
            do
            {
                hikerPositions = hikerPositions.SelectMany(hikerPosition =>
                    GetSurroundingPositionsWithValue(hikerPosition, map, valueToBeFound)).ToArray();
                valueToBeFound++;
            } while (hikerPositions.Length > 0 && valueToBeFound <= 9);

            numberOfPaths += distinct ? hikerPositions.Distinct().Count() : hikerPositions.Length;
        }

        return numberOfPaths;
    }

    private static IEnumerable<Position> GetPositionsWithValue(int[][] map, int valueToBeFound)
    {
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == valueToBeFound)
                {
                    yield return new Position(x, y);
                }
            }
        }
    }

    private static IEnumerable<Position> GetSurroundingPositionsWithValue(Position startPosition, int[][] map,
        int value)
    {
        var gridSize = map.Length;
        var newPos = startPosition with { Y = startPosition.Y + 1 };
        if (IsInBounds(gridSize, newPos) && map[newPos.Y][newPos.X] == value)
        {
            yield return newPos;
        }

        newPos = startPosition with { Y = startPosition.Y - 1 };
        if (IsInBounds(gridSize, newPos) && map[newPos.Y][newPos.X] == value)
        {
            yield return newPos;
        }

        newPos = startPosition with { X = startPosition.X + 1 };
        if (IsInBounds(gridSize, newPos) && map[newPos.Y][newPos.X] == value)
        {
            yield return newPos;
        }

        newPos = startPosition with { X = startPosition.X -1 };
        if (IsInBounds(gridSize, newPos) && map[newPos.Y][newPos.X] == value)
        {
            yield return newPos;
        }
    }
    
    private static bool IsInBounds(int gridSize, Position position) =>
        position.X >= 0 && position.X < gridSize && position.Y >= 0 && position.Y < gridSize;

    private record Position(int X, int Y);
}