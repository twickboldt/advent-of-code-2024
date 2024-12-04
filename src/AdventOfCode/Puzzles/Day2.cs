using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class Day2 : Day
{
    public override int SolvePuzzle1(string[] lines)
    {
        return lines.Count(line => IsReportValid(SplitLine(line)));
    }

    public override int SolvePuzzle2(string[] lines)
    {
        var count = 0;
        foreach (var line in lines)
        {
            var levels = SplitLine(line);
            if (IsReportValid(levels))
            {
                count++;
                continue;
            }
            
            for (var i = 0; i < levels.Length; i++)
            {
                var levelsWithoutLevelAtIndex = GetLevelsWithoutLevelAtIndex(levels, i).ToArray();
                if (IsReportValid(levelsWithoutLevelAtIndex))
                {
                    count++;
                    break;
                }
            }
        }

        return count;
    }

    private static bool IsReportValid(int[] levels)
    {
        var isIncrease = levels[0] > levels[1];

        int? previous = null;
        for (var i = 0; i < levels.Length; i++)
        {
            var level = levels[i];
            if (previous != null)
            {
                var diff = previous.Value - level;
                var isValid = isIncrease ? diff > 0 : diff < 0;
                if (!isValid)
                {
                    return false;
                }

                var absDiff = Math.Abs(diff);
                if (absDiff is < 1 or > 3)
                {
                    return false;
                }
            }

            previous = level;
        }

        return true;
    }

    private static IEnumerable<int> GetLevelsWithoutLevelAtIndex(int[] levels, int index)
    {
        return levels.Where((_, i) => i != index);
    }

    private static int[] SplitLine(string line)
    {
        var levels = line.TrimEnd('\n').Split(' ').Select(int.Parse).ToArray();
        return levels;
    }
}