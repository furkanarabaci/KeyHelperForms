using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyHelperForms
{
    class Character
    {
        //TODO : Add things like HP and MP, they are trivial but somehow needed.
        public List<KeyThread> KeyThreads { get; private set; } //Starts from 1 and ends with 0
        public Process ClientProcess { get; } //Will be set initially and will be bound until the end.
        public bool StartState { get; private set; }
        public bool HiddenState { get; private set; }
        public string CharacterName { get; private set; }
        public Character(Process paramProcess)
        {
            ClientProcess = paramProcess; //Gonna bind to the process
            KeyThreads = new List<KeyThread>();
            foreach (int keys in Variables.KeyList())
            {
                KeyThreads.Add(new KeyThread(keys, paramProcess));
            }
            DefaultValues();
            RefreshCharacterValues();
            CheckHiddenState();
        }
        public void StartPressing()
        {
            StartState = true;
            foreach(KeyThread currentKey in KeyThreads)
            {
                currentKey.Start(); //Will ignore non-active keys.
            }
        }
        public void StopPressing()
        {
            StartState = false;
            foreach (KeyThread currentKey in KeyThreads)
            {
                currentKey.Stop();
            }
        }
        public void RefreshCharacterValues()
        {
            string previousName = CharacterName;
            CharacterName = ProcessHandler.ReadStringAddress(ClientProcess, Variables.Addresses.characterName); //Listview will ignore \0, so no need to replace
            if(previousName != null && !previousName.Equals(CharacterName))
            {
                //Means the user restarted the client and logged in to a different client.
                DefaultValues();
            }
        }
        public void DefaultValues()
        {
            foreach(KeyThread currentKey in KeyThreads)
            {
                currentKey.RenewValues(); //Set things to initial state.
            }
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
