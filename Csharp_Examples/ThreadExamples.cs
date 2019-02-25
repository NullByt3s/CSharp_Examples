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


            Console.WriteLine("End of Thread tests.");
            Console.ReadLine();
        }


        private static void Speak()
        {
            Console.WriteLine("Hello World");
            MyTypeOfThead();
        }

        private static void MyTypeOfThead()
        {
            Console.WriteLine("I'm a {0} thread", Thread.CurrentThread.IsThreadPoolThread? "Thread pool":"Custom");
            Console.WriteLine(Thread.CurrentThread.Priority);
        }
    }
}
