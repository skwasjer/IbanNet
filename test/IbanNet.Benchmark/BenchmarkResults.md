# IbanNet Benchmark Results

## Performance for >= v5.6

A single validation:

```
BenchmarkDotNet v0.13.10, Windows 10 (10.0.19045.3930/22H2/2022Update)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  Job-GROKDG : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  Job-KWQHNY : .NET 6.0.26 (6.0.2623.60508), X64 RyuJIT AVX2
  Job-SIIXVA : .NET Core 3.1.32 (CoreCLR 4.700.22.55902, CoreFX 4.700.22.56512), X64 RyuJIT AVX2
  Job-FTQOOW : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
```

| Method   | Runtime            | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------- |------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Validate | .NET 8.0           | 134.5 ns | 2.72 ns | 3.99 ns |  1.00 |    0.00 | 0.0253 |     160 B |        1.00 |
| Validate | .NET 6.0           | 222.1 ns | 3.72 ns | 3.48 ns |  1.65 |    0.06 | 0.0279 |     176 B |        1.10 |
| Validate | .NET Core 3.1      | 274.3 ns | 5.48 ns | 6.73 ns |  2.05 |    0.07 | 0.0277 |     176 B |        1.10 |
| Validate | .NET Framework 4.8 | 292.5 ns | 2.39 ns | 2.00 ns |  2.16 |    0.06 | 0.0277 |     177 B |        1.11 |


### Comparison with other validators

> Worth mentioning is that IbanNet validates more strictly than the other alternative (competing) libraries, yet comes out quite a lot faster and has a much lower memory footprint.

#### Legend

- *Singleton_CacheReuse*: strict validation, singleton validator, reuse of rules and pattern cache
- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

| Method               | Runtime            | Count | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0      | Allocated | Alloc Ratio |
|--------------------- |------------------- |------ |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton_CacheReuse | .NET 8.0           | 10000 |  1.277 ms | 0.0074 ms | 0.0066 ms |  0.48 |    0.01 |  253.9063 |   1.53 MB |        0.90 |
| Singleton_CacheReuse | .NET 6.0           | 10000 |  2.148 ms | 0.0221 ms | 0.0207 ms |  0.81 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
| Singleton            | .NET 8.0           | 10000 |  2.638 ms | 0.0234 ms | 0.0208 ms |  1.00 |    0.00 |  281.2500 |    1.7 MB |        1.00 |
| Singleton_CacheReuse | .NET Core 3.1      | 10000 |  2.730 ms | 0.0158 ms | 0.0148 ms |  1.03 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse | .NET Framework 4.8 | 10000 |  2.988 ms | 0.0293 ms | 0.0274 ms |  1.13 |    0.02 |  277.3438 |   1.68 MB |        0.99 |
| Singleton            | .NET 6.0           | 10000 |  3.084 ms | 0.0143 ms | 0.0126 ms |  1.17 |    0.01 |  281.2500 |    1.7 MB |        1.00 |
| Singleton            | .NET Core 3.1      | 10000 |  3.663 ms | 0.0190 ms | 0.0177 ms |  1.39 |    0.01 |  281.2500 |    1.7 MB |        1.00 |
| Singleton            | .NET Framework 4.8 | 10000 |  3.957 ms | 0.0292 ms | 0.0273 ms |  1.50 |    0.01 |  281.2500 |   1.71 MB |        1.00 |
| Transient            | .NET 8.0           | 10000 |  5.062 ms | 0.0360 ms | 0.0337 ms |  1.92 |    0.02 | 1195.3125 |   7.19 MB |        4.23 |
| Transient            | .NET 6.0           | 10000 |  5.759 ms | 0.0297 ms | 0.0263 ms |  2.18 |    0.02 | 1265.6250 |   7.58 MB |        4.45 |
| Transient            | .NET Core 3.1      | 10000 |  6.647 ms | 0.0342 ms | 0.0286 ms |  2.52 |    0.02 | 1265.6250 |   7.58 MB |        4.45 |
| Transient            | .NET Framework 4.8 | 10000 |  6.907 ms | 0.0980 ms | 0.0869 ms |  2.62 |    0.04 | 1289.0625 |   7.75 MB |        4.56 |
| NuGet_IbanValidator  | .NET 8.0           | 10000 | 12.850 ms | 0.0745 ms | 0.0622 ms |  4.87 |    0.04 | 3062.5000 |  18.36 MB |       10.80 |
| NuGet_IbanValidator  | .NET 6.0           | 10000 | 13.608 ms | 0.1048 ms | 0.0929 ms |  5.16 |    0.05 | 3375.0000 |  20.21 MB |       11.88 |
| NuGet_IBAN4NET       | .NET 8.0           | 10000 | 28.821 ms | 0.1193 ms | 0.1115 ms | 10.93 |    0.10 | 1687.5000 |  10.18 MB |        5.99 |
| NuGet_IbanValidator  | .NET Core 3.1      | 10000 | 36.133 ms | 0.2985 ms | 0.2792 ms | 13.69 |    0.17 | 8071.4286 |  48.67 MB |       28.62 |
| NuGet_IBAN4NET       | .NET 6.0           | 10000 | 36.304 ms | 0.2308 ms | 0.2046 ms | 13.76 |    0.12 | 1642.8571 |  10.19 MB |        5.99 |
| NuGet_IBAN4NET       | .NET Core 3.1      | 10000 | 37.391 ms | 0.2343 ms | 0.2192 ms | 14.18 |    0.14 | 1642.8571 |  10.19 MB |        5.99 |
| NuGet_IbanValidator  | .NET Framework 4.8 | 10000 | 53.135 ms | 0.4288 ms | 0.3801 ms | 20.14 |    0.22 | 6600.0000 |  39.89 MB |       23.45 |
| NuGet_IBAN4NET       | .NET Framework 4.8 | 10000 | 53.914 ms | 0.3762 ms | 0.3519 ms | 20.44 |    0.23 | 2000.0000 |  12.05 MB |        7.08 |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net8.0 --runtimes net80 net60 netcoreapp31 net48
```
