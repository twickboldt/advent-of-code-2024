using Xunit.Abstractions;

namespace AdventOfCode.Tests;

public class Day8Tests
{
    private readonly Day8 _instance = new();

    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1(Input.Split('\n'));
        result.Should().Be(14);
    }

    [Fact]
    public void TestPuzzle2()
    {
        var result = _instance.SolvePuzzle2(Input.Split('\n'));
        result.Should().Be(34);
    }

    private const string Input = """
                                 ............
                                 ........0...
                                 .....0......
                                 .......0....
                                 ....0.......
                                 ......A.....
                                 ............
                                 ............
                                 ........A...
                                 .........A..
                                 ............
                                 ............
                                 """;
}