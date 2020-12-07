using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAP_Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Worker worker;
        private void btnStart_Click(object sender, EventArgs e)
        {
            worker = new Worker();
            worker.OnWorkCompleted += WorkOver;
            worker.WorkAsync();
            Console.WriteLine(string.Format("线程：{0}", Thread.CurrentThread.ManagedThreadId));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            worker.CancelAsync();
        }
        private void WorkOver(object sender, Worker.WorkerEventArgs e)
        {
            if (null != e)
            {
                if (Worker.WorkerStatus.Cancel == e.Status)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                Console.WriteLine(string.Format("线程：{0}，委托回调完成.", Thread.CurrentThread.ManagedThreadId));
            }
        }
    }
}
