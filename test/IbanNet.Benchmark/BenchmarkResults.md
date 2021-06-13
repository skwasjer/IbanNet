# IbanNet Benchmark Results

## Performance for v5.x

### Strict vs Loose validation

```
BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19041.1052 (2004/May2020Update/20H1)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=5.0.204
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-XFDFLA : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-GVSOEG : .NET Core 3.1.16 (CoreCLR 4.700.21.26205, CoreFX 4.700.21.26205), X64 RyuJIT
  Job-RCWYTV : .NET Framework 4.8 (4.8.4360.0), X64 RyuJIT
  Job-HBIEPP : .NET Core 2.1.28 (CoreCLR 4.6.30015.01, CoreFX 4.6.30015.01), X64 RyuJIT
```

|   Method |        Job |            Runtime |    Toolchain |      validator |     Mean |    Error |   StdDev |   Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------- |------------------- |------------- |--------------- |---------:|---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| Validate | Job-XFDFLA |           .NET 5.0 | netcoreapp50 |  IbanNet Loose | 330.6 ns |  2.77 ns |  2.45 ns | 329.7 ns |  1.00 |    0.00 | 0.0505 |     - |     - |     320 B |
| Validate | Job-GVSOEG |      .NET Core 3.1 | netcoreapp31 |  IbanNet Loose | 345.0 ns |  2.67 ns |  2.23 ns | 344.5 ns |  1.04 |    0.01 | 0.0505 |     - |     - |     320 B |
| Validate | Job-RCWYTV | .NET Framework 4.8 |        net48 |  IbanNet Loose | 374.0 ns |  2.90 ns |  2.57 ns | 373.5 ns |  1.13 |    0.01 | 0.0520 |     - |     - |     329 B |
| Validate | Job-HBIEPP |      .NET Core 2.1 | netcoreapp21 |  IbanNet Loose | 411.9 ns |  7.65 ns |  7.15 ns | 412.0 ns |  1.25 |    0.02 | 0.0520 |     - |     - |     328 B |
|          |            |                    |              |                |          |          |          |          |       |         |        |       |       |           |
| Validate | Job-XFDFLA |           .NET 5.0 | netcoreapp50 | IbanNet Strict | 456.7 ns |  5.75 ns |  5.38 ns | 455.1 ns |  1.00 |    0.00 | 0.0505 |     - |     - |     320 B |
| Validate | Job-GVSOEG |      .NET Core 3.1 | netcoreapp31 | IbanNet Strict | 467.9 ns |  4.23 ns |  3.53 ns | 469.4 ns |  1.03 |    0.01 | 0.0505 |     - |     - |     320 B |
| Validate | Job-RCWYTV | .NET Framework 4.8 |        net48 | IbanNet Strict | 491.9 ns |  3.11 ns |  2.76 ns | 491.9 ns |  1.08 |    0.02 | 0.0515 |     - |     - |     329 B |
| Validate | Job-HBIEPP |      .NET Core 2.1 | netcoreapp21 | IbanNet Strict | 557.9 ns | 11.11 ns | 29.07 ns | 541.9 ns |  1.27 |    0.05 | 0.0515 |     - |     - |     328 B |

### Comparison with other validators

```
BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19041.1052 (2004/May2020Update/20H1)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=5.0.204
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-RRXHFD : .NET Core 3.1.16 (CoreCLR 4.700.21.26205, CoreFX 4.700.21.26205), X64 RyuJIT
  Job-HMVMWA : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-YJYSTN : .NET Framework 4.8 (4.8.4360.0), X64 RyuJIT
  Job-NHZRIZ : .NET Core 2.1.28 (CoreCLR 4.6.30015.01, CoreFX 4.6.30015.01), X64 RyuJIT
```

|                    Method |        Job |            Runtime |    Toolchain | Count |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |----------- |------------------- |------------- |------ |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             IbanNet_Loose | Job-RRXHFD |      .NET Core 3.1 | netcoreapp31 |    10 |  3.760 us | 0.0243 us | 0.0227 us |  0.77 |    0.02 | 0.5074 |     - |     - |      3 KB |
|             IbanNet_Loose | Job-HMVMWA |           .NET 5.0 | netcoreapp50 |    10 |  3.761 us | 0.0694 us | 0.0615 us |  0.77 |    0.02 | 0.5035 |     - |     - |      3 KB |
|             IbanNet_Loose | Job-YJYSTN | .NET Framework 4.8 |        net48 |    10 |  4.073 us | 0.0364 us | 0.0304 us |  0.83 |    0.02 | 0.5112 |     - |     - |      3 KB |
|             IbanNet_Loose | Job-NHZRIZ |      .NET Core 2.1 | netcoreapp21 |    10 |  4.383 us | 0.0844 us | 0.0972 us |  0.89 |    0.03 | 0.5112 |     - |     - |      3 KB |
| IbanNet_Strict_CacheReuse | Job-HMVMWA |           .NET 5.0 | netcoreapp50 |    10 |  4.711 us | 0.0212 us | 0.0177 us |  0.96 |    0.02 | 0.5035 |     - |     - |      3 KB |
|            IbanNet_Strict | Job-HMVMWA |           .NET 5.0 | netcoreapp50 |    10 |  4.938 us | 0.0957 us | 0.1102 us |  1.00 |    0.00 | 0.5035 |     - |     - |      3 KB |
| IbanNet_Strict_CacheReuse | Job-RRXHFD |      .NET Core 3.1 | netcoreapp31 |    10 |  5.078 us | 0.0922 us | 0.0862 us |  1.03 |    0.03 | 0.5035 |     - |     - |      3 KB |
|            IbanNet_Strict | Job-RRXHFD |      .NET Core 3.1 | netcoreapp31 |    10 |  5.125 us | 0.0872 us | 0.0815 us |  1.04 |    0.03 | 0.5035 |     - |     - |      3 KB |
| IbanNet_Strict_CacheReuse | Job-YJYSTN | .NET Framework 4.8 |        net48 |    10 |  5.207 us | 0.0171 us | 0.0134 us |  1.06 |    0.03 | 0.5188 |     - |     - |      3 KB |
|            IbanNet_Strict | Job-YJYSTN | .NET Framework 4.8 |        net48 |    10 |  5.363 us | 0.0554 us | 0.0463 us |  1.09 |    0.03 | 0.5112 |     - |     - |      3 KB |
| IbanNet_Strict_CacheReuse | Job-NHZRIZ |      .NET Core 2.1 | netcoreapp21 |    10 |  5.508 us | 0.1031 us | 0.0964 us |  1.12 |    0.03 | 0.5188 |     - |     - |      3 KB |
|            IbanNet_Strict | Job-NHZRIZ |      .NET Core 2.1 | netcoreapp21 |    10 |  5.689 us | 0.1129 us | 0.1209 us |  1.15 |    0.03 | 0.5112 |     - |     - |      3 KB |
|       NuGet_IbanValidator | Job-HMVMWA |           .NET 5.0 | netcoreapp50 |    10 | 24.737 us | 0.1517 us | 0.1490 us |  5.03 |    0.12 | 6.0730 |     - |     - |     37 KB |
|       NuGet_IbanValidator | Job-RRXHFD |      .NET Core 3.1 | netcoreapp31 |    10 | 33.410 us | 0.1973 us | 0.1846 us |  6.81 |    0.14 | 6.1646 |     - |     - |     38 KB |
|            NuGet_IBAN4NET | Job-HMVMWA |           .NET 5.0 | netcoreapp50 |    10 | 37.593 us | 0.6758 us | 0.6321 us |  7.66 |    0.21 | 1.8311 |     - |     - |     11 KB |
|            NuGet_IBAN4NET | Job-RRXHFD |      .NET Core 3.1 | netcoreapp31 |    10 | 38.154 us | 0.3107 us | 0.2754 us |  7.76 |    0.18 | 1.8311 |     - |     - |     11 KB |
|       NuGet_IbanValidator | Job-NHZRIZ |      .NET Core 2.1 | netcoreapp21 |    10 | 39.242 us | 0.4142 us | 0.3459 us |  7.98 |    0.20 | 6.2256 |     - |     - |     38 KB |
|       NuGet_IbanValidator | Job-YJYSTN | .NET Framework 4.8 |        net48 |    10 | 44.737 us | 0.3962 us | 0.3513 us |  9.10 |    0.21 | 5.0049 |     - |     - |     31 KB |
|            NuGet_IBAN4NET | Job-YJYSTN | .NET Framework 4.8 |        net48 |    10 | 52.782 us | 0.8263 us | 0.7325 us | 10.74 |    0.29 | 2.1362 |     - |     - |     13 KB |
|            NuGet_IBAN4NET | Job-NHZRIZ |      .NET Core 2.1 | netcoreapp21 |    10 | 54.260 us | 1.0797 us | 1.2434 us | 10.99 |    0.32 | 1.8921 |     - |     - |     12 KB |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net5.0 netcoreapp3.1 netcoreapp2.1 net48 --runtimes netcoreapp50 netcoreapp31 netcoreapp21 net48
```
