namespace AdventOfCode.Tests;

public class Day6Tests
{
    private readonly Day6 _instance;

    public Day6Tests()
    {
        _instance = new Day6();
    }
    
    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1(Input.Split('\n'));
        result.Should().Be(41);
    }
    
    [Fact]
    public void TestPuzzle2()
    {
        var result = _instance.SolvePuzzle2(Input.Split('\n'));
        result.Should().Be(6);
    }

    private const string Input = """
                                 ....#.....
                                 .........#
                                 ..........
                                 ..#.......
                                 .......#..
                                 ..........
                                 .#..^.....
                                 ........#.
                                 #.........
                                 ......#...
                                 """;
}