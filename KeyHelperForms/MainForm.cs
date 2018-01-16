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
       //Memory read and some magic.
        const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
        int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);


        //Keypress things.
        bool startState = false;
        List<bool> checkState;
        KeyThreadArray threadHelperArray;


        public MainForm()
        {

            InitializeComponent();
            checkState = new List<bool>();
            for (int i = 0; i < 10; i++)
            {
                checkState.Add(false); //I am too lazy to use LINQ.
            }
            threadHelperArray = new KeyThreadArray();
        }

        private void RAMReader()
        {


            Process[] processList = Process.GetProcesses();

            foreach (Process process in processList)
            {
                ListViewItem item = new ListViewItem();

                if (process.ProcessName.ToString() == "PVO_Client")
                {
                    IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

                    int bytesRead = 0;
                    byte[] buffer = new byte[1024];

                    ReadProcessMemory((int)processHandle, 0x010916BF, buffer, buffer.Length, ref bytesRead);


                    item.SubItems.Add(process.ProcessName);
                    item.Text = process.Id.ToString();
                    item.SubItems.Add(Encoding.ASCII.GetString(buffer));
                    item.Tag = process;
                    listView1.Items.Add(item);

                }
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
        RAMReader();
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
        }

        private void button_StartStop_Click(object sender, EventArgs e)
        {
            threadHelperArray.ChangeChecks(checkState);
            threadHelperArray.StartAll();
            if (!startState)
            {
                button_StartStop.Text = "Stop";
                startState = true;
            }
            else
            {
                threadHelperArray.StopAll();
                button_StartStop.Text = "Start";
                startState = false;
            }
        }
        private void ChangeState(CheckBox currentCheckBox, int index)
        {
            if (currentCheckBox.Checked)
            {
                checkState[index] = true;
            }
            else
            {
                checkState[index] = false;
            }
        }
        private void checkBox_key1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key1, 0);
        }

        private void checkBox_key2_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key2, 1);
        }

        private void checkBox_key3_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key3, 2);
        }

        private void checkBox_key4_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key4, 3);
        }

        private void checkBox_key5_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key5, 4);
        }

        private void checkBox_key6_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key6, 5);
        }

        private void checkBox_key7_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key7, 6);
        }
        private void checkBox_key8_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key8, 7);
        }

        private void checkBox_key9_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key9, 8);
        }

        private void checkBox_key0_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState(checkBox_key0, 9);
        }

        private void btnOffset_Click(object sender, EventArgs e)
        {
            RAMReader();
           
        }

    }
}
