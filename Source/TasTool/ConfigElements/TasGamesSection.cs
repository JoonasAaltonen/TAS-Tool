using System.Configuration;

namespace TasTool.ConfigElements
{
    public class TasGamesSection : ConfigurationSection
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
    }

    
}