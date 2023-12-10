# HashPerf

Really **confusing?** benchmark.

Maybe you're thinking about hashmaps as silver bullets.
Well, they **ARE** fast, but with some costs.

`dotnet run -c Release`

At end of the day, final decision: should I use *primary key* - or *chain of responsibity pattern*?
Look for method: `HashLookup_PrimaryKey`
I is up to you, as always.

## Notes
* `*a` is the best case for array - first item
* `*f` is the worst case for array - last item

## Finally some fake statistics 

```
BenchmarkDotNet v0.13.11, Manjaro Linux
Intel Celeron J4025 CPU 2.00GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT SSE4.2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT SSE4.2


```
| Method                      | script               | Mean      | Error     | StdDev    |
|---------------------------- |--------------------- |----------:|----------:|----------:|
| **ArrayLookup_For**             | **10*a,(...)e,7*f [50]** | **18.027 μs** | **0.0262 μs** | **0.0245 μs** |
| HashLookup_PrimaryKey       | 10*a,(...)e,7*f [50] | 13.367 μs | 0.0308 μs | 0.0288 μs |
| HashLookup_Full             | 10*a,(...)e,7*f [50] | 23.820 μs | 0.0287 μs | 0.0255 μs |
| HashLookup_Full_TryGetValue | 10*a,(...)e,7*f [50] | 14.365 μs | 0.0174 μs | 0.0146 μs |
| **ArrayLookup_For**             | **100*a**                |  **1.284 μs** | **0.0014 μs** | **0.0013 μs** |
| HashLookup_PrimaryKey       | 100*a                |  2.801 μs | 0.0028 μs | 0.0025 μs |
| HashLookup_Full             | 100*a                |  5.100 μs | 0.0080 μs | 0.0071 μs |
| HashLookup_Full_TryGetValue | 100*a                |  2.885 μs | 0.0041 μs | 0.0038 μs |
| **ArrayLookup_For**             | **100*f**                |  **5.548 μs** | **0.0094 μs** | **0.0079 μs** |
| HashLookup_PrimaryKey       | 100*f                |  2.786 μs | 0.0040 μs | 0.0036 μs |
| HashLookup_Full             | 100*f                |  5.096 μs | 0.0056 μs | 0.0050 μs |
| HashLookup_Full_TryGetValue | 100*f                |  2.902 μs | 0.0053 μs | 0.0047 μs |



There is no ✅ or ❌.

Pavel Prchal, 2023