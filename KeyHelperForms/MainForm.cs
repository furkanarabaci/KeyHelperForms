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
        bool startState = false;
        KeyThread threadHelper;
        KeyThread threadHelper2;
        public MainForm()
        {
            InitializeComponent();
            threadHelper = new KeyThread(Variables.VK_KEY_1);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button_StartStop_Click(object sender, EventArgs e)
        {
            threadHelper.Start();
            if (!startState)
            {
                button_StartStop.Text = "Stop";
                startState = true;
            }
            else
            {
                threadHelper.Stop();
                button_StartStop.Text = "Start";
                startState = false;
            }
        }
        
    }
}
