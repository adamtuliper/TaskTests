using System;
using System.Threading.Tasks;

namespace TaskTests
{
    class Program
    {
        //What happens when we call everything with async/await?
        //Note this Main method is ALSO await!! WOOHOO! C# 7.1 feature :)
        static async Task Main2(string[] args)
        {
            Console.WriteLine("Calling Cleanup");
            await SomeTaskMethod();
            Console.WriteLine("Cleanup called");
            Console.ReadLine();
        }

        //ut oh what happens when we call task.delay without
        //being in async code and not using await?
        static void MainDelayNoWait(string[] args)
        {
            Console.WriteLine("Calling Delay");
            Task.Delay(10000);
            Console.WriteLine("Delay called");
            Console.ReadLine();
        }

        //Start not async, but run async code and wait until it's done
        static void Main(string[] args)
        {
            Console.WriteLine("Calling Cleanup");
            var t = Task.Run(async () => { await SomeTaskMethod(); });
            t.Wait();
            Console.WriteLine("Cleanup called");
            Console.ReadLine();
        }

        //Non-async and we kick off task.delay in an async method.
        //But we're starting non-async. What happens. Study this case carefully
        static void Main1(string[] args)
        {
            Console.WriteLine("Calling SomeTaskMethod");
            //No await, causes separate task to be created. 
            SomeTaskMethod();
            Console.WriteLine("Cleanup SomeTaskMethod");
            Console.ReadLine();
        }


        public static async Task SomeTaskMethod()
        {
            //Just some random slowness
            for (int j = 0; j < 2000; j++)
            {
                var width = Console.Title;
            }

            Console.WriteLine("After console.title reading business");
            int i;
            for (i = 0; i < 5; i++)
            {
                Console.WriteLine($"{DateTime.Now} right before task delay");
                await Task.Delay(5000);
                Console.WriteLine($"{DateTime.Now} Just delayed 5");
            }
        }

    }
}
