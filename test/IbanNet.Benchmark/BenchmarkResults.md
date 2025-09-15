# IbanNet Benchmark Results

## Performance for >= v5.16.0

A single validation:

```
BenchmarkDotNet v0.15.2, Windows 10 (10.0.19045.6332/22H2/2022Update)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.100-preview.7.25380.108
  [Host]     : .NET 8.0.20 (8.0.2025.41914), X64 RyuJIT AVX2
  Job-YQLAGV : .NET 8.0.20 (8.0.2025.41914), X64 RyuJIT AVX2
  Job-ZEDXUT : .NET 6.0.36 (6.0.3624.51421), X64 RyuJIT AVX2
  Job-OWJQPM : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
```

| Method   | Runtime            | Mean     | Error   | StdDev  | Ratio | Gen0   | Allocated | Alloc Ratio |
|--------- |------------------- |---------:|--------:|--------:|------:|-------:|----------:|------------:|
| Validate | .NET 8.0           | 177.3 ns | 1.38 ns | 1.15 ns |  1.00 | 0.0279 |     176 B |        1.00 |
| Validate | .NET 6.0           | 224.4 ns | 0.29 ns | 0.26 ns |  1.27 | 0.0279 |     176 B |        1.00 |
| Validate | .NET Framework 4.8 | 311.8 ns | 1.48 ns | 1.38 ns |  1.76 | 0.0277 |     177 B |        1.01 |


### Bulk (10k) runs

This benchmark validates 10,000 IBANs in a loop. It demonstrates the cost of reusing the validator instance versus creating a new instance for each validation.

#### Legend

- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

| Method    | Runtime            | Count | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0      | Allocated | Alloc Ratio |
|---------- |------------------- |------ |---------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton | .NET 8.0           | 10000 | 2.692 ms | 0.0332 ms | 0.0277 ms |  1.00 |    0.01 |  281.2500 |    1.7 MB |        1.00 |
| Singleton | .NET 6.0           | 10000 | 3.180 ms | 0.0087 ms | 0.0073 ms |  1.18 |    0.01 |  281.2500 |    1.7 MB |        1.00 |
| Singleton | .NET Framework 4.8 | 10000 | 4.048 ms | 0.0808 ms | 0.0865 ms |  1.50 |    0.03 |  281.2500 |   1.71 MB |        1.00 |
| Transient | .NET 8.0           | 10000 | 5.217 ms | 0.0720 ms | 0.0739 ms |  1.94 |    0.03 | 1195.3125 |    7.2 MB |        4.23 |
| Transient | .NET 6.0           | 10000 | 5.807 ms | 0.0377 ms | 0.0353 ms |  2.16 |    0.02 | 1265.6250 |   7.58 MB |        4.45 |
| Transient | .NET Framework 4.8 | 10000 | 6.985 ms | 0.0371 ms | 0.0310 ms |  2.60 |    0.03 | 1289.0625 |   7.75 MB |        4.55 |

### Initialize registry

The registry lazy loads definitions. This benchmark only measures the cost of the constructor, definitions are not yet loaded.

| Method     | Runtime            | args      | Mean     | Error    | StdDev   | Median   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------- |------------------- |---------- |---------:|---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| Initialize | .NET 8.0           | Swift     | 25.65 ns | 0.118 ns | 0.105 ns | 25.63 ns |  1.00 |    0.01 | 0.0242 |     152 B |        1.00 |
| Initialize | .NET 6.0           | Swift     | 26.79 ns | 0.425 ns | 0.722 ns | 26.44 ns |  1.04 |    0.03 | 0.0242 |     152 B |        1.00 |
| Initialize | .NET Framework 4.8 | Swift     | 32.40 ns | 0.631 ns | 0.675 ns | 32.04 ns |  1.26 |    0.03 | 0.0255 |     160 B |        1.05 |
|            |                    |           |          |          |          |          |       |         |        |           |             |
| Initialize | .NET 8.0           | Wikipedia | 27.49 ns | 0.540 ns | 0.479 ns | 27.23 ns |  1.00 |    0.02 | 0.0242 |     152 B |        1.00 |
| Initialize | .NET 6.0           | Wikipedia | 27.81 ns | 0.482 ns | 0.428 ns | 27.80 ns |  1.01 |    0.02 | 0.0242 |     152 B |        1.00 |
| Initialize | .NET Framework 4.8 | Wikipedia | 31.95 ns | 0.185 ns | 0.155 ns | 31.92 ns |  1.16 |    0.02 | 0.0255 |     160 B |        1.05 |

### Look up country in registry

- cold = first run
- warm = subsequent runs
> .NET 8+ is slower on first run, because the registry uses `FrozenDictionary` which has a higher initialization cost but is faster on repeated lookups.  
> Furthermore, the `WikipediaRegistryProvider` is a bit slower and allocates more memory on initialization because it is not optimized to avoid executing the pattern tokenizers.  

| Method | Runtime            | args            | Mean          | Error       | StdDev      | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------- |------------------- |---------------- |--------------:|------------:|------------:|------:|--------:|-------:|-------:|----------:|------------:|
| Lookup | .NET 6.0           | Swift, cold     | 11,709.171 ns | 129.0066 ns | 107.7263 ns |  0.71 |    0.01 | 2.3804 | 0.0763 |   15016 B |        0.59 |
| Lookup | .NET Framework 4.8 | Swift, cold     | 13,716.381 ns |  21.4214 ns |  16.7245 ns |  0.83 |    0.01 | 2.3956 | 0.0916 |   15116 B |        0.60 |
| Lookup | .NET 8.0           | Swift, cold     | 16,568.963 ns | 217.9237 ns | 203.8460 ns |  1.00 |    0.02 | 4.0283 | 0.1831 |   25336 B |        1.00 |
|        |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET 8.0           | Swift, warm     |      8.484 ns |   0.0296 ns |   0.0262 ns |  1.00 |    0.00 |      - |      - |         - |          NA |
| Lookup | .NET 6.0           | Swift, warm     |     17.480 ns |   0.0230 ns |   0.0192 ns |  2.06 |    0.01 |      - |      - |         - |          NA |
| Lookup | .NET Framework 4.8 | Swift, warm     |     46.586 ns |   0.1078 ns |   0.0842 ns |  5.49 |    0.02 |      - |      - |         - |          NA |
|        |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET 6.0           | Wikipedia, cold | 14,787.281 ns |  27.0009 ns |  23.9356 ns |  0.72 |    0.01 | 3.5858 | 0.2136 |   22520 B |        0.64 |
| Lookup | .NET Framework 4.8 | Wikipedia, cold | 17,305.278 ns |  38.4931 ns |  30.0529 ns |  0.85 |    0.02 | 3.5706 | 0.1831 |   22643 B |        0.64 |
| Lookup | .NET 8.0           | Wikipedia, cold | 20,460.055 ns | 408.9646 ns | 401.6579 ns |  1.00 |    0.03 | 5.5847 | 0.3052 |   35144 B |        1.00 |
|        |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET 8.0           | Wikipedia, warm |      8.823 ns |   0.0639 ns |   0.0566 ns |  1.00 |    0.01 |      - |      - |         - |          NA |
| Lookup | .NET 6.0           | Wikipedia, warm |     17.830 ns |   0.1659 ns |   0.1552 ns |  2.02 |    0.02 |      - |      - |         - |          NA |
| Lookup | .NET Framework 4.8 | Wikipedia, warm |     46.609 ns |   0.7311 ns |   0.6481 ns |  5.28 |    0.08 |      - |      - |         - |          NA |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net8.0 --runtimes net80 net60 net48
```
