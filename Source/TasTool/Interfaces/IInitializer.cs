using TasTool.Track;

namespace TasTool.Interfaces
{
    public interface IInitializer
    {
        void Initialize(string gameName, string trackFilePath);

        ITasConfig Config { get; }
        TrackCommands TrackCommands { get; }
        string DebugMessage { get; set; }
        bool InitSuccessful { get; }
    }
}