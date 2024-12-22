using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day22 : Day
{
    private readonly long _numberOfSecretsToGenerate = 2000;

    public override long SolvePuzzle1(string[] input)
    {
        var result = 0L;
        Parallel.ForEach(input, line =>
        {
            var secret = CalculateSecretPart1(long.Parse(line));
            Interlocked.Add(ref result, secret);
        });
        return result;
    }

    public override long SolvePuzzle2(string[] input)
    {
        var mostBananasToGet = input.AsParallel().Select(line => CalculateBananaPrices(long.Parse(line)))
            .SelectMany(bananaPrices => bananaPrices).GroupBy(b => b.Key).Select(b => b.Sum(x => x.Value))
            .OrderByDescending(b => b)
            .First();

        return mostBananasToGet;
    }
    
    private Dictionary<string, long> CalculateBananaPrices(long secret)
    {
        var priceDiffs = new int[_numberOfSecretsToGenerate -1];
        var cache = new Dictionary<string, long>();
        var previousPrice = 0;
        for (var i = 0; i < _numberOfSecretsToGenerate; i++)
        {
            var y = secret * 64;
            secret ^= y;
            secret %= 16777216;

            y = secret / 32;
            secret ^= y;
            secret %= 16777216;

            y = secret * 2048;
            secret ^= y;
            secret %= 16777216;

            var currentPrice = (int) secret % 10;
            if (i > 0)
            {
                var priceDiff = currentPrice - previousPrice;
                priceDiffs[i - 1] = priceDiff;
                if (i >= 4)
                {
                    var changeSequence = string.Join(",", priceDiffs.Skip(i - 4).Take(4));
                    cache.TryAdd(changeSequence, currentPrice);
                }
            }

            previousPrice = currentPrice;
        }

        return cache;
    }

    private long CalculateSecretPart1(long secret)
    {
        for (var i = 0; i < _numberOfSecretsToGenerate; i++)
        {
            var y = secret * 64;
            secret ^= y;
            secret %= 16777216;

            y = secret / 32;
            secret ^= y;
            secret %= 16777216;

            y = secret * 2048;
            secret ^= y;
            secret %= 16777216;
        }

        return secret;
    }
}