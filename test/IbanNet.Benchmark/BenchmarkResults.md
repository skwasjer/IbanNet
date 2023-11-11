# IbanNet Benchmark Results

## Performance for >= v5.6

A single validation:

```
BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2846/22H2/2022Update)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.203
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-FSXHEA : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-SUVHVP : .NET 6.0.16 (6.0.1623.17311), X64 RyuJIT AVX2
  Job-RGIAGU : .NET Core 3.1.32 (CoreCLR 4.700.22.55902, CoreFX 4.700.22.56512), X64 RyuJIT AVX2
  Job-EMDPBD : .NET Framework 4.8 (4.8.4614.0), X64 RyuJIT VectorSize=256
```

|   Method |            Runtime |     Mean |   Error |  StdDev |   Median | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------- |------------------- |---------:|--------:|--------:|---------:|------:|--------:|-------:|----------:|------------:|
| Validate |           .NET 7.0 | 268.4 ns | 5.34 ns | 8.15 ns | 264.4 ns |  1.00 |    0.00 | 0.0277 |     176 B |        1.00 |
| Validate |           .NET 6.0 | 285.8 ns | 3.48 ns | 3.25 ns | 286.4 ns |  1.05 |    0.04 | 0.0277 |     176 B |        1.00 |
| Validate |      .NET Core 3.1 | 341.0 ns | 6.15 ns | 5.13 ns | 338.9 ns |  1.25 |    0.03 | 0.0277 |     176 B |        1.00 |
| Validate | .NET Framework 4.8 | 366.3 ns | 1.97 ns | 1.75 ns | 366.6 ns |  1.35 |    0.05 | 0.0277 |     177 B |        1.01 |


### Comparison with other validators

> Worth mentioning is that IbanNet validates more strictly than the other alternative (competing) libraries, yet comes out quite a lot faster and has a much lower memory footprint.

#### Legend

- *Singleton_CacheReuse*: strict validation, singleton validator, reuse of rules and pattern cache
- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

|               Method |            Runtime | Count |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |      Gen0 | Allocated | Alloc Ratio |
|--------------------- |------------------- |------ |----------:|----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton_CacheReuse |           .NET 7.0 | 10000 |  2.672 ms | 0.0069 ms | 0.0061 ms |  2.672 ms |  0.78 |    0.00 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |           .NET 6.0 | 10000 |  2.684 ms | 0.0091 ms | 0.0071 ms |  2.685 ms |  0.78 |    0.00 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse |      .NET Core 3.1 | 10000 |  3.354 ms | 0.0525 ms | 0.0465 ms |  3.339 ms |  0.97 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
|            Singleton |           .NET 7.0 | 10000 |  3.446 ms | 0.0069 ms | 0.0065 ms |  3.445 ms |  1.00 |    0.00 |  281.2500 |    1.7 MB |        1.00 |
| Singleton_CacheReuse | .NET Framework 4.8 | 10000 |  3.518 ms | 0.0428 ms | 0.0358 ms |  3.498 ms |  1.02 |    0.01 |  277.3438 |   1.68 MB |        0.99 |
|            Singleton |           .NET 6.0 | 10000 |  3.589 ms | 0.0109 ms | 0.0091 ms |  3.585 ms |  1.04 |    0.00 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton |      .NET Core 3.1 | 10000 |  4.208 ms | 0.0100 ms | 0.0089 ms |  4.212 ms |  1.22 |    0.00 |  281.2500 |    1.7 MB |        1.00 |
|            Singleton | .NET Framework 4.8 | 10000 |  4.350 ms | 0.0169 ms | 0.0158 ms |  4.353 ms |  1.26 |    0.00 |  281.2500 |   1.71 MB |        1.00 |
|            Transient |           .NET 7.0 | 10000 |  5.832 ms | 0.0207 ms | 0.0162 ms |  5.833 ms |  1.69 |    0.01 | 1250.0000 |    7.5 MB |        4.41 |
|            Transient |           .NET 6.0 | 10000 |  6.093 ms | 0.0200 ms | 0.0167 ms |  6.097 ms |  1.77 |    0.01 | 1250.0000 |    7.5 MB |        4.41 |
|            Transient |      .NET Core 3.1 | 10000 |  7.046 ms | 0.0185 ms | 0.0164 ms |  7.044 ms |  2.04 |    0.01 | 1250.0000 |    7.5 MB |        4.41 |
|            Transient | .NET Framework 4.8 | 10000 |  7.529 ms | 0.0730 ms | 0.0610 ms |  7.510 ms |  2.18 |    0.02 | 1273.4375 |   7.68 MB |        4.51 |
|  NuGet_IbanValidator |           .NET 6.0 | 10000 | 14.384 ms | 0.2862 ms | 0.5974 ms | 14.129 ms |  4.33 |    0.22 | 3500.0000 |  20.95 MB |       12.30 |
|  NuGet_IbanValidator |           .NET 7.0 | 10000 | 15.251 ms | 0.0512 ms | 0.0399 ms | 15.257 ms |  4.42 |    0.01 | 3500.0000 |  20.95 MB |       12.30 |
|       NuGet_IBAN4NET |           .NET 7.0 | 10000 | 33.835 ms | 0.2105 ms | 0.1757 ms | 33.789 ms |  9.82 |    0.06 | 1666.6667 |  10.36 MB |        6.09 |
|       NuGet_IBAN4NET |           .NET 6.0 | 10000 | 36.158 ms | 0.6958 ms | 0.6509 ms | 36.122 ms | 10.49 |    0.19 | 1714.2857 |  10.36 MB |        6.09 |
|  NuGet_IbanValidator |      .NET Core 3.1 | 10000 | 38.064 ms | 0.4709 ms | 0.4404 ms | 37.905 ms | 11.05 |    0.13 | 8428.5714 |  50.45 MB |       29.63 |
|       NuGet_IBAN4NET |      .NET Core 3.1 | 10000 | 38.723 ms | 0.0543 ms | 0.0453 ms | 38.737 ms | 11.24 |    0.03 | 1692.3077 |  10.36 MB |        6.09 |
|  NuGet_IbanValidator | .NET Framework 4.8 | 10000 | 52.702 ms | 0.4519 ms | 0.4227 ms | 52.538 ms | 15.29 |    0.14 | 6800.0000 |  41.33 MB |       24.27 |
|       NuGet_IBAN4NET | .NET Framework 4.8 | 10000 | 54.086 ms | 1.0518 ms | 0.9838 ms | 54.180 ms | 15.69 |    0.27 | 2000.0000 |  12.24 MB |        7.19 |


### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net7.0 net6.0 netcoreapp3.1 net472 --runtimes net70 net60 netcoreapp31 net48 /p:AllValidators=1
```
