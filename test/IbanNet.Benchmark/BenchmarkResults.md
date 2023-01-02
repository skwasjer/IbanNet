# IbanNet Benchmark Results

## Performance for >= v5.6

A single validation:

```
BenchmarkDotNet=v0.13.3, OS=Windows 10 (10.0.19045.2364)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 7.0.1 (7.0.122.56804), X64 RyuJIT AVX2
  Job-PDVMXO : .NET 7.0.1 (7.0.122.56804), X64 RyuJIT AVX2
  Job-JGFNRE : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT AVX2
  Job-NNJZCH : .NET Core 3.1.32 (CoreCLR 4.700.22.55902, CoreFX 4.700.22.56512), X64 RyuJIT AVX2
  Job-VKJTKL : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT VectorSize=256
```

|   Method |            Runtime |     Mean |   Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------- |------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Validate |           .NET 7.0 | 276.2 ns | 2.26 ns | 2.11 ns |  1.00 |    0.00 | 0.0277 |     176 B |        1.00 |
| Validate |           .NET 6.0 | 293.0 ns | 4.48 ns | 3.97 ns |  1.06 |    0.01 | 0.0277 |     176 B |        1.00 |
| Validate |      .NET Core 3.1 | 350.2 ns | 6.77 ns | 7.24 ns |  1.27 |    0.03 | 0.0277 |     176 B |        1.00 |
| Validate | .NET Framework 4.8 | 364.6 ns | 1.48 ns | 1.16 ns |  1.32 |    0.01 | 0.0277 |     177 B |        1.01 |


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
  Job-RBDIUL : .NET 7.0.1 (7.0.122.56804), X64 RyuJIT AVX2
  Job-QIOUZG : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT AVX2
  Job-LVAWME : .NET Core 3.1.32 (CoreCLR 4.700.22.55902, CoreFX 4.700.22.56512), X64 RyuJIT AVX2
  Job-ZHHYVC : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT VectorSize=256
```

|               Method |            Runtime | Count |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |      Gen0 | Allocated | Alloc Ratio |
|--------------------- |------------------- |------ |----------:|----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton_CacheReuse |           .NET 7.0 | 10000 |  2.910 ms | 0.0213 ms | 0.0228 ms |  2.902 ms |  0.77 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |           .NET 6.0 | 10000 |  2.969 ms | 0.0135 ms | 0.0119 ms |  2.965 ms |  0.79 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |      .NET Core 3.1 | 10000 |  3.715 ms | 0.0739 ms | 0.1476 ms |  3.628 ms |  1.01 |    0.05 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse | .NET Framework 4.8 | 10000 |  3.753 ms | 0.0735 ms | 0.1054 ms |  3.727 ms |  1.01 |    0.02 |  277.3438 |   1.68 MB |        0.99 |
|            Singleton |           .NET 7.0 | 10000 |  3.776 ms | 0.0369 ms | 0.0328 ms |  3.764 ms |  1.00 |    0.00 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton |           .NET 6.0 | 10000 |  3.902 ms | 0.0133 ms | 0.0111 ms |  3.902 ms |  1.03 |    0.01 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton |      .NET Core 3.1 | 10000 |  4.672 ms | 0.0931 ms | 0.1749 ms |  4.656 ms |  1.27 |    0.04 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton | .NET Framework 4.8 | 10000 |  4.699 ms | 0.0701 ms | 0.0656 ms |  4.682 ms |  1.25 |    0.02 |  281.2500 |   1.71 MB |        1.00 |
|            Transient |           .NET 6.0 | 10000 |  6.536 ms | 0.0154 ms | 0.0129 ms |  6.533 ms |  1.73 |    0.01 | 1250.0000 |    7.5 MB |        4.40 |
|            Transient |           .NET 7.0 | 10000 |  6.621 ms | 0.1289 ms | 0.1142 ms |  6.598 ms |  1.75 |    0.04 | 1250.0000 |    7.5 MB |        4.40 |
|            Transient | .NET Framework 4.8 | 10000 |  7.526 ms | 0.0815 ms | 0.0763 ms |  7.497 ms |  1.99 |    0.03 | 1273.4375 |   7.68 MB |        4.51 |
|            Transient |      .NET Core 3.1 | 10000 |  7.560 ms | 0.1259 ms | 0.1116 ms |  7.522 ms |  2.00 |    0.04 | 1250.0000 |    7.5 MB |        4.40 |
|  NuGet_IbanValidator |           .NET 6.0 | 10000 | 15.038 ms | 0.0928 ms | 0.0725 ms | 15.014 ms |  3.98 |    0.03 | 3546.8750 |  21.24 MB |       12.47 |
|  NuGet_IbanValidator |           .NET 7.0 | 10000 | 16.310 ms | 0.2873 ms | 0.2950 ms | 16.170 ms |  4.33 |    0.11 | 3531.2500 |  21.24 MB |       12.47 |
|       NuGet_IBAN4NET |           .NET 7.0 | 10000 | 35.783 ms | 0.1430 ms | 0.1268 ms | 35.783 ms |  9.48 |    0.08 | 1714.2857 |  10.48 MB |        6.15 |
|       NuGet_IBAN4NET |           .NET 6.0 | 10000 | 38.713 ms | 0.2846 ms | 0.2523 ms | 38.619 ms | 10.25 |    0.12 | 1692.3077 |  10.48 MB |        6.15 |
|  NuGet_IbanValidator |      .NET Core 3.1 | 10000 | 42.291 ms | 0.7503 ms | 1.3530 ms | 41.826 ms | 11.40 |    0.54 | 8545.4545 |  51.14 MB |       30.03 |
|       NuGet_IBAN4NET |      .NET Core 3.1 | 10000 | 43.518 ms | 0.9643 ms | 2.8280 ms | 41.949 ms | 11.40 |    0.46 | 1692.3077 |  10.48 MB |        6.15 |
|       NuGet_IBAN4NET | .NET Framework 4.8 | 10000 | 55.620 ms | 0.6644 ms | 0.5890 ms | 55.308 ms | 14.73 |    0.16 | 2000.0000 |  12.38 MB |        7.27 |
|  NuGet_IbanValidator | .NET Framework 4.8 | 10000 | 55.681 ms | 0.8736 ms | 0.8172 ms | 55.684 ms | 14.77 |    0.20 | 6900.0000 |  41.89 MB |       24.60 |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net7.0 net6.0 netcoreapp3.1 net472 --runtimes net70 net60 netcoreapp31 net48
```
