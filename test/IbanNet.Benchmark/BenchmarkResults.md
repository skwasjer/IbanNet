# IbanNet Benchmark Results

## Performance for >= v5.16.0

A single validation:

```
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.4894/22H2/2022Update)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.400
  [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2
  Job-EYAHAL : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2
  Job-LNJUSU : .NET 6.0.33 (6.0.3324.36610), X64 RyuJIT AVX2
  Job-MFVJWZ : .NET Framework 4.8.1 (4.8.9261.0), X64 RyuJIT VectorSize=256
```

| Method   | Runtime            | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------- |------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Validate | .NET 8.0           | 137.0 ns | 0.88 ns | 0.78 ns |  1.00 |    0.01 | 0.0253 |     160 B |        1.00 |
| Validate | .NET 6.0           | 233.5 ns | 4.06 ns | 3.39 ns |  1.70 |    0.03 | 0.0277 |     176 B |        1.10 |
| Validate | .NET Framework 4.8 | 322.5 ns | 6.35 ns | 8.70 ns |  2.35 |    0.06 | 0.0277 |     177 B |        1.11 |


### Bulk (10k) runs

#### Legend

- *Singleton_CacheReuse*: strict validation, singleton validator, reuse of rules and pattern cache
- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

| Method               | Runtime            | Count | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0      | Allocated | Alloc Ratio |
|--------------------- |------------------- |------ |---------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton_CacheReuse | .NET 8.0           | 10000 | 1.391 ms | 0.0243 ms | 0.0215 ms |  0.50 |    0.01 |  253.9063 |   1.53 MB |        0.90 |
| Singleton_CacheReuse | .NET 6.0           | 10000 | 2.346 ms | 0.0453 ms | 0.0484 ms |  0.84 |    0.02 |  277.3438 |   1.68 MB |        0.99 |
| Singleton            | .NET 8.0           | 10000 | 2.809 ms | 0.0371 ms | 0.0567 ms |  1.00 |    0.03 |  281.2500 |    1.7 MB |        1.00 |
| Singleton_CacheReuse | .NET Framework 4.8 | 10000 | 3.254 ms | 0.0211 ms | 0.0197 ms |  1.16 |    0.02 |  277.3438 |   1.68 MB |        0.99 |
| Singleton            | .NET 6.0           | 10000 | 3.363 ms | 0.0658 ms | 0.0855 ms |  1.20 |    0.04 |  281.2500 |    1.7 MB |        1.00 |
| Singleton            | .NET Framework 4.8 | 10000 | 4.248 ms | 0.0492 ms | 0.0461 ms |  1.51 |    0.03 |  281.2500 |   1.71 MB |        1.00 |
| Transient            | .NET 8.0           | 10000 | 5.311 ms | 0.0384 ms | 0.0320 ms |  1.89 |    0.04 | 1195.3125 |    7.2 MB |        4.23 |
| Transient            | .NET 6.0           | 10000 | 6.085 ms | 0.1215 ms | 0.1300 ms |  2.17 |    0.06 | 1265.6250 |   7.58 MB |        4.45 |
| Transient            | .NET Framework 4.8 | 10000 | 7.467 ms | 0.1491 ms | 0.1322 ms |  2.66 |    0.07 | 1289.0625 |   7.75 MB |        4.55 |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net8.0 --runtimes net80 net60 net48
```
