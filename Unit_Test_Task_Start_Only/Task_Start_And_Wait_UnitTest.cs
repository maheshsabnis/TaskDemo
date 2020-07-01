using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit_Test_Task_Start_Only
{
    [TestClass]
    public class Task_Start_And_Wait_UnitTest
    {
        [TestMethod]
        public void Test_Create_A_New_Task_Using_Action_Delegate()
        {
            var action = new Action(() => 
            { 
                Task.Delay(5000); 
                Console.WriteLine("Hello Task World"); 
            });

            Task myTask = new Task(action);
            myTask.Start();

            myTask.Wait();
        }

        [TestMethod]
        public void Test_Create_A_New_Task_Using_Action_Delegate_With_Parameter()
        {
            var action = new Action<int>((n) => 
            {
                for (int i = 0; i < n; i++)
                {
                    Task.Delay(5000);
                    Console.WriteLine($"{i}");
                }
            });

            Task myTask = new Task(() => { action(500); });
            myTask.Start();

            myTask.Wait();
        }

        [TestMethod]
        public void Test_Create_A_New_Task_Using_Func_Delegate_With_Return()
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

            Task<int> myTask = new Task<int>(() => { return func(50); });
            myTask.Start();

            myTask.Wait();

            Console.WriteLine($"My total {myTask.Result}");
        }
    }
}
