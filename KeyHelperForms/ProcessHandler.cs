using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyHelperForms
{
    class ProcessHandler
    {
        public ProcessHandler()
        {

        }

        //Memory read and some magic.
        const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
        int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        private string ReadAddress(Process process, int addressValue)
        {
            //Address value in hexadecimal. The variable address points can vary, so use it at your own risk.
            //Process process = Process.GetProcessesByName(Variables.processName)[0];
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

            int bytesRead = 0;
            byte[] buffer = new byte[Variables.bufferSize];

            ReadProcessMemory((int)processHandle, addressValue, buffer, buffer.Length, ref bytesRead);

            return Encoding.ASCII.GetString(buffer);
        }

        private static List<Process> GetProcesses()
        {
            return Process.GetProcesses();

            foreach (Process process in processList)
            {
                ListViewItem item = new ListViewItem();

                if (process.ProcessName.ToString() == Variables.processName)
                {
                    string currentPid = process.Id.ToString();
                    item.SubItems.Add(process.ProcessName);
                    item.Text = currentPid;
                    item.Tag = process;
                    if (!DoPIDExistInListView(currentPid))
                    {
                        listView1.Items.Add(item);
                    }
                }
            }
        }

    }
}
