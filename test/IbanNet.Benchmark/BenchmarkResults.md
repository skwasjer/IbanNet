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
| Validate | .NET 8.0           | .NET 8.0           | 185.0 ns | 0.80 ns | 0.63 ns |  1.00 |    0.00 | 0.0203 |     128 B |        1.00 |
| Validate | .NET Framework 4.8 | .NET Framework 4.8 | 288.3 ns | 3.98 ns | 3.32 ns |  1.56 |    0.02 | 0.0200 |     128 B |        1.00 |


### Bulk (10k) runs

This benchmark validates 10,000 IBANs in a loop. It demonstrates the cost of reusing the validator instance versus creating a new instance for each validation.

#### Legend

- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

| Method    | Job                | Runtime            | Count | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0      | Allocated | Alloc Ratio |
|---------- |------------------- |------------------- |------ |---------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton | .NET 8.0           | .NET 8.0           | 10000 | 2.540 ms | 0.0231 ms | 0.0181 ms |  1.00 |    0.01 |  207.0313 |   1.24 MB |        1.00 |
| Singleton | .NET Framework 4.8 | .NET Framework 4.8 | 10000 | 4.158 ms | 0.0171 ms | 0.0152 ms |  1.64 |    0.01 |  203.1250 |   1.25 MB |        1.00 |
| Transient | .NET 8.0           | .NET 8.0           | 10000 | 5.208 ms | 0.0713 ms | 0.0632 ms |  2.05 |    0.03 | 1125.0000 |   6.74 MB |        5.41 |
| Transient | .NET Framework 4.8 | .NET Framework 4.8 | 10000 | 7.654 ms | 0.1526 ms | 0.2420 ms |  3.01 |    0.10 | 1210.9375 |   7.29 MB |        5.86 |


### Validation per provider

Validating with `SwiftRegistryProvider` is faster and allocates less memory than the `WikipediaRegistryProvider`, because the pattern matcher is unrolled instead of depending on a more generic match implementation (using an iterator).

| Method   | Job                | Runtime            | args      | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------- |------------------- |------------------- |---------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Validate | .NET 8.0           | .NET 8.0           | Swift     | 153.2 ns | 0.98 ns | 0.92 ns |  1.00 |    0.01 | 0.0191 |     120 B |        1.00 |
| Validate | .NET Framework 4.8 | .NET Framework 4.8 | Swift     | 274.8 ns | 2.72 ns | 2.41 ns |  1.79 |    0.02 | 0.0191 |     120 B |        1.00 |
|          |                    |                    |           |          |         |         |       |         |        |           |             |
| Validate | .NET 8.0           | .NET 8.0           | Wikipedia | 187.1 ns | 1.20 ns | 1.12 ns |  1.00 |    0.01 | 0.0191 |     120 B |        1.00 |
| Validate | .NET Framework 4.8 | .NET Framework 4.8 | Wikipedia | 359.1 ns | 5.76 ns | 4.81 ns |  1.92 |    0.03 | 0.0191 |     120 B |        1.00 |


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

| Method | Job                | Runtime            | buffer           | Mean     | Error    | StdDev   | Ratio | RatioSD | Allocated | Alloc Ratio |
|------- |------------------- |------------------- |----------------- |---------:|---------:|---------:|------:|--------:|----------:|------------:|
| Test   | .NET 8.0           | .NET 8.0           | 0123456789012345 | 19.42 ns | 0.377 ns | 0.315 ns |  1.00 |    0.02 |         - |          NA |
| Test   | .NET Framework 4.8 | .NET Framework 4.8 | 0123456789012345 | 21.79 ns | 0.244 ns | 0.228 ns |  1.12 |    0.02 |         - |          NA |
|        |                    |                    |                  |          |          |          |       |         |           |             |
| Test   | .NET 8.0           | .NET 8.0           | 01234567ABCDEFGH | 24.85 ns | 0.103 ns | 0.092 ns |  1.00 |    0.01 |         - |          NA |
| Test   | .NET Framework 4.8 | .NET Framework 4.8 | 01234567ABCDEFGH | 29.39 ns | 0.151 ns | 0.118 ns |  1.18 |    0.01 |         - |          NA |
|        |                    |                    |                  |          |          |          |       |         |           |             |
| Test   | .NET 8.0           | .NET 8.0           | ABCDEFGHIJKLMNOP | 24.73 ns | 0.040 ns | 0.033 ns |  1.00 |    0.00 |         - |          NA |
| Test   | .NET Framework 4.8 | .NET Framework 4.8 | ABCDEFGHIJKLMNOP | 32.65 ns | 0.182 ns | 0.142 ns |  1.32 |    0.01 |         - |          NA |
### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net8.0 --runtimes net80 net48
```
