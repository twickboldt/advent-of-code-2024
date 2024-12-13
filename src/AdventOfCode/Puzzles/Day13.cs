using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day13 : Day
{
    private const long Adjustment = 10000000000000;

    private record Position(long X, long Y, long AdjustedX, long AdjustedY)
    {
        public Position(long X, long Y) : this(X, Y, X + Adjustment, Y + Adjustment)
        {
        }

        public Position Multiplay(long mulitplier) => new(X * mulitplier, Y * mulitplier);
    }

    private record Game(Position ButtonA, Position ButtonB, Position Prize)
    {
        private const int TokenPriceButtonA = 3;
        private const int TokenPriceButtonB = 1;

        public int NumberOfTokensToWinPart1()
        {
            var lookupA = Enumerable.Range(1, 100).Select(i => ButtonA.Multiplay(i)).ToList();
            var lookupB = Enumerable.Range(1, 100).Select(i => ButtonB.Multiplay(i)).ToList();

            var tokensToWin = new List<int>();
            for (var i = 1; i <= lookupA.Count; i++)
            {
                for (var j = 1; j <= lookupB.Count; j++)
                {
                    if (IsWin(lookupA[i - 1], lookupB[j - 1]))
                    {
                        tokensToWin.Add(i * TokenPriceButtonA + j * TokenPriceButtonB);
                    }
                }
            }

            return tokensToWin.Count > 0 ? tokensToWin.Min() : 0;
        }

        public long NumberOfTokensToWinPart2()
        {
            var lookupA = GetRange(1, Math.Min(Prize.AdjustedX / ButtonA.X, Prize.AdjustedY / ButtonA.Y))
                .Select(i => ButtonA.Multiplay(i)).ToList();
            var lookupB = GetRange(1, Math.Min(Prize.AdjustedX / ButtonB.X, Prize.AdjustedY / ButtonB.Y))
                .Select(i => ButtonB.Multiplay(i)).ToList();

            var tokensToWin = new List<int>();
            for (var i = 1; i <= lookupA.Count; i++)
            {
                for (var j = 1; j <= lookupB.Count; j++)
                {
                    if (IsWin(lookupA[i - 1], lookupB[j - 1]))
                    {
                        tokensToWin.Add(i * TokenPriceButtonA + j * TokenPriceButtonB);
                    }
                }
            }

            return tokensToWin.Count > 0 ? tokensToWin.Min() : 0;
        }

        private static IEnumerable<long> GetRange(int start, long count)
        {
            for (var i = start; i <= count; i++)
            {
                yield return i;
            }
        }

        private bool IsWin(Position buttonA, Position buttonB)
        {
            return Prize.X == buttonA.X + buttonB.X && Prize.Y == buttonA.Y + buttonB.Y;
        }
    }

    public override long SolvePuzzle1(string[] input)
    {
        return ParseInput(input).Sum(game => game.NumberOfTokensToWinPart1());
    }

    public override long SolvePuzzle2(string[] input)
    {
        return ParseInput(input).Sum(game => game.NumberOfTokensToWinPart2());
    }

    private static IEnumerable<Game> ParseInput(string[] input)
    {
        var buttonA = new Position(0, 0);
        var buttonB = new Position(0, 0);
        for (var i = 0; i < input.Length; i++)
        {
            var rest = i % 4;
            if (rest == 0)
            {
                var replacedInput = input[i].Replace("Button A: X", "").Replace(" Y", "").Split(',');
                buttonA = new Position(int.Parse(replacedInput[0]), int.Parse(replacedInput[1]));
            }

            if (rest == 1)
            {
                var replacedInput = input[i].Replace("Button B: X", "").Replace(" Y", "").Split(',');
                buttonB = new Position(int.Parse(replacedInput[0]), int.Parse(replacedInput[1]));
            }

            if (rest == 2)
            {
                var replacedInput = input[i].Replace("Prize: X=", "").Replace(" Y=", "").Split(',');
                var prize = new Position(int.Parse(replacedInput[0]), int.Parse(replacedInput[1]));

                yield return new Game(buttonA, buttonB, prize);
            }
        }
    }
}