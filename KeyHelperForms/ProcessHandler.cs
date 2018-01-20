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
        //Memory read and some magic.
        const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
        int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        public string ReadAddress(Process process, int addressValue, Object dataType)
        {
            //Address value in hexadecimal. The variable address points can vary, so use it at your own risk.
            //dataType requires a value, you can pass anything as a parameter.
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

            int bytesRead = 0;
            byte[] buffer = new byte[Variables.bufferSize];

            ReadProcessMemory((int)processHandle, addressValue, buffer, buffer.Length, ref bytesRead);
            if (dataType.GetType() == typeof(int))
            {
                return BitConverter.ToInt32(buffer, 0).ToString();
            }
            return Encoding.UTF8.GetString(buffer); // Return string as default.
       }
        public List<Process> GetProcesses()
        {
            return Process.GetProcesses().ToList();
        }

    }
}
