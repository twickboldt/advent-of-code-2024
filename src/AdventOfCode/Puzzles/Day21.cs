using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day21 : Day
{
    private static Dictionary<char, Position> _codePad = new()
    {
        ['7'] = new Position(0, 0),
        ['8'] = new Position(1, 0),
        ['9'] = new Position(2, 0),
        ['4'] = new Position(0, 1),
        ['5'] = new Position(1, 1),
        ['6'] = new Position(2, 1),
        ['1'] = new Position(0, 2),
        ['2'] = new Position(1, 2),
        ['3'] = new Position(2, 2),
        ['0'] = new Position(1, 3),
        ['A'] = new Position(2, 3)
    };

    private static Dictionary<char, Position> _roboPad = new()
    {
        ['^'] = new Position(1, 0),
        ['A'] = new Position(2, 0),
        ['<'] = new Position(0, 1),
        ['v'] = new Position(1, 1),
        ['>'] = new Position(2, 1),
    };
    
    public override long SolvePuzzle1(string[] input)
    {
        var result = 0L;
        foreach (var line in input)
        {
            var codepadMovements = GetMovementsForCodePad(line).ToArray();
            var radiationRobotMovements = GetMovementsForRoboPad(codepadMovements).ToArray();
            var freezingRobotMovements = GetMovementsForRoboPad(radiationRobotMovements).ToArray();

            result += freezingRobotMovements.Count() * int.Parse(line.Replace("A", ""));
        }

        return result;
    }

    private static IEnumerable<char> GetMovementsForCodePad(string code)
    {
        var start = _codePad['A'];
        foreach (var codeChar in code)
        {
            var target = _codePad[codeChar];
            foreach (var movement in start.GetMovementOnCodePad(target))
            {
                yield return movement;
            }

            yield return 'A';

            start = target;
        }
    }

    private static IEnumerable<char> GetMovementsForRoboPad(IEnumerable<char> code)
    {
        var start = _roboPad['A'];
        foreach (var codeChar in code)
        {
            var target = _roboPad[codeChar];
            foreach (var movement in start.GetMovementOnRoboPad(target))
            {
                yield return movement;
            }

            yield return 'A';

            start = target;
        }
    }

    public override long SolvePuzzle2(string[] input)
    {
        throw new NotImplementedException();
    }

    private record CharCombo(IEnumerable<char> Chars);
    private record Position(int X, int Y)
    {
        public IEnumerable<char> GetMovementOnCodePad(Position target)
        {
            // on the code pad we first have to move up/right before down/left to avoid the gap
            var xDiff = target.X - X;
            var yDiff = target.Y - Y;
            
            if (xDiff > 0)
            {
                // right
                while (xDiff > 0)
                {
                    yield return '>';
                    xDiff--;
                }
            }

            if (yDiff < 0)
            {
                // up
                while(yDiff < 0)
                {
                    yield return '^';
                    yDiff++;
                }
            }
            
            if (xDiff < 0)
            {
                // left
                while(xDiff < 0)
                {
                    yield return '<';
                    xDiff++;
                }
            }
            
            if (yDiff > 0)
            {
                // down
                while (yDiff > 0)
                {
                    yield return 'v';
                    yDiff--;
                }
            }
        }
        
        public IEnumerable<char> GetMovementOnRoboPad(Position target)
        {
            // on the robo pad we first have to move down/right before up/left to avoid the gap
            var xDiff = target.X - X;
            var yDiff = target.Y - Y;
            
            if (xDiff > 0)
            {
                // right
                while (xDiff > 0)
                {
                    yield return '>';
                    xDiff--;
                }
            }
            
            if (yDiff > 0)
            {
                // down
                while (yDiff > 0)
                {
                    yield return 'v';
                    yDiff--;
                }
            }
            
            if (xDiff < 0)
            {
                // left
                while(xDiff < 0)
                {
                    yield return '<';
                    xDiff++;
                }
            }

            if (yDiff < 0)
            {
                // up
                while(yDiff < 0)
                {
                    yield return '^';
                    yDiff++;
                }
            }
        }
    }
}