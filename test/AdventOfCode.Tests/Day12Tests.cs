namespace AdventOfCode.Tests;

public class Day12Tests
{
    private readonly Day12 _instance = new();

    [Fact]
    public void TestPuzzle1_Input1()
    {
        var result = _instance.SolvePuzzle1(Input1.Split('\n'));
        result.Should().Be(140);
    }

    [Fact]
    public void TestPuzzle1_Input2()
    {
        var result = _instance.SolvePuzzle1(Input2.Split('\n'));
        result.Should().Be(772);
    }

    [Fact]
    public void TestPuzzle1_Input3()
    {
        var result = _instance.SolvePuzzle1(Input3.Split('\n'));
        result.Should().Be(1930);
    }

    [Fact]
    public void TestPuzzle2_Input1()
    {
        var result = _instance.SolvePuzzle2(Input1.Split('\n'));
        result.Should().Be(80);
    }

    [Fact]
    public void TestPuzzle2_Input2()
    {
        var result = _instance.SolvePuzzle2(Input2.Split('\n'));
        result.Should().Be(436);
    }

    [Fact]
    public void TestPuzzle2_Input3()
    {
        var result = _instance.SolvePuzzle2(Input3.Split('\n'));
        result.Should().Be(1206);
    }

    [Fact]
    public void TestPuzzle2_Input4()
    {
        var result = _instance.SolvePuzzle2(Input4.Split('\n'));
        result.Should().Be(236);
    }

    [Fact]
    public void TestPuzzle2_Input5()
    {
        var result = _instance.SolvePuzzle2(Input5.Split('\n'));
        result.Should().Be(368);
    }

    private const string Input1 = """
                                  AAAA
                                  BBCD
                                  BBCC
                                  EEEC
                                  """;

    private const string Input2 = """
                                  OOOOO
                                  OXOXO
                                  OOOOO
                                  OXOXO
                                  OOOOO
                                  """;

    private const string Input3 = """
                                  RRRRIICCFF
                                  RRRRIICCCF
                                  VVRRRCCFFF
                                  VVRCCCJFFF
                                  VVVVCJJCFE
                                  VVIVCCJJEE
                                  VVIIICJJEE
                                  MIIIIIJJEE
                                  MIIISIJEEE
                                  MMMISSJEEE
                                  """;

    private const string Input4 = """
                                  EEEEE
                                  EXXXX
                                  EEEEE
                                  EXXXX
                                  EEEEE
                                  """;

    private const string Input5 = """
                                  AAAAAA
                                  AAABBA
                                  AAABBA
                                  ABBAAA
                                  ABBAAA
                                  AAAAAA
                                  """;
}