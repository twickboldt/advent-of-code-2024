using System.Text;
using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day8 : Day
{
    public override long SolvePuzzle1(string[] input)
    {
        var antennaPositions = new List<(char Character, int X, int Y)>();
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                var character = input[y][x];
                if (character == '.')
                {
                    continue;
                }

                antennaPositions.Add((character, x, y));
            }
        }

        var antennaPositionsByFrequency = antennaPositions.GroupBy(p => p.Character);
        var antinodes = new List<(int X, int Y)>();
        foreach (var positions in antennaPositionsByFrequency)
        {
            antinodes.AddRange(GetAntinodes(positions.ToArray(), input.Length - 1));
        }

        return antinodes.Distinct().Count();
    }

    private static List<(int X, int Y)> GetAntinodes((char Character, int X, int Y)[] positions, int maxPosition)
    {
        var antinodes = new List<(int X, int Y)>();
        for (var i = 0; i < positions.Length; i++)
        {
            var position1 = positions[i];
            for (var j = i + 1; j < positions.Length; j++)
            {
                var position2 = positions[j];
                var xDiff = position1.X - position2.X;
                var yDiff = position1.Y - position2.Y;
                var antinodeX = position1.X + xDiff;
                var antinodeY = position1.Y + yDiff;
                if (antinodeX >= 0 && antinodeX <= maxPosition && antinodeY >= 0 && antinodeY <= maxPosition)
                {
                    antinodes.Add((antinodeX, antinodeY));
                }

                antinodeX = position2.X + xDiff * -1;
                antinodeY = position2.Y + yDiff * -1;

                if (antinodeX >= 0 && antinodeX <= maxPosition && antinodeY >= 0 && antinodeY <= maxPosition)
                {
                    antinodes.Add((antinodeX, antinodeY));
                }
            }
        }

        return antinodes;
    }


    public override long SolvePuzzle2(string[] input)
    {
        throw new NotImplementedException();
    }
}