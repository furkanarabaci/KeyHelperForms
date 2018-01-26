using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyHelperForms
{
    class KeyThread //Base class which is responsible for single key press on a single process.
    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        private int keyToPress;
        private Process targetProcess;
        private bool working; //This will be changed by Start() and Stop()
        public Thread PressThread { get; private set; }
        public bool IsActive { get; set; } //This will be set only from outside ( from checkbox selected )
        public int DelayTime { get; set; } //Should be minimum 1. Will be set from outside
        public KeyThread(int pKey, Process paramProcess)
        {
            keyToPress = pKey;
            targetProcess = paramProcess;
            RenewValues();
            RenewThread();
        }
        private void RenewThread()
        {
            PressThread = new Thread(new ThreadStart(MainThread))
            {
                IsBackground = true
            };
        }
        private void MainThread()
        {
            while (true)
            {
                PostMessage(targetProcess.MainWindowHandle, Variables.WM_KEYDOWN, keyToPress, 0);
                Thread.Sleep(DelayTime);
                if (!working)
                {
                    break;
                }
            }
        }
        public void Start()
        {
            if (!working && IsActive)
            {
                RenewThread();
                PressThread.Start();
                working = true;
            }         
        }
        public void Stop()
        {
            if (working)
            {
                PressThread.Abort();
                working = false;
            }      
        }
        public void RenewValues()
        {
            working = false;
            IsActive = false;
            DelayTime = Variables.DefaultKeyDelay;
        }
    }
}
