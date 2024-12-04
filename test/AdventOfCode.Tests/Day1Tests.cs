namespace AdventOfCode.Tests;

public class Day1Tests
{
    private readonly Day1 _instance = new();

    [Fact]
    public void TestPuzzle1()
    {
        const string input = """
                             3   4
                             4   3
                             2   5
                             1   3
                             3   9
                             3   3
                             """;


        var lines = input.Split('\n');
        var result = _instance.SolvePuzzle1(lines);
        result.Should().Be(11);
    }

    [Fact]
    public void TestPuzzle2()
    {
        const string input = """
                             3   4
                             4   3
                             2   5
                             1   3
                             3   9
                             3   3
                             """;

        var lines = input.Split('\n');
        var result = _instance.SolvePuzzle2(lines);
        result.Should().Be(31);
    }
}