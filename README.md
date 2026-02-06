Benchmark of different ways to remove items from a C# List.

Instructions:

```Batch
dotnet restore
dotnet run -c release
```

You can edit the variables in top Program.cs before running again.
Different amount of values in a list gives different results.

Output sample:

```
| Method                                    | Mean       | Error      | StdDev     | Median     | Ratio | RatioSD |
|------------------------------------------ |-----------:|-----------:|-----------:|-----------:|------:|--------:|
| ForwardIterate                            |   5.374 us |  0.2657 us |   1.342 us |   4.800 us |  0.19 |    0.06 |
| BackwardIterate                           |   4.468 us |  0.2118 us |   1.088 us |   4.000 us |  0.16 |    0.05 |
| ForwardIterateWithCheck                   |  28.736 us |  0.9116 us |   4.587 us |  28.000 us |  1.02 |    0.22 |
| BackwardIterateWithCheck                  |  27.183 us |  0.7592 us |   3.749 us |  26.850 us |  0.97 |    0.19 |
| ForwardNull                               |  31.290 us |  1.0264 us |   5.031 us |  30.300 us |  1.11 |    0.24 |
| BackwardNull                              |  30.237 us |  0.5701 us |   2.800 us |  29.100 us |  1.08 |    0.18 |
| ForwardRemove                             | 119.577 us |  7.9446 us |  40.046 us |  97.300 us |  4.26 |    1.56 |
| BackwardRemove                            | 162.349 us |  5.2653 us |  25.557 us | 170.000 us |  5.78 |    1.24 |
| ForwardRemoveAt                           |  44.233 us |  0.6419 us |   3.134 us |  44.300 us |  1.57 |    0.25 |
| BackwardRemoveAt                          |  43.508 us |  0.6917 us |   3.436 us |  42.100 us |  1.55 |    0.25 |
| RemoveAllPredicate                        |  31.007 us |  0.4255 us |   2.069 us |  30.300 us |  1.10 |    0.17 |
| ForwardRemoveAllAtPercentThreshold        |  38.102 us |  0.5625 us |   2.714 us |  37.050 us |  1.36 |    0.22 |
| BackwardRemoveAllAtPercentThreshold       |  36.647 us |  0.4089 us |   2.012 us |  36.000 us |  1.30 |    0.20 |
| ForwardRemoveAllWhenNullCountIsMoreThanX  | 259.687 us | 12.8488 us |  65.118 us | 241.000 us |  9.24 |    2.69 |
| BackwardRemoveAllWhenNullCountIsMoreThanX | 236.752 us | 12.4826 us |  63.828 us | 208.950 us |  8.43 |    2.59 |
| ForwardRemoveAllWhenNullFound             | 462.820 us | 22.3848 us | 114.865 us | 390.900 us | 16.48 |    4.75 |
| BackwardRemoveAllWhenNullFound            | 387.473 us | 23.6624 us | 121.846 us | 309.200 us | 13.79 |    4.80 |
| MoveNullsToEnd                            |  39.763 us |  1.4504 us |   7.217 us |  37.100 us |  1.42 |    0.33 |
| IterateThrough100000Nulls                 |  53.499 us |  0.8104 us |   3.972 us |  52.700 us |  1.90 |    0.31 |
```