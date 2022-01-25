using TasTool.Track;

namespace TasTool.Interfaces
{
    public interface ITrackParserJson
    {
        TrackDataJson ParseTrack(string trackName, out string debugMessage, out bool success);
    }
}