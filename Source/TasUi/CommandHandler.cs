using System.Windows.Forms;
using System.Windows.Input;
using TasTool.Interfaces;
using TasTool.Track;
using WinformsEnabler;
using Timer = System.Timers.Timer;

namespace TasUi
{
    public class CommandHandler
    {
        private Timer timer;
        public bool Running;
        //private KeyboardHandler keyboardHandler;
        private WinformsKeyboardHandler keyboardHandler;
        public CommandHandler(MainWindow mainWindow, IInitializer tasInitializer, KeyboardHandlerTypes keyboardHandlerTypeType)
        {
            //keyboardHandler = new KeyboardHandler();
            keyboardHandler = new WinformsKeyboardHandler();
            
        }

        public void StartRun(TrackCommands commands)
        {
            int index = 0;

            timer = new Timer(commands.Timing);
            timer.Start();
            Running = true;
            timer.Elapsed += (sender, args) => ReadNextCommands(commands, ref index);
        }

        private void ReadNextCommands(TrackCommands commands, ref int i)
        {
            if (i == commands.Length - 1)
            {
                StopRun();
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

        public void HandleKeys(byte key, byte nextState)
        {
            if (nextState == 1 && keyboardHandler.IsKeyPressed(key) == false)
            {
                keyboardHandler.HoldKey(key);
            }
            if (nextState == 0 && keyboardHandler.IsKeyPressed(key) == true)
            {
                keyboardHandler.ReleaseKey(key);
            }
        }

        private void CheckOrSetKeyStatus(byte key)
        {

        }

        public void StopRun()
        {
            timer.Stop();
            keyboardHandler.ReleaseKey((byte)Key.D4);
            keyboardHandler.ReleaseKey((byte)Key.A);
            keyboardHandler.ReleaseKey((byte)Key.S);
            keyboardHandler.ReleaseKey((byte)Key.D);
            Running = false;
        }
    }
}