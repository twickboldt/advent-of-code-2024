namespace AdventOfCode.Tests;

public class Day20Tests
{
    private readonly Day20 _instance = new(1);
    
    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1(Input.Split('\n'));
        result.Should().Be(44);
    }
    
    [Fact]
    public void TestPuzzle2()
    {
        var instance = new Day20(50);
        var result = instance.SolvePuzzle2(Input.Split('\n'));
        result.Should().Be(285);
    }
    
    private const string Input = """
                                 ###############
                                 #...#...#.....#
                                 #.#.#.#.#.###.#
                                 #S#...#.#.#...#
                                 #######.#.#.###
                                 #######.#.#...#
                                 #######.#.###.#
                                 ###..E#...#...#
                                 ###.#######.###
                                 #...###...#...#
                                 #.#####.#.###.#
                                 #.#...#.#.#...#
                                 #.#.#.#.#.#.###
                                 #...#...#...###
                                 ###############
                                 """;
}