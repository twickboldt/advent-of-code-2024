using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day14 : Day
{
    private const int MaxX = 101;
    private const int MaxY = 103;
    // private const int MaxX = 11;
    // private const int MaxY = 7;
    private const int NumberOfMoves = 100;
    private const int MiddleX = MaxX / 2;
    private const int MiddleY = MaxY / 2;

    public override long SolvePuzzle1(string[] input)
    {
        var robots = input.Select(Parse).ToArray();

        return robots.Select(robot => robot.Move(NumberOfMoves))
            .Where(p => p.Y != MiddleY || p.X != MiddleX)
            .GroupBy(p => p.Quadrant)
            .Where(p => p.Key != 0)
            .Aggregate(1, (current, robot) => current * robot.Count());
    }

    public override long SolvePuzzle2(string[] input)
    {
        throw new NotImplementedException();
    }

    private static Robot Parse(string input)
    {
        var split = input.Replace("p=", "").Replace("v=", "").Replace(" ", ",").Split(',');
        return new Robot(new Position(int.Parse(split[0]), int.Parse(split[1])),
            new Position(int.Parse(split[2]), int.Parse(split[3])));
    }

    private record Position(long X, long Y)
    {
        public int Quadrant => X switch
        {
            < MiddleX when Y < MiddleY => 1,
            < MiddleX when Y > MiddleY => 2,
            > MiddleX when Y < MiddleY => 3,
            > MiddleX when Y > MiddleY => 4,
            _ => 0
        };
    }

    private record Robot(Position Start, Position Velocity)
    {
        public Position Move(int numberOfMoves)
        {
            var movementY = Velocity.Y * numberOfMoves;
            var movementX = Velocity.X * numberOfMoves;
            var newX = (Start.X + movementX) % MaxX;
            var newY = (Start.Y + movementY) % MaxY;
            var newX1 = newX < 0 ? MaxX + newX : newX;
            var newY1 = newY < 0 ? MaxY + newY : newY;

            return new Position(newX1, newY1);
        }
    }
}