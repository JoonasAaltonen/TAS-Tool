using System.Linq;
using System.Runtime.CompilerServices;
using TasTool.Handlers;
using TasTool.Interfaces;
using TasTool.Track;
using TasUi.KeyboardHandlers;
using Timer = System.Timers.Timer;

namespace TasUi
{
    public class CommandHandler
    {
        private Timer timer;
        public bool Running = false;
        private KeyboardHandlerTypes enabledKeyboardHandler;
        private KeyboardHandler keyboardHandler;

        public CommandHandler(MainWindow mainWindow, IInitializer tasInitializer, KeyboardHandlerTypes enabledKeyboardHandlerType)
        {
            this.enabledKeyboardHandler = enabledKeyboardHandlerType;
        }

        public void StartRun(TrackData data)
        {
            int index = 0;
            keyboardHandler = GetKeyboardHandler(enabledKeyboardHandler);
            keyboardHandler.MapKeys(data);

            timer = new Timer(data.Timing);
            timer.Start();
            Running = true;
            timer.Elapsed += (sender, args) => ReadNextInputs(data, ref index);
        }

        public void StopRun()
        {
            if (Running)
            {
                timer.Stop();
                Running = false;
                keyboardHandler.ReleaseAllKeys(); 
            }
        }

        private void ReadNextInputs(TrackData data, ref int index)
        {
            if (index >= data.Length)
            {
                StopRun();
                return;
            }
            foreach (Input input in data.CommandInputs)
            {
                byte nextState = input.CommandKey.OnOffPattern[index];
                MappedKey selectedKey = keyboardHandler.MappedKeys.Single(k => k.KeyName == input.CommandKey.KeyName);

                keyboardHandler.HandleKeys(selectedKey.Key, nextState);
            }

            index++;
        }

        private KeyboardHandler GetKeyboardHandler(KeyboardHandlerTypes enabledKeyboardHandlerType)
        {
            if (enabledKeyboardHandlerType == KeyboardHandlerTypes.WinFormsKeyboardHandler)
            {
                return new WinformsKeyboardHandler();
            }
            else
            {
                return new WpfKeyboardHandler();
            }

        }
    }
}