using System.Collections.Generic;
using TasTool.ConfigElements;
using TasTool.Track;

namespace TasTool.Interfaces
{
    public interface ITasMediator
    {
        void Initialize(string gameName, string trackFilePath);

        ITasConfig Config { get; }
        IInputHandler InputHandler { get; }
        TrackDataJson TrackData { get; }
        List<CommandData> CommandData { get; }
        string DebugMessage { get; set; }
        bool InitSuccessful { get; }
    }
}