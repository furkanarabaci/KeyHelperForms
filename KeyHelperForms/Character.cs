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
        [DllImport("User32")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string strClassName, string strWindowName);
        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);
        //TODO : Add things like HP and MP, they are trivial but somehow needed.

        public List<bool> CheckState { get; set; }
        public Process ClientProcess { get; }
        public bool StartState { get; private set; }
        public bool HiddenState { get; private set; }
        public string CharacterName { get; private set; }
        private IntPtr processWindowHandle;
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
            StartState = false;
        }
        public void HideShowClient()
        {
            if (HiddenState)
            {
                //Means windows is hidden. show it.
                HandleWindow(Variables.WindowHandles.show);
            }
            else
            {
                HandleWindow(Variables.WindowHandles.hide);
            }
            HiddenState = !HiddenState; //Invert it cuz the state is changed.
        }
        private void HandleWindow(int state)
        {
            ShowWindow(processWindowHandle, state);
        }
        private void CheckHiddenState()
        {
            //Checks if the client is already hidden when we open the program. Also assign handle number.
            ClientProcess.Refresh();
            IntPtr tmphwnd = ClientProcess.MainWindowHandle;
            if (!tmphwnd.Equals(IntPtr.Zero))
            {
                //Means the client is not hidden, save the window handle and proceed.
                processWindowHandle = tmphwnd; //Temporary becomes permament.
                HiddenState = false;
                return;
            }
            else
            {
                //Means user started the program with client already hidden. We shall restore it !
                IntPtr currentHandle = IntPtr.Zero;
                int currentPid = 0;
                int currentTid = 0;
                do
                {
                    currentHandle = FindWindowEx(IntPtr.Zero, currentHandle, null, null); // get the window handle
                    currentTid = GetWindowThreadProcessId(currentHandle, out currentPid); // get the process id (and thread id)
                    if (currentPid == ClientProcess.Id) //Check if its our process
                    {
                        //We got our handle. Save it !
                        processWindowHandle = currentHandle;
                        HiddenState = true;
                        break;
                    }
                } while (!currentHandle.Equals(IntPtr.Zero));
            }
            
        }
    }
}
