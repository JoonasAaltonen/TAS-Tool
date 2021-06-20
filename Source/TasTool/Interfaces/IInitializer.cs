using TasTool.ConfigElements;
using TasTool.Track;

namespace TasTool.Interfaces
{
    public interface IInitializer
    {
        void Initialize(string gameName, string trackFilePath);

        ITasConfig Config { get; }
        TrackData TrackData { get; }
        string DebugMessage { get; set; }
        bool InitSuccessful { get; }
    }
}