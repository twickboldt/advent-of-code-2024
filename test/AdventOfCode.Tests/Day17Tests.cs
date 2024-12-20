namespace AdventOfCode.Tests;

public class Day17Tests
{
    private readonly Day17 _instance = new();

    [Fact]
    public void SolvePuzzle1()
    {
        var result = _instance.SolvePuzzle1(Input.Split('\n'));
        result.Should().Be(4635635210);
    }

    [Fact]
    public void SolvePuzzle2()
    {
        var result = _instance.SolvePuzzle2(Input2.Split('\n'));
        result.Should().Be(117440);
    }

    [Fact]
    public void TestCondition1()
    {
        var program = new Day17.Program(0, 0, 9, [2, 6]);
        program.Run();
        program.B.Should().Be(1);
    }

    [Fact]
    public void TestCondition2()
    {
        var program = new Day17.Program(10, 0, 0, [5, 0, 5, 1, 5, 4]);
        var result = program.Run();
        result.Should().BeEquivalentTo([0, 1, 2]);
    }

    [Fact]
    public void TestCondition3()
    {
        var program = new Day17.Program(2024, 0, 0, [0, 1, 5, 4, 3, 0]);
        var result = program.Run();
        result.Should().BeEquivalentTo([4, 2, 5, 6, 7, 7, 7, 7, 3, 1, 0]);
        program.A.Should().Be(0);
    }

    [Fact]
    public void TestCondition4()
    {
        var program = new Day17.Program(0, 29, 0, [1, 7]);
        program.Run();
        program.B.Should().Be(26);
    }

    [Fact]
    public void TestCondition5()
    {
        var program = new Day17.Program(0, 2024, 43690, [4, 0]);
        program.Run();
        program.B.Should().Be(44354);
    }

    private const string Input = """
                                 Register A: 729
                                 Register B: 0
                                 Register C: 0

                                 Program: 0,1,5,4,3,0
                                 """;

    private const string Input2 = """
                                  Register A: 117440
                                  Register B: 0
                                  Register C: 0

                                  Program: 0,3,5,4,3,0
                                  """;
}