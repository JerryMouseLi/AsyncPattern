using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APM
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== 异步调用 AsyncInvokeTest =====");
            WebResponseHandler handler = new WebResponseHandler(WebContentLength.GetResult);
            //IAsyncResult: 异步操作接口(interface)
            //BeginInvoke: 委托(delegate)的一个异步方法的开始
            IAsyncResult result = handler.BeginInvoke( null, null);
            Console.WriteLine("继续做别的事情。");
            //异步操作返回
            Console.WriteLine(handler.EndInvoke(result));
            Console.ReadKey();
        }
    }
    public delegate string WebResponseHandler();
    public class WebContentLength
    {
        public static string GetResult()
        {
            var client = new WebClient();
            var content =  client.DownloadString(new Uri("http://cnblogs.com"));
            return "网页字数统计："+content.Length;
        }
    }
}
