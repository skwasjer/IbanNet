# IbanNet Benchmark Results

## Performance for >= v5.18.0

A single validation:

```
BenchmarkDotNet v0.15.7, Windows 10 (10.0.19045.6575/22H2/2022Update)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.100
  [Host]             : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v3
  .NET 8.0           : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v3
  .NET Framework 4.8 : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
```

| Method   | Job                | Runtime            | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------- |------------------- |------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Validate | .NET 10.0          | .NET 10.0          | 149.7 ns | 2.39 ns | 2.00 ns |  1.00 |    0.02 | 0.0088 |      56 B |        1.00 |
| Validate | .NET 8.0           | .NET 8.0           | 169.8 ns | 1.30 ns | 1.15 ns |  1.13 |    0.02 | 0.0088 |      56 B |        1.00 |
| Validate | .NET Framework 4.8 | .NET Framework 4.8 | 269.6 ns | 3.88 ns | 3.63 ns |  1.80 |    0.03 | 0.0086 |      56 B |        1.00 |

### Bulk (10k) runs

This benchmark validates 10,000 IBANs in a loop. It demonstrates the cost of reusing the validator instance versus creating a new instance for each validation.

#### Legend

- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

| Method    | Job                | Runtime            | Count | Mean     | Error     | StdDev    | Median   | Ratio | RatioSD | Gen0      | Allocated  | Alloc Ratio |
|---------- |------------------- |------------------- |------ |---------:|----------:|----------:|---------:|------:|--------:|----------:|-----------:|------------:|
| Singleton | .NET 10.0          | .NET 10.0          | 10000 | 2.279 ms | 0.0452 ms | 0.0464 ms | 2.267 ms |  1.00 |    0.03 |   85.9375 |  546.88 KB |        1.00 |
| Singleton | .NET 8.0           | .NET 8.0           | 10000 | 2.559 ms | 0.0358 ms | 0.0299 ms | 2.547 ms |  1.12 |    0.03 |   85.9375 |  546.88 KB |        1.00 |
| Singleton | .NET Framework 4.8 | .NET Framework 4.8 | 10000 | 4.389 ms | 0.0766 ms | 0.1123 ms | 4.365 ms |  1.93 |    0.06 |   85.9375 |  548.44 KB |        1.00 |
| Transient | .NET 10.0          | .NET 10.0          | 10000 | 4.851 ms | 0.1634 ms | 0.4819 ms | 4.667 ms |  2.13 |    0.21 |  968.7500 |  5937.5 KB |       10.86 |
| Transient | .NET 8.0           | .NET 8.0           | 10000 | 5.155 ms | 0.0320 ms | 0.0250 ms | 5.151 ms |  2.26 |    0.04 |  968.7500 |  5937.5 KB |       10.86 |
| Transient | .NET Framework 4.8 | .NET Framework 4.8 | 10000 | 7.541 ms | 0.1480 ms | 0.2123 ms | 7.462 ms |  3.31 |    0.11 | 1054.6875 | 6503.44 KB |       11.89 |

### Validation per provider

Validating with `SwiftRegistryProvider` is faster and allocates less memory than the `WikipediaRegistryProvider`, because the pattern matcher is unrolled instead of depending on a more generic match implementation (using an iterator).

| Method   | Job                | Runtime            | args      | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------- |------------------- |------------------- |---------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Validate | .NET 10.0          | .NET 10.0          | Swift     | 121.0 ns | 2.37 ns | 2.64 ns |  1.00 |    0.03 | 0.0088 |      56 B |        1.00 |
| Validate | .NET 8.0           | .NET 8.0           | Swift     | 145.9 ns | 1.20 ns | 1.07 ns |  1.21 |    0.03 | 0.0088 |      56 B |        1.00 |
| Validate | .NET Framework 4.8 | .NET Framework 4.8 | Swift     | 253.5 ns | 3.10 ns | 2.59 ns |  2.10 |    0.05 | 0.0086 |      56 B |        1.00 |
|          |                    |                    |           |          |         |         |       |         |        |           |             |
| Validate | .NET 10.0          | .NET 10.0          | Wikipedia | 161.7 ns | 3.08 ns | 2.73 ns |  1.00 |    0.02 | 0.0088 |      56 B |        1.00 |
| Validate | .NET 8.0           | .NET 8.0           | Wikipedia | 176.6 ns | 1.39 ns | 1.16 ns |  1.09 |    0.02 | 0.0088 |      56 B |        1.00 |
| Validate | .NET Framework 4.8 | .NET Framework 4.8 | Wikipedia | 352.9 ns | 7.05 ns | 9.66 ns |  2.18 |    0.07 | 0.0086 |      56 B |        1.00 |

### Initialize registry

The registry lazy loads definitions. This benchmark only measures the cost of the constructor, definitions are not yet loaded.

| Method     | Job                | Runtime            | args      | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------- |------------------- |------------------- |---------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| Initialize | .NET 10.0          | .NET 10.0          | Swift     | 25.28 ns | 0.528 ns | 0.648 ns |  1.00 |    0.04 | 0.0255 |     160 B |        1.00 |
| Initialize | .NET 8.0           | .NET 8.0           | Swift     | 28.39 ns | 0.573 ns | 0.613 ns |  1.12 |    0.04 | 0.0255 |     160 B |        1.00 |
| Initialize | .NET Framework 4.8 | .NET Framework 4.8 | Swift     | 33.91 ns | 0.684 ns | 0.640 ns |  1.34 |    0.04 | 0.0268 |     168 B |        1.05 |
|            |                    |                    |           |          |          |          |       |         |        |           |             |
| Initialize | .NET 10.0          | .NET 10.0          | Wikipedia | 23.86 ns | 0.473 ns | 0.465 ns |  1.00 |    0.03 | 0.0255 |     160 B |        1.00 |
| Initialize | .NET 8.0           | .NET 8.0           | Wikipedia | 26.09 ns | 0.433 ns | 0.405 ns |  1.09 |    0.03 | 0.0255 |     160 B |        1.00 |
| Initialize | .NET Framework 4.8 | .NET Framework 4.8 | Wikipedia | 33.94 ns | 0.607 ns | 0.538 ns |  1.42 |    0.03 | 0.0268 |     168 B |        1.05 |


### Look up country in registry

- cold = first run
- warm = subsequent runs
> .NET 8+ is slower on first run, because the registry uses `FrozenDictionary` which has a higher initialization cost but is faster on repeated lookups.  
> Furthermore, the `WikipediaRegistryProvider` is a bit slower and allocates more memory on initialization because it is not optimized to avoid executing the pattern tokenizers.  

| Method | Job                | Runtime            | args            | Mean          | Error       | StdDev      | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------- |------------------- |------------------- |---------------- |--------------:|------------:|------------:|------:|--------:|-------:|-------:|----------:|------------:|
| Lookup | .NET 10.0          | .NET 10.0          | Swift, cold     | 13,870.923 ns |  76.3401 ns |  67.6735 ns |  1.00 |    0.01 | 4.0436 | 0.0916 |   25392 B |        1.00 |
| Lookup | .NET Framework 4.8 | .NET Framework 4.8 | Swift, cold     | 14,673.388 ns | 109.1174 ns | 102.0685 ns |  1.06 |    0.01 | 2.4109 | 0.1068 |   15189 B |        0.60 |
| Lookup | .NET 8.0           | .NET 8.0           | Swift, cold     | 17,471.782 ns | 227.3556 ns | 212.6685 ns |  1.26 |    0.02 | 4.0283 | 0.2441 |   25408 B |        1.00 |
|        |                    |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET 10.0          | .NET 10.0          | Swift, warm     |      6.624 ns |   0.0833 ns |   0.0779 ns |  1.00 |    0.02 |      - |      - |         - |          NA |
| Lookup | .NET 8.0           | .NET 8.0           | Swift, warm     |      8.705 ns |   0.1017 ns |   0.0952 ns |  1.31 |    0.02 |      - |      - |         - |          NA |
| Lookup | .NET Framework 4.8 | .NET Framework 4.8 | Swift, warm     |     47.295 ns |   0.3044 ns |   0.2847 ns |  7.14 |    0.09 |      - |      - |         - |          NA |
|        |                    |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET 10.0          | .NET 10.0          | Wikipedia, cold | 16,733.107 ns | 167.6565 ns | 148.6231 ns |  1.00 |    0.01 | 5.5847 | 0.3357 |   35200 B |        1.00 |
| Lookup | .NET Framework 4.8 | .NET Framework 4.8 | Wikipedia, cold | 18,002.044 ns | 334.7232 ns | 313.1003 ns |  1.08 |    0.02 | 3.6011 | 0.1831 |   22715 B |        0.65 |
| Lookup | .NET 8.0           | .NET 8.0           | Wikipedia, cold | 21,260.999 ns | 281.7994 ns | 263.5953 ns |  1.27 |    0.02 | 5.5847 | 0.3662 |   35216 B |        1.00 |
|        |                    |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET 10.0          | .NET 10.0          | Wikipedia, warm |      6.610 ns |   0.0626 ns |   0.0523 ns |  1.00 |    0.01 |      - |      - |         - |          NA |
| Lookup | .NET 8.0           | .NET 8.0           | Wikipedia, warm |      8.642 ns |   0.0714 ns |   0.0633 ns |  1.31 |    0.01 |      - |      - |         - |          NA |
| Lookup | .NET Framework 4.8 | .NET Framework 4.8 | Wikipedia, warm |     46.113 ns |   0.3820 ns |   0.3574 ns |  6.98 |    0.07 |      - |      - |         - |          NA |

### Mod-97,10

| Method | Job                | Runtime            | buffer           | Mean     | Error    | StdDev   | Ratio | RatioSD | Allocated | Alloc Ratio |
|------- |------------------- |------------------- |----------------- |---------:|---------:|---------:|------:|--------:|----------:|------------:|
| Test   | .NET 10.0          | .NET 10.0          | 0123456789012345 | 18.96 ns | 0.384 ns | 0.359 ns |  1.00 |    0.03 |         - |          NA |
| Test   | .NET 8.0           | .NET 8.0           | 0123456789012345 | 20.00 ns | 0.257 ns | 0.241 ns |  1.06 |    0.02 |         - |          NA |
| Test   | .NET Framework 4.8 | .NET Framework 4.8 | 0123456789012345 | 29.88 ns | 0.302 ns | 0.283 ns |  1.58 |    0.03 |         - |          NA |
|        |                    |                    |                  |          |          |          |       |         |           |             |
| Test   | .NET 10.0          | .NET 10.0          | 01234567ABCDEFGH | 21.75 ns | 0.123 ns | 0.109 ns |  1.00 |    0.01 |         - |          NA |
| Test   | .NET 8.0           | .NET 8.0           | 01234567ABCDEFGH | 30.72 ns | 0.192 ns | 0.179 ns |  1.41 |    0.01 |         - |          NA |
| Test   | .NET Framework 4.8 | .NET Framework 4.8 | 01234567ABCDEFGH | 44.83 ns | 0.433 ns | 0.405 ns |  2.06 |    0.02 |         - |          NA |
|        |                    |                    |                  |          |          |          |       |         |           |             |
| Test   | .NET 10.0          | .NET 10.0          | ABCDEFGHIJKLMNOP | 23.21 ns | 0.199 ns | 0.176 ns |  1.00 |    0.01 |         - |          NA |
| Test   | .NET 8.0           | .NET 8.0           | ABCDEFGHIJKLMNOP | 24.60 ns | 0.181 ns | 0.151 ns |  1.06 |    0.01 |         - |          NA |
| Test   | .NET Framework 4.8 | .NET Framework 4.8 | ABCDEFGHIJKLMNOP | 43.40 ns | 0.304 ns | 0.285 ns |  1.87 |    0.02 |         - |          NA |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net10.0 --runtimes net10.0 net80 net48
```
