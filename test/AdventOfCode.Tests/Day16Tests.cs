namespace AdventOfCode.Tests;

public class Day16Tests
{
    private readonly Day16 _instance = new();
    
    [Fact]
    public void TestPuzzle1()
    {
        var result = _instance.SolvePuzzle1(Input.Split('\n'));
        result.Should().Be(7036);
    }
    [Fact]
    public void TestPuzzle1_2()
    {
        var result = _instance.SolvePuzzle1(Input2.Split('\n'));
        result.Should().Be(11048);
    }
    
    private const string Input = """
                                 ###############
                                 #.......#....E#
                                 #.#.###.#.###.#
                                 #.....#.#...#.#
                                 #.###.#####.#.#
                                 #.#.#.......#.#
                                 #.#.#####.###.#
                                 #...........#.#
                                 ###.#.#####.#.#
                                 #...#.....#.#.#
                                 #.#.#.###.#.#.#
                                 #.....#...#.#.#
                                 #.###.#.#.#.#.#
                                 #S..#.....#...#
                                 ###############
                                 """;

    private const string Input2 = """
                                  #################
                                  #...#...#...#..E#
                                  #.#.#.#.#.#.#.#.#
                                  #.#.#.#...#...#.#
                                  #.#.#.#.###.#.#.#
                                  #...#.#.#.....#.#
                                  #.#.#.#.#.#####.#
                                  #.#...#.#.#.....#
                                  #.#.#####.#.###.#
                                  #.#.#.......#...#
                                  #.#.###.#####.###
                                  #.#.#...#.....#.#
                                  #.#.#.#####.###.#
                                  #.#.#.........#.#
                                  #.#.#.#########.#
                                  #S#.............#
                                  #################
                                  """;
}