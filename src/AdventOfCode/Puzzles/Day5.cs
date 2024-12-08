using AdventOfCode.Common;

namespace AdventOfCode.Puzzles;

public class Day5 : Day
{
    public override long SolvePuzzle1(string[] input)
    {
        var (orderingRules, prints) = SplitIntoOrderingRulesAndPrints(input);

        var result = 0;
        foreach (var print in prints)
        {
            var pages = print.Split(',').Select(int.Parse).ToArray();
            if (orderingRules.Any(rule => !rule.IsSatisfied(pages)))
            {
                continue;
            }

            var middleIndex = GetMiddleIndex(pages);
            result += pages[middleIndex];
        }

        return result;
    }

    public override long SolvePuzzle2(string[] input)
    {
        var (orderingRules, prints) = SplitIntoOrderingRulesAndPrints(input);

        var result = 0;
        foreach (var print in prints)
        {
            var pages = print.Split(',').Select(int.Parse).ToArray();
            if (orderingRules.All(rule => rule.IsSatisfied(pages)))
            {
                continue;
            }
            
            var sortedPages = SortPages(pages, orderingRules);

            var middleIndex = GetMiddleIndex(pages);
            result += pages[middleIndex];
        }

        return result;
    }

    private static int[] SortPages(int[] pages, List<OrderingRule> orderingRules)
    {
        var relevantRules = orderingRules.Where(rule => rule.IsRelevant(pages)).ToArray();

        for (int i = 0; i < pages.Length; i++)
        {
            for (int j = 0; j < pages.Length; j++)
            {
                if (IsBigger(pages[i], pages[j], relevantRules))
                {
                    (pages[i], pages[j]) = (pages[j], pages[i]);
                }
            }
        }
        
        return pages;
    }

    private static bool IsBigger(int page1, int page2, OrderingRule[] relevantRules)
    {
        var rules = relevantRules.Where(rule => rule.IsRelevant([page1, page2]));
        return rules.All(rule => rule.IsSatisfied([page1, page2]));
    }

    private static int GetMiddleIndex(int[] pages)
    {
        return (int)Math.Round((decimal)pages.Length / 2, MidpointRounding.AwayFromZero) - 1;
    }

    private static (List<OrderingRule> orderingRules, List<string> prints) SplitIntoOrderingRulesAndPrints(
        string[] input)
    {
        var orderingRules = new List<OrderingRule>();
        var prints = new List<string>();

        var isOrderingRule = true;
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                isOrderingRule = false;
                continue;
            }

            if (isOrderingRule)
            {
                var pages = line.Split('|');
                orderingRules.Add(new OrderingRule(int.Parse(pages[0]), int.Parse(pages[1])));
            }
            else
            {
                prints.Add(line);
            }
        }

        return (orderingRules, prints);
    }
}

public record OrderingRule(int Page1, int Page2)
{
    public bool IsSatisfied(int[] pages)
    {
        if (!IsRelevant(pages))
        {
            return true;
        }

        return Array.IndexOf(pages, Page1) < Array.IndexOf(pages, Page2);
    }

    public bool IsRelevant(int[] pages) => pages.Contains(Page1) && pages.Contains(Page2);
}