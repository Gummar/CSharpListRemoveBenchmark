using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace CSharpListRemoveBenchmark
{

    public class MyObject {}

    // [MemoryDiagnoser]
    [IterationCount(300)]
    public class MyBenchmarks
    {
        private int myObjAmount = 400;
        private float nullPercentTarget = 0.2f; // Percentage of values that will become null, every iteration, the myObjAmount will be multiplied by this value until it reaches minValuesToIterateEachCycle
        private int nullsToRemoveAll = 2; // Used in the two RemoveAllWhenNullCountIsMoreThanX functions
        private int onlyNullsAmount = 100000;
        private int objAmountToIncreaseListSize = 400; // If our list has less values than objAmountToIncreaseListSize when we reach the benchmarked function end, we raise the next List size to objAmountToIncreaseListSize
        private List<MyObject?> list = [];
        private Random myRandom = new();
        // private HashSet<MyObject?> baseHashSet = [];
        private List<MyObject?> onlyNulls = [];
        private HashSet<MyObject?> valuesToRemove = [];
        private List<int> possibleObjAmounts = [];
        private int currentNullsCount = 0;
        private int testNum = 0;

        [GlobalSetup]
        public void Setup()
        {
            testNum = 0;
            int tempObjAmount = myObjAmount;
            possibleObjAmounts = []; //When myObjAmount is 800 and nullPercentTarget is 0.2, we will have [800,640,512,409,327,261,208,166,132,105,84,67,53,42,33,26,20,16,12,9,7,5,4,3,2,1] in the list, objAmountToIncreaseListSize removes the last elements from the list that are less than objAmountToIncreaseListSize
            while (tempObjAmount >= objAmountToIncreaseListSize)
            {
                Console.WriteLine(tempObjAmount);
                possibleObjAmounts.Add(tempObjAmount);
                tempObjAmount = (int)(tempObjAmount * (1f - nullPercentTarget));
            }
            onlyNulls = [..Enumerable.Repeat<MyObject?>(null, onlyNullsAmount)];
        }

        [IterationSetup]
        public void NextTest()
        {
            currentNullsCount = 0;
            if (testNum >= possibleObjAmounts.Count)
            {
                testNum = 0;
            }
            list = new(possibleObjAmounts[testNum]);
            while (list.Count < possibleObjAmounts[testNum])
            {
                list.Add(new());
            }
            int removeCount = (int)(possibleObjAmounts[testNum] * nullPercentTarget);
            valuesToRemove = new(removeCount);
            while (valuesToRemove.Count < removeCount)
            {
                valuesToRemove.Add(list[myRandom.Next(list.Count)]);
            }
            testNum++;
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
        // In a real situation, we don't know which values to remove, we also have to iterate through a List without waiting if we want to give access to another for/while iterator to access the same list
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
            currentNullsCount = 0;
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
            currentNullsCount = 0;
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
        public void SwapNullsToEnd()
        {
            int j = 0;
            for (int i = 0;i < list.Count;i++)
            {
                if (valuesToRemove.Contains(list[i]))
                {
                    list[j] = list[i];
                    j++;
                }
            }
            for (int i = j;i < list.Count;i++)
            {
                list[i] = null;
            }
        }

        [Benchmark]
        public void SwapNullsToEndIgnoringOrder()
        {
            int i = 0;
            int j = list.Count - 1;

            while (i < j)
            {
                while (i < j && !valuesToRemove.Contains(list[i]))
                {
                    i++;
                }
                while (i < j && valuesToRemove.Contains(list[i]))
                {
                    j--;
                }
                if (i < j)
                {
                    (list[j], list[i]) = (null, list[j]);

                    i++;
                    j--;
                }
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