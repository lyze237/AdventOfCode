using AoC.Framework;

namespace AoC._2024;

public class Day5() : Day<((int,int)[] rules, List<List<int>> pages)>(2024, 5, "143", "123")
{
    protected override object DoPart1(((int, int)[] rules, List<List<int>> pages) input) => 
        input.pages.Where(page => IsPageValid(input.rules, page)).Sum(page => page[page.Count / 2]);
    
    protected override object DoPart2(((int, int)[] rules, List<List<int>> pages) input)
    {
        var total = 0;
        
        foreach (var page in input.pages.Where(p => !IsPageValid(input.rules, p)))
        {
            bool isGood;
            do
            {
                isGood = true;
                foreach (var rule in input.rules)
                {
                    if (!IsInvalidRuleOnPage(page, rule, out var wrongRule)) 
                        continue;
                    
                    page[wrongRule.Item1] = rule.Item2;
                    page[wrongRule.Item2] = rule.Item1;
                        
                    isGood = false;
                    break;
                }
            } while (!isGood);
            
            total += page[page.Count / 2];
        }

        return total;
    }

    private static bool IsInvalidRuleOnPage(List<int> page, (int, int) rule, out (int, int) wrongRule)
    {
        if (page.Contains(rule.Item1) && page.Contains(rule.Item2))
        {
            var index1 = page.IndexOf(rule.Item1);
            var index2 = page.IndexOf(rule.Item2);

            if (index1 > index2)
            {
                wrongRule = (index1, index2);
                return true;
            }
        }

        wrongRule = default;
        return false;
    }

    private static bool IsPageValid((int, int)[] rules, List<int> page)
    {
        var isGood = true;
        
        foreach (var rule in rules)
            if (IsInvalidRuleOnPage(page, rule, out _))
                isGood = false;
        
        return isGood;
    }
    
    protected override ((int, int)[] rules, List<List<int>> pages) ParseInput(string input)
    {
        var inputArray = input.Split("\n");
        var rules = inputArray
            .Where(i => i.Contains('|'))
            .Select(i => i.Split("|"))
            .Select(a => (Convert.ToInt32(a[0]), Convert.ToInt32(a[1])))
            .ToArray();

        var pages = inputArray
            .Where(i => i.Contains(','))
            .Select(i => i.Split(","))
            .Select(ia => ia.Select(n => Convert.ToInt32(n)).ToList())
            .ToList();

        return (rules, pages);
    }
}