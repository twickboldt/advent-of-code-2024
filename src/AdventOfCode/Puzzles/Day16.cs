using System.Collections.Immutable;
using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day16 : Day
{
    public override long SolvePuzzle1(string[] input)
    {
        var (start, end) = GetStartAndEnd(input);

        var queue = new Queue<WalkingPath>([new WalkingPath([new PositionAndDirection(start, Direction.Right)])]);

        var bestPrice = int.MaxValue;
        var cache = new Dictionary<PositionAndDirection, int>();
        while (queue.Count != 0)
        {
            var path = queue.Dequeue();

            if (path.Price > bestPrice)
            {
                continue;
            }


            var lastPosition = path.Path.First();
            if (lastPosition.Position.IsSamePosition(end))
            {
                if (bestPrice > path.Price)
                {
                    bestPrice = path.Price;
                }

                continue;
            }


            var l = path.Left();
            var position = l.Path.First();
            if (input[position.Position.Y][position.Position.X] != '#' &&
                (!cache.TryGetValue(position, out var price) || l.Price < price))
            {
                cache[position] = l.Price;
                queue.Enqueue(l);
            }

            var r = path.Right();
            position = r.Path.First();
            if (input[position.Position.Y][position.Position.X] != '#' &&
                (!cache.TryGetValue(position, out price) || r.Price < price))
            {
                cache[position] = r.Price;
                queue.Enqueue(r);
            }

            var s = path.Step();
            position = s.Path.First();
            if (input[position.Position.Y][position.Position.X] != '#' &&
                (!cache.TryGetValue(position, out price) || s.Price < price))
            {
                cache[position] = s.Price;
                queue.Enqueue(s);
            }
        }

        return bestPrice;
    }

    public override long SolvePuzzle2(string[] input)
    {
        var (start, end) = GetStartAndEnd(input);

        var queue = new Queue<WalkingPath>([new WalkingPath([new PositionAndDirection(start, Direction.Right)])]);

        var bestPrice = int.MaxValue;
        var bestPaths = new List<WalkingPath>();
        var cache = new Dictionary<PositionAndDirection, int>();
        while (queue.Count != 0)
        {
            var path = queue.Dequeue();

            if (path.Price > bestPrice)
            {
                continue;
            }


            var lastPosition = path.Path.First();
            if (lastPosition.Position.IsSamePosition(end))
            {
                if (bestPrice == path.Price)
                {
                    bestPaths.Add(path);
                }
                if (bestPrice > path.Price)
                {
                    bestPrice = path.Price;
                    bestPaths = [path];
                }

                continue;
            }


            var l = path.Left();
            var position = l.Path.First();
            if (input[position.Position.Y][position.Position.X] != '#' &&
                (!cache.TryGetValue(position, out var price) || l.Price < price))
            {
                cache[position] = l.Price;
                queue.Enqueue(l);
            }

            var r = path.Right();
            position = r.Path.First();
            if (input[position.Position.Y][position.Position.X] != '#' &&
                (!cache.TryGetValue(position, out price) || r.Price < price))
            {
                cache[position] = r.Price;
                queue.Enqueue(r);
            }

            var s = path.Step();
            position = s.Path.First();
            if (input[position.Position.Y][position.Position.X] != '#' &&
                (!cache.TryGetValue(position, out price) || s.Price < price))
            {
                cache[position] = s.Price;
                queue.Enqueue(s);
            }
        }

        return bestPaths.SelectMany(path => path.Path.Select(p => p.Position)).Distinct().Count();
        // return bestPaths.SelectMany(path => path.Path.Select(p => (p.Position.X, p.Position.Y))).Distinct().Count();
    }

    private static (Position Start, Position End) GetStartAndEnd(string[] input)
    {
        var start = new Position(-1, -1);
        var end = new Position(-1, -1);
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == 'S')
                {
                    start = new Position(x, y);
                }

                if (input[y][x] == 'E')
                {
                    end = new Position(x, y);
                }
            }
        }

        return (start, end);
    }

    private static List<(PositionAndDirection, int)> GetValidDirections(string[] input, Position start,
        WalkingPath path)
    {
        var directions = new List<(PositionAndDirection, int)>();
        if (path.Direction != Direction.Up && input[start.Y + 1][start.X] != '#')
        {
            var newPos = (new PositionAndDirection(start with { Y = start.Y + 1 }, Direction.Down),
                path.Direction == Direction.Down ? 1 : 1001);
            if (!path.Path.Contains(newPos.Item1))
            {
                directions.Add(newPos);
            }
        }

        if (path.Direction != Direction.Down && input[start.Y - 1][start.X] != '#')
        {
            var newPos = (new PositionAndDirection(start with { Y = start.Y - 1 }, Direction.Up),
                path.Direction == Direction.Up ? 1 : 1001);
            if (!path.Path.Contains(newPos.Item1))
            {
                directions.Add(newPos);
            }
        }

        if (path.Direction != Direction.Right && input[start.Y][start.X - 1] != '#')
        {
            var newPos = (new PositionAndDirection(start with { X = start.X - 1 }, Direction.Left),
                path.Direction == Direction.Left ? 1 : 1001);
            if (!path.Path.Contains(newPos.Item1))
            {
                directions.Add(newPos);
            }
        }

        if (path.Direction != Direction.Left && input[start.Y][start.X + 1] != '#')
        {
            var newPos = (new PositionAndDirection(start with { X = start.X + 1 }, Direction.Right),
                path.Direction == Direction.Right ? 1 : 1001);
            if (!path.Path.Contains(newPos.Item1))
            {
                directions.Add(newPos);
            }
        }

        return directions;
    }

    private record Position(int X, int Y)
    {
        public bool IsSamePosition(Position position) => X == position.X && Y == position.Y;
    }

    private record PositionAndDirection(Position Position, Direction Direction) : IEquatable<PositionAndDirection>
    {
        public int Price { get; private set; } = 0;

        public PositionAndDirection Left()
        {
            // Price = 1000;
            return this with { Direction = (Direction)(((int)Direction + 3) % 4), Price = 1000 };
        }

        public PositionAndDirection Right()
        {
            // Price = 1000;
            return this with { Direction = (Direction)(((int)Direction + 1) % 4), Price = 1000 };
        }

        public PositionAndDirection Step()
        {
            // Price = 1;
            return this with
            {
                Position = Direction switch
                {
                    Direction.Up => Position with { Y = Position.Y - 1 },
                    Direction.Right => Position with { X = Position.X + 1 },
                    Direction.Down => Position with { Y = Position.Y + 1 },
                    Direction.Left => Position with { X = Position.X - 1 },
                    _ => throw new ArgumentOutOfRangeException()
                },
                Price = 1
            };
        }

        public virtual bool Equals(PositionAndDirection? other)
        {
            return other != null && Position.IsSamePosition(other.Position) && Direction == other.Direction;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, (int)Direction);
        }
    }

    private enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    private record WalkingPath(ImmutableStack<PositionAndDirection> Path)
    {
        public Direction Direction => Path.First().Direction;
        public int Price => Path.Sum(p => p.Price);
        public WalkingPath Left() => this with { Path = Path.Push(Path.First().Left()) };
        public WalkingPath Right() => this with { Path = Path.Push(Path.First().Right()) };
        public WalkingPath Step() => this with { Path = Path.Push(Path.First().Step()) };
    }
}