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
        public List<bool> CheckState { get; set; }
        Process ClientProcess { get; }
        public bool StartState { get; set; }
        public Character(Process paramProcess) : base(paramProcess)
        {
            CheckState = Enumerable.Repeat(false, 10).ToList();
            StartState = false;
            ClientProcess = paramProcess; //Gonna bind to the process
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
