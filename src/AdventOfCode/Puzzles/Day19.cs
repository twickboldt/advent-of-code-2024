using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day19 : Day
{
    private readonly Dictionary<string, bool> _cache = new();
    
    public override long SolvePuzzle1(string[] input)
    {
        var (availableTowels, displayedPatterns) = ParseInput(input);

        var result = 0;
        foreach (var pattern in displayedPatterns)
        {
            if (CheckPattern(availableTowels, pattern))
            {
                result++;
            }
        }

        return result;
    }

    public override long SolvePuzzle2(string[] input)
    {
        throw new NotImplementedException();
    }
    
    private bool CheckPattern(string[] availableTowels, string pattern)
    {
        foreach (var towel in availableTowels)
        {
            if (_cache.TryGetValue(pattern, out var result))
            {
                return result;
            }
            
            if (pattern.StartsWith(towel))
            {
                var remainingPattern = pattern[towel.Length..];
                if (remainingPattern.Length == 0 || CheckPattern(availableTowels, remainingPattern))
                {
                    _cache[pattern] = true;
                    return true;
                }
            }
        }

        _cache.Add(pattern, false);
        return false;
    }

    private static (string[] availableTowels, string[] displayedPatterns) ParseInput(string[] input)
    {
        var availableTowels = input[0].Split(", ");
        var displayedPatterns = input[2..];
        return (availableTowels, displayedPatterns);
    }
}