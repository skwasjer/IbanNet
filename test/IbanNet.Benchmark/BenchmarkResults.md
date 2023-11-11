# IbanNet Benchmark Results

## Performance for >= v5.6

A single validation:

```
BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100-rc.2.23502.2
  [Host]     : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2
  Job-KKHTHD : .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2
  Job-JRRQIK : .NET 7.0.12 (7.0.1223.47720), X64 RyuJIT AVX2
  Job-AHKJZW : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2
  Job-CWCIQA : .NET Core 3.1.32 (CoreCLR 4.700.22.55902, CoreFX 4.700.22.56512), X64 RyuJIT AVX2
  Job-ALVDSL : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
```

| Method   | Runtime            | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------- |------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Validate | .NET 8.0           | 255.3 ns | 4.96 ns | 5.10 ns |  1.00 |    0.00 | 0.0305 |     192 B |        1.00 |
| Validate | .NET 7.0           | 271.0 ns | 2.77 ns | 2.31 ns |  1.06 |    0.03 | 0.0277 |     176 B |        0.92 |
| Validate | .NET 6.0           | 284.5 ns | 3.25 ns | 3.04 ns |  1.11 |    0.02 | 0.0277 |     176 B |        0.92 |
| Validate | .NET Core 3.1      | 344.6 ns | 2.87 ns | 2.54 ns |  1.35 |    0.03 | 0.0277 |     176 B |        0.92 |
| Validate | .NET Framework 4.8 | 367.7 ns | 1.03 ns | 0.80 ns |  1.44 |    0.03 | 0.0277 |     177 B |        0.92 |


### Comparison with other validators

> Worth mentioning is that IbanNet validates more strictly than the other alternative (competing) libraries, yet comes out quite a lot faster and has a much lower memory footprint.

#### Legend

- *Singleton_CacheReuse*: strict validation, singleton validator, reuse of rules and pattern cache
- *Singleton*: strict validation, singleton validator
- *Transient*: strict validation, transient validator (per validation). Notice the extra allocations/GC. This is not recommended, and purely for demonstration.

| Method               | Runtime            | Count | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0      | Allocated | Alloc Ratio |
|--------------------- |------------------- |------ |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| Singleton_CacheReuse | .NET 8.0           | 10000 |  2.672 ms | 0.0507 ms | 0.0564 ms |  0.82 |    0.03 |  304.6875 |   1.83 MB |        1.08 |
| Singleton_CacheReuse | .NET 6.0           | 10000 |  2.825 ms | 0.0562 ms | 0.0552 ms |  0.87 |    0.03 |  277.3438 |   1.68 MB |        0.99 |
| Singleton_CacheReuse | .NET 7.0           | 10000 |  2.831 ms | 0.0467 ms | 0.0390 ms |  0.87 |    0.03 |  277.3438 |   1.68 MB |        0.99 |
| Singleton            | .NET 8.0           | 10000 |  3.255 ms | 0.0639 ms | 0.0831 ms |  1.00 |    0.00 |  281.2500 |    1.7 MB |        1.00 |
| Singleton_CacheReuse | .NET Core 3.1      | 10000 |  3.462 ms | 0.0385 ms | 0.0360 ms |  1.07 |    0.03 |  277.3438 |   1.68 MB |        0.99 |
| Singleton            | .NET 7.0           | 10000 |  3.598 ms | 0.0669 ms | 0.0657 ms |  1.11 |    0.05 |  281.2500 |    1.7 MB |        1.00 |
| Singleton            | .NET 6.0           | 10000 |  3.710 ms | 0.0576 ms | 0.0538 ms |  1.14 |    0.04 |  281.2500 |    1.7 MB |        1.00 |
| Singleton_CacheReuse | .NET Framework 4.8 | 10000 |  3.726 ms | 0.0138 ms | 0.0122 ms |  1.15 |    0.03 |  277.3438 |   1.68 MB |        0.99 |
| Singleton            | .NET Core 3.1      | 10000 |  4.400 ms | 0.0197 ms | 0.0164 ms |  1.36 |    0.04 |  281.2500 |    1.7 MB |        1.00 |
| Singleton            | .NET Framework 4.8 | 10000 |  4.639 ms | 0.0310 ms | 0.0275 ms |  1.43 |    0.04 |  281.2500 |   1.71 MB |        1.00 |
| Transient            | .NET 8.0           | 10000 |  5.590 ms | 0.0703 ms | 0.0657 ms |  1.72 |    0.05 | 1195.3125 |   7.19 MB |        4.23 |
| Transient            | .NET 6.0           | 10000 |  6.464 ms | 0.1194 ms | 0.1117 ms |  1.99 |    0.07 | 1265.6250 |   7.58 MB |        4.45 |
| Transient            | .NET 7.0           | 10000 |  6.489 ms | 0.1261 ms | 0.2142 ms |  2.02 |    0.08 | 1265.6250 |   7.58 MB |        4.45 |
| Transient            | .NET Core 3.1      | 10000 |  7.422 ms | 0.0394 ms | 0.0329 ms |  2.29 |    0.07 | 1265.6250 |   7.58 MB |        4.45 |
| Transient            | .NET Framework 4.8 | 10000 |  7.752 ms | 0.0908 ms | 0.0805 ms |  2.39 |    0.06 | 1289.0625 |   7.75 MB |        4.56 |
| NuGet_IbanValidator  | .NET 8.0           | 10000 | 13.739 ms | 0.1706 ms | 0.1513 ms |  4.24 |    0.14 | 3062.5000 |  18.38 MB |       10.81 |
| NuGet_IbanValidator  | .NET 6.0           | 10000 | 14.225 ms | 0.2840 ms | 0.4975 ms |  4.44 |    0.19 | 3375.0000 |  20.21 MB |       11.88 |
| NuGet_IbanValidator  | .NET 7.0           | 10000 | 15.817 ms | 0.3072 ms | 0.3287 ms |  4.86 |    0.15 | 3375.0000 |  20.21 MB |       11.88 |
| NuGet_IBAN4NET       | .NET 8.0           | 10000 | 29.135 ms | 0.5341 ms | 0.9494 ms |  9.03 |    0.31 | 1687.5000 |  10.18 MB |        5.99 |
| NuGet_IBAN4NET       | .NET 6.0           | 10000 | 36.407 ms | 0.7124 ms | 0.6664 ms | 11.20 |    0.39 | 1642.8571 |  10.19 MB |        5.99 |
| NuGet_IBAN4NET       | .NET 7.0           | 10000 | 36.707 ms | 0.3702 ms | 0.3463 ms | 11.29 |    0.30 | 1642.8571 |  10.19 MB |        5.99 |
| NuGet_IbanValidator  | .NET Core 3.1      | 10000 | 38.961 ms | 0.7167 ms | 0.6353 ms | 12.01 |    0.37 | 8076.9231 |  48.67 MB |       28.62 |
| NuGet_IBAN4NET       | .NET Core 3.1      | 10000 | 40.821 ms | 0.4082 ms | 0.3819 ms | 12.56 |    0.33 | 1666.6667 |  10.19 MB |        5.99 |
| NuGet_IBAN4NET       | .NET Framework 4.8 | 10000 | 55.719 ms | 0.1811 ms | 0.1512 ms | 17.19 |    0.50 | 2000.0000 |  12.05 MB |        7.08 |
| NuGet_IbanValidator  | .NET Framework 4.8 | 10000 | 55.832 ms | 0.2081 ms | 0.1738 ms | 17.23 |    0.51 | 6555.5556 |  39.89 MB |       23.45 |


### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net8.0 --runtimes net80 net70 net60 netcoreapp31 net48
```
