using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day7 : Day
{
    public override long SolvePuzzle1(string[] input)
    {
        long sum = 0;
        Parallel.ForEach(input, new ParallelOptions { MaxDegreeOfParallelism = 1 }, line =>
        {
            var result = long.Parse(line[..line.IndexOf(':')]);
            var calibrationValues = line.Split(' ')[1..].Select(long.Parse).ToArray();

            var numberOfCalculations = Math.Pow(2, calibrationValues.Length - 1);
            for (var i = 0; i < numberOfCalculations; i++)
            {
                var calculatedResult = calibrationValues[0];
                for (var j = 0; j < calibrationValues.Length - 1; j++)
                {
                    var bitIstGesetzt = (i & (1 << j)) != 0;
                    calculatedResult = bitIstGesetzt
                        ? calculatedResult * calibrationValues[j + 1]
                        : calculatedResult + calibrationValues[j + 1];
                }

                if (calculatedResult == result)
                {
                    // result found
                    Interlocked.Add(ref sum, result);
                    break;
                }
            }
        });

        return sum;
    }

    public override long SolvePuzzle2(string[] input)
    {
        long sum = 0;
        Parallel.ForEach(input, new ParallelOptions { MaxDegreeOfParallelism = 1 }, line =>
        {
            var result = long.Parse(line[..line.IndexOf(':')]);
            var calibrationValues = line.Split(' ')[1..].Select(long.Parse).ToArray();

            var numberOfCalculations = Math.Pow(3, calibrationValues.Length - 1);
            for (var i = 0; i < numberOfCalculations; i++)
            {
                var calculatedResult = calibrationValues[0];
                for (var j = 0; j < calibrationValues.Length - 1; j++)
                {
                    var operation = i / (int)Math.Pow(3, j) % 3;
                    if (operation == 0)
                    {
                        calculatedResult += calibrationValues[j + 1];
                    }
                    else if (operation == 1)
                    {
                        calculatedResult *= calibrationValues[j + 1];
                    }
                    else
                    {
                        calculatedResult = long.Parse(calculatedResult + calibrationValues[j + 1].ToString());
                    }
                }

                if (calculatedResult == result)
                {
                    // result found
                    Interlocked.Add(ref sum, result);
                    break;
                }
            }
        });

        return sum;
    }
}