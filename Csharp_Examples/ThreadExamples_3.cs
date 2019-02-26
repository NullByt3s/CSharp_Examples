using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace Csharp_Examples
{
    public static class ThreadExamples_3
    {
        public static void RunTests()
        {
            //IO Bound Tasks

            //Not very efficient, you are creating a thread and making it useless untill it finishes downloading
            Task<string> downloadTask = DownloadWebPageAsync("https://www.w3schools.com");
            while (!downloadTask.IsCompleted)
            {
                Console.WriteLine(".");
                Thread.Sleep(250);
            }

            Console.WriteLine(downloadTask.Result);

            AsyncTester atest = new AsyncTester();
            atest.RunIt(new object(), null);

            Console.ReadLine();
        }

        private static string Download_WebPage(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            {
                //returns the web page content
                return reader.ReadToEnd();
            }
        }

        private static Task<string> DownloadWebPageAsync(string url)
        {
            return Task.Factory.StartNew(() => Download_WebPage(url));
        }




    }
}

class AsyncTester
{

    public async void RunIt(object sender, EventArgs e)
    {
        await SleepAsyncA(5000);
        Console.WriteLine("Done Sleeping");
    }

    //Regular wrapping way, not good
    public Task SleepAsyncA(int timeout)
    {
        return Task.Run(() => Thread.Sleep(timeout));
    }

    //Better way, threads are not being held up,
    //Threads are made available for others while its sleeping
    public Task SleepAsyncB(int timeout)
    {
        TaskCompletionSource<bool> tcs = null;
        var t = new Timer(delegate { tcs.TrySetResult(true); }, null, -1, -1);
        tcs = new TaskCompletionSource<bool>(t);
        t.Change(timeout, -1);
        return tcs.Task;
    }
}