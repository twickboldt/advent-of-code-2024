namespace AdventOfCode.Tests;

public class Day11Tests
{
    private readonly Day11 _instance = new();

    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1([Input]);
        result.Should().Be(55312);
    }

    private const string Input = "125 17";
}