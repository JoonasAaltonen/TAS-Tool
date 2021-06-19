using System.Collections.Generic;
using TasTool.ConfigElements;

namespace TasTool.Interfaces
{
    public interface ITasConfig
    {
        Dictionary<string, string> AvailableTracks { get; set; }
        List<TasGameConfigElement> AvailableGames { get; set; }
        List<KeyboardHandlerConfigElement> AvailableKeyboardHandlers { get; set; }
        (string LpClassName, string WindowCaption) GetGameWindowDetails(string gameName);
    }
}