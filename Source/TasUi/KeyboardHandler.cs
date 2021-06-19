﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace TasUi
{
    public class KeyboardHandler
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        const int KEY_DOWN_EVENT = 0x0001; //Key down flag
        const int KEY_UP_EVENT = 0x0002; //Key up flag

        public static bool WIsPressed = false;
        public static bool AIsPressed = false;
        public static bool SIsPressed = false;
        public static bool DIsPressed = false;

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
                case (byte)Key.D4:
                    WIsPressed = status;
                    break;
                case (byte)Key.Left:
                    AIsPressed = status;
                    break;
                case (byte)Key.Down:
                    SIsPressed = status;
                    break;
                case (byte)Key.Right:
                    DIsPressed = status;
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
                case (byte)Key.D4:
                    return WIsPressed;
                case (byte)Key.Left:
                    return AIsPressed;
                case (byte)Key.Down:
                    return SIsPressed;
                case (byte)Key.Right:
                    return DIsPressed;
                default:
                    Console.WriteLine("Key not acceptable");
                    throw new ArgumentOutOfRangeException("Key not acceptable");
            }
        }
    }
}