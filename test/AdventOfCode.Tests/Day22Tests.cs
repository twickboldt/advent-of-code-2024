namespace AdventOfCode.Tests;

public class Day22Tests
{
    private readonly Day22 _instance = new();

    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1(Input.Split('\n'));
        result.Should().Be(37327623);
    }
    
    [Fact]
    public void TestPuzzle2()
    {
        var result = _instance.SolvePuzzle2(["1", "2", "3", "2024"]);
        result.Should().Be(23);
    }

    private const string Input = """
                                 1
                                 10
                                 100
                                 2024
                                 """;
}