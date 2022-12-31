# IbanNet Benchmark Results

## Performance for >= v5.6

A single validation:

```
BenchmarkDotNet=v0.13.3, OS=Windows 10 (10.0.19045.2364)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 7.0.1 (7.0.122.56804), X64 RyuJIT AVX2
  Job-JXTOBY : .NET 7.0.1 (7.0.122.56804), X64 RyuJIT AVX2
  Job-YYCDTF : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT AVX2
  Job-HWMJPY : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT VectorSize=256
  Job-UNOVKA : .NET Core 3.1.32 (CoreCLR 4.700.22.55902, CoreFX 4.700.22.56512), X64 RyuJIT AVX2
```

|   Method |            Runtime |     Mean |    Error |   StdDev |   Median | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------- |------------------- |---------:|---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| Validate |           .NET 7.0 | 311.0 ns |  6.13 ns |  7.98 ns | 308.1 ns |  1.00 |    0.00 | 0.0277 |     176 B |        1.00 |
| Validate |           .NET 6.0 | 355.7 ns | 13.62 ns | 40.14 ns | 356.1 ns |  1.25 |    0.10 | 0.0277 |     176 B |        1.00 |
| Validate | .NET Framework 4.8 | 383.8 ns |  2.62 ns |  2.33 ns | 382.9 ns |  1.24 |    0.04 | 0.0277 |     177 B |        1.01 |
| Validate |      .NET Core 3.1 | 401.6 ns | 12.17 ns | 35.89 ns | 392.1 ns |  1.28 |    0.12 | 0.0277 |     176 B |        1.00 |

### Comparison with other validators

> Worth mentioning is that IbanNet validates more strictly than the other alternative (competing) libraries, yet comes out quite a lot faster and has a much lower memory footprint.

#### Legend

- *Singleton_CacheReuse*: strict validation, singleton validator, reuse of rules and pattern cache
- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

```
BenchmarkDotNet=v0.13.3, OS=Windows 10 (10.0.19045.2364)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 7.0.1 (7.0.122.56804), X64 RyuJIT AVX2
  Job-DJBKVJ : .NET 7.0.1 (7.0.122.56804), X64 RyuJIT AVX2
  Job-CBFSLH : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT AVX2
  Job-RFDFFY : .NET Core 3.1.32 (CoreCLR 4.700.22.55902, CoreFX 4.700.22.56512), X64 RyuJIT AVX2
  Job-DYFJDB : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT VectorSize=256
```

|               Method |            Runtime | Count |      Mean |     Error |    StdDev | Ratio | RatioSD |      Gen0 | Allocated | Alloc Ratio |
|--------------------- |------------------- |------ |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton_CacheReuse |           .NET 7.0 | 10000 |  3.172 ms | 0.0593 ms | 0.0554 ms |  0.79 |    0.02 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |           .NET 6.0 | 10000 |  3.239 ms | 0.0638 ms | 0.0597 ms |  0.80 |    0.02 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |      .NET Core 3.1 | 10000 |  3.989 ms | 0.0775 ms | 0.0647 ms |  0.99 |    0.02 |  273.4375 |   1.68 MB |        0.99 |
|            Singleton |           .NET 7.0 | 10000 |  4.031 ms | 0.0593 ms | 0.0554 ms |  1.00 |    0.00 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton |           .NET 6.0 | 10000 |  4.133 ms | 0.0605 ms | 0.0505 ms |  1.02 |    0.02 |  281.2500 |    1.7 MB |        1.00 |
| Singleton_CacheReuse | .NET Framework 4.8 | 10000 |  4.172 ms | 0.0729 ms | 0.1069 ms |  1.05 |    0.03 |  273.4375 |   1.68 MB |        0.99 |
|            Singleton |      .NET Core 3.1 | 10000 |  4.634 ms | 0.0281 ms | 0.0234 ms |  1.15 |    0.02 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton | .NET Framework 4.8 | 10000 |  4.962 ms | 0.0852 ms | 0.0711 ms |  1.23 |    0.03 |  281.2500 |   1.71 MB |        1.00 |
|            Transient |           .NET 6.0 | 10000 |  6.818 ms | 0.0812 ms | 0.0720 ms |  1.69 |    0.04 | 1250.0000 |    7.5 MB |        4.40 |
|            Transient |           .NET 7.0 | 10000 |  7.097 ms | 0.1381 ms | 0.1478 ms |  1.76 |    0.04 | 1250.0000 |    7.5 MB |        4.40 |
|            Transient |      .NET Core 3.1 | 10000 |  7.813 ms | 0.1265 ms | 0.1505 ms |  1.95 |    0.03 | 1250.0000 |    7.5 MB |        4.40 |
|            Transient | .NET Framework 4.8 | 10000 |  7.864 ms | 0.0449 ms | 0.0375 ms |  1.95 |    0.03 | 1265.6250 |   7.68 MB |        4.51 |
|  NuGet_IbanValidator |           .NET 6.0 | 10000 | 15.475 ms | 0.2135 ms | 0.1997 ms |  3.84 |    0.06 | 3531.2500 |  21.21 MB |       12.45 |
|  NuGet_IbanValidator |           .NET 7.0 | 10000 | 16.243 ms | 0.1429 ms | 0.1115 ms |  4.02 |    0.06 | 3531.2500 |  21.14 MB |       12.42 |
|       NuGet_IBAN4NET |           .NET 7.0 | 10000 | 35.108 ms | 0.3997 ms | 0.3544 ms |  8.71 |    0.13 | 1733.3333 |  10.48 MB |        6.15 |
|       NuGet_IBAN4NET |           .NET 6.0 | 10000 | 37.715 ms | 0.7264 ms | 0.7460 ms |  9.36 |    0.19 | 1714.2857 |  10.48 MB |        6.15 |
|       NuGet_IBAN4NET |      .NET Core 3.1 | 10000 | 40.411 ms | 0.4285 ms | 0.4008 ms | 10.03 |    0.21 | 1692.3077 |  10.48 MB |        6.15 |
|  NuGet_IbanValidator |      .NET Core 3.1 | 10000 | 41.101 ms | 0.8059 ms | 0.8957 ms | 10.19 |    0.30 | 8461.5385 |  51.08 MB |       30.00 |
|  NuGet_IbanValidator | .NET Framework 4.8 | 10000 | 56.401 ms | 0.7443 ms | 0.6962 ms | 14.00 |    0.33 | 6888.8889 |  41.73 MB |       24.50 |
|       NuGet_IBAN4NET | .NET Framework 4.8 | 10000 | 58.190 ms | 1.1510 ms | 1.5365 ms | 14.54 |    0.42 | 2000.0000 |  12.38 MB |        7.27 |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net7.0 net6.0 netcoreapp3.1 net472 --runtimes net70 net60 netcoreapp31 net48
```
