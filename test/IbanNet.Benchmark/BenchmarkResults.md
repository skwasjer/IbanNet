# IbanNet Benchmark Results

## Performance for v5.x

A single validation:

```
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1288 (21H1/May2021Update)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  Job-UJGZZS : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  Job-BEDKYF : .NET 5.0.12 (5.0.1221.52207), X64 RyuJIT
  Job-CPASBF : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT
  Job-DAAXOM : .NET Core 3.1.21 (CoreCLR 4.700.21.51404, CoreFX 4.700.21.51508), X64 RyuJIT
```

|   Method |        Job |            Runtime |    Toolchain |     Mean |   Error |  StdDev | Ratio | RatioSD |  Gen 0 | Allocated |
|--------- |----------- |------------------- |------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|
| Validate | Job-UJGZZS |           .NET 6.0 |        net60 | 405.7 ns | 1.18 ns | 0.99 ns |  1.00 |    0.00 | 0.0391 |     248 B |
| Validate | Job-BEDKYF |           .NET 5.0 |        net50 | 461.3 ns | 6.22 ns | 5.19 ns |  1.14 |    0.01 | 0.0391 |     248 B |
| Validate | Job-CPASBF | .NET Framework 4.8 |        net48 | 478.8 ns | 0.76 ns | 0.71 ns |  1.18 |    0.00 | 0.0515 |     329 B |
| Validate | Job-DAAXOM |      .NET Core 3.1 | netcoreapp31 | 482.0 ns | 9.15 ns | 8.56 ns |  1.19 |    0.02 | 0.0391 |     248 B |

### Comparison with other validators

> Worth mentioning is that IbanNet validates more strictly than the other alternative (competing) libraries, yet comes out quite a lot faster and has a much lower memory footprint.

```
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1288 (21H1/May2021Update)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  Job-DXSZGC : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  Job-MKLTTF : .NET 5.0.12 (5.0.1221.52207), X64 RyuJIT
  Job-PMDCAV : .NET Core 3.1.21 (CoreCLR 4.700.21.51404, CoreFX 4.700.21.51508), X64 RyuJIT
  Job-ARFCML : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT
```

|                    Method |        Job |            Runtime |    Toolchain | Count |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |     Gen 0 | Allocated |
|-------------------------- |----------- |------------------- |------------- |------ |----------:|----------:|----------:|----------:|------:|--------:|----------:|----------:|
| IbanNet_Strict_CacheReuse | Job-DXSZGC |           .NET 6.0 |        net60 | 10000 |  4.431 ms | 0.0836 ms | 0.1225 ms |  4.430 ms |  0.83 |    0.03 |  390.6250 |      2 MB |
| IbanNet_Strict_CacheReuse | Job-MKLTTF |           .NET 5.0 |        net50 | 10000 |  4.555 ms | 0.0446 ms | 0.0395 ms |  4.551 ms |  0.85 |    0.01 |  390.6250 |      2 MB |
| IbanNet_Strict_CacheReuse | Job-PMDCAV |      .NET Core 3.1 | netcoreapp31 | 10000 |  5.265 ms | 0.1048 ms | 0.2571 ms |  5.176 ms |  1.01 |    0.06 |  390.6250 |      2 MB |
|            IbanNet_Strict | Job-DXSZGC |           .NET 6.0 |        net60 | 10000 |  5.330 ms | 0.0673 ms | 0.0629 ms |  5.312 ms |  1.00 |    0.00 |  398.4375 |      2 MB |
| IbanNet_Strict_CacheReuse | Job-ARFCML | .NET Framework 4.8 |        net48 | 10000 |  5.669 ms | 0.1474 ms | 0.4252 ms |  5.550 ms |  1.14 |    0.08 |  515.6250 |      3 MB |
|            IbanNet_Strict | Job-MKLTTF |           .NET 5.0 |        net50 | 10000 |  5.740 ms | 0.0572 ms | 0.0507 ms |  5.734 ms |  1.08 |    0.02 |  398.4375 |      2 MB |
|            IbanNet_Strict | Job-PMDCAV |      .NET Core 3.1 | netcoreapp31 | 10000 |  6.378 ms | 0.0631 ms | 0.0527 ms |  6.391 ms |  1.19 |    0.02 |  398.4375 |      2 MB |
|            IbanNet_Strict | Job-ARFCML | .NET Framework 4.8 |        net48 | 10000 |  6.481 ms | 0.1220 ms | 0.1356 ms |  6.481 ms |  1.21 |    0.03 |  523.4375 |      3 MB |
|       NuGet_IbanValidator | Job-DXSZGC |           .NET 6.0 |        net60 | 10000 | 16.702 ms | 0.3033 ms | 0.3725 ms | 16.662 ms |  3.12 |    0.10 | 3562.5000 |     21 MB |
|       NuGet_IbanValidator | Job-MKLTTF |           .NET 5.0 |        net50 | 10000 | 31.301 ms | 0.6209 ms | 1.5578 ms | 30.674 ms |  6.07 |    0.34 | 8500.0000 |     51 MB |
|            NuGet_IBAN4NET | Job-DXSZGC |           .NET 6.0 |        net60 | 10000 | 38.771 ms | 0.7682 ms | 2.0637 ms | 37.810 ms |  7.77 |    0.42 | 1692.3077 |     10 MB |
|            NuGet_IBAN4NET | Job-MKLTTF |           .NET 5.0 |        net50 | 10000 | 39.311 ms | 0.7769 ms | 1.1628 ms | 39.115 ms |  7.41 |    0.26 | 1714.2857 |     10 MB |
|            NuGet_IBAN4NET | Job-PMDCAV |      .NET Core 3.1 | netcoreapp31 | 10000 | 41.247 ms | 0.7769 ms | 0.7267 ms | 40.989 ms |  7.74 |    0.18 | 1692.3077 |     10 MB |
|       NuGet_IbanValidator | Job-PMDCAV |      .NET Core 3.1 | netcoreapp31 | 10000 | 43.580 ms | 0.8649 ms | 1.1839 ms | 43.251 ms |  8.23 |    0.26 | 8545.4545 |     52 MB |
|            NuGet_IBAN4NET | Job-ARFCML | .NET Framework 4.8 |        net48 | 10000 | 55.038 ms | 0.0788 ms | 0.0658 ms | 55.048 ms | 10.31 |    0.12 | 2000.0000 |     12 MB |
|       NuGet_IbanValidator | Job-ARFCML | .NET Framework 4.8 |        net48 | 10000 | 55.239 ms | 0.1225 ms | 0.1023 ms | 55.222 ms | 10.35 |    0.12 | 7000.0000 |     42 MB |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net6.0 net5.0 netcoreapp3.1 net472 --runtimes net60 net50 netcoreapp31 net48
```
