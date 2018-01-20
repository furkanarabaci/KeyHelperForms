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
        ProcessHandler processHelper; //Will read various addresses and save it here.
        public List<bool> CheckState { get; set; }
        Process ClientProcess { get; }
        public int ProcessId { get; } // This is just for ease of access
        public bool StartState { get; set; }
        public string CharacterName { get; private set; }
        public Character(Process paramProcess) : base(paramProcess)
        {
            CheckState = Enumerable.Repeat(false, 10).ToList();
            StartState = false;
            ClientProcess = paramProcess; //Gonna bind to the process
            ProcessId = paramProcess.Id;
            processHelper = new ProcessHandler();
            CharacterName = processHelper.ReadStringAddress(paramProcess, Variables.Addresses.characterName);
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
    }
}
