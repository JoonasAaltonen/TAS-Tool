using TasTool.Handlers;
using TasTool.Interfaces;
using TasTool.Track;
using Timer = System.Timers.Timer;

namespace TasUi
{
    public class CommandHandler
    {
        private Timer timer;
        public bool Running;
        private KeyboardHandler keyboardHandler;

        public CommandHandler(MainWindow mainWindow, IInitializer tasInitializer, KeyboardHandlerTypes enabledKeyboardHandlerType)
        {
            if (enabledKeyboardHandlerType == KeyboardHandlerTypes.WinFormsKeyboardHandler)
            {
                keyboardHandler = new WinformsKeyboardHandler();
            }
            else
            {
                keyboardHandler = new WpfKeyboardHandler();
            }

        }

        public void StartRun(TrackCommands commands)
        {
            int index = 0;

            timer = new Timer(commands.Timing);
            timer.Start();
            Running = true;
            timer.Elapsed += (sender, args) => keyboardHandler.ReadNextCommands(commands, ref index);
        }

        public void StopRun()
        {
            timer.Stop();
            Running = false;
        }
    }
}