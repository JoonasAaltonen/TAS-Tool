using TasTool.Track;

namespace TasTool.Interfaces
{
    public interface ITrackParser
    {
        TrackCommands ParseTrack(string trackName, out string debugMessage);
    }
}