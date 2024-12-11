using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

using Cache = Dictionary<(long, int), long>;

public class Day11 : Day
{
    public override long SolvePuzzle1(string[] input)
    {
        var inputNumbers = input[0].Split(' ').Select(long.Parse).ToArray();

        return GetNumberOfStonesForBlinks(inputNumbers, 25);
    }

    public override long SolvePuzzle2(string[] input)
    {
        var inputNumbers = input[0].Split(' ').Select(long.Parse).ToArray();

        return GetNumberOfStonesForBlinks(inputNumbers, 75);
    }

    private static long GetNumberOfStonesForBlinks(long[] stones, int numberOfBlinks)
    {
        long count = 0;
        var cache = new Cache();
        foreach (var stone in stones)
        {
            count += CountStones(stone, numberOfBlinks, cache);
        }

        return count;
    }
    
    private static long CountStones(long input, int numberOfBlinks, Cache cache)
    {
        if (cache.TryGetValue((input, numberOfBlinks), out var cachedValue))
        {
            return cachedValue;
        }

        if (numberOfBlinks == 1)
        {
            return Blink(input).Length;
        }

        var newStones = Blink(input);
        var numberOfStones = newStones.Sum(stone => CountStones(stone, numberOfBlinks - 1, cache));
        cache[(input, numberOfBlinks)] = numberOfStones;
        return numberOfStones;
    }
    
    private static long[] Blink(long input)
    {
        if (input == 0)
        {
            return [1];
        }

        var stringNumber = input.ToString();
        if (stringNumber.Length % 2 == 0)
        {
            var halfLength = stringNumber.Length / 2;
            return [long.Parse(stringNumber[..halfLength]), long.Parse(stringNumber[halfLength..])];
        }

        return [input * 2024];
    }
}