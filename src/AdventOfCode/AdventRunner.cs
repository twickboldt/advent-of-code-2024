using System.Diagnostics;
using AdventOfCode.Common;

namespace AdventOfCode;

public class AdventRunner
{
    private readonly IEnumerable<Day> _implementations;

    public AdventRunner(IEnumerable<Day> implementations)
    {
        _implementations = implementations;
    }

    public async Task Run()
    {
        var lastImplementation = _implementations.OrderByDescending(i => i.GetType().Name).First();
        var dayNumber = int.Parse(lastImplementation.GetType().Name.Replace("Day", ""));
        try
        {
            var sw = new Stopwatch();
            sw.Start();
            var resultPuzzle1 = lastImplementation.SolvePuzzle1(await GetInput(dayNumber));
            sw.Stop();
            Console.WriteLine($"Day {dayNumber} Puzzle 1: {resultPuzzle1} took: {sw.Elapsed}");

            var puzzle2HasDifferentInput = File.Exists($"inputs/day{dayNumber}_{2}.txt");
            sw.Reset();
            sw.Start();
            var resultPuzzle2 =
                lastImplementation.SolvePuzzle2(await GetInput(dayNumber, puzzle2HasDifferentInput ? 2 : 1));
            sw.Stop();
            Console.WriteLine($"Day {dayNumber} Puzzle 2: {resultPuzzle2} took: {sw.Elapsed}");
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private static async Task<string[]> GetInput(int day, int puzzle = 1)
    {
        var input = await File.ReadAllLinesAsync($"inputs/day{day}_{puzzle}.txt");

        return input;
    }
}