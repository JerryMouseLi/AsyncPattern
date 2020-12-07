using System;
using System.Net;

namespace EAP
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += Wc_DownloadStringCompleted;
            wc.DownloadStringAsync(new Uri("http://www.baidu.com"));
            Console.WriteLine("执行其他任务。");
            Console.ReadKey();
        }
        private static void Wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Console.WriteLine("网页字数统计：" + e.Result.Length);
        }
    }
}
