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
        var implementations = _implementations.OrderByDescending(i => i.GetType().Name);
        foreach (var implementation in implementations)
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
            sw.Start();
            var resultPuzzle1 = lastImplementation.SolvePuzzle1(await GetInput(dayNumber));
            sw.Stop();
            Console.WriteLine($"Day {dayNumber} Puzzle 1: {resultPuzzle1} took: {sw.Elapsed} | {sw.ElapsedMilliseconds}ms");

            var puzzle2HasDifferentInput = File.Exists($"inputs/day{dayNumber}_{2}.txt");
            sw.Reset();
            sw.Start();
            var resultPuzzle2 =
                lastImplementation.SolvePuzzle2(await GetInput(dayNumber, puzzle2HasDifferentInput ? 2 : 1));
            sw.Stop();
            Console.WriteLine($"Day {dayNumber} Puzzle 2: {resultPuzzle2} took: {sw.Elapsed} | {sw.ElapsedMilliseconds}ms");
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