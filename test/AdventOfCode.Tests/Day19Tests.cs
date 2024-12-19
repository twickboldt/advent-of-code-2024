namespace AdventOfCode.Tests;

public class Day19Tests
{
    private readonly Day19 _instance = new();

    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1(Input.Split('\n'));
        result.Should().Be(6);
    }

    [Fact]
    public void TestPuzzle2()
    {
        var result = _instance.SolvePuzzle2(Input.Split('\n'));
        result.Should().Be(1);
    }

    // private const string Input = """
    //                              r, wr, b, g, bwu, rb, gb, br
    //                              
    //                              bwurrg
    //                              """;
    private const string Input = """
                                 r, wr, b, g, bwu, rb, gb, br
                                 
                                 brwrr
                                 bggr
                                 gbbr
                                 rrbgbr
                                 ubwu
                                 bwurrg
                                 brgr
                                 bbrgwb
                                 """;
}