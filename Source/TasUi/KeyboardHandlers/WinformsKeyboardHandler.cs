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
    


        public override void MapKeys(TrackData data)
        {
            foreach (Input input in data.CommandInputs)
            {
                MappedKeys.Add(new MappedKey((byte)(Keys)Enum.Parse(typeof(Keys), input.CommandKey.KeyName, true), input.CommandKey.KeyName, false));
            }
        }
    }
}