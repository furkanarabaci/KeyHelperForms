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
    class KeyThread
    {
        Thread pressThread;
        bool state = false; //This will be changed by Start() and Stop()
        private int key;

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        public KeyThread(int pKey)
        {
            this.key = pKey;
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
                Process[] processes = Process.GetProcessesByName(Variables.processName);
                foreach (Process proc in processes)
                {
                    PostMessage(proc.MainWindowHandle, Variables.WM_KEYDOWN, key, 0);
                    Thread.Sleep(Variables.sleepTime);
                }
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
