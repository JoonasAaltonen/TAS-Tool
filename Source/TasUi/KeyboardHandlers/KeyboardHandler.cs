using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Documents;
using TasTool.Track;

namespace TasUi.KeyboardHandlers
{
    public abstract class KeyboardHandler
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        const int KEY_DOWN_EVENT = 0x0001; //Key down flag
        const int KEY_UP_EVENT = 0x0002; //Key up flag

        public List<MappedKey> MappedKeys;

        public KeyboardHandler()
        {
            MappedKeys = new List<MappedKey>();
        }
        
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

        public void ReleaseAllKeys()
        {
            foreach (MappedKey mappedKey in MappedKeys)
            {
                ReleaseKey(mappedKey.Key);
            }
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

        public bool IsKeyPressed(byte key)
        {
            var a = MappedKeys.Single(k => k.Key == key).IsPressed;
            return a;
        }

        public void SetKeyStatus(byte key, bool status)
        {
            foreach (MappedKey mappedKey in MappedKeys.Where(k => k.Key == key))
            {
                mappedKey.IsPressed = status;
            }
        }
        
        public abstract void MapKeys(List<CommandData> data);
    }
}