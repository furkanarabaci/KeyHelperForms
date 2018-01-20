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
        ProcessHandler processHelper; //Will read various addresses and save it here.
        public List<bool> CheckState { get; private set; }
        Process ClientProcess { get; }
        public int ProcessId { get; } // This is just for ease of access
        public bool StartState { get; private set; }
        public string CharacterName { get; private set; }
        public Character(Process paramProcess) : base(paramProcess)
        {
            CheckState = Enumerable.Repeat(false, 10).ToList();
            StartState = false;
            ClientProcess = paramProcess; //Gonna bind to the process
            ProcessId = paramProcess.Id;
            processHelper = new ProcessHandler();
            ReadFromProcess();
        }
        private void ReadFromProcess()
        {
            CharacterName = processHelper.ReadStringAddress(ClientProcess, Variables.Addresses.characterName);
            //TODO : HP MP or other various changes will be added here later.
        }
        public void StartPressing()
        {
            StartState = false;
            ChangeChecks(CheckState); //KeyThreadArray function
            StartAll(); //KeyThreadArray function

        }
        public void StopPressing()
        {
            StartState = true;
            StopAll(); //KeyThreadArray function
        }
    }
}
