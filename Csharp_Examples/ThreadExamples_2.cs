using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;

namespace Csharp_Examples
{
    public static class ThreadExamples_2
    {
        public static void RunTests()
        {
            TimeIt(SequentialChancesToWin);
            TimeIt(TaskBasedChancesToWin);
        }

        private static void TimeIt(Action action)
        {
            Stopwatch timer = Stopwatch.StartNew();
            action();
            Console.WriteLine("Time it took: " + timer.Elapsed);
        }

        private static void TaskBasedChancesToWin()
        {
            BigInteger n = 49000;
            BigInteger r = 600;

            //Run Simultaneously
            Task<BigInteger> part1 = Task<BigInteger>.Factory.StartNew(() => Factorial(n));
            Task<BigInteger> part2 = Task<BigInteger>.Factory.StartNew(() => Factorial(n - r));
            Task<BigInteger> part3 = Task<BigInteger>.Factory.StartNew(() => Factorial(r));

            BigInteger chances = part1.Result / (part2.Result * part3.Result);

            Console.WriteLine("Task Based Chances: " + chances);
        }

        private static void SequentialChancesToWin()
        {
            BigInteger n = 49000;
            BigInteger r = 600;

            //Run One by One
            BigInteger part1 = Factorial(n);
            BigInteger part2 = Factorial(n - r);
            BigInteger part3 = Factorial(r);

            BigInteger chances = part1 / (part2 * part3);

            Console.WriteLine("Sequential Chances: " + chances);
        }

        static BigInteger Factorial(BigInteger factor)
        {
            BigInteger factorial = 1;

            for(BigInteger i = 1; i <= factor; i++)
            {
                factorial *= i;
            }

            return factorial;
        }
    }
}
