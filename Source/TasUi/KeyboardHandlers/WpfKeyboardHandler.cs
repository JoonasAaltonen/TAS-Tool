using System;
using System.Collections.Generic;
using System.Windows.Input;
using TasTool.Track;


namespace TasUi.KeyboardHandlers
{
    public class WpfKeyboardHandler : KeyboardHandler
    {
        private List<(Key keyEnum, string keyName)> keysInUse = new List<(Key keyEnum, string keyName)>();

        public override void MapKeysJson(TrackDataJson data)
        {
            foreach (Input input in data.CommandInputs)
            {
                MappedKeys.Add(new MappedKey((byte)(Key)Enum.Parse(typeof(Key), input.CommandKey.KeyName, true), input.CommandKey.KeyName, false));
            }
        }

        public override void MapKeysCsv(List<CommandData> data)
        {
            List<string> keyNames = new List<string>();
            foreach (CommandData row in data)
            {
                if (keyNames.Count < 0 && !keyNames.Exists(k => k.Equals(row.KeyCode)))
                {
                    keyNames.Add(row.KeyCode);
                }
            }

            foreach (string keyName in keyNames)
            {
                MappedKeys.Add(new MappedKey((byte)(Key)Enum.Parse(typeof(Key), keyName, true), keyName, false));
            }
        }
        
    }
}