using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyHelperForms
{
    class Character : KeyThreadArray
    {
        //TODO : Add things like HP and MP, they are trivial but somehow needed.
        public List<bool> CheckState { get; set; }
        public Process ClientProcess { get; }
        public bool StartState { get; set; }
        public string CharacterName { get; private set; }
        public Character(Process paramProcess) : base(paramProcess)
        {
            ClientProcess = paramProcess; //Gonna bind to the process
            RefreshProcess();
            RefreshCharacterValues();
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
            CharacterName = ProcessHandler.ReadStringAddress(ClientProcess, Variables.Addresses.characterName).Replace(Variables.nullTerminator, String.Empty);
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
            StartState = false;
        }
    }
}
