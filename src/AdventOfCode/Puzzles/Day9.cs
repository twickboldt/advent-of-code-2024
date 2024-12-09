using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day9 : Day
{
    private record Chunk(int Size, int Index, bool IsFreeSpace);

    public override long SolvePuzzle1(string[] input)
    {
        var inputNumbers = input[0].Select(c => int.Parse(c.ToString())).ToArray();

        var diskSpaces = new List<int>();

        var result = new int?[inputNumbers.Sum()];
        var index = 0;
        for (var i = 0; i < inputNumbers.Length; i++)
        {
            var inputNumber = inputNumbers[i];
            if (i % 2 == 0)
            {
                // inputNumber is file block
                for (int j = 0; j < inputNumber; j++)
                {
                    result[index] = i / 2;
                    diskSpaces.Add(i / 2);
                    index++;
                }
            }
            else
            {
                // inputNumber is free space block
                for (var j = 0; j < inputNumber; j++)
                {
                    index++;
                }
            }
        }

        var freeSpaceCounter = 0;
        var realResultCounter = 0;
        var realResult = new int[diskSpaces.Count];
        // sort numbers into free spaces
        for (var i = 0; i < diskSpaces.Count; i++)
        {
            if (result[i] != null)
            {
                realResult[realResultCounter] = result[i]!.Value;
                realResultCounter++;
                continue;
            }

            realResult[realResultCounter] = diskSpaces[^(freeSpaceCounter + 1)];
            realResultCounter++;
            freeSpaceCounter++;
        }

        var tmp = realResult.Select((r, i) => r * i).ToArray();
        long returnResult = 0;
        foreach (var t in tmp)
        {
            returnResult += t;
        }

        return returnResult;
    }

    public override long SolvePuzzle2(string[] input)
    {
        Chunk?[] inputNumbers = input[0].Select((c, i) => new Chunk(int.Parse(c.ToString()), i, i % 2 != 0))
            .ToArray();


        var defragmentedResults = new List<Chunk?>();
        for (var i = 0; i < inputNumbers.Length; i++)
        {
            var chunk = inputNumbers[i];

            // a file chunk that has been moved already
            if (chunk == null)
            {
                continue;
            }

            if (!chunk.IsFreeSpace)
            {
                defragmentedResults.Add(chunk);
                inputNumbers[i] = null;
            }
            else
            {
                var freeSpaceSize = chunk.Size;
                for (var j = 1; j <= inputNumbers.Length; j++)
                {
                    var file = inputNumbers[^j];
                    if (file == null || file.IsFreeSpace)
                    {
                        continue;
                    }

                    if (freeSpaceSize >= file.Size)
                    {
                        defragmentedResults.Add(file);
                        freeSpaceSize -= file.Size;
                        inputNumbers[^j] = file with { IsFreeSpace = true };
                    }

                    if (freeSpaceSize == 0)
                    {
                        break;
                    }
                }

                if (freeSpaceSize > 0)
                {
                    defragmentedResults.Add(new Chunk(freeSpaceSize, 0, true));
                }
            }
        }

        var indexCounter = 0;
        long returnResult = 0;
        foreach (var result in defragmentedResults)
        {
            for (var i = 0; i < result!.Size; i++)
            {
                if (!result.IsFreeSpace)
                {
                    returnResult += indexCounter * (result.Index / 2);
                }

                indexCounter++;
            }

            if (result.Size == 0)
            {
                indexCounter++;
            }
        }

        return returnResult;
    }
}