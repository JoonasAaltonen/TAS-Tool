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
        private Stopwatch stopwatch;
        public bool Running = false;
        private KeyboardHandlerTypes enabledKeyboardHandler;
        private KeyboardHandler keyboardHandler;
        private MainWindow mainWindow;
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;

        public CommandHandler(MainWindow mainWindow, ITasMediator tasMediator, KeyboardHandlerTypes enabledKeyboardHandlerType)
        {
            enabledKeyboardHandler = enabledKeyboardHandlerType;
            this.mainWindow = mainWindow;
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

        public async Task StartRun(List<CommandData> trackData)
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
            Running = true;
            keyboardHandler = GetKeyboardHandler(enabledKeyboardHandler);
            keyboardHandler.MapKeys(trackData);

            mainWindow.InvokeTextBoxValueChange(mainWindow.RunnerTextBox, "Running");

            stopwatch = new Stopwatch();
            stopwatch.Start();

            int index = 0;
            foreach (CommandData commandData in trackData)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                try
                {
                    MappedKey selectedKey = keyboardHandler.MappedKeys.Single(k => k.KeyName == commandData.KeyCode);
                    mainWindow.InvokeTextBoxValueChange(mainWindow.CommandTextBox, index.ToString());
                    mainWindow.InvokeTextBoxValueChange(mainWindow.ElapsedTimeTextBox, stopwatch.ElapsedMilliseconds.ToString());
                    index++;

                    await HandleNextInput((int)commandData.DeltaTime, selectedKey.Key, (byte)commandData.Action);
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
            // When command data csv file has been read and executed
            StopRun();
        }

        async Task HandleNextInput(int deltaTime, byte key, byte nextState)
        {
            await Task.Delay(deltaTime);
            keyboardHandler.HandleKeys(key, nextState);
        }

        public void StopRun()
        {
            cancellationTokenSource.Cancel();
            Running = false;
            keyboardHandler.ReleaseAllKeys();
            mainWindow.InvokeTextBoxValueChange(mainWindow.RunnerTextBox, "Stopped");
        }

        public void TestHolding()
        {
            keyboardHandler = GetKeyboardHandler(enabledKeyboardHandler);
            keyboardHandler.HoldKey(24);
        }
    }
}