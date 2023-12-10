using BenchmarkDotNet.Attributes;

public class Hash
{
    static readonly Item[] Items = new Item[]
    {
        new("a"),
        new("b"),
        new("c")
    };

    [Benchmark]
    public void ArrayLookup() 
    {

    }

    [Benchmark]
    public void HashLookup()
    {

    }
}