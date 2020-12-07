using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static EAP_Winform.Worker;

namespace EAP_Winform
{
    public class Test
    {
        Worker _worker;
        Action _action;
        EventHandler<WorkerEventArgs> _onCompelted;
        public Test(Worker worker,Action action, EventHandler<WorkerEventArgs> onCompelted)
        {
            _worker = worker;
            _action = action;
            _onCompelted = onCompelted;
        }

        public void AsyncCallBack(IAsyncResult asyncResult)
        {
            var t = _worker;
            WorkerEventArgs e = null;
            try
            {
                _action.EndInvoke(asyncResult);
            }
            catch (ThreadAbortException ex)
            {
                e = new WorkerEventArgs() { Status = WorkerStatus.Cancel, Message = "异步操作被取消" };
            }

                if (null != _onCompelted)
                {
                _onCompelted.Invoke(this, e);
                }
        }
    }
}
