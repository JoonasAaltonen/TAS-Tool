using System.Collections.Generic;
using System.Diagnostics;
using TasTool.Interfaces;
using TasTool.Track;

namespace TasTool.InputRecording
{
    public class InputHandler : IInputHandler
    {
        public List<CommandData> InputDataBuffer;

        private Stopwatch absoluteTimeWatch;
        private Stopwatch deltaTimeWatch;
        private IInputWriter inputWriter;
        private bool recordingIsStarted = false;

        public InputHandler(IInputWriter inputWriter)
        {
            this.inputWriter = inputWriter;
            InputDataBuffer = new List<CommandData>();
            absoluteTimeWatch = new Stopwatch();
            deltaTimeWatch = new Stopwatch();
        }

        private void StartInputTimers()
        {
            absoluteTimeWatch.Reset();
            deltaTimeWatch.Reset();
            absoluteTimeWatch.Start();
            deltaTimeWatch.Start();
        }

        public void StopInputRecording()
        {
            absoluteTimeWatch.Stop();
            deltaTimeWatch.Stop();
            recordingIsStarted = false;
            SaveInputData(InputDataBuffer);
            InputDataBuffer.Clear();
            
        }

        public void RecordInput(string keyCode, KeyAction action)
        {
            if (!recordingIsStarted)
            {
                StartInputTimers();
                recordingIsStarted = true;
            }
            if (action == KeyAction.Up || (action == KeyAction.Down && !IsKeyHeld(keyCode, InputDataBuffer)))   // Add inputs when it's not a key being held down (causes multiple key down events)
            {
                AddInputToBuffer(keyCode, action);
            }
        }

        private void SaveInputData(List<CommandData> inputData)
        {
            inputWriter.WriteDataToFile(inputData);
        }

        private void AddInputToBuffer(string keyCode, KeyAction action)
        {
            InputDataBuffer.Add(new CommandData
            {
                AbsoluteTime = absoluteTimeWatch.ElapsedMilliseconds,
                Action = action,
                DeltaTime = deltaTimeWatch.ElapsedMilliseconds,
                KeyCode = keyCode
            });
            deltaTimeWatch.Restart();
        }

        private bool IsKeyHeld(string keyCode, List<CommandData> inputs)
        {
            if (inputs.Exists(i => i.KeyCode == keyCode) &&                             // if key has been pressed before
                inputs.FindLast(i => i.KeyCode == keyCode).Action == KeyAction.Down)    // and the last action for the said key was press down
                {
                    return true;
                }
            return false;
        }
    }
}