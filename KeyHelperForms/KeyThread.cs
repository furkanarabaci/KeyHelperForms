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
        Thread pressThread;
        Process targetProcess;
        bool state = false; //This will be changed by Start() and Stop()
        private int keyToPress;

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        public KeyThread(int pKey, Process paramProcess)
        {
            keyToPress = pKey;
            targetProcess = paramProcess;
            RenewThread();
        }
        private void RenewThread()
        {
            pressThread = new Thread(new ThreadStart(MainThread))
            {
                IsBackground = true
            };
        }
        private void MainThread()
        {
            while (true)
            {
                PostMessage(targetProcess.MainWindowHandle, Variables.WM_KEYDOWN, keyToPress, 0);
                Thread.Sleep(Variables.sleepTime);
                if (!state)
                {
                    break;
                }
            }
        }
        public bool IsThreadWorking()
        {
            return pressThread.IsAlive;
        }
        public void Start()
        {
            if (!state)
            {
                RenewThread();
                pressThread.Start();
            }
            state = true;
        }
        public void Stop()
        {
            if (state)
            {
                pressThread.Abort();
            }
            state = false;
        }
    }
}
