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

| Method   | Job                | Runtime            | Mean     | Error   | StdDev  | Ratio | Gen0   | Allocated | Alloc Ratio |
|--------- |------------------- |------------------- |---------:|--------:|--------:|------:|-------:|----------:|------------:|
| Validate | .NET 8.0           | .NET 8.0           | 179.5 ns | 0.74 ns | 0.62 ns |  1.00 | 0.0088 |      56 B |        1.00 |
| Validate | .NET Framework 4.8 | .NET Framework 4.8 | 276.1 ns | 1.66 ns | 1.47 ns |  1.54 | 0.0086 |      56 B |        1.00 |

### Bulk (10k) runs

This benchmark validates 10,000 IBANs in a loop. It demonstrates the cost of reusing the validator instance versus creating a new instance for each validation.

#### Legend

- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

| Method    | Job                | Runtime            | Count | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0      | Allocated  | Alloc Ratio |
|---------- |------------------- |------------------- |------ |---------:|----------:|----------:|------:|--------:|----------:|-----------:|------------:|
| Singleton | .NET 8.0           | .NET 8.0           | 10000 | 2.540 ms | 0.0195 ms | 0.0173 ms |  1.00 |    0.01 |   85.9375 |  546.88 KB |        1.00 |
| Singleton | .NET Framework 4.8 | .NET Framework 4.8 | 10000 | 4.183 ms | 0.0166 ms | 0.0139 ms |  1.65 |    0.01 |   85.9375 |  548.44 KB |        1.00 |
| Transient | .NET 8.0           | .NET 8.0           | 10000 | 5.048 ms | 0.0431 ms | 0.0382 ms |  1.99 |    0.02 |  968.7500 |  5937.5 KB |       10.86 |
| Transient | .NET Framework 4.8 | .NET Framework 4.8 | 10000 | 7.145 ms | 0.0449 ms | 0.0398 ms |  2.81 |    0.02 | 1054.6875 | 6503.44 KB |       11.89 |

### Validation per provider

Validating with `SwiftRegistryProvider` is faster and allocates less memory than the `WikipediaRegistryProvider`, because the pattern matcher is unrolled instead of depending on a more generic match implementation (using an iterator).

| Method   | Job                | Runtime            | args      | Mean     | Error   | StdDev  | Ratio | Gen0   | Allocated | Alloc Ratio |
|--------- |------------------- |------------------- |---------- |---------:|--------:|--------:|------:|-------:|----------:|------------:|
| Validate | .NET 8.0           | .NET 8.0           | Swift     | 143.0 ns | 0.29 ns | 0.25 ns |  1.00 | 0.0088 |      56 B |        1.00 |
| Validate | .NET Framework 4.8 | .NET Framework 4.8 | Swift     | 251.9 ns | 1.45 ns | 1.35 ns |  1.76 | 0.0086 |      56 B |        1.00 |
|          |                    |                    |           |          |         |         |       |        |           |             |
| Validate | .NET 8.0           | .NET 8.0           | Wikipedia | 185.7 ns | 1.38 ns | 1.15 ns |  1.00 | 0.0088 |      56 B |        1.00 |
| Validate | .NET Framework 4.8 | .NET Framework 4.8 | Wikipedia | 356.5 ns | 0.99 ns | 0.88 ns |  1.92 | 0.0086 |      56 B |        1.00 |

### Initialize registry

The registry lazy loads definitions. This benchmark only measures the cost of the constructor, definitions are not yet loaded.

| Method     | Runtime            | args      | Mean     | Error    | StdDev   | Median   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------- |------------------- |---------- |---------:|---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| Initialize | .NET 8.0           | Swift     | 25.65 ns | 0.118 ns | 0.105 ns | 25.63 ns |  1.00 |    0.01 | 0.0242 |     152 B |        1.00 |
| Initialize | .NET Framework 4.8 | Swift     | 32.40 ns | 0.631 ns | 0.675 ns | 32.04 ns |  1.26 |    0.03 | 0.0255 |     160 B |        1.05 |
|            |                    |           |          |          |          |          |       |         |        |           |             |
| Initialize | .NET 8.0           | Wikipedia | 27.49 ns | 0.540 ns | 0.479 ns | 27.23 ns |  1.00 |    0.02 | 0.0242 |     152 B |        1.00 |
| Initialize | .NET Framework 4.8 | Wikipedia | 31.95 ns | 0.185 ns | 0.155 ns | 31.92 ns |  1.16 |    0.02 | 0.0255 |     160 B |        1.05 |


### Look up country in registry

- cold = first run
- warm = subsequent runs
> .NET 8+ is slower on first run, because the registry uses `FrozenDictionary` which has a higher initialization cost but is faster on repeated lookups.  
> Furthermore, the `WikipediaRegistryProvider` is a bit slower and allocates more memory on initialization because it is not optimized to avoid executing the pattern tokenizers.  

| Method | Runtime            | args            | Mean          | Error       | StdDev      | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------- |------------------- |---------------- |--------------:|------------:|------------:|------:|--------:|-------:|-------:|----------:|------------:|
| Lookup | .NET Framework 4.8 | Swift, cold     | 14,373.741 ns | 278.3103 ns | 341.7900 ns |  0.85 |    0.03 | 2.3956 | 0.0916 |   15116 B |        0.60 |
| Lookup | .NET 8.0           | Swift, cold     | 16,887.011 ns | 325.7609 ns | 375.1467 ns |  1.00 |    0.03 | 4.0283 | 0.1831 |   25336 B |        1.00 |
|        |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET 8.0           | Swift, warm     |      8.396 ns |   0.0386 ns |   0.0361 ns |  1.00 |    0.01 |      - |      - |         - |          NA |
| Lookup | .NET Framework 4.8 | Swift, warm     |     46.590 ns |   0.2670 ns |   0.2084 ns |  5.55 |    0.03 |      - |      - |         - |          NA |
|        |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET Framework 4.8 | Wikipedia, cold | 18,027.040 ns | 288.7946 ns | 270.1387 ns |  0.88 |    0.01 | 3.5706 | 0.1831 |   22642 B |        0.64 |
| Lookup | .NET 8.0           | Wikipedia, cold | 20,375.924 ns | 176.6611 ns | 137.9253 ns |  1.00 |    0.01 | 5.5847 | 0.3052 |   35144 B |        1.00 |
|        |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET 8.0           | Wikipedia, warm |      8.401 ns |   0.0482 ns |   0.0451 ns |  1.00 |    0.01 |      - |      - |         - |          NA |
| Lookup | .NET Framework 4.8 | Wikipedia, warm |     45.350 ns |   0.1471 ns |   0.1304 ns |  5.40 |    0.03 |      - |      - |         - |          NA |

### Mod-97,10

| Method | Job                | Runtime            | buffer           | Mean     | Error    | StdDev   | Ratio | Allocated | Alloc Ratio |
|------- |------------------- |------------------- |----------------- |---------:|---------:|---------:|------:|----------:|------------:|
| Test   | .NET 8.0           | .NET 8.0           | 0123456789012345 | 19.63 ns | 0.110 ns | 0.092 ns |  1.00 |         - |          NA |
| Test   | .NET Framework 4.8 | .NET Framework 4.8 | 0123456789012345 | 27.75 ns | 0.150 ns | 0.141 ns |  1.41 |         - |          NA |
|        |                    |                    |                  |          |          |          |       |           |             |
| Test   | .NET 8.0           | .NET 8.0           | 01234567ABCDEFGH | 26.93 ns | 0.190 ns | 0.168 ns |  1.00 |         - |          NA |
| Test   | .NET Framework 4.8 | .NET Framework 4.8 | 01234567ABCDEFGH | 37.03 ns | 0.061 ns | 0.054 ns |  1.38 |         - |          NA |
|        |                    |                    |                  |          |          |          |       |           |             |
| Test   | .NET 8.0           | .NET 8.0           | ABCDEFGHIJKLMNOP | 25.59 ns | 0.097 ns | 0.081 ns |  1.00 |         - |          NA |
| Test   | .NET Framework 4.8 | .NET Framework 4.8 | ABCDEFGHIJKLMNOP | 40.39 ns | 0.361 ns | 0.338 ns |  1.58 |         - |          NA |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net8.0 --runtimes net80 net48
```
