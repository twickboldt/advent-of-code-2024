namespace AdventOfCode.Tests;

public class Day4Tests
{
    private readonly Day4 _instance;

    public Day4Tests()
    {
        _instance = new Day4();
    }
    
    [Fact]
    public void TestPuzzle1()
    {
        const string input = """
                             MMMSXXMASM
                             MSAMXMSMSA
                             AMXSXMAAMM
                             MSAMASMSMX
                             XMASAMXAMM
                             XXAMMXXAMA
                             SMSMSASXSS
                             SAXAMASAAA
                             MAMMMXMMMM
                             MXMXAXMASX
                             """;

        var result = _instance.SolvePuzzle1(input.Split('\n'));
        result.Should().Be(18);
    }

    [Fact]
    public void TestPuzzle2()
    {
        const string input = """
                             MMMSXXMASM
                             MSAMXMSMSA
                             AMXSXMAAMM
                             MSAMASMSMX
                             XMASAMXAMM
                             XXAMMXXAMA
                             SMSMSASXSS
                             SAXAMASAAA
                             MAMMMXMMMM
                             MXMXAXMASX
                             """;
        
        var result = _instance.SolvePuzzle2(input.Split('\n'));
        result.Should().Be(9);
    }
}