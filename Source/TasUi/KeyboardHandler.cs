using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TasTool.Track;

namespace TasUi
{
    public abstract class KeyboardHandler
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        const int KEY_DOWN_EVENT = 0x0001; //Key down flag
        const int KEY_UP_EVENT = 0x0002; //Key up flag
        
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

        public void HandleKeys(byte key, byte nextState)
        {
            if (nextState == 1 && IsKeyPressed(key) == false)
            {
                HoldKey(key);
            }
            if (nextState == 0 && IsKeyPressed(key) == true)
            {
                ReleaseKey(key);
            }
        }

        public abstract void ReadNextCommands(TrackCommands commands, ref int i);

        public abstract void SetKeyStatus(byte key, bool status);

        public abstract bool IsKeyPressed(byte key);

        public abstract void StopRunning();
    }
}