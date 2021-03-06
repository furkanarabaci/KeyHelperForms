﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace KeyHelperForms
{

    public partial class MainForm : Form
    {
        CharacterHandler characterHelper;
        ProcessThread processThread;
        NotifyIcon notifyIcon = new NotifyIcon();
        HotkeyHandler startHandler;
        HotkeyHandler stopHandler;
        int selectedIndex = -1; //Will hold the selected item, we need to do work accordingly. Invalid at default. Multiselect is disabled.
        bool isBalloonShown = false;
        public MainForm()
        {
            InitializeComponent();
            characterHelper = new CharacterHandler();
            Resize += ImportStatusForm_Resize;

            notifyIcon.DoubleClick += notifyIcon_MouseDoubleClick;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.BalloonTipTitle = "Minimize to tray";
            notifyIcon.BalloonTipText = "You can still access application by doubleclicking this icon";
            notifyIcon.Icon = Icon;

            startHandler = new HotkeyHandler(Constants.NOMOD, Keys.F5, this);
            stopHandler = new HotkeyHandler(Constants.NOMOD, Keys.F6, this);
        }
        #region start/stop keyhelper
        private void StartSinglePress(Character currentCharacter)
        {
            if (!currentCharacter.StartState)
            {
                currentCharacter.StartPressing();
            }
        }
        private void StopSinglePress(Character currentCharacter)
        {
            if (currentCharacter.StartState)
            {
                currentCharacter.StopPressing();
            }
            
        }
        private void StartAllPress()
        {
            foreach (ListViewItem currentItem in listView_Characters.Items)
            {
                currentItem.SubItems[1].Text = Variables.Texts.stateStart;
            }
            foreach (Character indexCharacter in characterHelper.Characters)
            {
                StartSinglePress(indexCharacter);
            }
            DeactivateCheckBoxes();
            DeactivateNumerics();
        }
        private void StopAllPress()
        {
            foreach (ListViewItem currentItem in listView_Characters.Items)
            {
                currentItem.SubItems[1].Text = Variables.Texts.stateStop;
            }
            foreach (Character indexCharacter in characterHelper.Characters)
            {
                StopSinglePress(indexCharacter);
            }
            ActivateCheckBoxes();
            ActivateNumerics();
        }
        #endregion
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
            {
                if(m.WParam.ToInt32() == startHandler.id)
                {
                    StartAllPress();         
                }
                if (m.WParam.ToInt32() == stopHandler.id)
                {
                    StopAllPress();
                }
            }
            base.WndProc(ref m);
        }
        private void notifyIcon_MouseDoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            notifyIcon.Visible = false; // TODO: Move the notifyIcon handler to somewhere else.
        }

        private void ImportStatusForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && !isBalloonShown)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(3000);
                ShowInTaskbar = false;
                isBalloonShown = true;
            }
        }

        private void GeneralEventInvoke() //ONLY CALL THIS WITH EVENTS
        {
            if (selectedIndex == -1) //Meaning user clicked to an empty space, deselecting everything.
            {
                DeactivateCheckBoxes();
                DeactivateButtons();
            }
            else //Open up configurations
            {
                Character selectedCharacter = characterHelper.Characters[selectedIndex];
                if (selectedCharacter.StartState)
                {
                    DeactivateCheckBoxes();
                }
                else
                {
                    ActivateCheckBoxes();
                }
                ActivateButtons();
                if (selectedCharacter.HiddenState)
                {
                    btn_hideShow.Text = Variables.Texts.show;
                }
                else
                {
                    btn_hideShow.Text = Variables.Texts.hide;
                }
            }
        }
        #region MainForm and submethods
        private void MainForm_Load(object sender, EventArgs e)
        {
            processThread = new ProcessThread(RefreshListView); //It starts itself.
            DeactivateCheckBoxes(); //No selected items, so disabled.
            DeactivateButtons();
            DeactivateNumerics();
            try
            {
                startHandler.Register();
                stopHandler.Register();
            }
            catch(Exception ex)
            {
                // TODO: Do stuff with exception
            }
            
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //I had trouble with restoring hidden clients, so here i am.
            characterHelper.ShowEveryClient();
            try
            {
                startHandler.UnRegister();
                stopHandler.UnRegister();
            }
            catch (Exception ex)
            {
                // TODO: Do stuff with exception
            }
        }
        #endregion

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
                characterHelper.Characters[selectedIndex].KeyThreads[index].IsActive = true;
            }
            else
            {
                characterHelper.Characters[selectedIndex].KeyThreads[index].IsActive = false;
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
                checkBox_key1.Checked = characterHelper.Characters[selectedIndex].KeyThreads[0].IsActive;
                checkBox_key2.Checked = characterHelper.Characters[selectedIndex].KeyThreads[1].IsActive;
                checkBox_key3.Checked = characterHelper.Characters[selectedIndex].KeyThreads[2].IsActive;
                checkBox_key4.Checked = characterHelper.Characters[selectedIndex].KeyThreads[3].IsActive;
                checkBox_key5.Checked = characterHelper.Characters[selectedIndex].KeyThreads[4].IsActive;
                checkBox_key6.Checked = characterHelper.Characters[selectedIndex].KeyThreads[5].IsActive;
                checkBox_key7.Checked = characterHelper.Characters[selectedIndex].KeyThreads[6].IsActive;
                checkBox_key8.Checked = characterHelper.Characters[selectedIndex].KeyThreads[7].IsActive;
                checkBox_key9.Checked = characterHelper.Characters[selectedIndex].KeyThreads[8].IsActive;
                checkBox_key0.Checked = characterHelper.Characters[selectedIndex].KeyThreads[9].IsActive;
            }
        }
        #endregion

        #region ListBox and submethods
        private void RefreshListView() //The method which thread executes, so value this.
        {
            //TODO : There could be a better way. Now it works.
            if (listView_Characters.InvokeRequired)
            {
                listView_Characters.Invoke((MethodInvoker)delegate ()
                {
                    RefreshListView();
                });
            }
            else
            {
                
                bool didProcessChange = characterHelper.RenewAndCheckForChange(ProcessHandler.GetRelativeProcesses());
                if (didProcessChange)
                {
                    listView_Characters.Items.Clear(); //My way of refreshing.
                    AddRowsToList();
                }
                else
                {
                    //Seems nothing has been changed.
                }
            }

        }
        private void AddRowsToList()
        {
            //ListView is strictly binded to characterHelper.Characters, so we always refresh the list when we see a change.
            foreach (Character characterToAdd in characterHelper.Characters)
            {
                ListViewItem item = new ListViewItem
                {
                    Text = characterToAdd.CharacterName //First column refers to text, not subitems.
                };
                item.SubItems.Add(Variables.Texts.stateStop); //Stopped at default.
                listView_Characters.Items.Add(item);
            }

        }
        private void listView_Characters_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeIndex();
            ChangeCheckBoxes();
            ChangeNumerics();
            GeneralEventInvoke();
        } 
        private void ChangeIndex() //Submethod for listview, only for simplification purposes.
        {
            if (listView_Characters.SelectedItems.Count > 0)
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
             * Again, i do SelectedIems[0] without hesitation, because i made MultiSelect false
             */
            ChangeIndex(); // We are double clicking, so it will always have one index selected.
            Character currentCharacter = characterHelper.Characters[selectedIndex];
            if (currentCharacter.StartState) //Means if it is working BEFORE it is clicked.
            {
                listView_Characters.SelectedItems[0].SubItems[1].Text = Variables.Texts.stateStop;
                StopSinglePress(currentCharacter);
                ActivateCheckBoxes();
                ActivateNumerics();
            }
            else
            {
                listView_Characters.SelectedItems[0].SubItems[1].Text = Variables.Texts.stateStart;
                StartSinglePress(currentCharacter);
                DeactivateCheckBoxes();
                DeactivateNumerics();
            }
        }
        #endregion

        #region Button and submethods
        private void btn_hideShow_Click(object sender, EventArgs e)
        {
            if(selectedIndex == -1)
            {
                return; //Don't click hide button if you didnt select a thing.
            }
            try
            {
                Character selectedCharacter = characterHelper.Characters[selectedIndex];
                selectedCharacter.HideShowClient();
                GeneralEventInvoke();
            }
            catch(IndexOutOfRangeException)
            {

            }
        }
        private void DeactivateButtons()
        {
            btn_hideShow.Enabled = false;
        }
        private void ActivateButtons()
        {
            btn_hideShow.Enabled = true;
        }
        #endregion

        #region NumericUpDown and submethods
        public void ChangeNumerics()
        {
            //We selected a character, so we must enable the numerics.
            if(selectedIndex == -1)
            {
                ResetNumerics();
            }
            else
            {
                if (!characterHelper.Characters[selectedIndex].StartState)
                {
                    ActivateNumerics(); // Only activate if our selected character is not started to press.
                }
                numericUpDown_key1.Value = characterHelper.Characters[selectedIndex].KeyThreads[0].DelayTime;
                numericUpDown_key2.Value = characterHelper.Characters[selectedIndex].KeyThreads[1].DelayTime;
                numericUpDown_key3.Value = characterHelper.Characters[selectedIndex].KeyThreads[2].DelayTime;
                numericUpDown_key4.Value = characterHelper.Characters[selectedIndex].KeyThreads[3].DelayTime;
                numericUpDown_key5.Value = characterHelper.Characters[selectedIndex].KeyThreads[4].DelayTime;
                numericUpDown_key6.Value = characterHelper.Characters[selectedIndex].KeyThreads[5].DelayTime;
                numericUpDown_key7.Value = characterHelper.Characters[selectedIndex].KeyThreads[6].DelayTime;
                numericUpDown_key8.Value = characterHelper.Characters[selectedIndex].KeyThreads[7].DelayTime;
                numericUpDown_key9.Value = characterHelper.Characters[selectedIndex].KeyThreads[8].DelayTime;
                numericUpDown_key0.Value = characterHelper.Characters[selectedIndex].KeyThreads[9].DelayTime;
            }
        }
        public void DeactivateNumerics()
        {
            numericUpDown_key0.Enabled = false;
            numericUpDown_key1.Enabled = false;
            numericUpDown_key2.Enabled = false;
            numericUpDown_key3.Enabled = false;
            numericUpDown_key4.Enabled = false;
            numericUpDown_key5.Enabled = false;
            numericUpDown_key6.Enabled = false;
            numericUpDown_key7.Enabled = false;
            numericUpDown_key8.Enabled = false;
            numericUpDown_key9.Enabled = false;
        }
        public void ActivateNumerics()
        {
            numericUpDown_key0.Enabled = true;
            numericUpDown_key1.Enabled = true;
            numericUpDown_key2.Enabled = true;
            numericUpDown_key3.Enabled = true;
            numericUpDown_key4.Enabled = true;
            numericUpDown_key5.Enabled = true;
            numericUpDown_key6.Enabled = true;
            numericUpDown_key7.Enabled = true;
            numericUpDown_key8.Enabled = true;
            numericUpDown_key9.Enabled = true;
        }
        public void ResetNumerics()
        {
            //Means we clicked to an empty space, so also disable the user from changing it.
            DeactivateNumerics();
            numericUpDown_key0.Value = 1;
            numericUpDown_key1.Value = 1;
            numericUpDown_key2.Value = 1;
            numericUpDown_key3.Value = 1;
            numericUpDown_key4.Value = 1;
            numericUpDown_key5.Value = 1;
            numericUpDown_key6.Value = 1;
            numericUpDown_key7.Value = 1;
            numericUpDown_key8.Value = 1;
            numericUpDown_key9.Value = 1;
        }
        private void NumericUpDownValueChangedHandle(NumericUpDown paramUpDown,int index)
        {
            if(selectedIndex > -1)
            {
                characterHelper.Characters[selectedIndex].KeyThreads[index].DelayTime = Convert.ToInt32(paramUpDown.Value);
            }
        }
        private void numericUpDown_key1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDownValueChangedHandle(numericUpDown_key1, 0);
        }

        private void numericUpDown_key2_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDownValueChangedHandle(numericUpDown_key2, 1);
        }

        private void numericUpDown_key3_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDownValueChangedHandle(numericUpDown_key3, 2);
        }

        private void numericUpDown_key4_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDownValueChangedHandle(numericUpDown_key4, 3);
        }

        private void numericUpDown_key5_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDownValueChangedHandle(numericUpDown_key5, 4);
        }

        private void numericUpDown_key6_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDownValueChangedHandle(numericUpDown_key6, 5);
        }

        private void numericUpDown_key7_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDownValueChangedHandle(numericUpDown_key7, 6);
        }

        private void numericUpDown_key8_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDownValueChangedHandle(numericUpDown_key8, 7);
        }

        private void numericUpDown_key9_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDownValueChangedHandle(numericUpDown_key9, 8);
        }

        private void numericUpDown_key0_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDownValueChangedHandle(numericUpDown_key0, 9);
        }
        #endregion
    }
}
