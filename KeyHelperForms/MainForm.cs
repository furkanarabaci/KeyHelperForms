using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace KeyHelperForms
{
    public partial class MainForm : Form
    {
        KeyThread threadHelper;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            threadHelper = new KeyThread();
        }

        private void button_StartStop_Click(object sender, EventArgs e)
        {
            if (threadHelper.isThreadWorking())
            {
                button_StartStop.Text = "Stop";
            }
            else
            {
                button_StartStop.Text = "Start";
                //Thread will stop itself.
            }
        }
        
    }
}
