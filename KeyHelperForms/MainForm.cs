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
        CharacterHandler characterHelper;
        int selectedIndex = -1; //Will hold the selected item, we need to do work accordingly. Invalid at default. Multiselect is disabled.
        public MainForm()
        {
            InitializeComponent();
            characterHelper = new CharacterHandler();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshListView();
            DeactivateCheckBoxes(); //No selected items, so disabled.
        }
        #region CheckBoxes and submethods
        private void checkBox_key1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckState(checkBox_key1, 0);
        }

        private void checkBox_key2_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckState(checkBox_key2, 1);
        }

        private void checkBox_key3_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckState(checkBox_key3, 2);
        }

        private void checkBox_key4_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckState(checkBox_key4, 3);
        }

        private void checkBox_key5_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckState(checkBox_key5, 4);
        }

        private void checkBox_key6_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckState(checkBox_key6, 5);
        }

        private void checkBox_key7_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckState(checkBox_key7, 6);
        }
        private void checkBox_key8_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckState(checkBox_key8, 7);
        }

        private void checkBox_key9_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckState(checkBox_key9, 8);
        }

        private void checkBox_key0_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckState(checkBox_key0, 9);
        }
        private void ChangeCheckState(CheckBox currentCheckBox, int index)
        {
            if (selectedIndex == -1)
            {
                return; //Don't take an OutOfBoundException head on !
            }
            if (currentCheckBox.Checked)
            {
                characterHelper.Characters[selectedIndex].CheckState[index] = true;
            }
            else
            {
                characterHelper.Characters[selectedIndex].CheckState[index] = false;
            }
        }
        private void ActivateCheckBoxes()
        {
            checkBox_key1.Enabled = true;
            checkBox_key2.Enabled = true;
            checkBox_key3.Enabled = true;
            checkBox_key4.Enabled = true;
            checkBox_key5.Enabled = true;
            checkBox_key6.Enabled = true;
            checkBox_key7.Enabled = true;
            checkBox_key8.Enabled = true;
            checkBox_key9.Enabled = true;
            checkBox_key0.Enabled = true;

        }
        private void DeactivateCheckBoxes()
        {
            checkBox_key1.Enabled = false;
            checkBox_key2.Enabled = false;
            checkBox_key3.Enabled = false;
            checkBox_key4.Enabled = false;
            checkBox_key5.Enabled = false;
            checkBox_key6.Enabled = false;
            checkBox_key7.Enabled = false;
            checkBox_key8.Enabled = false;
            checkBox_key9.Enabled = false;
            checkBox_key0.Enabled = false;
        }
        private void ChangeCheckBoxes()
        {
            if (selectedIndex == -1)
            {
                checkBox_key1.Checked = false;
                checkBox_key2.Checked = false;
                checkBox_key3.Checked = false;
                checkBox_key4.Checked = false;
                checkBox_key5.Checked = false;
                checkBox_key6.Checked = false;
                checkBox_key7.Checked = false;
                checkBox_key8.Checked = false;
                checkBox_key9.Checked = false;
                checkBox_key0.Checked = false;
            }
            else
            {
                checkBox_key1.Checked = characterHelper.Characters[selectedIndex].CheckState[0];
                checkBox_key2.Checked = characterHelper.Characters[selectedIndex].CheckState[1];
                checkBox_key3.Checked = characterHelper.Characters[selectedIndex].CheckState[2];
                checkBox_key4.Checked = characterHelper.Characters[selectedIndex].CheckState[3];
                checkBox_key5.Checked = characterHelper.Characters[selectedIndex].CheckState[4];
                checkBox_key6.Checked = characterHelper.Characters[selectedIndex].CheckState[5];
                checkBox_key7.Checked = characterHelper.Characters[selectedIndex].CheckState[6];
                checkBox_key8.Checked = characterHelper.Characters[selectedIndex].CheckState[7];
                checkBox_key9.Checked = characterHelper.Characters[selectedIndex].CheckState[8];
                checkBox_key0.Checked = characterHelper.Characters[selectedIndex].CheckState[9];
            }
        }
        #endregion
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //TODO : This button is pointless, remove it and check for processes initially.
            listView_Characters.Items.Clear(); //Also works as refresh.
            RefreshListView();
        }
        #region ListBox and submethods
        private void RefreshListView()
        {
            /* ListView structure -> 0 : charName | 1 : state */
            //TODO : Here is too ugly, simplify it and move it elsewhere.
            List<Process> processList = ProcessHandler.GetRelativeProcesses();
            foreach (Process process in processList)
            {
                AddNewRow(process);
            }
        }
        private void AddNewRow(Process processToBind)
        {
            //Adds a new row to listview and to the characterHandler object.
            characterHelper.AddCharacter(processToBind);
            int characterIndex = characterHelper.Characters.Count - 1; //We know that our current process is last one added.
            ListViewItem item = new ListViewItem
            {
                Text = characterHelper.Characters[characterIndex].CharacterName, //First column refers to text, not subitems.
            };
            item.SubItems.Add(Variables.Texts.stateStop); //Stopped at default.
            listView_Characters.Items.Add(item);
        }
        private void RemoveRow(Character characterToDelete) 
        {
            //Removes the given row from characterHandler object. No need to delete it from listview, we are refreshing anyways.
            //Listview items are binded to characters, that's why i am taking the object to make things easier.
            characterHelper.RemoveCharacter(characterToDelete);
        }
        private void listView_Characters_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeIndex();
            ChangeCheckBoxes();
            if (selectedIndex == -1) //Meaning user clicked to empty space, deselecting everything.
            {
                DeactivateCheckBoxes();
                
            }
            else
            {
                if (characterHelper.Characters[selectedIndex].StartState)
                {
                    DeactivateCheckBoxes();
                }
                else
                {
                    ActivateCheckBoxes();
                }
            }      
        }
        private void ChangeIndex() //Submethod for listview, only for simplification purposes.
        {
            if(listView_Characters.SelectedItems.Count > 0)
            {
                selectedIndex = listView_Characters.SelectedItems[0].Index;
            }
            else
            {
                selectedIndex = -1;
            }
        }
        private void listView_Characters_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /* ListView structure -> 0 : charName | 1 : state */
            /** 
             * Again, i do SelectedIems[0] without hesitation, cuz i made MultiSelect false
             */
            ChangeIndex(); // We are double clicking, so it will always have one index selected.
            if (characterHelper.Characters[selectedIndex].StartState) //Means if it is working BEFORE it is clicked.
            {
                //So we will stop pressing here
                characterHelper.Characters[selectedIndex].StopPressing();
                listView_Characters.SelectedItems[0].SubItems[1].Text = Variables.Texts.stateStop;
                ActivateCheckBoxes();
            }
            else
            {
                //We will start pressing here
                characterHelper.Characters[selectedIndex].StartPressing();
                listView_Characters.SelectedItems[0].SubItems[1].Text = Variables.Texts.stateStart;
                DeactivateCheckBoxes();  
            }
        }
        #endregion
    }
}
