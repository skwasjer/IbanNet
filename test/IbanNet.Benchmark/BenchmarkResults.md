# IbanNet Benchmark Results

## Performance for >= v5.18.0

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

| Method   | Runtime            | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------- |------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Validate | .NET 8.0           | 183.4 ns | 1.12 ns | 0.87 ns |  1.00 |    0.01 | 0.0203 |     128 B |        1.00 |
| Validate | .NET 6.0           | 222.0 ns | 0.93 ns | 0.77 ns |  1.21 |    0.01 | 0.0203 |     128 B |        1.00 |
| Validate | .NET Framework 4.8 | 313.6 ns | 2.72 ns | 2.54 ns |  1.71 |    0.02 | 0.0200 |     128 B |        1.00 |


### Bulk (10k) runs

This benchmark validates 10,000 IBANs in a loop. It demonstrates the cost of reusing the validator instance versus creating a new instance for each validation.

#### Legend

- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

| Method    | Runtime            | Count | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0      | Allocated | Alloc Ratio |
|---------- |------------------- |------ |---------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton | .NET 8.0           | 10000 | 2.919 ms | 0.0289 ms | 0.0256 ms |  1.00 |    0.01 |  207.0313 |   1.24 MB |        1.00 |
| Singleton | .NET 6.0           | 10000 | 3.225 ms | 0.0074 ms | 0.0066 ms |  1.10 |    0.01 |  207.0313 |   1.24 MB |        1.00 |
| Singleton | .NET Framework 4.8 | 10000 | 4.075 ms | 0.0188 ms | 0.0176 ms |  1.40 |    0.01 |  203.1250 |   1.25 MB |        1.00 |
| Transient | .NET 8.0           | 10000 | 5.352 ms | 0.0480 ms | 0.0401 ms |  1.83 |    0.02 | 1125.0000 |   6.74 MB |        5.41 |
| Transient | .NET 6.0           | 10000 | 5.746 ms | 0.0296 ms | 0.0277 ms |  1.97 |    0.02 | 1187.5000 |   7.12 MB |        5.72 |
| Transient | .NET Framework 4.8 | 10000 | 7.141 ms | 0.0462 ms | 0.0386 ms |  2.45 |    0.02 | 1210.9375 |   7.29 MB |        5.86 |


### Validation per provider

Validating with `SwiftRegistryProvider` is faster and allocates less memory than the `WikipediaRegistryProvider`, because the pattern matcher is unrolled instead of depending on a more generic match implementation (using an iterator).

| Method   | Runtime            | args      | Mean     | Error   | StdDev   | Median   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------- |------------------- |---------- |---------:|--------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| Validate | .NET 8.0           | Swift     | 163.7 ns | 4.10 ns | 12.10 ns | 155.7 ns |  1.01 |    0.10 | 0.0191 |     120 B |        1.00 |
| Validate | .NET 6.0           | Swift     | 185.7 ns | 0.63 ns |  0.59 ns | 185.7 ns |  1.14 |    0.08 | 0.0191 |     120 B |        1.00 |
| Validate | .NET Framework 4.8 | Swift     | 280.7 ns | 1.40 ns |  1.24 ns | 281.3 ns |  1.72 |    0.12 | 0.0191 |     120 B |        1.00 |
|          |                    |           |          |         |          |          |       |         |        |           |             |
| Validate | .NET 8.0           | Wikipedia | 187.3 ns | 0.96 ns |  0.85 ns | 187.5 ns |  1.00 |    0.01 | 0.0191 |     120 B |        1.00 |
| Validate | .NET 6.0           | Wikipedia | 234.5 ns | 0.85 ns |  0.79 ns | 234.7 ns |  1.25 |    0.01 | 0.0191 |     120 B |        1.00 |
| Validate | .NET Framework 4.8 | Wikipedia | 363.1 ns | 2.28 ns |  2.13 ns | 363.3 ns |  1.94 |    0.01 | 0.0191 |     120 B |        1.00 |


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
| Lookup | .NET 6.0           | Swift, cold     | 12,163.234 ns | 232.7871 ns | 249.0796 ns |  0.72 |    0.02 | 2.3804 | 0.0763 |   15016 B |        0.59 |
| Lookup | .NET Framework 4.8 | Swift, cold     | 14,373.741 ns | 278.3103 ns | 341.7900 ns |  0.85 |    0.03 | 2.3956 | 0.0916 |   15116 B |        0.60 |
| Lookup | .NET 8.0           | Swift, cold     | 16,887.011 ns | 325.7609 ns | 375.1467 ns |  1.00 |    0.03 | 4.0283 | 0.1831 |   25336 B |        1.00 |
|        |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET 8.0           | Swift, warm     |      8.396 ns |   0.0386 ns |   0.0361 ns |  1.00 |    0.01 |      - |      - |         - |          NA |
| Lookup | .NET 6.0           | Swift, warm     |     17.405 ns |   0.0818 ns |   0.0765 ns |  2.07 |    0.01 |      - |      - |         - |          NA |
| Lookup | .NET Framework 4.8 | Swift, warm     |     46.590 ns |   0.2670 ns |   0.2084 ns |  5.55 |    0.03 |      - |      - |         - |          NA |
|        |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET 6.0           | Wikipedia, cold | 14,969.504 ns | 243.0710 ns | 227.3687 ns |  0.73 |    0.01 | 3.5706 | 0.2136 |   22520 B |        0.64 |
| Lookup | .NET Framework 4.8 | Wikipedia, cold | 18,027.040 ns | 288.7946 ns | 270.1387 ns |  0.88 |    0.01 | 3.5706 | 0.1831 |   22642 B |        0.64 |
| Lookup | .NET 8.0           | Wikipedia, cold | 20,375.924 ns | 176.6611 ns | 137.9253 ns |  1.00 |    0.01 | 5.5847 | 0.3052 |   35144 B |        1.00 |
|        |                    |                 |               |             |             |       |         |        |        |           |             |
| Lookup | .NET 8.0           | Wikipedia, warm |      8.401 ns |   0.0482 ns |   0.0451 ns |  1.00 |    0.01 |      - |      - |         - |          NA |
| Lookup | .NET 6.0           | Wikipedia, warm |     17.483 ns |   0.1148 ns |   0.0959 ns |  2.08 |    0.02 |      - |      - |         - |          NA |
| Lookup | .NET Framework 4.8 | Wikipedia, warm |     45.350 ns |   0.1471 ns |   0.1304 ns |  5.40 |    0.03 |      - |      - |         - |          NA |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net8.0 --runtimes net80 net60 net48
```
