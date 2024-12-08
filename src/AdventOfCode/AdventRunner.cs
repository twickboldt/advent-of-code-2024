using System.Diagnostics;
using AdventOfCode.Common;

namespace AdventOfCode;

public class AdventRunner(IEnumerable<Day> implementations)
{
    public async Task Run()
    {
        var implementations1 = implementations.OrderByDescending(i => i.GetType().Name);
        foreach (var implementation in implementations1)
        {
            var dayNumber = int.Parse(implementation.GetType().Name.Replace("Day", ""));
            await ExecuteSolver(implementation, dayNumber);
            Console.WriteLine();
        }
    }

    private static async Task ExecuteSolver(Day lastImplementation, int dayNumber)
    {
        try
        {
            var sw = new Stopwatch();
            var input = await GetInput(dayNumber);
            sw.Start();
            var result = lastImplementation.SolvePuzzle1(input);
            sw.Stop();
            WriteResult(dayNumber, 1, result, sw);

            var puzzle2HasDifferentInput = File.Exists($"inputs/day{dayNumber}_{2}.txt");
            sw.Reset();
            input = await GetInput(dayNumber, puzzle2HasDifferentInput ? 2 : 1);
            sw.Start();
            result =
                lastImplementation.SolvePuzzle2(input);
            sw.Stop();
            WriteResult(dayNumber, 2, result, sw);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private static void WriteResult(int dayNumber, int puzzleNumber, long result, Stopwatch sw)
    {
        var timeString = sw.Elapsed.TotalMicroseconds > 1000
            ? $"{sw.Elapsed.TotalMilliseconds}ms"
            : $"{sw.Elapsed.TotalMicroseconds}µs";
        Console.WriteLine($"Day {dayNumber} Puzzle {puzzleNumber}: {result} took: {timeString}");
    }

    private static async Task<string[]> GetInput(int day, int puzzle = 1)
    {
        var input = await File.ReadAllLinesAsync($"inputs/day{day}_{puzzle}.txt");

        return input;
    }
}