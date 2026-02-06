using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace CSharpListRemoveBenchmark
{

    public class MyObject {}

    // [MemoryDiagnoser]
    [IterationCount(300)]
    public class MyBenchmarks
    {
        private int myObjAmount = 800;
        private float nullPercentTarget = 0.2f; // Percentage of values that will become null
        private int nullsToRemoveAll = 2; // Used in the two RemoveAllWhenNullCountIsMoreThanX functions
        private int onlyNullsAmount = 100000;
        private List<MyObject?> list = [];
        private Random myRandom = new();
        // private HashSet<MyObject?> baseHashSet = [];
        private List<MyObject?> baseList = [];
        private List<MyObject?> onlyNulls = [];
        private HashSet<MyObject?> valuesToRemove = [];

        [GlobalSetup]
        public void Setup()
        {
            // myRandom = new();

            // baseHashSet = new(myObjAmount);
            // while (baseHashSet.Count < myObjAmount)
            // {
            //    baseHashSet.Add(new());
            // }
            // baseList = [..baseHashSet];
            // int i = baseList.Count;
            // while (i > 1)
            // {
            //    i--;
            //    int j = myRandom.Next(i+1);
            //    (baseList[i], baseList[j]) = (baseList[j], baseList[i]);
            // }
            baseList = new(myObjAmount);
            while (baseList.Count < myObjAmount)
            {
                baseList.Add(new());
            }
            onlyNulls = [..Enumerable.Repeat<MyObject?>(null, onlyNullsAmount)];
        }

        [IterationSetup]
        public void NextTest()
        {
            list = [..baseList];
            int removeCount = (int)(myObjAmount * nullPercentTarget);
            valuesToRemove = new(removeCount);
            while (valuesToRemove.Count < removeCount)
            {
                valuesToRemove.Add(baseList[myRandom.Next(baseList.Count)]);
            }
        }


        [Benchmark]
        public void ForwardIterate()
        {
            for (int i = 0;i < list.Count;i++)
            {
            }
        }

        [Benchmark]
        public void BackwardIterate()
        {
            for (int i = list.Count - 1;i >= 0;i--)
            {
            }
        }

        [Benchmark(Baseline = true)]
        public void ForwardIterateWithCheck()
        {
            for (int i = 0;i < list.Count;i++)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                }
            }
        }

        [Benchmark]
        public void BackwardIterateWithCheck()
        {
            for (int i = list.Count - 1;i >= 0;i--)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                }
            }
        }

        [Benchmark]
        public void ForwardNull()
        {
            for (int i = 0;i < list.Count;i++)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list[i] = null;
                }
            }
        }

        [Benchmark]
        public void BackwardNull()
        {
            for (int i = list.Count - 1;i >= 0;i--)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list[i] = null;
                }
            }
        }

        [Benchmark]
        public void ForwardRemove()
        {
            for (int i = 0;i < list.Count;i++)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list.Remove(list[i]);
                    i--;
                }
            }
        }

        [Benchmark]
        public void BackwardRemove()
        {
            for (int i = list.Count - 1;i >= 0;i--)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list.Remove(list[i]);
                }
            }
        }

        [Benchmark]
        public void ForwardRemoveAt()
        {
            for (int i = 0;i < list.Count;i++)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }

        [Benchmark]
        public void BackwardRemoveAt()
        {
            for (int i = list.Count - 1;i >= 0;i--)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list.RemoveAt(i);
                }
            }
        }

        [Benchmark]
        // In a real situation, we don't know which values to remove, we also have to iterate through a List without waiting if we want to give access to another for/white iterator to access the same list
        public void RemoveAllPredicate()
        {
            list.RemoveAll(x => valuesToRemove.Contains(x));
        }

        [Benchmark]
        public void ForwardRemoveAllAtPercentThreshold()
        {
            for (int i = 0;i < list.Count;i++)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list[i] = null;
                }
            }
            list.RemoveAll(x => x == null);
        }

        [Benchmark]
        public void BackwardRemoveAllAtPercentThreshold()
        {
            for (int i = list.Count - 1;i >= 0;i--)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list[i] = null;
                }
            }
            list.RemoveAll(x => x == null);
        }

        [Benchmark]
        public void ForwardRemoveAllWhenNullCountIsMoreThanX()
        {
            int currentNullsCount = 0;
            for (int i = 0;i < list.Count;i++)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list[i] = null;
                    currentNullsCount++;
                    if (currentNullsCount >= nullsToRemoveAll)
                    {
                        list.RemoveAll(x => x == null);
                        i -= currentNullsCount;
                        currentNullsCount = 0;
                    }
                }
            }
            if (currentNullsCount > 0)
            {
                list.RemoveAll(x => x == null);
            }
        }

        [Benchmark]
        public void BackwardRemoveAllWhenNullCountIsMoreThanX()
        {
            int currentNullsCount = 0;
            for(int i = list.Count - 1;i >= 0;i--)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list[i] = null;
                    currentNullsCount++;
                    if (currentNullsCount >= nullsToRemoveAll)
                    {
                        list.RemoveAll(x => x == null);
                        currentNullsCount = 0;
                    }
                }
            }
            if (currentNullsCount > 0)
            {
                list.RemoveAll(x => x == null);
            }
        }

        [Benchmark]
        public void ForwardRemoveAllWhenNullFound()
        {
            for (int i = 0;i < list.Count;i++)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list[i] = null;
                    list.RemoveAll(x => x == null);
                    i--;
                }
            }
        }

        [Benchmark]
        public void BackwardRemoveAllWhenNullFound()
        {
            for (int i = list.Count - 1;i >= 0;i--)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list[i] = null;
                    list.RemoveAll(x => x == null);
                }
            }
        }

        [Benchmark]
        public void MoveNullsToEnd()
        {
            int writeIndex = 0;

            // 1. Move all non-null values to the front
            for (int i = 0; i < list.Count; i++)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list[writeIndex] = list[i];
                    writeIndex++;
                }
            }

            // 2. Fill the rest of the list with null
            while (writeIndex < list.Count)
            {
                list[writeIndex] = null;
                writeIndex++;
            }
        }

        [Benchmark]
        public void IterateThrough100000Nulls()
        {
            for(int i = 0;i < onlyNulls.Count;i++)
            {
                MyObject? nullRef = onlyNulls[i];
                if (nullRef == null)
                {
                    continue;
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            // This runs the benchmarks
            BenchmarkRunner.Run<MyBenchmarks>();
        }
    }
}