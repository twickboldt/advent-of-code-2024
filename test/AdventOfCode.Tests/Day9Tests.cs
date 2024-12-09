namespace AdventOfCode.Tests;

public class Day9Tests
{
    private readonly Day9 _instance = new();
    
    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1([Input]);
        result.Should().Be(1928);
    }
    
    [Fact]
    public void TestPuzzle2()
    {
        var result = _instance.SolvePuzzle2([Input]);
        result.Should().Be(2858);
    }

    private const string Input = "2333133121414131402";
}