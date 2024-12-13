using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day12 : Day
{
    private record Position(int X, int Y);

    private record PositionMitContactCount(Position Position, int ContactCount);

    private record Plot(char Type, Position Position);

    private record Region(char Type, Position[] Plots)
    {
        public long Price()
        {
            long price = 0;
            foreach (var plot in Plots)
            {
                price += 4 - GetNumberOfSurroundingPlots(plot, Plots);
            }

            return price * Plots.Length;
        }

        public long PriceWithDiscount()
        {
            if (Plots.Length == 1)
            {
                return 4 * Plots.Length;
            }

            var fences = Plots.SelectMany(plot => GetFencePositions(plot, Plots)).ToArray();
            var fencesMitContactCount = fences.Distinct().ToDictionary(p => p,
                PositionMitContactCount? (p) => new PositionMitContactCount(p, GetContactCount(p, fences)));

            var fencesCount = 0;
            foreach (var fence in fencesMitContactCount)
            {
                if (fence.Value == null)
                {
                    continue;
                }

                var x = GetSurroundingPlots(fence.Value.Position, ref fencesMitContactCount);
                fencesCount += x.Sum(y => y.ContactCount - 1) + 1;

                fencesMitContactCount[fence.Key] = null;
            }

            return fencesCount * Plots.Length;
        }

        public long PriceWithDiscountNotWorking()
        {
            var allFencePositions = Plots.SelectMany(plot => GetFencePositions(plot, Plots)).OrderBy(fence => fence.X)
                .ThenBy(fence => fence.Y).ToArray();

            long gapCount = 0;
            var xFencePositions = allFencePositions.GroupBy(fence => fence.X);
            foreach (var group in xFencePositions)
            {
                var previousFenceValue = -10;
                foreach (var fence in group.OrderBy(fence => fence.Y))
                {
                    if (Math.Abs(fence.Y - previousFenceValue) > 1)
                    {
                        gapCount++;
                    }

                    previousFenceValue = fence.Y;
                }
            }

            var yFencePositions = allFencePositions.GroupBy(fence => fence.Y);
            foreach (var group in yFencePositions)
            {
                var previousFenceValue = -10;
                foreach (var fence in group.OrderBy(fence => fence.X))
                {
                    if (Math.Abs(fence.X - previousFenceValue) > 1)
                    {
                        gapCount++;
                    }

                    previousFenceValue = fence.X;
                }
            }

            return gapCount * Plots.Length;
        }
    }

    private static readonly (int, int)[] Directions = new[]
    {
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0)
    };

    public override long SolvePuzzle1(string[] input)
    {
        var plots = GetPlots(input).ToArray();

        var groupedPlots = plots.GroupBy(plot => plot.Type);

        var regions = groupedPlots.SelectMany(GetRegions).ToArray();

        return regions.Sum(region => region.Price());
    }

    public override long SolvePuzzle2(string[] input)
    {
        var plots = GetPlots(input).ToArray();

        var groupedPlots = plots.GroupBy(plot => plot.Type);

        var regions = groupedPlots.SelectMany(GetRegions).ToArray();

        return regions.Sum(region => region.PriceWithDiscount());
    }

    private static Region[] GetRegions(IGrouping<char, Plot> group)
    {
        var positionsLeft = new HashSet<Position>(group.Select(plot => plot.Position));

        var regions = new List<Region>();
        foreach (var plot in group)
        {
            if (!positionsLeft.Contains(plot.Position))
            {
                continue;
            }

            var surroundingPlots = GetSurroundingPlots([plot.Position],
                ref positionsLeft);
            regions.Add(new Region(group.Key, surroundingPlots.Append(plot.Position).ToArray()));
        }

        return regions.ToArray();
    }

    private static Position[] GetSurroundingPlots(Position[] startingPlots, ref HashSet<Position> positionsLeft)
    {
        var plots = new List<Position>();
        foreach (var startingPlot in startingPlots)
        {
            positionsLeft.Remove(startingPlot);
            var surroundingPlots =
                GetSurroundingPlots(startingPlot, ref positionsLeft).ToArray();

            plots.AddRange(surroundingPlots);
            plots.AddRange(GetSurroundingPlots(surroundingPlots, ref positionsLeft));
        }

        return plots.ToArray();
    }

    private static Position[] GetSurroundingPlots(Position startingPlot, ref HashSet<Position> positionsLeft)
    {
        var surroundingPlots = new List<Position>();
        foreach (var direction in Directions)
        {
            var position = new Position(startingPlot.X + direction.Item1,
                startingPlot.Y + direction.Item2);
            if (positionsLeft.Contains(position))
            {
                positionsLeft.Remove(position);
                surroundingPlots.Add(position);
            }
        }

        return surroundingPlots.ToArray();
    }

    private static PositionMitContactCount[] GetSurroundingPlots(Position startingPlot,
        ref Dictionary<Position, PositionMitContactCount?> positionsLeft)
    {
        var surroundingPlots = new List<PositionMitContactCount>();
        foreach (var direction in Directions)
        {
            var position = new Position(startingPlot.X + direction.Item1,
                startingPlot.Y + direction.Item2);
            if (positionsLeft.TryGetValue(position, out var value) && value != null)
            {
                surroundingPlots.Add(value);
                positionsLeft[position] = null;
                surroundingPlots.AddRange(GetSurroundingPlots(value.Position, ref positionsLeft));
            }
        }

        return surroundingPlots.ToArray();
    }

    private static int GetNumberOfSurroundingPlots(Position startingPlot, Position[] otherPlots)
    {
        var surroundingPlots = 0;
        foreach (var direction in Directions)
        {
            var position = new Position(startingPlot.X + direction.Item1,
                startingPlot.Y + direction.Item2);
            if (otherPlots.Contains(position))
            {
                surroundingPlots++;
            }
        }

        return surroundingPlots;
    }

    private static Position[] GetFencePositions(Position startingPlot, Position[] plotsMitPflanzen)
    {
        var fencePositions = new List<Position>();
        foreach (var direction in Directions)
        {
            var position = new Position(startingPlot.X + direction.Item1,
                startingPlot.Y + direction.Item2);
            if (!plotsMitPflanzen.Contains(position))
            {
                fencePositions.Add(position);
            }
        }

        return fencePositions.ToArray();
    }

    private static int GetContactCount(Position startingPlot, Position[] otherPlots)
    {
        var contactCount = 0;
        foreach (var direction in Directions)
        {
            var position = new Position(startingPlot.X + direction.Item1,
                startingPlot.Y + direction.Item2);
            if (otherPlots.Contains(position))
            {
                contactCount++;
            }
        }

        return contactCount;
    }

    private static IEnumerable<Plot> GetPlots(string[] input)
    {
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                yield return new Plot(input[y][x], new Position(x, y));
            }
        }
    }
}