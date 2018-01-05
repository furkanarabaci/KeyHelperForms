using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyHelperForms
{
    static class Program
    {
        const UInt32 WM_KEYDOWN = 0x0100;
        const int VK_F5 = 0x74;

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            while (true)
            {
                Process[] processes = Process.GetProcessesByName("opera");
                foreach(Process proc in processes)
                {
                    PostMessage(proc.MainWindowHandle, WM_KEYDOWN, VK_F5, 0);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
