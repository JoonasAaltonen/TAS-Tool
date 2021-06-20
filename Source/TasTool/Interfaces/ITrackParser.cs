using TasTool.Track;

namespace TasTool.Interfaces
{
    public interface ITrackParser
    {
        TrackData ParseTrack(string trackName, out string debugMessage, out bool success);
    }
}