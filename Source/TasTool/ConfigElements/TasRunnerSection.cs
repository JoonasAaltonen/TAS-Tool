using System.Configuration;

namespace TasTool.ConfigElements
{
    public class TasRunnerSection : ConfigurationSection
    {
        [ConfigurationProperty("TasGameCollection", IsDefaultCollection = true)]
        public TasGameCollection TasGameCollection 
        {
            get
            {
                TasGameCollection gamesCollection = (TasGameCollection) base["TasGameCollection"];
                return gamesCollection;
            }

        }

        [ConfigurationProperty("KeyboardHandlerCollection", IsDefaultCollection = true)]
        public KeyboardHandlerCollection KeyboardHandlerCollection
        {
            get
            {
                KeyboardHandlerCollection keyboardCollection = (KeyboardHandlerCollection)base["KeyboardHandlerCollection"];
                return keyboardCollection;
            }

        }

    }

    
}