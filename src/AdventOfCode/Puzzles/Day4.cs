using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day4 : Day
{
    public override int SolvePuzzle1(string[] input)
    {
        var xPositions = GetLetterPositions(input, 'X').ToArray();
        var mPositions = GetLetterPositions(input, 'M').ToArray();

        var numberOfXmases = 0;
        foreach (var xPosition in xPositions)
        {
            var adjacentMPositions = mPositions.Where(m => m.IsAdjacent(xPosition));
            foreach (var adjacentMPosition in adjacentMPositions)
            {
                var diff = adjacentMPosition.Diff(xPosition);
                var potentialAPosition = adjacentMPosition.Add(diff);
                var potentialSPosition = potentialAPosition.Add(diff);
                if (GetLetter(input, potentialAPosition) == 'A' &&
                    GetLetter(input, potentialSPosition) == 'S')
                {
                    numberOfXmases++;
                }
            }
        }

        return numberOfXmases;
    }

    public override int SolvePuzzle2(string[] input)
    {
        var aPositions = GetLetterPositions(input, 'A').ToArray();
        var sPositions = GetLetterPositions(input, 'S').ToArray();
        var mPositions = GetLetterPositions(input, 'M').ToArray();

        var numberOfXmases = 0;
        foreach (var aPosition in aPositions)
        {
            var adjacentMPositions = mPositions.Where(m => m.IsDiagonallyAdjacent(aPosition))
                .Select(mPosition => mPosition.Diff(aPosition)).ToArray();
            var adjacentSPositions = sPositions.Where(s => s.IsDiagonallyAdjacent(aPosition))
                .Select(sPosition => sPosition.Diff(aPosition)).ToArray();

            if (adjacentMPositions.Length != 2 || adjacentSPositions.Length != 2)
            {
                continue;
            }

            if (adjacentSPositions.Any(sPosition =>
                    sPosition.X == adjacentMPositions[0].X * -1 && sPosition.Y == adjacentMPositions[0].Y * -1))
            {
                numberOfXmases++;
            }
        }

        return numberOfXmases;
    }

    private static IEnumerable<Position> GetLetterPositions(string[] input, char letter)
    {
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var minIndex = line.IndexOf(letter);
            while (minIndex > -1)
            {
                yield return new Position(minIndex, i);
                minIndex = line.IndexOf(letter, minIndex + 1);
            }
        }
    }

    private static char GetLetter(string[] input, Position position)
    {
        try
        {
            if (input.Length >= position.Y && input[position.Y].Length >= position.X)
            {
                return input[position.Y][position.X];
            }
        }
        catch (Exception)
        {
            return '_';
        }

        return '_';
    }
}

public record Position(int X, int Y)
{
    public bool IsAdjacent(Position other)
    {
        return Math.Abs(X - other.X) <= 1 && Math.Abs(Y - other.Y) <= 1;
    }

    public bool IsDiagonallyAdjacent(Position other)
    {
        return Math.Abs(X - other.X) == 1 && Math.Abs(Y - other.Y) == 1;
    }

    public Position Diff(Position other)
    {
        return new Position(X - other.X, Y - other.Y);
    }

    public Position Add(Position other)
    {
        return new Position(X + other.X, Y + other.Y);
    }
}