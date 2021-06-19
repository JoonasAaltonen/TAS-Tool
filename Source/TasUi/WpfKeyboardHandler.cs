using System;
using System.Runtime.InteropServices;
using System.Windows.Input;
using TasTool.Track;

namespace TasUi
{
    public class WpfKeyboardHandler : KeyboardHandler
    {
        public static bool WIsPressed = false;
        public static bool AIsPressed = false;
        public static bool SIsPressed = false;
        public static bool DIsPressed = false;
        
        public override void ReadNextCommands(TrackCommands commands, ref int i)
        {
            if (i >= commands.Length - 1)
            {
                StopRunning();
            }
            byte keyW = (byte)Key.Up;
            byte keyA = (byte)Key.Left;
            byte keyS = (byte)Key.Down;
            byte keyD = (byte)Key.Right;
            byte nextState;

            // handle W
            nextState = commands.UpArrowOnOff[i];
            HandleKeys(keyW, nextState);

            nextState = commands.LeftArrowOnOff[i];
            HandleKeys(keyA, nextState);

            nextState = commands.DownArrowOnOff[i];
            HandleKeys(keyS, nextState);

            nextState = commands.RightArrowOnOff[i];
            HandleKeys(keyD, nextState);

            i++;
        }
        
        public override void SetKeyStatus(byte key, bool status)
        {
            switch (key)
            {
                case (byte)Key.Up:
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
        public override bool IsKeyPressed(byte key)
        {
            switch (key)
            {
                case (byte)Key.Up:
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
        public override void StopRunning()
        {
            ReleaseKey((byte)Key.Up);
            ReleaseKey((byte)Key.Left);
            ReleaseKey((byte)Key.Down);
            ReleaseKey((byte)Key.Right);
        }
    }
}