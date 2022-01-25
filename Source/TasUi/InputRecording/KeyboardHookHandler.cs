using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using TasTool;
using TasTool.InputRecording;
using TasTool.Interfaces;

namespace TasUi.InputRecording
{
    public class KeyboardHookHandler
    {
        private IKeyboardEvents globalKeyboardHook;
        private IInputHandler inputHandler;

        public KeyboardHookHandler(ITasMediator tasMediator)
        {
            inputHandler = tasMediator.InputHandler;
        }

        public void StartInputRecording()
        {
            SubscribeToKeyboardEvents();
        }

        public void StopInputRecording()
        {
            UnsubscribeToKeyboardEvents();
            inputHandler.StopInputRecording();
        }

        private void SubscribeToKeyboardEvents()
        {
            globalKeyboardHook = Hook.GlobalEvents();
            globalKeyboardHook.KeyDown += GlobalKeyboardHookOnKeyDown;
            globalKeyboardHook.KeyUp += GlobalKeyboardHookOnKeyUp;
        }

        private void UnsubscribeToKeyboardEvents()
        {
            globalKeyboardHook.KeyDown -= GlobalKeyboardHookOnKeyDown;
            globalKeyboardHook.KeyUp -= GlobalKeyboardHookOnKeyUp;

        }

        private void GlobalKeyboardHookOnKeyUp(object sender, KeyEventArgs e)
        {
            inputHandler.RecordInput(Enum.GetName(typeof(Keys), e.KeyCode), KeyAction.Up);
        }

        private void GlobalKeyboardHookOnKeyDown(object sender, KeyEventArgs e) 
        {
            inputHandler.RecordInput(Enum.GetName(typeof(Keys), e.KeyCode), KeyAction.Down);
        }
    }
}