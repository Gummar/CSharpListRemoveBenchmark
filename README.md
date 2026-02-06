Benchmark of different ways to remove items from a C# List using BenchmarkDotNet.

Instructions:

```Batch
dotnet restore
dotnet run -c release
```

You can edit the variables in top Program.cs before running again.
Different amount of values in a list gives different results.

Output sample:

```
| Method                                    | Mean       | Error     | StdDev     | Median     | Ratio | RatioSD |
|------------------------------------------ |-----------:|----------:|-----------:|-----------:|------:|--------:|
| ForwardIterate                            |   2.749 us | 0.1186 us |  0.6085 us |   2.550 us |  0.14 |    0.03 |
| BackwardIterate                           |   2.430 us | 0.0910 us |  0.4593 us |   2.300 us |  0.13 |    0.02 |
| ForwardIterateWithCheck                   |  19.383 us | 0.1794 us |  0.8925 us |  19.150 us |  1.00 |    0.06 |
| BackwardIterateWithCheck                  |  18.358 us | 0.3154 us |  1.5637 us |  18.400 us |  0.95 |    0.09 |
| ForwardNull                               |  20.540 us | 0.2074 us |  1.0085 us |  20.400 us |  1.06 |    0.07 |
| BackwardNull                              |  19.748 us | 0.1322 us |  0.6326 us |  19.700 us |  1.02 |    0.06 |
| ForwardRemove                             |  51.570 us | 1.5120 us |  7.2812 us |  53.100 us |  2.67 |    0.39 |
| BackwardRemove                            |  56.524 us | 1.5857 us |  7.6057 us |  58.200 us |  2.92 |    0.41 |
| ForwardRemoveAt                           |  26.201 us | 0.3627 us |  1.7947 us |  25.900 us |  1.35 |    0.11 |
| BackwardRemoveAt                          |  24.936 us | 0.1649 us |  0.8038 us |  24.800 us |  1.29 |    0.07 |
| RemoveAllPredicate                        |  20.290 us | 0.1392 us |  0.6704 us |  20.200 us |  1.05 |    0.06 |
| ForwardRemoveAllAtPercentThreshold        |  24.243 us | 0.2832 us |  1.3933 us |  23.900 us |  1.25 |    0.09 |
| BackwardRemoveAllAtPercentThreshold       |  24.574 us | 0.3536 us |  1.7132 us |  24.000 us |  1.27 |    0.10 |
| ForwardRemoveAllWhenNullCountIsMoreThanX  |  83.479 us | 2.8381 us | 13.6675 us |  89.350 us |  4.32 |    0.73 |
| BackwardRemoveAllWhenNullCountIsMoreThanX |  81.246 us | 2.5261 us | 12.2131 us |  83.700 us |  4.20 |    0.66 |
| ForwardRemoveAllWhenNullFound             | 152.701 us | 5.8212 us | 29.5547 us | 161.050 us |  7.89 |    1.57 |
| BackwardRemoveAllWhenNullFound            | 135.279 us | 6.0919 us | 30.1436 us | 145.750 us |  6.99 |    1.59 |
| SwapNullsToEnd                            |  23.971 us | 0.3504 us |  1.7435 us |  23.900 us |  1.24 |    0.11 |
| SwapNullsToEndIgnoringOrder               |  11.249 us | 0.2251 us |  1.1241 us |  11.100 us |  0.58 |    0.06 |
| IterateThrough100000Nulls                 |  50.010 us | 0.3158 us |  1.5476 us |  49.300 us |  2.59 |    0.14 |
```
