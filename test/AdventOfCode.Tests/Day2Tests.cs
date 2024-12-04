namespace AdventOfCode.Tests;

public class Day2Tests
{
    private readonly Day2 _instance = new();
    
    [Fact]
    public void TestPuzzle1()
    {
        const string input = """
                             7 6 4 2 1
                             1 2 7 8 9
                             9 7 6 2 1
                             1 3 2 4 5
                             8 6 4 4 1
                             1 3 6 7 9
                             """;
        
        var lines = input.Split('\n');
        
        var result = _instance.SolvePuzzle1(lines);
        result.Should().Be(2);
    }
    
    [Fact]
    public void TestPuzzle2()
    {
        const string input = """
                             7 6 4 2 1
                             1 2 7 8 9
                             9 7 6 2 1
                             1 3 2 4 5
                             8 6 4 4 1
                             1 3 6 7 9
                             """;
        
        var lines = input.Split('\n');
        
        var result = _instance.SolvePuzzle2(lines);
        result.Should().Be(4);
    }
    
    [Fact]
    public void TestPuzzle2_1()
    {
        const string input = """
                             8 11 9 11 14
                             """;
        
        var lines = input.Split('\n');
        
        var result = _instance.SolvePuzzle2(lines);
        result.Should().Be(1);
    }
}