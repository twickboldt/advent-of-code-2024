using System.Collections;
using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public partial class Day17 : Day
{
    public override long SolvePuzzle1(string[] input)
    {
        var program = ParseInput(input);
        var result = program.Run();
        return long.Parse(string.Join("", result));
    }

    public override long SolvePuzzle2(string[] input)
    {
        throw new NotImplementedException();
        var program = ParseInput(input);

        var solutions = new List<int>();

        // Parallel.ForEach(Enumerable.Range(0, int.MaxValue), i =>
        // {
        //     var newProgram = program with { A = i };
        //     var result = newProgram.Run();
        //     if (result.SequenceEqual(program.Instructions))
        //     {
        //         Console.WriteLine($"Solution found: {i}");
        //         lock (solutions)
        //         {
        //             solutions.Add(i);
        //         }
        //     }
        // });

        // return solutions.Min();

        for (long i = int.MaxValue - 1; i < long.MaxValue; i++)
        {
            var newProgram = program with { A = i };
            var result = newProgram.Run();
            if (result.SequenceEqual(program.Instructions))
            {
                return i;
            }
        }

        throw new Exception("No solution found");
    }

    private static Program ParseInput(string[] input)
    {
        var numbers = Regex.Matches(string.Join('\n', input), @"\d+").Select(m => long.Parse(m.Value)).ToArray();

        return new Program(numbers[0], numbers[1], numbers[2], numbers[3..]);
    }

    public record Program
    {
        public long A { get; set; }
        public long B { get; set; }
        public long C { get; set; }
        public long[] Instructions { get; }

        public Program(long a, long b, long c, long[] instructions)
        {
            A = a;
            B = b;
            C = c;
            Instructions = instructions;
        }

        public List<long> Run()
        {
            var pointer = 0L;
            var output = new List<long>();

            while (pointer < Instructions.Length)
            {
                var instruction = Instructions[pointer];
                var operand = Instructions[pointer + 1];

                switch (instruction)
                {
                    case 0:
                        A = (long)(A / Math.Pow(2, GetComboOperand(operand)));
                        pointer += 2;
                        break;
                    case 1:
                        // bitwise XOR
                        B ^= operand;
                        pointer += 2;
                        break;
                    case 2:
                        B = GetComboOperand(operand) % 8;
                        pointer += 2;
                        break;
                    case 3:
                        if (A != 0)
                        {
                            pointer = operand;
                            break;
                        }

                        pointer += 2;
                        break;
                    case 4:
                        B ^= C;
                        pointer += 2;
                        break;
                    case 5:
                        output.Add(GetComboOperand(operand) % 8);
                        pointer += 2;
                        break;
                    case 6:
                        B = (long)(A / Math.Pow(2, GetComboOperand(operand)));
                        pointer += 2;
                        break;
                    case 7:
                        C = (long)(A / Math.Pow(2, GetComboOperand(operand)));
                        pointer += 2;
                        break;
                }
            }

            // Console.WriteLine(string.Join(',', output));
            return output;
        }

        private long GetComboOperand(long operand)
        {
            return operand switch
            {
                >= 0 and <= 3 => operand,
                4 => A,
                5 => B,
                6 => C,
                _ => throw new InvalidOperationException()
            };
        }
    }

    // [GeneratedRegex(@"\d+")]
    // private static partial Regex NumberRegex();
}