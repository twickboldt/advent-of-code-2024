using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Day20(int minimumPathSaving) : Day
{
    public Day20() : this(100)
    {
    }

    public override long SolvePuzzle1(string[] input)
    {
        return Solve(input, 2);
    }

    public override long SolvePuzzle2(string[] input)
    {
        return Solve(input, 20);
    }

    private long Solve(string[] input, int maxDiff)
    {
        var (start, end) = GetStartAndEnd(input);

        var path = GetPath(start, end, input).Reverse().ToArray();

        var cheatCount = 0;
        for (var i = 0; i < path.Length - minimumPathSaving; i++)
        {
            var position = path.ElementAt(i);
            cheatCount += path.Skip(i + minimumPathSaving).Where((andere, index) =>
            {
                var diff = Math.Abs(position.X - andere.X) + Math.Abs(position.Y - andere.Y);

                // cheat can't be active for too long and has to shortcut enough fields
                return diff <= maxDiff && diff <= index;
            }).Count();
        }

        return cheatCount;
    }

    private static ImmutableStack<Position> GetPath(Position start, Position end, string[] input)
    {
        var queue = new Queue<WalkingPath>([new WalkingPath([start], true)]);

        var cache = new Dictionary<(Position, bool), int>();
        ImmutableStack<Position> bestPath = [];
        var bestPathLength = int.MaxValue;
        while (queue.Count != 0)
        {
            var path = queue.Dequeue();

            if (path.Steps > bestPathLength)
            {
                continue;
            }

            foreach (var nextPosition in path.NextSteps(input.Length - 1))
            {
                if (path.Steps > bestPathLength)
                {
                    continue;
                }

                if (path.Path.First() == end)
                {
                    if (bestPathLength > path.Steps)
                    {
                        bestPath = path.Path;
                        bestPathLength = path.Steps;
                    }
                }

                var newPath = path with { Path = path.Path.Push(nextPosition) };
                if (input[nextPosition.Y][nextPosition.X] == '#')
                {
                    continue;
                }

                if (cache.TryGetValue((nextPosition, path.CheatUsed), out var steps) && steps < newPath.Steps)
                {
                    continue;
                }

                cache[(nextPosition, newPath.CheatUsed)] = newPath.Steps;
                queue.Enqueue(newPath);
            }
        }

        // return bestPathLength;
        return bestPath;
    }

    private static (Position Start, Position End) GetStartAndEnd(string[] input)
    {
        Position? start = null;
        Position? end = null;
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == 'S')
                {
                    start = new Position(x, y);
                    if (end != null)
                    {
                        return (start, end);
                    }
                }

                if (input[y][x] == 'E')
                {
                    end = new Position(x, y);
                    if (start != null)
                    {
                        return (start, end);
                    }
                }
            }
        }

        throw new Exception("Start or end not found");
    }

    private record Position(int X, int Y);

    private record WalkingPath(ImmutableStack<Position> Path, bool CheatUsed)
    {
        public int Steps => Path.Count() - 1;

        public IEnumerable<Position> NextSteps(int gridSize)
        {
            var lastPosition = Path.First();

            // up
            var nextStep = lastPosition with { Y = lastPosition.Y - 1 };
            if (nextStep.Y >= 0 && !Path.Contains(nextStep))
            {
                yield return nextStep;
            }

            // down
            nextStep = lastPosition with { Y = lastPosition.Y + 1 };
            if (nextStep.Y <= gridSize && !Path.Contains(nextStep))
            {
                yield return nextStep;
            }

            // left
            nextStep = lastPosition with { X = lastPosition.X - 1 };
            if (nextStep.X >= 0 && !Path.Contains(nextStep))
            {
                yield return nextStep;
            }

            // right
            nextStep = lastPosition with { X = lastPosition.X + 1 };
            if (nextStep.X <= gridSize && !Path.Contains(nextStep))
            {
                yield return nextStep;
            }
        }
    }
}