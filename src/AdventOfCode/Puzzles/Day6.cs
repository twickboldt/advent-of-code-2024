using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day6 : Day
{
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

    public override int SolvePuzzle2(string[] input)
    {
        var (obstaclePositions, guardPosition) = GetStartingPositions(input);

        var counter = 0;
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                var newObstacle = new Position(x, y);
                if (obstaclePositions.Contains(newObstacle))
                {
                    continue;
                }
                var temperedObstacles = new List<Position>(obstaclePositions) { newObstacle };
                if (CausesLoop(temperedObstacles, guardPosition, input))
                {
                    counter++;
                }
            }

            Console.WriteLine($"Ran line {y}");
        }

        return counter;
    }

    private static (List<Position>, Position) GetStartingPositions(string[] input)
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

    private static bool CausesLoop(List<Position> obstaclePositions, Position guardPosition, string[] input)
    {
        var guardDirection = Direction.Up;
        // var allGuardPositions = new List<Position>([guardPosition]);
        var runLoopDetection = false;
        var positionsSinceLastLoopDetection = new List<Position>();
        var positionsSinceLastLoopDetection2 = new List<Position>();
        var stepCounter = 0;
        var maxSteps = input.Length * input[0].Length;
        while (true)
        {
            var newPosition = Move(guardPosition, guardDirection);

            while (obstaclePositions.Contains(newPosition))
            {
                guardDirection = TurnRight(guardDirection);
                newPosition = Move(guardPosition, guardDirection);
                if (guardDirection == Direction.Up)
                {
                    runLoopDetection = true;
                }
            }

            guardPosition = newPosition;

            // guard has left the area
            if (!IsInBounds(input, newPosition))
            {
                break;
            }

            positionsSinceLastLoopDetection.Add(guardPosition);

            if (runLoopDetection)
            {
                // var loopDetected = positionsSinceLastLoopDetection.Count > 0 &&
                //                    positionsSinceLastLoopDetection.SequenceEqual(positionsSinceLastLoopDetection2);
                var loopDetected = stepCounter > maxSteps;

                if (loopDetected)
                {
                    return true;
                }
                
                positionsSinceLastLoopDetection2 = [..positionsSinceLastLoopDetection];
                positionsSinceLastLoopDetection.Clear();
                
                runLoopDetection = false;
            }

            stepCounter++;
        }

        return false;
    }

    private static bool IsInBounds(string[] input, Position position)
    {
        return position is { X: >= 0, Y: >= 0 } && input.Length > position.Y && input[position.Y].Length > position.X;
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
}