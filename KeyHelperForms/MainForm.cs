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


        private void MainForm_Load(object sender, EventArgs e)
        {
            loadProcessList();
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
            listView1.Clear();
            RAMReader();
            loadProcessList();
        }
        private bool DoPIDExistInListView(string pid)
        {
            //ListView structure -> 0 : name, 1 : pid, 2 : char 
            foreach(ListViewItem item in listView1.Items)
            {
                if (item.SubItems[1].ToString().Equals(pid)){
                    return true;
                }
            }
            return false;
        }
    }
}
