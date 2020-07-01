using System;
using System.Threading.Tasks;

namespace Console_App_Task_With_Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            var func = new Func<int, int>((n) =>
            {
                int total = 0;

                for (int i = 0; i < n; i++)
                {
                    Task.Delay(5000);

                    total += (int)Math.Pow(i, 2);

                    Console.WriteLine($"{total}");
                }

                return total;
            });

            Task<int> myTask1 = new Task<int>(() => { return func(50); });

            Task<int> myTask2Square =
                myTask1.ContinueWith((myTask) =>
                {
                    return (int)Math.Sqrt(myTask.Result);
                }, TaskContinuationOptions.OnlyOnRanToCompletion);


            myTask1.Start();

            Task.WaitAll(myTask1, myTask2Square);

            Console.WriteLine($"First result: {myTask1.Result}");

            Console.WriteLine($"second result: {myTask2Square.Result}");

            Console.ReadKey();

        }
    }
}
