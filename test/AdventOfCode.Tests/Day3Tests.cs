namespace AdventOfCode.Tests;

public class Day3Tests
{
    private readonly Day3 _instance = new();
    
    [Fact]
    public void TestPuzzle1()
    {
        const string input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

        var result = _instance.SolvePuzzle1([input]);
        result.Should().Be(161);
    }

    [Fact]
    public void TestPuzzle2()
    {
        const string input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

        var result = _instance.SolvePuzzle2([input]);
        result.Should().Be(48);
    }
}