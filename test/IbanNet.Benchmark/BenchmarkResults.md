# IbanNet Benchmark Results

## Performance for v5.x

A single validation:

```
BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19041.1052 (2004/May2020Update/20H1)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=5.0.204
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-YXMQIL : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-XHZTZU : .NET Core 3.1.16 (CoreCLR 4.700.21.26205, CoreFX 4.700.21.26205), X64 RyuJIT
  Job-MDAWAJ : .NET Framework 4.8 (4.8.4360.0), X64 RyuJIT
  Job-YBROFO : .NET Core 2.1.28 (CoreCLR 4.6.30015.01, CoreFX 4.6.30015.01), X64 RyuJIT
```

|   Method |        Job |            Runtime |    Toolchain |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------- |------------------- |------------- |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| Validate | Job-YXMQIL |           .NET 5.0 | netcoreapp50 | 484.9 ns |  9.64 ns | 11.10 ns |  1.00 |    0.00 | 0.0505 |     - |     - |     320 B |
| Validate | Job-XHZTZU |      .NET Core 3.1 | netcoreapp31 | 491.4 ns |  7.27 ns |  6.80 ns |  1.01 |    0.03 | 0.0505 |     - |     - |     320 B |
| Validate | Job-MDAWAJ | .NET Framework 4.8 |        net48 | 516.5 ns | 10.28 ns | 13.37 ns |  1.07 |    0.04 | 0.0515 |     - |     - |     329 B |
| Validate | Job-YBROFO |      .NET Core 2.1 | netcoreapp21 | 528.9 ns |  8.19 ns |  7.66 ns |  1.09 |    0.03 | 0.0515 |     - |     - |     328 B |

### Comparison with other validators

```
BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19041.1052 (2004/May2020Update/20H1)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=5.0.204
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-ZEOLNS : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-TSIAFH : .NET Core 3.1.16 (CoreCLR 4.700.21.26205, CoreFX 4.700.21.26205), X64 RyuJIT
  Job-GNFNBA : .NET Framework 4.8 (4.8.4360.0), X64 RyuJIT
  Job-DNGLZY : .NET Core 2.1.28 (CoreCLR 4.6.30015.01, CoreFX 4.6.30015.01), X64 RyuJIT
```

|                    Method |        Job |            Runtime |    Toolchain | Count |      Mean |     Error |    StdDev | Ratio | RatioSD |     Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |----------- |------------------- |------------- |------ |----------:|----------:|----------:|------:|--------:|----------:|------:|------:|----------:|
| IbanNet_Strict_CacheReuse | Job-ZEOLNS |           .NET 5.0 | netcoreapp50 | 10000 |  4.671 ms | 0.0648 ms | 0.0574 ms |  0.91 |    0.02 |  507.8125 |     - |     - |      3 MB |
| IbanNet_Strict_CacheReuse | Job-TSIAFH |      .NET Core 3.1 | netcoreapp31 | 10000 |  4.922 ms | 0.0980 ms | 0.1129 ms |  0.96 |    0.02 |  507.8125 |     - |     - |      3 MB |
| IbanNet_Strict_CacheReuse | Job-GNFNBA | .NET Framework 4.8 |        net48 | 10000 |  5.038 ms | 0.0724 ms | 0.0566 ms |  0.98 |    0.02 |  515.6250 |     - |     - |      3 MB |
|            IbanNet_Strict | Job-ZEOLNS |           .NET 5.0 | netcoreapp50 | 10000 |  5.131 ms | 0.0751 ms | 0.0702 ms |  1.00 |    0.00 |  515.6250 |     - |     - |      3 MB |
|            IbanNet_Strict | Job-TSIAFH |      .NET Core 3.1 | netcoreapp31 | 10000 |  5.348 ms | 0.0990 ms | 0.0827 ms |  1.04 |    0.02 |  515.6250 |     - |     - |      3 MB |
|            IbanNet_Strict | Job-GNFNBA | .NET Framework 4.8 |        net48 | 10000 |  5.451 ms | 0.0380 ms | 0.0296 ms |  1.06 |    0.02 |  523.4375 |     - |     - |      3 MB |
| IbanNet_Strict_CacheReuse | Job-DNGLZY |      .NET Core 2.1 | netcoreapp21 | 10000 |  5.582 ms | 0.0927 ms | 0.0822 ms |  1.09 |    0.02 |  515.6250 |     - |     - |      3 MB |
|            IbanNet_Strict | Job-DNGLZY |      .NET Core 2.1 | netcoreapp21 | 10000 |  6.020 ms | 0.1179 ms | 0.1801 ms |  1.18 |    0.05 |  523.4375 |     - |     - |      3 MB |
|       NuGet_IbanValidator | Job-ZEOLNS |           .NET 5.0 | netcoreapp50 | 10000 | 24.959 ms | 0.3493 ms | 0.2727 ms |  4.87 |    0.08 | 6437.5000 |     - |     - |     39 MB |
|       NuGet_IbanValidator | Job-TSIAFH |      .NET Core 3.1 | netcoreapp31 | 10000 | 34.244 ms | 0.6772 ms | 0.7246 ms |  6.65 |    0.16 | 6533.3333 |     - |     - |     39 MB |
|            NuGet_IBAN4NET | Job-ZEOLNS |           .NET 5.0 | netcoreapp50 | 10000 | 38.129 ms | 0.6681 ms | 0.6562 ms |  7.43 |    0.13 | 1714.2857 |     - |     - |     11 MB |
|       NuGet_IbanValidator | Job-DNGLZY |      .NET Core 2.1 | netcoreapp21 | 10000 | 39.484 ms | 0.3398 ms | 0.2838 ms |  7.69 |    0.13 | 6615.3846 |     - |     - |     40 MB |
|            NuGet_IBAN4NET | Job-TSIAFH |      .NET Core 3.1 | netcoreapp31 | 10000 | 39.969 ms | 0.5974 ms | 0.5588 ms |  7.79 |    0.15 | 1692.3077 |     - |     - |     11 MB |
|       NuGet_IbanValidator | Job-GNFNBA | .NET Framework 4.8 |        net48 | 10000 | 45.044 ms | 0.7042 ms | 0.6588 ms |  8.78 |    0.16 | 5166.6667 |     - |     - |     31 MB |
|            NuGet_IBAN4NET | Job-GNFNBA | .NET Framework 4.8 |        net48 | 10000 | 55.377 ms | 0.1612 ms | 0.1429 ms | 10.80 |    0.14 | 2000.0000 |     - |     - |     12 MB |
|            NuGet_IBAN4NET | Job-DNGLZY |      .NET Core 2.1 | netcoreapp21 | 10000 | 59.625 ms | 0.8941 ms | 0.8364 ms | 11.62 |    0.22 | 1777.7778 |     - |     - |     11 MB |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net5.0 netcoreapp3.1 netcoreapp2.1 net48 --runtimes netcoreapp50 netcoreapp31 netcoreapp21 net48
```
