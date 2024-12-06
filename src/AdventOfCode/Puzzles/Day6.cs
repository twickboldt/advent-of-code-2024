using System.Diagnostics;
using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day6 : Day
{
    public override int SolvePuzzle2(string[] input)
    {
        var sw = Stopwatch.StartNew();
        var (obstaclePositions, guardPosition, newObstaclePositions) = GetStartingPositions2(input);

        var counter = 0;

        Parallel.ForEach(newObstaclePositions, newObstacle =>
        {
            if (CausesLoop(obstaclePositions, newObstacle, guardPosition, input))
            {
                counter++;
            }
        });

        sw.Stop();
        Console.WriteLine($"Runtime: {sw.Elapsed}ms");
        return counter;
    }

    private static (SortedSet<ushort> obstaclePositions, ushort guardPosition, List<ushort> newObstaclePositions)
        GetStartingPositions2(string[] input)
    {
        var obstaclePositions = new List<ushort>();
        var newObstaclePositions = new List<ushort>();
        ushort guardPosition = 0;
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                var pos = (ushort)((byte)x << 8 | (byte)y);
                switch (input[y][x])
                {
                    case '#':
                        obstaclePositions.Add(pos);
                        break;
                    case '^':
                        guardPosition = pos;
                        break;
                    default:
                        newObstaclePositions.Add(pos);
                        break;
                }
            }
        }

        return (new SortedSet<ushort>(obstaclePositions), guardPosition, newObstaclePositions);
    }

    private static (List<Position> obstaclePositions, Position guardPosition)
        GetStartingPositions(string[] input)
    {
        var obstaclePositions = new List<Position>();
        var guardPosition = new Position(0, 0);
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                switch (input[y][x])
                {
                    case '#':
                        obstaclePositions.Add(new Position(x, y));
                        break;
                    case '^':
                        guardPosition = new Position(x, y);
                        break;
                }
            }
        }

        return (obstaclePositions, guardPosition);
    }

    private static bool CausesLoop(SortedSet<ushort> obstaclePositions, ushort placedObstacle, ushort guardPosition,
        string[] input)
    {
        var guardDirection = Direction.Up;
        var guardPositions = new List<int>();
        while (true)
        {
            var newPosition = Move(guardPosition, guardDirection);

            while (obstaclePositions.Contains(newPosition) || newPosition == placedObstacle)
            {
                guardDirection = TurnRight(guardDirection);
                newPosition = Move(guardPosition, guardDirection);
                var newPositionAndDirection = newPosition << 16 | (byte)guardDirection;
                if (guardPositions.Contains(newPositionAndDirection))
                {
                    return true;
                }
                
                guardPositions.Add(newPositionAndDirection);
            }

            guardPosition = newPosition;

            // guard has left the area
            if (!IsInBounds(input, newPosition))
            {
                break;
            }
        }

        return false;
    }

    private static bool IsInBounds(string[] input, ushort position)
    {
        var x = (byte)(position >> 8);
        var y = (byte)position;
        return x != byte.MaxValue && y != byte.MaxValue && input.Length > y && input[y].Length > x;
    }

    private static bool IsInBounds(string[] input, Position position)
    {
        return position is { X: >= 0, Y: >= 0 } && input.Length > position.Y && input[position.Y].Length > position.X;
    }

    private static ushort Move(ushort position, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return (ushort)(position - 1);
            case Direction.Right:
                return (ushort)(position + 256);
            case Direction.Down:
                return (ushort)(position + 1);
            case Direction.Left:
                return (ushort)(position - 256);
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    private static Position Move(Position position, Direction direction)
    {
        return direction switch
        {
            Direction.Up => position with { Y = position.Y - 1 },
            Direction.Right => position with { X = position.X + 1 },
            Direction.Down => position with { Y = position.Y + 1 },
            Direction.Left => position with { X = position.X - 1 },
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    private static Direction TurnRight(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public record Position(int X, int Y);

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public override int SolvePuzzle1(string[] input)
    {
        var (obstaclePositions, guardPosition) = GetStartingPositions(input);

        var guardDirection = Direction.Up;
        var allGuardPositions = new List<Position>([guardPosition]);
        while (true)
        {
            var newPosition = Move(guardPosition, guardDirection);

            while (obstaclePositions.Contains(newPosition))
            {
                guardDirection = TurnRight(guardDirection);
                newPosition = Move(guardPosition, guardDirection);
            }

            guardPosition = newPosition;

            // guard has left the area
            if (!IsInBounds(input, newPosition))
            {
                break;
            }

            allGuardPositions.Add(guardPosition);
        }

        return allGuardPositions.Distinct().Count();
    }
}