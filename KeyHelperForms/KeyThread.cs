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

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        public KeyThread()
        {
            pressThread = new Thread(new ThreadStart(StartWorking));
        }

        private void StartWorking()
        {
            while (pressThread.IsAlive)
            {
                Process[] processes = Process.GetProcessesByName(Variables.processName);
                foreach (Process proc in processes)
                {
                    PostMessage(proc.MainWindowHandle, Variables.WM_KEYDOWN, Variables.VK_F4, 0);
                    Thread.Sleep(Variables.sleepTime);
                }
            }
        }
        public bool isThreadWorking()
        {
            return pressThread.IsAlive;
        }
        public void Start()
        {
            pressThread.Start();
        }
    }
}
