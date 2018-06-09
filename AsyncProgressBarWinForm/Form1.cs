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

namespace AsyncProgressBarWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            var progress = new Progress<int>(val =>
            {
                pgbStatus.Value = val;
                lblStatus.Text = val.ToString() + "%";
            });

            LongRunningTask(progress);

        }


        private void LongRunningTask(IProgress<int> progress)
        {
            Task.Run(() =>
            {
                RunJob(progress);

                return true;
            });
            return;
        }

        private void RunJob(IProgress<int> progress)
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(100);
                if (progress != null)
                    progress.Report(i);
            }
        }
    }
}
