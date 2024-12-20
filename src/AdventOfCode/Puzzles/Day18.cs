using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Day18(int gridSize, int bytesToFall) : Day
{
    public Day18() : this(71, 1024)
    {
    }

    public override long SolvePuzzle1(string[] input)
    {
        var corruptedBytes = ParseInput(input).Take(bytesToFall).ToArray();
        var queue = new Queue<WalkingPath>([new WalkingPath([new Position(0, 0)])]);

        var cache = new Dictionary<Position, int>();
        var bestStepCount = int.MaxValue;
        while (queue.Count != 0)
        {
            var path = queue.Dequeue();

            if (path.StepCount > bestStepCount)
            {
                continue;
            }

            var lastPosition = path.Path.First();
            if (lastPosition.X == gridSize - 1 && lastPosition.Y == gridSize - 1)
            {
                if (bestStepCount > path.StepCount)
                {
                    bestStepCount = path.StepCount;
                }

                continue;
            }

            if (cache.TryGetValue(lastPosition, out var stepCount) && stepCount <= path.StepCount)
            {
                continue;
            }

            cache[lastPosition] = path.StepCount;

            // step forward
            var forward = lastPosition with { Y = lastPosition.Y - 1 };
            if (!path.Path.Contains(forward) && !corruptedBytes.Contains(forward) && forward.Y >= 0)
            {
                queue.Enqueue(new WalkingPath(path.Path.Push(forward)));
            }

            // step down
            var down = lastPosition with { Y = lastPosition.Y + 1 };
            if (!path.Path.Contains(down) && !corruptedBytes.Contains(down) && down.Y < gridSize)
            {
                queue.Enqueue(new WalkingPath(path.Path.Push(down)));
            }

            // step left
            var left = lastPosition with { X = lastPosition.X - 1 };
            if (!path.Path.Contains(left) && !corruptedBytes.Contains(left) && left.X >= 0)
            {
                queue.Enqueue(new WalkingPath(path.Path.Push(left)));
            }

            // step right
            var right = lastPosition with { X = lastPosition.X + 1 };
            if (!path.Path.Contains(right) && !corruptedBytes.Contains(right) && right.X < gridSize)
            {
                queue.Enqueue(new WalkingPath(path.Path.Push(right)));
            }
        }

        return bestStepCount - 1; // remove the start position from step count
    }

    public override long SolvePuzzle2(string[] input)
    {
        var corruptedBytes = ParseInput(input).ToArray();

        object lockObj = new();
        var corruptedByteThatCutsOff = int.MaxValue;

        Parallel.ForEach(Enumerable.Range(bytesToFall, corruptedBytes.Length - 1), numberOfBytes =>
        {
            if (!PathFound(corruptedBytes.Take(numberOfBytes).ToArray()))
            {
                if (numberOfBytes < corruptedByteThatCutsOff)
                {
                    lock (lockObj)
                    {
                        corruptedByteThatCutsOff = numberOfBytes;
                    }
                }
            }
        });

        var cutoff = corruptedBytes[corruptedByteThatCutsOff-1];
        Console.WriteLine($"PathBlock found: {cutoff.X},{cutoff.Y}");

        return 1;
    }

    private bool PathFound(Position[] corruptedBytes)
    {
        var queue = new Queue<WalkingPath>([new WalkingPath([new Position(0, 0)])]);

        var pathFound = false;
        var cache = new Dictionary<Position, int>();
        while (queue.Count != 0)
        {
            var path = queue.Dequeue();

            var lastPosition = path.Path.First();
            if (lastPosition.X == gridSize - 1 && lastPosition.Y == gridSize - 1)
            {
                pathFound = true;
                break;
            }

            if (cache.TryGetValue(lastPosition, out var stepCount) && stepCount <= path.StepCount)
            {
                continue;
            }

            cache[lastPosition] = path.StepCount;

            // step forward
            var forward = lastPosition with { Y = lastPosition.Y - 1 };
            if (!path.Path.Contains(forward) && !corruptedBytes.Contains(forward) && forward.Y >= 0)
            {
                queue.Enqueue(new WalkingPath(path.Path.Push(forward)));
            }

            // step down
            var down = lastPosition with { Y = lastPosition.Y + 1 };
            if (!path.Path.Contains(down) && !corruptedBytes.Contains(down) && down.Y < gridSize)
            {
                queue.Enqueue(new WalkingPath(path.Path.Push(down)));
            }

            // step left
            var left = lastPosition with { X = lastPosition.X - 1 };
            if (!path.Path.Contains(left) && !corruptedBytes.Contains(left) && left.X >= 0)
            {
                queue.Enqueue(new WalkingPath(path.Path.Push(left)));
            }

            // step right
            var right = lastPosition with { X = lastPosition.X + 1 };
            if (!path.Path.Contains(right) && !corruptedBytes.Contains(right) && right.X < gridSize)
            {
                queue.Enqueue(new WalkingPath(path.Path.Push(right)));
            }
        }

        return pathFound;
    }

    private static IEnumerable<Position> ParseInput(string[] input)
    {
        return input.Select(line => line.Split(','))
            .Select(split => new Position(int.Parse(split[0]), int.Parse(split[1])));
    }

    private record Position(int X, int Y);

    private record WalkingPath(ImmutableStack<Position> Path)
    {
        public int StepCount => Path.Count();
    }
}