using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyHelperForms
{
    class HotkeyHandler
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        const int START_HOTKEY_ID = 1;
        const int STOP_HOTKEY_ID = 2;

        private int modifier;
        private int key;
        private IntPtr hWnd;
        public int id { get; }
        

        public HotkeyHandler(int modifier, Keys key, Form form)
        {
            this.modifier = modifier;
            this.key = (int)key;
            hWnd = form.Handle;
            id = GetHashCode();
        }
        public override int GetHashCode()
        {
            return modifier ^ key ^ hWnd.ToInt32();
        }
        public void Register()
        {
            bool result = RegisterHotKey(hWnd, id, modifier, key);
            if (!result) throw new Exception(key.ToString() + " could not be registered");
        }
        public void UnRegister()
        {
            bool result = UnregisterHotKey(hWnd, id);
            if (!result) throw new Exception(key.ToString() + " could not be unregistered");
        }
    }
    public static class Constants
        {
            public const int NOMOD = 0x0000;
            public const int ALT = 0x0001;
            public const int CTRL = 0x0002;
            public const int SHIFT = 0x0004;
            public const int WIN = 0x0008;
            public const int WM_HOTKEY_MSG_ID = 0x0312;
        }
}
