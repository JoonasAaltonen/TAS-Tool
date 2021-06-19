using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TasTool.Track;

namespace TasUi
{
    public class WinformsKeyboardHandler : KeyboardHandler
    {
        public static bool WIsPressed = false;
        public static bool AIsPressed = false;
        public static bool SIsPressed = false;
        public static bool DIsPressed = false;

        public override void ReadNextCommands(TrackCommands commands, ref int i)
        {
            if (i == commands.Length - 1)
            {
                StopRunning();
            }
            byte keyW = (byte)Keys.Up;
            byte keyA = (byte)Keys.Left;
            byte keyS = (byte)Keys.Down;
            byte keyD = (byte)Keys.Right;
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
                case (byte)Keys.Up:
                    WIsPressed = status;
                    break;
                case (byte)Keys.Left:
                    AIsPressed = status;
                    break;
                case (byte)Keys.Down:
                    SIsPressed = status;
                    break;
                case (byte)Keys.Right:
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
                case (byte)Keys.Up:
                    return WIsPressed;
                case (byte)Keys.Left:
                    return AIsPressed;
                case (byte)Keys.Down:
                    return SIsPressed;
                case (byte)Keys.Right:
                    return DIsPressed;
                default:
                    Console.WriteLine("Key not acceptable");
                    throw new ArgumentOutOfRangeException("Key not acceptable");
            }
        }
        public override void StopRunning()
        {
            ReleaseKey((byte)Keys.Up);
            ReleaseKey((byte)Keys.Left);
            ReleaseKey((byte)Keys.Down);
            ReleaseKey((byte)Keys.Right);
        }
    }
}