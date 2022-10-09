# IbanNet Benchmark Results

## Performance for >= v5.6

A single validation:

```
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2006/21H1/May2021Update)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.400
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  Job-NFSSBI : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  Job-QDSTAA : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2
  Job-GUHOQA : .NET Core 3.1.29 (CoreCLR 4.700.22.41602, CoreFX 4.700.22.41702), X64 RyuJIT AVX2
  Job-PGTAHY : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT VectorSize=256
```

|   Method |            Runtime |     Mean |   Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------- |------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Validate |           .NET 6.0 | 323.6 ns | 5.08 ns | 4.50 ns |  1.00 |    0.00 | 0.0277 |     176 B |        1.00 |
| Validate |           .NET 5.0 | 366.4 ns | 7.10 ns | 8.72 ns |  1.12 |    0.03 | 0.0277 |     176 B |        1.00 |
| Validate |      .NET Core 3.1 | 376.1 ns | 5.40 ns | 4.78 ns |  1.16 |    0.02 | 0.0277 |     176 B |        1.00 |
| Validate | .NET Framework 4.8 | 405.1 ns | 4.87 ns | 4.56 ns |  1.25 |    0.02 | 0.0277 |     177 B |        1.01 |
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
  Job-LFHVLX : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  Job-CUBWLJ : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2
  Job-KIEBXE : .NET Core 3.1.29 (CoreCLR 4.700.22.41602, CoreFX 4.700.22.41702), X64 RyuJIT AVX2
  Job-JLDEMB : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT VectorSize=256
```

|               Method |            Runtime | Count |      Mean |     Error |    StdDev | Ratio | RatioSD |      Gen0 | Allocated | Alloc Ratio |
|--------------------- |------------------- |------ |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton_CacheReuse |           .NET 6.0 | 10000 |  3.276 ms | 0.0303 ms | 0.0268 ms |  0.79 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |           .NET 5.0 | 10000 |  3.696 ms | 0.0710 ms | 0.0818 ms |  0.89 |    0.02 |  273.4375 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |      .NET Core 3.1 | 10000 |  3.798 ms | 0.0750 ms | 0.0736 ms |  0.92 |    0.03 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse | .NET Framework 4.8 | 10000 |  4.028 ms | 0.0793 ms | 0.0742 ms |  0.97 |    0.02 |  273.4375 |   1.68 MB |        0.99 |
|            Singleton |           .NET 6.0 | 10000 |  4.143 ms | 0.0569 ms | 0.0609 ms |  1.00 |    0.00 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton |           .NET 5.0 | 10000 |  4.560 ms | 0.0851 ms | 0.0796 ms |  1.10 |    0.03 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton |      .NET Core 3.1 | 10000 |  4.736 ms | 0.0937 ms | 0.1002 ms |  1.14 |    0.03 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton | .NET Framework 4.8 | 10000 |  4.879 ms | 0.0534 ms | 0.0473 ms |  1.18 |    0.02 |  281.2500 |   1.71 MB |        1.00 |
|            Transient |           .NET 6.0 | 10000 |  6.717 ms | 0.0874 ms | 0.0730 ms |  1.62 |    0.02 | 1250.0000 |    7.5 MB |        4.40 |
|            Transient |      .NET Core 3.1 | 10000 |  7.458 ms | 0.0667 ms | 0.0591 ms |  1.80 |    0.03 | 1250.0000 |    7.5 MB |        4.40 |
|            Transient |           .NET 5.0 | 10000 |  7.503 ms | 0.1051 ms | 0.0932 ms |  1.81 |    0.02 | 1250.0000 |    7.5 MB |        4.40 |
|            Transient | .NET Framework 4.8 | 10000 |  7.874 ms | 0.0958 ms | 0.0849 ms |  1.90 |    0.04 | 1273.4375 |   7.68 MB |        4.51 |
|  NuGet_IbanValidator |           .NET 6.0 | 10000 | 15.335 ms | 0.3061 ms | 0.5198 ms |  3.75 |    0.13 | 3531.2500 |  21.23 MB |       12.47 |
|  NuGet_IbanValidator |           .NET 5.0 | 10000 | 29.159 ms | 0.3801 ms | 0.3369 ms |  7.02 |    0.15 | 8406.2500 |  50.36 MB |       29.58 |
|       NuGet_IBAN4NET |           .NET 6.0 | 10000 | 36.833 ms | 0.7348 ms | 1.2870 ms |  8.98 |    0.37 | 1642.8571 |  10.23 MB |        6.01 |
|       NuGet_IBAN4NET |           .NET 5.0 | 10000 | 38.657 ms | 0.7540 ms | 1.8211 ms |  9.77 |    0.57 | 1692.3077 |  10.23 MB |        6.01 |
|       NuGet_IBAN4NET |      .NET Core 3.1 | 10000 | 40.878 ms | 0.8082 ms | 0.7938 ms |  9.86 |    0.24 | 1692.3077 |  10.23 MB |        6.01 |
|  NuGet_IbanValidator |      .NET Core 3.1 | 10000 | 41.037 ms | 0.8142 ms | 0.8361 ms |  9.91 |    0.26 | 8500.0000 |  50.98 MB |       29.94 |
|  NuGet_IbanValidator | .NET Framework 4.8 | 10000 | 55.434 ms | 1.0411 ms | 1.2786 ms | 13.40 |    0.37 | 6900.0000 |   41.8 MB |       24.55 |
|       NuGet_IBAN4NET | .NET Framework 4.8 | 10000 | 58.154 ms | 1.0624 ms | 0.9418 ms | 14.01 |    0.28 | 2000.0000 |   12.1 MB |        7.11 |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net6.0 net5.0 netcoreapp3.1 net472 --runtimes net60 net50 netcoreapp31 net48
```
