using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Forms;
using TasTool.Track;

namespace TasUi.KeyboardHandlers
{
    public class WinformsKeyboardHandler : KeyboardHandler
    {
        private List<(Keys keyEnum, string keyName, bool isPressed)> keysInUse = new List<(Keys keyEnum, string keyName, bool isPressed)>();
        
        public override void MapKeys(List<CommandData> data)
        {
            List<string> keyNames = new List<string>();
            foreach (CommandData row in data)
            {
                if (keyNames.Count <= 0 || !keyNames.Exists(k => k.ToString() == row.KeyCode))
                {
                    keyNames.Add(row.KeyCode);
                }
            }

            foreach (string keyName in keyNames)
            {
                MappedKeys.Add(new MappedKey((byte)(Keys)Enum.Parse(typeof(Keys), keyName, true), keyName, false));
            }
        }
    }
}