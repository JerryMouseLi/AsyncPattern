using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace APMCallBack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== 异步回调 AsyncInvokeTest =====");
            WebResponseHandler handler = new WebResponseHandler(WebContentLength.GetResult);
            //异步操作接口(注意BeginInvoke方法的不同！)
            IAsyncResult result = handler.BeginInvoke( new AsyncCallback(CalllBack), "AsycState:OK");
            Console.WriteLine("继续做别的事情。");
            Console.ReadKey();
        }
        static void CalllBack(IAsyncResult result)
        {
            WebResponseHandler handler = (WebResponseHandler)((AsyncResult)result).AsyncDelegate;
            Console.WriteLine(handler.EndInvoke(result));
            Console.WriteLine(result.AsyncState);
        }
    }
    public delegate string WebResponseHandler();
    public class WebContentLength
    {
        public static string GetResult()
        {
            var client = new WebClient();
            var content = client.DownloadString(new Uri("http://cnblogs.com"));
            return "网页字数统计：" + content.Length;
        }
    }

}
