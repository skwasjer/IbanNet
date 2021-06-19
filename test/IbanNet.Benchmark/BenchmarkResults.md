# IbanNet Benchmark Results

## Performance for v5.x

A single validation:

```
BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19041.1052 (2004/May2020Update/20H1)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=5.0.204
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-VAFPOR : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-SUPFRH : .NET Core 3.1.16 (CoreCLR 4.700.21.26205, CoreFX 4.700.21.26205), X64 RyuJIT
  Job-DVBDQH : .NET Framework 4.8 (4.8.4360.0), X64 RyuJIT
  Job-WCAPRC : .NET Core 2.1.28 (CoreCLR 4.6.30015.01, CoreFX 4.6.30015.01), X64 RyuJIT
```

|   Method |        Job |            Runtime |    Toolchain |     Mean |   Error |  StdDev | Ratio |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------- |------------------- |------------- |---------:|--------:|--------:|------:|-------:|------:|------:|----------:|
| Validate | Job-VAFPOR |           .NET 5.0 | netcoreapp50 | 441.0 ns | 3.20 ns | 2.99 ns |  1.00 | 0.0505 |     - |     - |     320 B |
| Validate | Job-SUPFRH |      .NET Core 3.1 | netcoreapp31 | 459.0 ns | 1.11 ns | 0.98 ns |  1.04 | 0.0505 |     - |     - |     320 B |
| Validate | Job-DVBDQH | .NET Framework 4.8 |        net48 | 461.9 ns | 3.11 ns | 2.75 ns |  1.05 | 0.0520 |     - |     - |     329 B |
| Validate | Job-WCAPRC |      .NET Core 2.1 | netcoreapp21 | 491.4 ns | 2.39 ns | 1.87 ns |  1.12 | 0.0515 |     - |     - |     328 B |

### Comparison with other validators

> Worth mentioning is that IbanNet validates more strictly than the other alternative (competing) libraries, yet comes out quite a lot faster and has a much lower memory footprint.

```
BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19041.1052 (2004/May2020Update/20H1)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=5.0.204
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-TESZCN : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  Job-JFOJBI : .NET Core 3.1.16 (CoreCLR 4.700.21.26205, CoreFX 4.700.21.26205), X64 RyuJIT
  Job-AMQDFL : .NET Framework 4.8 (4.8.4360.0), X64 RyuJIT
  Job-FSRDZX : .NET Core 2.1.28 (CoreCLR 4.6.30015.01, CoreFX 4.6.30015.01), X64 RyuJIT
```

|                    Method |        Job |            Runtime |    Toolchain | Count |      Mean |     Error |    StdDev | Ratio | RatioSD |     Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |----------- |------------------- |------------- |------ |----------:|----------:|----------:|------:|--------:|----------:|------:|------:|----------:|
| IbanNet_Strict_CacheReuse | Job-TESZCN |           .NET 5.0 | netcoreapp50 | 10000 |  4.455 ms | 0.0354 ms | 0.0331 ms |  0.92 |    0.01 |  507.8125 |     - |     - |      3 MB |
| IbanNet_Strict_CacheReuse | Job-JFOJBI |      .NET Core 3.1 | netcoreapp31 | 10000 |  4.743 ms | 0.0108 ms | 0.0096 ms |  0.98 |    0.00 |  507.8125 |     - |     - |      3 MB |
| IbanNet_Strict_CacheReuse | Job-AMQDFL | .NET Framework 4.8 |        net48 | 10000 |  4.822 ms | 0.0140 ms | 0.0131 ms |  1.00 |    0.00 |  515.6250 |     - |     - |      3 MB |
|            IbanNet_Strict | Job-TESZCN |           .NET 5.0 | netcoreapp50 | 10000 |  4.828 ms | 0.0097 ms | 0.0086 ms |  1.00 |    0.00 |  515.6250 |     - |     - |      3 MB |
|            IbanNet_Strict | Job-AMQDFL | .NET Framework 4.8 |        net48 | 10000 |  4.910 ms | 0.0182 ms | 0.0162 ms |  1.02 |    0.00 |  523.4375 |     - |     - |      3 MB |
|            IbanNet_Strict | Job-JFOJBI |      .NET Core 3.1 | netcoreapp31 | 10000 |  4.924 ms | 0.0073 ms | 0.0061 ms |  1.02 |    0.00 |  515.6250 |     - |     - |      3 MB |
| IbanNet_Strict_CacheReuse | Job-FSRDZX |      .NET Core 2.1 | netcoreapp21 | 10000 |  4.974 ms | 0.0101 ms | 0.0094 ms |  1.03 |    0.00 |  515.6250 |     - |     - |      3 MB |
|            IbanNet_Strict | Job-FSRDZX |      .NET Core 2.1 | netcoreapp21 | 10000 |  5.310 ms | 0.0128 ms | 0.0120 ms |  1.10 |    0.00 |  523.4375 |     - |     - |      3 MB |
|       NuGet_IbanValidator | Job-TESZCN |           .NET 5.0 | netcoreapp50 | 10000 | 22.355 ms | 0.1944 ms | 0.1819 ms |  4.63 |    0.04 | 6437.5000 |     - |     - |     39 MB |
|       NuGet_IbanValidator | Job-JFOJBI |      .NET Core 3.1 | netcoreapp31 | 10000 | 30.720 ms | 0.0573 ms | 0.0479 ms |  6.36 |    0.02 | 6562.5000 |     - |     - |     39 MB |
|            NuGet_IBAN4NET | Job-TESZCN |           .NET 5.0 | netcoreapp50 | 10000 | 35.475 ms | 0.1463 ms | 0.1297 ms |  7.35 |    0.02 | 1733.3333 |     - |     - |     11 MB |
|       NuGet_IbanValidator | Job-FSRDZX |      .NET Core 2.1 | netcoreapp21 | 10000 | 35.744 ms | 0.0731 ms | 0.0610 ms |  7.40 |    0.02 | 6642.8571 |     - |     - |     40 MB |
|            NuGet_IBAN4NET | Job-JFOJBI |      .NET Core 3.1 | netcoreapp31 | 10000 | 37.151 ms | 0.3005 ms | 0.2811 ms |  7.69 |    0.06 | 1714.2857 |     - |     - |     11 MB |
|       NuGet_IbanValidator | Job-AMQDFL | .NET Framework 4.8 |        net48 | 10000 | 43.087 ms | 0.0523 ms | 0.0489 ms |  8.92 |    0.02 | 5166.6667 |     - |     - |     31 MB |
|            NuGet_IBAN4NET | Job-AMQDFL | .NET Framework 4.8 |        net48 | 10000 | 53.713 ms | 0.1241 ms | 0.1160 ms | 11.12 |    0.03 | 2000.0000 |     - |     - |     12 MB |
|            NuGet_IBAN4NET | Job-FSRDZX |      .NET Core 2.1 | netcoreapp21 | 10000 | 53.852 ms | 0.0885 ms | 0.0828 ms | 11.15 |    0.03 | 1800.0000 |     - |     - |     11 MB |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net5.0 netcoreapp3.1 netcoreapp2.1 net48 --runtimes netcoreapp50 netcoreapp31 netcoreapp21 net48
```
