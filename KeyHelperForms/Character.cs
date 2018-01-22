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
            CheckState = Enumerable.Repeat(false, 10).ToList();
            StartState = false;
            ClientProcess = paramProcess; //Gonna bind to the process
            CharacterName = ProcessHandler.ReadStringAddress(paramProcess, Variables.Addresses.characterName);
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
