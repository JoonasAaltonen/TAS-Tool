using System.Configuration;

namespace TasTool.ConfigElements
{
    // Custom type for handling games in the config
    public class TasGame
    {
        [ConfigurationProperty("Name")]
        public string Name { get; }
        [ConfigurationProperty("LpClassName")]
        public string LpClassName { get; }
        [ConfigurationProperty("WindowCaption")]
        public string WindowCaption { get; }
    }
}