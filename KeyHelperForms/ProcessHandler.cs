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
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        [DllImport("User32")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        public static string ReadStringAddress(Process process, int addressValue)
        {
            //Address value in hexadecimal. The variable address points can vary, so use it at your own risk.
            //dataType requires a value, you can pass anything as a parameter.
            IntPtr processHandle = OpenProcess(Variables.PROCESS_WM_READ, false, process.Id);

            int bytesRead = 0;
            byte[] buffer = new byte[Variables.stringBufferSize];

            ReadProcessMemory((int)processHandle, addressValue, buffer, buffer.Length, ref bytesRead);
            return Encoding.UTF8.GetString(buffer); // Return string as default.
        }
        public static int ReadIntAddress(Process process, int addressValue)
        {
            IntPtr processHandle = OpenProcess(Variables.PROCESS_WM_READ, false, process.Id);

            int bytesRead = 0;
            byte[] buffer = new byte[Variables.intBufferSize];

            ReadProcessMemory((int)processHandle, addressValue, buffer, buffer.Length, ref bytesRead);

            return BitConverter.ToInt32(buffer, 0);
        }
        public static List<Process> GetRelativeProcesses()
        {
            List<Process> relativeProcesses = new List<Process>();
            Process[] processList = Process.GetProcesses(); //To avoid runtime issues.
            foreach (Process currentProcess in processList)
            {
                if (currentProcess.ProcessName.ToString().Equals(Variables.processName, StringComparison.CurrentCultureIgnoreCase)) 
                {
                    //TODO : Here is too messy, i strongly think that there would be a better approach.
                    string characterName = ReadStringAddress(currentProcess, Variables.Addresses.characterName).Replace(Variables.nullTerminator, String.Empty);
                    if (characterName.Length > 0)
                    {
                        relativeProcesses.Add(currentProcess);
                    }
                }
            }
            return relativeProcesses;
        }
        public static void ChangeProcessState(IntPtr mainWindowHandle,int state)
        {
            ShowWindow(mainWindowHandle, state);
        }
    }
}
