using BenchmarkDotNet.Attributes;

public class Hash
{
    public static int ExecuteScript(string script, Func<string, bool> exec)
    {
        var total = 0;
        foreach(var scriptLine in script.Split(new char[] { ',' }))
        {
            var command = scriptLine.Split(new char[] { '*' });
            var n = int.Parse(command[0]);
            for(var i=0; i<n; i++)
            {
                exec(command[1]);
                total++;
            }
        }

        return total;
    }

    [Benchmark]
    [Arguments("10*a,1*b,1*c,1000*b,200*a")]
    public int ArrayLookup(string script) 
    {
        var Items_Array = new Item[]
        {
            new("a"),
            new("b"),
            new("c")
        };

        return ExecuteScript(script, (line) =>
        {
            var found = Items_Array.Any(item => item.IsMatch(line));
            return found;
        });
    }

    [Benchmark]
    [Arguments("10*a,1*b,1*c,1000*b,200*a")]
    public int HashLookup(string script)
    {
        var Items_Dictionary = new Dictionary<string, Item>();

        return ExecuteScript(script, (line) =>
        {
            if(!Items_Dictionary.ContainsKey(line))
            {
                Items_Dictionary[line] = new Item(line);
            }

            return Items_Dictionary[line].IsMatch(line);
        });
    }
}