# IbanNet Benchmark Results

## Performance for >= v5.6

A single validation:

```
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2006/21H1/May2021Update)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.400
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  Job-SIZKKU : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  Job-OBDVOP : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2
  Job-HDWPRM : .NET Core 3.1.29 (CoreCLR 4.700.22.41602, CoreFX 4.700.22.41702), X64 RyuJIT AVX2
  Job-DWSTHF : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT VectorSize=256
```

|   Method |            Runtime |     Mean |   Error |   StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------- |------------------- |---------:|--------:|---------:|------:|--------:|-------:|----------:|------------:|
| Validate |           .NET 6.0 | 349.3 ns | 6.75 ns |  8.29 ns |  1.00 |    0.00 | 0.0277 |     176 B |        1.00 |
| Validate |           .NET 5.0 | 410.3 ns | 7.84 ns | 14.54 ns |  1.20 |    0.04 | 0.0277 |     176 B |        1.00 |
| Validate |      .NET Core 3.1 | 424.2 ns | 8.49 ns |  7.09 ns |  1.20 |    0.04 | 0.0277 |     176 B |        1.00 |
| Validate | .NET Framework 4.8 | 429.9 ns | 3.90 ns |  3.65 ns |  1.22 |    0.03 | 0.0277 |     177 B |        1.01 |

### Comparison with other validators

> Worth mentioning is that IbanNet validates more strictly than the other alternative (competing) libraries, yet comes out quite a lot faster and has a much lower memory footprint.

#### Legend
- *Singleton_CacheReuse*: strict validation, singleton validator, reuse of rules and pattern cache
- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

```
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2006/21H1/May2021Update)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.400
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  Job-UMRFNC : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  Job-MPBMRJ : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2
  Job-AVDNXJ : .NET Core 3.1.29 (CoreCLR 4.700.22.41602, CoreFX 4.700.22.41702), X64 RyuJIT AVX2
  Job-FYAEEA : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT VectorSize=256
```

|               Method |            Runtime | Count |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |      Gen0 | Allocated | Alloc Ratio |
|--------------------- |------------------- |------ |----------:|----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton_CacheReuse |           .NET 6.0 | 10000 |  3.585 ms | 0.0194 ms | 0.0181 ms |  3.574 ms |  0.81 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |           .NET 5.0 | 10000 |  4.117 ms | 0.0533 ms | 0.0499 ms |  4.116 ms |  0.93 |    0.01 |  273.4375 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |      .NET Core 3.1 | 10000 |  4.193 ms | 0.0719 ms | 0.0673 ms |  4.175 ms |  0.94 |    0.01 |  273.4375 |   1.68 MB |        0.99 |
| Singleton_CacheReuse | .NET Framework 4.8 | 10000 |  4.288 ms | 0.0339 ms | 0.0283 ms |  4.280 ms |  0.97 |    0.01 |  273.4375 |   1.68 MB |        0.99 |
|     Singleton |           .NET 6.0 | 10000 |  4.421 ms | 0.0395 ms | 0.0309 ms |  4.421 ms |  1.00 |    0.00 |  281.2500 |    1.7 MB |        1.00 |
|     Singleton |      .NET Core 3.1 | 10000 |  4.900 ms | 0.0458 ms | 0.0428 ms |  4.877 ms |  1.11 |    0.01 |  281.2500 |    1.7 MB |        1.00 |
|     Singleton |           .NET 5.0 | 10000 |  4.956 ms | 0.0630 ms | 0.0590 ms |  4.925 ms |  1.12 |    0.01 |  281.2500 |    1.7 MB |        1.00 |
|     Singleton | .NET Framework 4.8 | 10000 |  5.092 ms | 0.0932 ms | 0.0872 ms |  5.083 ms |  1.15 |    0.02 |  281.2500 |   1.71 MB |        1.00 |
|     Transient* |           .NET 6.0 | 10000 |  6.939 ms | 0.1242 ms | 0.1101 ms |  6.962 ms |  1.56 |    0.03 | 1250.0000 |    7.5 MB |        4.40 |
|     Transient* |           .NET 5.0 | 10000 |  7.791 ms | 0.1446 ms | 0.3575 ms |  7.647 ms |  1.86 |    0.09 | 1250.0000 |    7.5 MB |        4.40 |
|     Transient* |      .NET Core 3.1 | 10000 |  7.824 ms | 0.1234 ms | 0.1154 ms |  7.796 ms |  1.77 |    0.02 | 1250.0000 |    7.5 MB |        4.40 |
|     Transient* | .NET Framework 4.8 | 10000 |  8.339 ms | 0.1594 ms | 0.1772 ms |  8.359 ms |  1.88 |    0.05 | 1265.6250 |   7.68 MB |        4.51 |
|  NuGet_IbanValidator |           .NET 6.0 | 10000 | 14.559 ms | 0.0973 ms | 0.0812 ms | 14.570 ms |  3.29 |    0.03 | 3531.2500 |  21.18 MB |       12.43 |
|  NuGet_IbanValidator |           .NET 5.0 | 10000 | 28.855 ms | 0.4032 ms | 0.3574 ms | 28.820 ms |  6.53 |    0.08 | 8437.5000 |  50.48 MB |       29.64 |
|       NuGet_IBAN4NET |           .NET 5.0 | 10000 | 36.459 ms | 0.1556 ms | 0.1215 ms | 36.496 ms |  8.25 |    0.06 | 1642.8571 |  10.23 MB |        6.01 |
|       NuGet_IBAN4NET |           .NET 6.0 | 10000 | 36.738 ms | 0.1195 ms | 0.1328 ms | 36.795 ms |  8.31 |    0.07 | 1642.8571 |  10.23 MB |        6.01 |
|  NuGet_IbanValidator |      .NET Core 3.1 | 10000 | 40.747 ms | 0.6070 ms | 0.5381 ms | 40.608 ms |  9.22 |    0.15 | 8461.5385 |  51.06 MB |       29.98 |
|       NuGet_IBAN4NET |      .NET Core 3.1 | 10000 | 40.892 ms | 0.6497 ms | 0.5760 ms | 40.963 ms |  9.25 |    0.16 | 1692.3077 |  10.23 MB |        6.01 |
|  NuGet_IbanValidator | .NET Framework 4.8 | 10000 | 55.266 ms | 1.0865 ms | 1.5231 ms | 55.089 ms | 12.44 |    0.37 | 6900.0000 |  41.79 MB |       24.54 |
|       NuGet_IBAN4NET | .NET Framework 4.8 | 10000 | 56.797 ms | 0.7445 ms | 0.6600 ms | 56.595 ms | 12.83 |    0.14 | 2000.0000 |   12.1 MB |        7.11 |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net6.0 net5.0 netcoreapp3.1 net472 --runtimes net60 net50 netcoreapp31 net48
```
