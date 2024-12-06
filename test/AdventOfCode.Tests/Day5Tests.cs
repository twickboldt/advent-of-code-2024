namespace AdventOfCode.Tests;

public class Day5Tests
{
    private readonly Day5 _instance;

    public Day5Tests()
    {
        _instance = new Day5();
    }
    
    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1(Input.Split('\n'));
        result.Should().Be(143);
    }

    [Fact]
    public void TestPuzzle2()
    {
        var result = _instance.SolvePuzzle2(Input.Split('\n'));
        result.Should().Be(123);
    }

    private const string Input = """
                                 47|53
                                 97|13
                                 97|61
                                 97|47
                                 75|29
                                 61|13
                                 75|53
                                 29|13
                                 97|29
                                 53|29
                                 61|53
                                 97|53
                                 61|29
                                 47|13
                                 75|47
                                 97|75
                                 47|61
                                 75|61
                                 47|29
                                 75|13
                                 53|13

                                 75,47,61,53,29
                                 97,61,53,29,13
                                 75,29,13
                                 75,97,47,61,53
                                 61,13,29
                                 97,13,75,29,47
                                 """;
}