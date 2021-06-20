using System;
using System.Collections.Generic;
using System.Windows.Input;
using TasTool.Track;


namespace TasUi.KeyboardHandlers
{
    public class WpfKeyboardHandler : KeyboardHandler
    {
        public static bool WIsPressed = false;
        public static bool AIsPressed = false;
        public static bool SIsPressed = false;
        public static bool DIsPressed = false;


        private List<(Key keyEnum, string keyName)> keysInUse = new List<(Key keyEnum, string keyName)>();

        public override void MapKeys(TrackData data)
        {
            foreach (Input input in data.CommandInputs)
            {
                MappedKeys.Add(new MappedKey((byte)(Key)Enum.Parse(typeof(Key), input.CommandKey.KeyName, true), input.CommandKey.KeyName, false));
            }
        }
    }
}