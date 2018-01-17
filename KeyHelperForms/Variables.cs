using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyHelperForms
{
    class Variables
    {
        public static string processName = "PVO_Client";
        public const UInt32 WM_KEYDOWN = 0x0100;

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

        public static int sleepTime = 1000;
        public const int bufferSize = 24; //Buffer for various things. 

        public const int characterNameAddress = 0x010916BF;
        public const int hpAddress = 0x00E70DD4;
        public const int mpAddress = 0x00E70DD8;
        public static List<int> KeyList() //Starts from 1, ends with 0
        {
            return new List<int>
            {
                VK_KEY_1,
                VK_KEY_2,
                VK_KEY_3,
                VK_KEY_4,
                VK_KEY_5,
                VK_KEY_6,
                VK_KEY_7,
                VK_KEY_8,
                VK_KEY_9,
                VK_KEY_0
            };
        }
    }
}
