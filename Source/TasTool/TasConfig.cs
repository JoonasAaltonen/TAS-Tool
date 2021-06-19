using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using TasTool.ConfigElements;
using TasTool.Interfaces;

namespace TasTool
{
    public class TasConfig : ITasConfig
    {
        public Dictionary<string, string> AvailableTracks { get; set; }
        public List<TasGameConfigElement> AvailableGames { get; set; }
        public List<KeyboardHandlerConfigElement> AvailableKeyboardHandlers { get; set; }

        public TasConfig()
        {
            AvailableGames = new List<TasGameConfigElement>();
            AvailableTracks = new Dictionary<string, string>();
            AvailableKeyboardHandlers = new List<KeyboardHandlerConfigElement>();
            ReadConfigFile();
        }

        private void ReadConfigFile()
        {
            TasRunnerSection tasRunner = ConfigurationManager.GetSection("TasRunnerSection") as TasRunnerSection;
            string trackFolderPath = ConfigurationManager.AppSettings["trackJsonLocation"];

            if (tasRunner != null && !string.IsNullOrEmpty(trackFolderPath))
            {
                foreach (TasGameConfigElement tasGameConfigElement in tasRunner.TasGameCollection)
                {
                    AvailableGames.Add(tasGameConfigElement);
                }

                string[] trackFiles = Directory.GetFiles(trackFolderPath);
                foreach (string trackFilePath in trackFiles)
                {
                    AvailableTracks.Add(Path.GetFileName(trackFilePath), trackFilePath);
                }

            }
            else
            {
                throw new ArgumentOutOfRangeException("Configuration section", "Unable to load config section 'TasRunnerSection' or read AppSettings key-value pair for 'trackJsonLocation'"); 
            }
        }

        public (string LpClassName, string WindowCaption) GetGameWindowDetails(string gameName)
        {
            // Sick LinQ : https://docs.microsoft.com/en-us/dotnet/csharp/linq/query-a-collection-of-objects
            var gameDetails = from game in AvailableGames
                where game.Name == gameName
                select new {LpClassName = game.LpClassName, WindowCaption = game.WindowCaption};

            try
            {
                var firstElement = gameDetails.First();
                return (firstElement.LpClassName, firstElement.WindowCaption);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}