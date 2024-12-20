namespace AdventOfCode.Tests;

public class Day14Tests
{
    private readonly Day14 _instance = new();

    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1(Input.Split('\n'));
        result.Should().Be(12);
    }

    [Fact]
    public void TestPuzzle1_Single()
    {
        var result = _instance.SolvePuzzle1(["p=2,4 v=2,-3"]);
        result.Should().Be(1);
    }
    
    private const string Input = """
                                 p=0,4 v=3,-3
                                 p=6,3 v=-1,-3
                                 p=10,3 v=-1,2
                                 p=2,0 v=2,-1
                                 p=0,0 v=1,3
                                 p=3,0 v=-2,-2
                                 p=7,6 v=-1,-3
                                 p=3,0 v=-1,-2
                                 p=9,3 v=2,3
                                 p=7,3 v=-1,2
                                 p=2,4 v=2,-3
                                 p=9,5 v=-3,-3
                                 """;
}