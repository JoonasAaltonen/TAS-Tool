using System.Collections.Generic;
using TasTool.ConfigElements;
using TasTool.Handlers;

namespace TasTool.Interfaces
{
    public interface ITasConfig
    {
        Dictionary<string, string> AvailableTracks { get; set; }
        List<TasGameConfigElement> AvailableGames { get; set; }
        (string LpClassName, string WindowCaption) GetGameWindowDetails(string gameName);
        public KeyboardHandlerTypes EnabledKeyboardHandlerType { get; }
    }
}