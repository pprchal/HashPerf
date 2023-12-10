using BenchmarkDotNet.Attributes;

public class Hash
{
    // random scenario
    const string ScriptRandom = "10*a,10*c,50*a,50*c,1*b,234*f,2*b,25*c,5*d,6*e,7*f";

    // best scenario for array (first item)
    const string ScriptFirst = "100*a";
    
    // worst scenario for array (last item)
    const string ScriptLast = "100*f";
    
    // imagine dispatch table with x as default value
    static Item[] CreateItemsArray() =>
    [
        new("a"),
        new("b"),
        new("c"),
        new("d"),
        new("e"),
        new("f"),
        new("x")
    ];

    static int ExecuteScript(string script, Func<string, bool> exec)
    {
        var total = 0;
        foreach(var scriptLine in script.Split([',']))
        {
            var command = scriptLine.Split(['*']);
            var n = int.Parse(command[0]);
            for(var i=0; i<n; i++)
            {
                exec(command[1]);
                total++;
            }
        }

        return total;
    }

    // [Benchmark]
    // [Arguments(ScriptRandom)]
    // [Arguments(ScriptFirst)]
    // [Arguments(ScriptLast)]
    // public int ArrayLookup_LinqAny(string script) 
    // {
    //     var items = CreateABCArray();
    //     return ExecuteScript(
    //         script,
    //         (line) => items.Any(item => item.IsMatch(line))
    //     );
    // }

    [Benchmark]
    [Arguments(ScriptRandom)]
    [Arguments(ScriptFirst)]
    [Arguments(ScriptLast)]
    public int ArrayLookup_For(string script) 
    {
        var items = CreateItemsArray();

        return ExecuteScript(script, (line) =>
        {
            for(var n = 0; n < items.Length; n++)
            {
                if(items[n].IsMatch(line))
                {
                    return true;
                }
            }

            return false;
        });
    }

    [Benchmark]
    [Arguments(ScriptRandom)]
    [Arguments(ScriptFirst)]
    [Arguments(ScriptLast)]
    public int HashLookup_PrimaryKey(string script)
    {
        // this is doublethink case:
        // cached key is fast, but you loose access to your interface
        //      all info must be contained in key !
        //      vs. return items[line].IsMatch(line);
        //
        // proceed results with caution!
        var items = new Dictionary<string, Item>();

        return ExecuteScript(script, (line) =>
        {
            if(!items.ContainsKey(line))
            {
                items[line] = new Item(line);
            }

            return true;
        });
    }

    [Benchmark]
    [Arguments(ScriptRandom)]
    [Arguments(ScriptFirst)]
    [Arguments(ScriptLast)]
    public int HashLookup_Full(string script)
    {
        var items = new Dictionary<string, Item>();

        return ExecuteScript(script, (line) =>
        {
            if(!items.ContainsKey(line))
            {
                items[line] = new Item(line);
            }

            return items[line].IsMatch(line);
        });
    }

    [Benchmark]
    [Arguments(ScriptRandom)]
    [Arguments(ScriptFirst)]
    [Arguments(ScriptLast)]
    public int HashLookup_Full_TryGetValue(string script)
    {
        var items = new Dictionary<string, Item>();

        return ExecuteScript(script, (line) =>
        {
            if(!items.TryGetValue(line, out var item))
            {
                items[line] = item = new Item(line);
            }
            return item.IsMatch(line);
        });
    }
}