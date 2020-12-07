using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAP_Winform
{
    /// <summary>
    /// EAP是对APM的封装
    /// </summary>
    public class Worker
    {
        public enum WorkerStatus
        {
            Cancel = 0, Running = 1, Completed = 2
        }
        public class WorkerEventArgs : EventArgs
        {
            public WorkerStatus Status { get; set; }
            public string Message { get; set; }
        }
        public Worker()
        {
        }
        public event EventHandler<WorkerEventArgs> OnWorkCompleted;
        IAsyncResult asyncResult = null;
        Thread thread = null;
        public void WorkAsync()
        {
            Worker _this = this;

            Action action = () =>
            {
                thread = Thread.CurrentThread;
                Thread.Sleep(1000);
                Console.WriteLine(string.Format("线程：{0}，Work Over.", Thread.CurrentThread.ManagedThreadId));

            };
            //result是IAsyncResult对象，此处无用
            //当
            asyncResult = action.BeginInvoke((result) =>
            {       
                 WorkerEventArgs e = null;
                try
                {
                    action.EndInvoke(result);
                }
                catch (ThreadAbortException ex)
                {
                    e = new WorkerEventArgs() { Status = WorkerStatus.Cancel, Message = "异步操作被取消" };
                }
                if (null != _this.OnWorkCompleted)
                {
                    _this.OnWorkCompleted.Invoke(this, e);
                }
            },this);

          //  asyncResult = action.BeginInvoke(new Test(this,action, OnWorkCompleted).AsyncCallBack, null);
        }
        public void CancelAsync()
        {
            if (null != thread)
                thread.Abort();
        }
    }
}
