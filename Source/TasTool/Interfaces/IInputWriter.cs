using System.Collections.Generic;
using TasTool.InputRecording;
using TasTool.Track;

namespace TasTool.Interfaces
{
    public interface IInputWriter
    {
        void WriteDataToFile(List<CommandData> inputList);
    }
}