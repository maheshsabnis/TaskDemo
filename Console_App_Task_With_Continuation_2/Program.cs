using System;
using System.Threading.Tasks;

namespace Console_App_Task_With_Continuation_2
{
    class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            Task<int> mytask = new Task<int>(() =>
            {
                Task.Delay(5000);
                int result = (random.Next(1, 100) % 2);

                if (result == 0)
                {
                    throw new Exception("Just a random exception");
                }
                else
                {
                    return random.Next(1, 100);
                }
            });

            mytask.ContinueWith((task) =>
            {
                Console.WriteLine($"Continue with these method when there is an exception. Message:{task.Exception.Message}");

            }, TaskContinuationOptions.OnlyOnFaulted);

            mytask.ContinueWith((task) =>
            {
                Console.WriteLine("Task done");

            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            try
            {
                mytask.Start();

                Console.WriteLine($"{mytask.Result}");
            }
            catch (AggregateException aex)
            {
                foreach (var e in aex.Flatten().InnerExceptions)
                {
                    if (e is Exception)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
