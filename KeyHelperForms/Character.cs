using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyHelperForms
{
    class Character : KeyThreadArray
    {
        //TODO : Add things like HP and MP, they are trivial but somehow needed.

        public List<bool> CheckState { get; set; }
        public List<int> KeyDelays { get; set; }
        public Process ClientProcess { get; }
        public bool StartState { get; private set; }
        public bool HiddenState { get; private set; }
        public string CharacterName { get; private set; }
        public Character(Process paramProcess) : base(paramProcess)
        {
            ClientProcess = paramProcess; //Gonna bind to the process
            RefreshProcess();
            RefreshCharacterValues();
            CheckHiddenState();
        }
        public void StartPressing()
        {
            StartState = true;
            ChangeChecks(CheckState); //KeyThreadArray function
            StartAll(); //KeyThreadArray function
        }
        public void StopPressing()
        {
            StartState = false;
            StopAll(); //KeyThreadArray function
        }
        public void RefreshCharacterValues()
        {
            string previousName = CharacterName;
            CharacterName = ProcessHandler.ReadStringAddress(ClientProcess, Variables.Addresses.characterName); //Listview will ignore \0, so no need to replace
            if(previousName != null && !previousName.Equals(CharacterName))
            {
                //Means the user restarted the client and logged in to a different client.
                RefreshProcess();
            }
        }
        private void RefreshProcess()
        {
            //Will occur when the user restarts and logs in to a different char, then uses keyhelper again. If so, reset everything to initial state.
            CheckState = Enumerable.Repeat(false, 10).ToList();
            KeyDelays = Enumerable.Repeat(1, 10).ToList(); //Our default key delay is 1 seconds.
            StartState = false;
        }
        public void HideShowClient()
        {
            if (HiddenState)
            {
                //Means windows is hidden. show it.
                ShowClient();
            }
            else
            {
                HideClient();
            }
        }
        public void ShowClient()
        {
            ProcessHandler.ChangeProcessState(ClientProcess.MainWindowHandle, Variables.WindowHandles.show);
            HiddenState = false;
        }
        public void HideClient()
        {
            ProcessHandler.ChangeProcessState(ClientProcess.MainWindowHandle, Variables.WindowHandles.hide);
            HiddenState = true;
        }
        private void CheckHiddenState()
        {
            //Checks the state of client. Does not change anything.
            ClientProcess.Refresh();
            if (!ClientProcess.MainWindowHandle.Equals(IntPtr.Zero))
            {
                //Means the client is not hidden, save the window handle and proceed.
                HiddenState = false;
                return;
            }
            else
            {
                //TODO : Find a method to restore hidden window without a window handle.
                HiddenState = true;
            }
            
        }
    }
}
