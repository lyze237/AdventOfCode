using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day7 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var dependencies = new List<(char pre, char post)>();
        Input.ToList().ForEach(x => dependencies.Add((x[5], x[36])));

        var allSteps = dependencies
            .Select(x => x.pre)
            .Concat(dependencies.Select(x => x.post))
            .Distinct()
            .OrderBy(x => x).ToList();

        var result = string.Empty;
        while (allSteps.Count > 0)
        {
            var valid = allSteps.First(s => dependencies.All(d => d.post != s));

            result += valid;

            allSteps.Remove(valid);
            dependencies.RemoveAll(d => d.pre == valid);
        }

        return result;
    }

    public override object ExecutePart2()
    {
        var deps = new List<(char Pre, char Post)>();

        Input.ToList().ForEach(x => deps.Add((x[5], x[36])));

        var allSteps = deps
            .Select(d => d.Pre)
            .Concat(deps.Select(d => d.Post))
            .Distinct()
            .OrderBy(d => d)
            .ToList();

        var workers = new List<int>(5) { 0, 0, 0, 0, 0 };
        var currentSecond = 0;
        var doneList = new List<(char step, int finish)>();

        while (allSteps.Count > 0 || workers.Count(w => w > currentSecond) > 0)
        {
            foreach (var x in doneList.Where(d => d.finish <= currentSecond).ToList())
            {
                deps.RemoveAll(d => d.Pre == x.step);
                doneList.Remove(x);
            }

            var valid = allSteps.Where(s => deps.All(d => d.Post != s)).ToList();

            for (var w = 0; w < workers.Count(x => x <= currentSecond) && valid.Count > 0; w++)
            {
                workers[w] = valid.First() - 'A' + 61 + currentSecond;
                allSteps.Remove(valid.First());
                doneList.Add((valid.First(), workers[w]));
                valid.RemoveAt(0);
            }

            currentSecond++;
        }

        return currentSecond;
    }
}