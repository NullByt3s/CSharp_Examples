using System;
using System.Threading.Tasks;
using System.Threading;

namespace Csharp_Examples
{
    public static class ThreadExamples
    {
        public static void RunTests()
        {
            //Tasks are background asynchronous operations, 


            //First way of creating a Task
            //Task t = new Task(Speak);
            //t.Start();

            //the program can finish before the thread even finishes executing
            //t.Wait(); //Forces the program to wait for the thread.

            //Other ways to create & execute Tasks
            //Task t2 = Task.Factory.StartNew(Speak);
            //Task t3 = Task.Run(new Action(Speak));


            //Check if its a thread pool or a custom thread

            //Thread pool thread
            //Task.Factory.StartNew(MyTypeOfThead).Wait();

            //Custom Thread
            //Task.Factory.StartNew(MyTypeOfThead, TaskCreationOptions.LongRunning).Wait();


            //Using lambdas as our action for adding parameters, 
            //                              Compiler creates closure for us
            Task.Factory.StartNew(() => SaySomething("Hello World.")).Wait();

            Task<int> t = Task.Run(() => Add(60, 39));
            //Task<int> t = Task.Run(() => { return 60 + 39;});

            //Get the result from the thread
            Console.WriteLine("The result of my adding function : " + t.Result); //This will force main thread to wait.

            Console.WriteLine("End of Thread tests.");

        }

        private static void Speak()
        {
            Console.WriteLine("Hello World");
            MyTypeOfThead();
        }

        private static void SaySomething(string something)
        {
            Console.WriteLine(something);
        }

        private static void MyTypeOfThead()
        {
            Console.WriteLine("I'm a {0} thread", Thread.CurrentThread.IsThreadPoolThread? "Thread pool":"Custom");
            Console.WriteLine(Thread.CurrentThread.Priority);
        }


        private static int Add(int x, int y)
        {
            return x + y;
        }
    }
}
