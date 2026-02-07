Benchmark of different ways to remove items from a C# List using BenchmarkDotNet.

Instructions:

```Batch
dotnet restore
dotnet run -c release
```

You can edit the variables in top Program.cs before running again.
Different amount of values in a list gives different results.

Output sample:
200 elements in the list
```
| Method                                    | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD |
|------------------------------------------ |----------:|----------:|----------:|----------:|------:|--------:|
| ForwardIterate                            |  1.642 us | 0.0610 us | 0.3133 us |  1.500 us |  0.16 |    0.03 |
| BackwardIterate                           |  1.490 us | 0.0573 us | 0.2959 us |  1.400 us |  0.14 |    0.03 |
| ForwardIterateWithCheck                   | 10.608 us | 0.0999 us | 0.4991 us | 10.600 us |  1.00 |    0.07 |
| BackwardIterateWithCheck                  | 10.451 us | 0.1053 us | 0.5231 us | 10.400 us |  0.99 |    0.07 |
| ForwardNull                               | 11.031 us | 0.1221 us | 0.5950 us | 11.000 us |  1.04 |    0.07 |
| BackwardNull                              | 10.936 us | 0.1145 us | 0.5568 us | 10.900 us |  1.03 |    0.07 |
| ForwardRemove                             | 20.755 us | 0.1627 us | 0.7914 us | 20.750 us |  1.96 |    0.12 |
| BackwardRemove                            | 22.012 us | 0.2063 us | 0.9957 us | 21.900 us |  2.08 |    0.14 |
| ForwardRemoveAt                           | 13.664 us | 0.2191 us | 1.0822 us | 13.350 us |  1.29 |    0.12 |
| BackwardRemoveAt                          | 12.928 us | 0.1280 us | 0.6152 us | 12.900 us |  1.22 |    0.08 |
| RemoveAllPredicate                        | 11.002 us | 0.1671 us | 0.8144 us | 10.700 us |  1.04 |    0.09 |
| ForwardRemoveAllAtPercentThreshold        | 13.188 us | 0.1142 us | 0.5610 us | 13.100 us |  1.25 |    0.08 |
| BackwardRemoveAllAtPercentThreshold       | 13.230 us | 0.1636 us | 0.7986 us | 13.100 us |  1.25 |    0.10 |
| ForwardRemoveAllWhenNullCountIsMoreThanX  | 30.613 us | 0.3269 us | 1.5299 us | 30.400 us |  2.89 |    0.20 |
| BackwardRemoveAllWhenNullCountIsMoreThanX | 28.959 us | 0.2914 us | 1.3577 us | 28.800 us |  2.74 |    0.18 |
| ForwardRemoveAllWhenNullFound             | 51.483 us | 0.9213 us | 4.2657 us | 50.500 us |  4.86 |    0.46 |
| BackwardRemoveAllWhenNullFound            | 45.135 us | 0.6380 us | 2.9792 us | 44.450 us |  4.26 |    0.35 |
| SwapNullsToEnd                            | 13.116 us | 0.1385 us | 0.6930 us | 13.100 us |  1.24 |    0.09 |
| SwapNullsToEndIgnoringOrder               |  9.572 us | 0.5700 us | 2.8937 us |  8.450 us |  0.90 |    0.28 |
| IterateThrough100000Nulls                 | 50.467 us | 0.3412 us | 1.7479 us | 49.600 us |  4.77 |    0.28 |
```

400 elements in the list
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

800 elements in the list
```
| Method                                    | Mean       | Error      | StdDev      | Median     | Ratio | RatioSD |
|------------------------------------------ |-----------:|-----------:|------------:|-----------:|------:|--------:|
| ForwardIterate                            |   4.648 us |  0.1215 us |   0.6025 us |   4.400 us |  0.12 |    0.02 |
| BackwardIterate                           |   4.014 us |  0.1178 us |   0.5895 us |   3.750 us |  0.10 |    0.02 |
| ForwardIterateWithCheck                   |  39.810 us |  0.3387 us |   1.6342 us |  39.400 us |  1.00 |    0.06 |
| BackwardIterateWithCheck                  |  38.482 us |  0.3461 us |   1.6961 us |  38.000 us |  0.97 |    0.06 |
| ForwardNull                               |  40.115 us |  0.6666 us |   3.2545 us |  40.800 us |  1.01 |    0.09 |
| BackwardNull                              |  45.337 us |  1.6443 us |   8.4967 us |  40.800 us |  1.14 |    0.22 |
| ForwardRemove                             | 370.269 us | 67.4009 us | 351.2762 us | 165.050 us |  9.32 |    8.84 |
| BackwardRemove                            | 176.857 us |  8.6351 us |  39.2850 us | 181.500 us |  4.45 |    1.00 |
| ForwardRemoveAt                           |  57.609 us |  0.6096 us |   3.0336 us |  56.500 us |  1.45 |    0.09 |
| BackwardRemoveAt                          |  55.212 us |  0.6906 us |   3.4555 us |  53.900 us |  1.39 |    0.10 |
| RemoveAllPredicate                        |  44.657 us |  1.4572 us |   7.5429 us |  40.150 us |  1.12 |    0.19 |
| ForwardRemoveAllAtPercentThreshold        |  53.441 us |  1.3680 us |   7.0812 us |  49.700 us |  1.34 |    0.19 |
| BackwardRemoveAllAtPercentThreshold       |  46.964 us |  0.2860 us |   1.3855 us |  46.700 us |  1.18 |    0.06 |
| ForwardRemoveAllWhenNullCountIsMoreThanX  | 266.838 us | 15.1764 us |  77.8759 us | 316.400 us |  6.71 |    1.97 |
| BackwardRemoveAllWhenNullCountIsMoreThanX | 245.014 us | 14.1123 us |  72.5424 us | 291.950 us |  6.16 |    1.84 |
| ForwardRemoveAllWhenNullFound             | 508.741 us | 23.7427 us | 122.6846 us | 430.250 us | 12.80 |    3.12 |
| BackwardRemoveAllWhenNullFound            | 462.812 us | 20.4277 us | 105.1893 us | 408.100 us | 11.64 |    2.68 |
| SwapNullsToEnd                            |  47.400 us |  0.3233 us |   1.5939 us |  47.100 us |  1.19 |    0.06 |
| SwapNullsToEndIgnoringOrder               |  20.449 us |  0.5598 us |   2.7593 us |  20.750 us |  0.51 |    0.07 |
| IterateThrough100000Nulls                 |  50.322 us |  0.3089 us |   1.5171 us |  49.600 us |  1.27 |    0.06 |
```
