# IbanNet Benchmark Results

## Release 4.3

```
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.630 (2004/?/20H1)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.100
  Job-VKWGJR : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  Job-VZHMRU : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  Job-SJRRVK : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  Job-ZHHGDR : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  Job-ZJJUAW : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), X64 RyuJIT
```
### Strict vs Loose validation

|   Method |        Job |       Runtime |    Toolchain |      validator |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------- |-------------- |------------- |--------------- |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| Validate | Job-VKWGJR | .NET Core 5.0 | netcoreapp50 |  IbanNet Loose | 460.6 ns |  1.23 ns |  1.15 ns |  1.00 |    0.00 | 0.1097 |     - |     - |     688 B |
| Validate | Job-VZHMRU | .NET Core 3.1 | netcoreapp31 |  IbanNet Loose | 549.7 ns |  1.00 ns |  0.78 ns |  1.19 |    0.00 | 0.1097 |     - |     - |     688 B |
| Validate | Job-SJRRVK | .NET Core 3.0 | netcoreapp30 |  IbanNet Loose | 569.6 ns |  1.90 ns |  1.77 ns |  1.24 |    0.01 | 0.1097 |     - |     - |     688 B |
| Validate | Job-ZHHGDR |      .NET 4.8 |        net48 |  IbanNet Loose | 635.7 ns |  7.82 ns |  7.31 ns |  1.38 |    0.02 | 0.0925 |     - |     - |     586 B |
| Validate | Job-ZJJUAW | .NET Core 2.2 | netcoreapp22 |  IbanNet Loose | 640.6 ns | 12.34 ns | 14.21 ns |  1.40 |    0.03 | 0.1097 |     - |     - |     696 B |
|          |            |               |              |                |          |          |          |       |         |        |       |       |           |
| Validate | Job-VKWGJR | .NET Core 5.0 | netcoreapp50 | IbanNet Strict | 614.1 ns |  1.56 ns |  1.46 ns |  1.00 |    0.00 | 0.1364 |     - |     - |     856 B |
| Validate | Job-VZHMRU | .NET Core 3.1 | netcoreapp31 | IbanNet Strict | 703.4 ns | 13.14 ns | 13.50 ns |  1.15 |    0.02 | 0.1364 |     - |     - |     856 B |
| Validate | Job-SJRRVK | .NET Core 3.0 | netcoreapp30 | IbanNet Strict | 713.9 ns |  1.61 ns |  1.26 ns |  1.16 |    0.00 | 0.1364 |     - |     - |     856 B |
| Validate | Job-ZJJUAW | .NET Core 2.2 | netcoreapp22 | IbanNet Strict | 787.4 ns |  5.82 ns |  5.44 ns |  1.28 |    0.01 | 0.1364 |     - |     - |     864 B |
| Validate | Job-ZHHGDR |      .NET 4.8 |        net48 | IbanNet Strict | 790.0 ns |  2.47 ns |  2.31 ns |  1.29 |    0.01 | 0.1192 |     - |     - |     754 B |

### Comparison with other validators

```
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.630 (2004/?/20H1)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.100
  Job-PMIWCW : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  Job-ZSANOP : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  Job-RETBQN : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  Job-UFXXMQ : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), X64 RyuJIT
  Job-GDDXTR : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
```

|                    Method |        Job |       Runtime |    Toolchain | Count |        Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |----------- |-------------- |------------- |------ |------------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             IbanNet_Loose | Job-PMIWCW | .NET Core 5.0 | netcoreapp50 |    10 |  5,803.4 ns |  17.94 ns |  15.91 ns |  0.77 |    0.01 | 1.1215 |     - |     - |    7056 B |
|             IbanNet_Loose | Job-RETBQN | .NET Core 3.1 | netcoreapp31 |    10 |  5,901.7 ns |  20.80 ns |  16.24 ns |  0.79 |    0.00 | 1.1215 |     - |     - |    7056 B |
|             IbanNet_Loose | Job-ZSANOP | .NET Core 3.0 | netcoreapp30 |    10 |  5,931.4 ns |  34.97 ns |  29.20 ns |  0.79 |    0.00 | 1.1215 |     - |     - |    7056 B |
|             IbanNet_Loose | Job-UFXXMQ | .NET Core 2.2 | netcoreapp22 |    10 |  6,470.3 ns |   9.66 ns |   9.04 ns |  0.86 |    0.01 | 1.1292 |     - |     - |    7112 B |
|             IbanNet_Loose | Job-GDDXTR |      .NET 4.8 |        net48 |    10 |  6,730.0 ns |  21.82 ns |  19.34 ns |  0.89 |    0.01 | 0.9613 |     - |     - |    6058 B |
| IbanNet_Strict_CacheReuse | Job-RETBQN | .NET Core 3.1 | netcoreapp31 |    10 |  7,024.2 ns |  17.95 ns |  14.99 ns |  0.93 |    0.01 | 1.3580 |     - |     - |    8560 B |
| IbanNet_Strict_CacheReuse | Job-ZSANOP | .NET Core 3.0 | netcoreapp30 |    10 |  7,108.0 ns |  17.81 ns |  15.79 ns |  0.94 |    0.01 | 1.3580 |     - |     - |    8560 B |
| IbanNet_Strict_CacheReuse | Job-PMIWCW | .NET Core 5.0 | netcoreapp50 |    10 |  7,355.4 ns |  10.52 ns |   9.33 ns |  0.98 |    0.01 | 1.3580 |     - |     - |    8560 B |
|            IbanNet_Strict | Job-PMIWCW | .NET Core 5.0 | netcoreapp50 |    10 |  7,527.6 ns |  59.80 ns |  55.93 ns |  1.00 |    0.00 | 1.3809 |     - |     - |    8696 B |
|            IbanNet_Strict | Job-ZSANOP | .NET Core 3.0 | netcoreapp30 |    10 |  7,622.0 ns |  19.06 ns |  15.92 ns |  1.01 |    0.01 | 1.3809 |     - |     - |    8696 B |
| IbanNet_Strict_CacheReuse | Job-UFXXMQ | .NET Core 2.2 | netcoreapp22 |    10 |  7,665.6 ns |  18.13 ns |  16.96 ns |  1.02 |    0.01 | 1.3580 |     - |     - |    8640 B |
|            IbanNet_Strict | Job-RETBQN | .NET Core 3.1 | netcoreapp31 |    10 |  7,678.1 ns |  21.21 ns |  16.56 ns |  1.02 |    0.01 | 1.3733 |     - |     - |    8696 B |
| IbanNet_Strict_CacheReuse | Job-GDDXTR |      .NET 4.8 |        net48 |    10 |  7,963.3 ns |  17.91 ns |  16.76 ns |  1.06 |    0.01 | 1.1902 |     - |     - |    7542 B |
|            IbanNet_Strict | Job-UFXXMQ | .NET Core 2.2 | netcoreapp22 |    10 |  8,330.8 ns |  24.96 ns |  22.13 ns |  1.11 |    0.01 | 1.3885 |     - |     - |    8784 B |
|            IbanNet_Strict | Job-GDDXTR |      .NET 4.8 |        net48 |    10 |  8,460.5 ns | 105.55 ns |  88.14 ns |  1.12 |    0.02 | 1.2207 |     - |     - |    7735 B |
|       NuGet_IbanValidator | Job-PMIWCW | .NET Core 5.0 | netcoreapp50 |    10 | 24,337.8 ns |  62.32 ns |  55.24 ns |  3.23 |    0.02 | 6.0730 |     - |     - |   38112 B |
|       NuGet_IbanValidator | Job-RETBQN | .NET Core 3.1 | netcoreapp31 |    10 | 31,345.9 ns |  85.85 ns |  71.69 ns |  4.17 |    0.04 | 6.1646 |     - |     - |   38912 B |
|       NuGet_IbanValidator | Job-ZSANOP | .NET Core 3.0 | netcoreapp30 |    10 | 31,383.8 ns |  96.05 ns |  89.85 ns |  4.17 |    0.04 | 6.1646 |     - |     - |   38912 B |
|            NuGet_IBAN4NET | Job-PMIWCW | .NET Core 5.0 | netcoreapp50 |    10 | 34,112.9 ns |  79.87 ns |  74.71 ns |  4.53 |    0.04 | 1.8311 |     - |     - |   11592 B |
|       NuGet_IbanValidator | Job-UFXXMQ | .NET Core 2.2 | netcoreapp22 |    10 | 36,419.1 ns | 123.36 ns | 109.35 ns |  4.84 |    0.04 | 6.2256 |     - |     - |   39400 B |
|            NuGet_IBAN4NET | Job-RETBQN | .NET Core 3.1 | netcoreapp31 |    10 | 36,620.7 ns |  66.36 ns |  62.07 ns |  4.87 |    0.04 | 1.8311 |     - |     - |   11592 B |
|            NuGet_IBAN4NET | Job-ZSANOP | .NET Core 3.0 | netcoreapp30 |    10 | 37,518.8 ns | 133.09 ns | 124.49 ns |  4.98 |    0.04 | 1.8311 |     - |     - |   11592 B |
|       NuGet_IbanValidator | Job-GDDXTR |      .NET 4.8 |        net48 |    10 | 42,729.8 ns | 138.67 ns | 115.80 ns |  5.68 |    0.05 | 5.0049 |     - |     - |   31549 B |
|            NuGet_IBAN4NET | Job-GDDXTR |      .NET 4.8 |        net48 |    10 | 51,105.5 ns |  66.16 ns |  61.89 ns |  6.79 |    0.05 | 2.1362 |     - |     - |   13689 B |
|            NuGet_IBAN4NET | Job-UFXXMQ | .NET Core 2.2 | netcoreapp22 |    10 | 52,736.5 ns | 182.35 ns | 161.65 ns |  7.01 |    0.06 | 1.8921 |     - |     - |   12208 B |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net5.0 netcoreapp3.1 netcoreapp2.1 net48 --runtimes netcoreapp50 netcoreapp31 netcoreapp21 net48
```
