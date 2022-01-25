using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Documents;
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
        private Stopwatch stopwatch;
        public bool Running = false;
        private KeyboardHandlerTypes enabledKeyboardHandler;
        private KeyboardHandler keyboardHandler;
        private MainWindow mainWindow;

        public CommandHandler(MainWindow mainWindow, ITasMediator tasMediator, KeyboardHandlerTypes enabledKeyboardHandlerType)
        {
            enabledKeyboardHandler = enabledKeyboardHandlerType;
            this.mainWindow = mainWindow;
        }

        public void StartRun(TrackDataJson data)
        {
            int index = 0;
            keyboardHandler = GetKeyboardHandler(enabledKeyboardHandler);
            keyboardHandler.MapKeysJson(data);

            timer = new Timer(data.Timing);
            timer.Start();
            Running = true;
            timer.Elapsed += (sender, args) => ReadNextInputs(data, ref index);

            stopwatch = new Stopwatch();
            stopwatch.Start();

            mainWindow.InvokeTextBoxValueChange(mainWindow.RunnerTextBox, "Running");
        }

        public void StopRun()
        {
            if (Running)
            {
                timer.Stop();
                stopwatch.Stop();
                Running = false;
                keyboardHandler.ReleaseAllKeys();
                mainWindow.InvokeTextBoxValueChange(mainWindow.RunnerTextBox, "Stopped");
            }
        }

        private void ReadNextInputs(TrackDataJson data, ref int index)
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

            mainWindow.InvokeTextBoxValueChange(mainWindow.CommandTextBox, index.ToString());
            mainWindow.InvokeTextBoxValueChange(mainWindow.ElapsedTimeTextBox, stopwatch.ElapsedMilliseconds.ToString());

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

        public async Task RunCsvInputs(List<CommandData> trackData)
        {
            keyboardHandler = GetKeyboardHandler(enabledKeyboardHandler);
            keyboardHandler.MapKeysCsv(trackData);

            foreach (CommandData commandData in trackData)
            {
                try
                {
                    MappedKey selectedKey = keyboardHandler.MappedKeys.Single(k => k.KeyName == commandData.KeyCode);
                    await HandleNextInput((int)commandData.DeltaTime, selectedKey.Key, (byte)commandData.Action);
                }
                catch (Exception)
                {

                    throw;
                }
                
            }

            StopCsvRun();
        }

        async Task HandleNextInput(int deltaTime, byte key, byte nextState)
        {
            await Task.Delay(deltaTime);
            keyboardHandler.HandleKeys(key, nextState);
        }

        public void StopCsvRun()
        {
            keyboardHandler.ReleaseAllKeys();
            mainWindow.InvokeTextBoxValueChange(mainWindow.RunnerTextBox, "Stopped");
        }
    }
}