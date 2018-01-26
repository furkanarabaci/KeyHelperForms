using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyHelperForms
{
    class Variables
    {
        public const string processName = "PVO_Client";
        public const UInt32 WM_KEYDOWN = 0x0100;
        public const int PROCESS_WM_READ = 0x0010;  //Memory read and some magic.
        struct Keys
        {
            public const int VK_KEY_0 = 0x30;
            public const int VK_KEY_1 = 0x31;
            public const int VK_KEY_2 = 0x32;
            public const int VK_KEY_3 = 0x33;
            public const int VK_KEY_4 = 0x34;
            public const int VK_KEY_5 = 0x35;
            public const int VK_KEY_6 = 0x36;
            public const int VK_KEY_7 = 0x37;
            public const int VK_KEY_8 = 0x38;
            public const int VK_KEY_9 = 0x39;
        }
        public struct Addresses
        {
            public const int characterName = 0x010916BF;
            public const int hpMaximum = 0x00E70DD4;
            public const int mpMaximum = 0x00E70DD8;
        }
        public struct WindowHandles
        {
            public const int hide = 0;
            public const int show = 5;
            public const int restore = 9;
        }
        public static int DefaultKeyDelay = 1; //In seconds.

        public static int processThreadSleep = 5000; //Sleep time for process refreshing thread in miliseconds.

        public const int intBufferSize = 24;
        public const int stringBufferSize = 12;
        public const string nullTerminator = "\0";
        public struct Texts
        {
            public static string start = "Start";
            public static string stop = "Stop";
            public static string stateStart = "Started";
            public static string stateStop = "Stopped";
            public static string hide = "Hide";
            public static string show = "Show";
        }
        
        public static List<int> KeyList() //Starts from 1, ends with 0
        {
            return new List<int>
            {
                Keys.VK_KEY_1,
                Keys.VK_KEY_2,
                Keys.VK_KEY_3,
                Keys.VK_KEY_4,
                Keys.VK_KEY_5,
                Keys.VK_KEY_6,
                Keys.VK_KEY_7,
                Keys.VK_KEY_8,
                Keys.VK_KEY_9,
                Keys.VK_KEY_0
            };
        }
    }
}
