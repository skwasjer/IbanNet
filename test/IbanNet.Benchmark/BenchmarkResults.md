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

#### Legend

- *Singleton_CacheReuse*: strict validation, singleton validator, reuse of rules and pattern cache
- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

| Method               | Runtime            | Count | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0      | Allocated | Alloc Ratio |
|--------------------- |------------------- |------ |---------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton_CacheReuse | .NET 8.0           | 10000 | 1.823 ms | 0.0053 ms | 0.0044 ms |  0.68 |    0.00 |  279.2969 |   1.68 MB |        0.99 |
| Singleton_CacheReuse | .NET 6.0           | 10000 | 2.233 ms | 0.0063 ms | 0.0052 ms |  0.84 |    0.00 |  277.3438 |   1.68 MB |        0.99 |
| Singleton            | .NET 8.0           | 10000 | 2.668 ms | 0.0143 ms | 0.0134 ms |  1.00 |    0.01 |  281.2500 |    1.7 MB |        1.00 |
| Singleton_CacheReuse | .NET Framework 4.8 | 10000 | 3.026 ms | 0.0092 ms | 0.0086 ms |  1.13 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
| Singleton            | .NET 6.0           | 10000 | 3.218 ms | 0.0348 ms | 0.0271 ms |  1.21 |    0.01 |  281.2500 |    1.7 MB |        1.00 |
| Singleton            | .NET Framework 4.8 | 10000 | 4.065 ms | 0.0240 ms | 0.0224 ms |  1.52 |    0.01 |  281.2500 |   1.71 MB |        1.00 |
| Transient            | .NET 8.0           | 10000 | 5.160 ms | 0.0430 ms | 0.0403 ms |  1.93 |    0.02 | 1195.3125 |    7.2 MB |        4.23 |
| Transient            | .NET 6.0           | 10000 | 5.708 ms | 0.0767 ms | 0.0717 ms |  2.14 |    0.03 | 1265.6250 |   7.58 MB |        4.45 |
| Transient            | .NET Framework 4.8 | 10000 | 7.068 ms | 0.0284 ms | 0.0251 ms |  2.65 |    0.02 | 1289.0625 |   7.75 MB |        4.55 |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net8.0 --runtimes net80 net60 net48
```
