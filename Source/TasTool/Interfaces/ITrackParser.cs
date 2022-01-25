using System.Collections.Generic;
using TasTool.Track;

namespace TasTool.Interfaces
{
    public interface ITrackParser
    {
        List<CommandData> ParseTrack(string filePath, out string debugMessage, out bool success);
    }
}