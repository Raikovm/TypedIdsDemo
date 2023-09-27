# Benchmarks


+ **TypedIds** - entities with self-written extension methods 
+ **StronglyTypedIds** - entities with code generated ids with StronglyTypedIds package
+ **UntypedIds** - raw untyped ids

### Insert 100 entities 
```
BenchmarkDotNet v0.13.8, Windows 11 (10.0.22000.739/21H2/SunValley)
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.102
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  Job-DLWEJO : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
```
| Method           | Mean     | Error    | StdDev   | Median   | Allocated |
|------------------|---------:|---------:|---------:|---------:|----------:|
| TypedIds         | 17.47 ms | 0.833 ms | 2.376 ms | 16.85 ms |   2.21 MB |
| StronglyTypedIds | 18.13 ms | 1.274 ms | 3.675 ms | 16.95 ms |   2.28 MB |
| UntypedIds       | 16.29 ms | 0.455 ms | 1.284 ms | 15.90 ms |   2.16 MB |


### Get

```
BenchmarkDotNet v0.13.8, Windows 11 (10.0.22000.739/21H2/SunValley)
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.102
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
```
| Method                  | Mean     | Error    | StdDev   | Gen0    | Allocated |
|------------------------ |---------:|---------:|---------:|--------:|----------:|
| TypedIds_GetAll         | 611.8 μs | 12.21 μs | 33.44 μs | 63.4766 |  260.6 KB |
| StronglyTypedIds_GetAll | 623.1 μs | 17.40 μs | 49.37 μs | 63.4766 |  260.6 KB |
| UntypedIds_GetAll       | 616.4 μs | 12.16 μs | 27.44 μs | 63.4766 |  260.6 KB |
| TypedIds_ById           | 243.0 μs |  4.84 μs |  8.22 μs |  0.9766 |   4.88 KB |
| StronglyTypedIds_ById   | 240.8 μs |  4.74 μs |  7.10 μs |  0.9766 |   4.88 KB |
| UntypedIds_ById         | 235.1 μs |  4.63 μs |  8.47 μs |  0.9766 |   4.38 KB |

