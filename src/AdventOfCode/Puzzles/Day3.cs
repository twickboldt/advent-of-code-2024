using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
public class Day3 : Day
{
    public override long SolvePuzzle1(string[] lines)
    {
        var sum = 0;
        foreach (var line in lines)
        {
            var matches = Regex.Matches(line, @"mul\((\d+),(\d+)\)");

            foreach (Match match in matches)
            {
                var mulResult = int.Parse(match.Groups[^2].Value) * int.Parse(match.Groups[^1].Value);
                sum += mulResult;
            }
        }

        return sum;
    }

    public override long SolvePuzzle2(string[] lines)
    {
        var input = string.Join("", lines);
        var cleanedInput = Regex.Replace(input, @"don't\(\).*?do\(\)", "");
        return SolvePuzzle1([cleanedInput]);
    }
}