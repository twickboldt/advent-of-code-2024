using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

[SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class Day1 : Day
{
    public override long SolvePuzzle1(string[] input)
    {
        var group1Locations = new List<int>();
        var group2Locations = new List<int>();
        foreach (var line in input)
        {
            var split = line.Split(' ');
            var location1 = int.Parse(split[0]);
            var location2 = int.Parse(split[^1]);

            group1Locations.Add(location1);
            group2Locations.Add(location2);
        }
        
        group1Locations.Sort();
        group2Locations.Sort();

        var totalDistance = 0;
        for (var i = 0; i < group1Locations.Count; i++)
        {
            var distance = group1Locations[i] - group2Locations[i];

            totalDistance += int.Abs(distance);
        }

        return totalDistance;
    }

    public override long SolvePuzzle2(string[] input)
    {
        var group1Locations = new List<int>();
        var group2Locations = new List<int>();
        foreach (var line in input)
        {
            var split = line.Split(' ');
            var location1 = int.Parse(split[0]);
            var location2 = int.Parse(split[^1]);

            group1Locations.Add(location1);
            group2Locations.Add(location2);
        }

        var group2CalibrationValues = group2Locations.GroupBy(location => location)
            .ToDictionary(group => group.Key, group => group.Count());

        var similarityScore = 0;
        foreach (var location in group1Locations)
        {
            group2CalibrationValues.TryGetValue(location, out var calibrationValue);
            similarityScore += calibrationValue * location;
        }

        return similarityScore;
    }
}