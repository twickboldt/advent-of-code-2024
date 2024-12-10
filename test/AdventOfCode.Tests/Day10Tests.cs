namespace AdventOfCode.Tests;

public class Day10Tests
{
    private readonly Day10 _instance = new();

    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1(Input.Split('\n'));
        result.Should().Be(36);
    }
    
    [Fact]
    public void TestPuzzle2()
    {
        var result = _instance.SolvePuzzle2(Input.Split('\n'));
        result.Should().Be(81);
    }
    
    private const string Input = """
                                 89010123
                                 78121874
                                 87430965
                                 96549874
                                 45678903
                                 32019012
                                 01329801
                                 10456732
                                 """;
}