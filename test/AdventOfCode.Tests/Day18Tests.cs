namespace AdventOfCode.Tests;

public class Day18Tests
{
    private readonly Day18 _instance = new(7, 12);

    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1(Input.Split('\n'));
        result.Should().Be(22);
    }

    [Fact]
    public void TestPuzzle2()
    {
        var result = _instance.SolvePuzzle2(Input.Split('\n'));
        result.Should().Be(1);
    }

    private const string Input = """
                                 5,4
                                 4,2
                                 4,5
                                 3,0
                                 2,1
                                 6,3
                                 2,4
                                 1,5
                                 0,6
                                 3,3
                                 2,6
                                 5,1
                                 1,2
                                 5,5
                                 2,5
                                 6,5
                                 1,4
                                 0,4
                                 6,4
                                 1,1
                                 6,1
                                 1,0
                                 0,5
                                 1,6
                                 2,0
                                 """;
}