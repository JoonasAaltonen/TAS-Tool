using TasTool.InputRecording;

namespace TasTool.Interfaces
{
    public interface IInputHandler
    {
        void StopInputRecording();
        void RecordInput(string keyCode, KeyAction action);
    }
}