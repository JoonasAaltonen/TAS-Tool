using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace Botti
{
    public class KeyboardHandler
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        const int KEY_DOWN_EVENT = 0x0001; //Key down flag
        const int KEY_UP_EVENT = 0x0002; //Key up flag

        public static bool wIsPressed = false;
        public static bool aIsPressed = false;
        public static bool sIsPressed = false;
        public static bool dIsPressed = false;

        public void HoldKey(byte key)
        {
            keybd_event(key, 045, KEY_DOWN_EVENT, 0);
            SetKeyStatus(key, true);
        }

        public void ReleaseKey(byte key)
        {
            keybd_event(key, 0, KEY_UP_EVENT, 0);
            SetKeyStatus(key, false);
        }

        private void SetKeyStatus(byte key, bool status)
        {
            switch (key)
            {
                case (byte)Key.W:
                    wIsPressed = status;
                    break;
                case (byte)Key.A:
                    aIsPressed = status;
                    break;
                case (byte)Key.S:
                    sIsPressed = status;
                    break;
                case (byte)Key.D:
                    dIsPressed = status;
                    break;
                default:
                    Console.WriteLine("Unexpected key pressed");
                    break;
            }
        }
        public bool IsKeyPressed(byte key)
        {
            switch (key)
            {
                case (byte)Key.W:
                    return wIsPressed;
                case (byte)Key.A:
                    return aIsPressed;
                case (byte)Key.S:
                    return sIsPressed;
                case (byte)Key.D:
                    return dIsPressed;
                default:
                    Console.WriteLine("Key not acceptable");
                    throw new ArgumentOutOfRangeException("Key not acceptable");
            }
        }
    }
}