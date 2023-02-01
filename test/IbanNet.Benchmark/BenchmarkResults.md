# IbanNet Benchmark Results

## Performance for >= v5.6

A single validation:

```
BenchmarkDotNet=v0.13.3, OS=Windows 10 (10.0.19045.2486)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.102
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  Job-VTDVNO : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  Job-GQWWHF : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2
  Job-NPFPAT : .NET Core 3.1.32 (CoreCLR 4.700.22.55902, CoreFX 4.700.22.56512), X64 RyuJIT AVX2
  Job-YKQXAF : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT VectorSize=256
```

|   Method |            Runtime |     Mean |   Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------- |------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Validate |           .NET 7.0 | 270.7 ns | 3.51 ns | 3.11 ns |  1.00 |    0.00 | 0.0277 |     176 B |        1.00 |
| Validate |           .NET 6.0 | 288.1 ns | 0.73 ns | 0.68 ns |  1.06 |    0.01 | 0.0277 |     176 B |        1.00 |
| Validate |      .NET Core 3.1 | 345.3 ns | 6.90 ns | 6.45 ns |  1.28 |    0.02 | 0.0277 |     176 B |        1.00 |
| Validate | .NET Framework 4.8 | 365.4 ns | 1.69 ns | 1.58 ns |  1.35 |    0.02 | 0.0277 |     177 B |        1.01 |


### Comparison with other validators

> Worth mentioning is that IbanNet validates more strictly than the other alternative (competing) libraries, yet comes out quite a lot faster and has a much lower memory footprint.

#### Legend

- *Singleton_CacheReuse*: strict validation, singleton validator, reuse of rules and pattern cache
- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

```
BenchmarkDotNet=v0.13.3, OS=Windows 10 (10.0.19045.2486)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.102
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  Job-NNDXRT : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  Job-HYIXPJ : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2
  Job-EADHIU : .NET Core 3.1.32 (CoreCLR 4.700.22.55902, CoreFX 4.700.22.56512), X64 RyuJIT AVX2
  Job-GMRPOZ : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT VectorSize=256
```

|               Method |            Runtime | Count |      Mean |     Error |    StdDev | Ratio | RatioSD |      Gen0 | Allocated | Alloc Ratio |
|--------------------- |------------------- |------ |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton_CacheReuse |           .NET 7.0 | 10000 |  2.810 ms | 0.0251 ms | 0.0222 ms |  0.78 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |           .NET 6.0 | 10000 |  2.863 ms | 0.0070 ms | 0.0066 ms |  0.79 |    0.00 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |      .NET Core 3.1 | 10000 |  3.497 ms | 0.0291 ms | 0.0243 ms |  0.97 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
|            Singleton |           .NET 7.0 | 10000 |  3.609 ms | 0.0150 ms | 0.0133 ms |  1.00 |    0.00 |  281.2500 |    1.7 MB |        1.00 |
| Singleton_CacheReuse | .NET Framework 4.8 | 10000 |  3.753 ms | 0.0289 ms | 0.0270 ms |  1.04 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
|            Singleton |           .NET 6.0 | 10000 |  3.768 ms | 0.0144 ms | 0.0113 ms |  1.04 |    0.00 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton |      .NET Core 3.1 | 10000 |  4.420 ms | 0.0118 ms | 0.0098 ms |  1.23 |    0.01 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton | .NET Framework 4.8 | 10000 |  4.683 ms | 0.0151 ms | 0.0134 ms |  1.30 |    0.01 |  281.2500 |   1.71 MB |        1.00 |
|            Transient |           .NET 7.0 | 10000 |  6.191 ms | 0.0151 ms | 0.0141 ms |  1.72 |    0.01 | 1250.0000 |    7.5 MB |        4.40 |
|            Transient |           .NET 6.0 | 10000 |  6.321 ms | 0.0308 ms | 0.0273 ms |  1.75 |    0.01 | 1250.0000 |    7.5 MB |        4.40 |
|            Transient | .NET Framework 4.8 | 10000 |  7.624 ms | 0.0122 ms | 0.0114 ms |  2.11 |    0.01 | 1273.4375 |   7.68 MB |        4.51 |
|            Transient |      .NET Core 3.1 | 10000 |  7.892 ms | 0.1554 ms | 0.1527 ms |  2.19 |    0.05 | 1250.0000 |    7.5 MB |        4.40 |
|  NuGet_IbanValidator |           .NET 6.0 | 10000 | 14.627 ms | 0.1462 ms | 0.1296 ms |  4.05 |    0.04 | 3546.8750 |  21.24 MB |       12.47 |
|  NuGet_IbanValidator |           .NET 7.0 | 10000 | 15.632 ms | 0.0604 ms | 0.0536 ms |  4.33 |    0.02 | 3546.8750 |  21.24 MB |       12.47 |
|       NuGet_IBAN4NET |           .NET 7.0 | 10000 | 34.414 ms | 0.1205 ms | 0.1068 ms |  9.54 |    0.03 | 1733.3333 |  10.48 MB |        6.15 |
|       NuGet_IBAN4NET |           .NET 6.0 | 10000 | 36.909 ms | 0.4327 ms | 0.4048 ms | 10.23 |    0.13 | 1692.3077 |  10.48 MB |        6.15 |
|  NuGet_IbanValidator |      .NET Core 3.1 | 10000 | 38.781 ms | 0.1029 ms | 0.0803 ms | 10.75 |    0.03 | 8538.4615 |  51.14 MB |       30.03 |
|       NuGet_IBAN4NET |      .NET Core 3.1 | 10000 | 41.141 ms | 0.1096 ms | 0.0972 ms | 11.40 |    0.04 | 1692.3077 |  10.48 MB |        6.15 |
|  NuGet_IbanValidator | .NET Framework 4.8 | 10000 | 54.538 ms | 0.7215 ms | 0.6396 ms | 15.11 |    0.17 | 6888.8889 |  41.89 MB |       24.60 |
|       NuGet_IBAN4NET | .NET Framework 4.8 | 10000 | 57.671 ms | 1.0930 ms | 1.3011 ms | 15.93 |    0.31 | 2000.0000 |  12.38 MB |        7.27 |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net7.0 net6.0 netcoreapp3.1 net472 --runtimes net70 net60 netcoreapp31 net48
```
