using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using TasTool.ConfigElements;
using TasTool.Handlers;
using TasTool.Interfaces;

namespace TasTool
{
    public class TasConfig : ITasConfig
    {
        public Dictionary<string, string> AvailableTracks { get; set; }
        public List<TasGameConfigElement> AvailableGames { get; set; }
        private List<KeyboardHandlerConfigElement> availableKeyboardHandlers { get; set; }
        public KeyboardHandlerTypes EnabledKeyboardHandlerType { get; private set; }


        public TasConfig()
        {
            AvailableGames = new List<TasGameConfigElement>();
            AvailableTracks = new Dictionary<string, string>();
            availableKeyboardHandlers = new List<KeyboardHandlerConfigElement>();
            ReadConfigFile();

            EnabledKeyboardHandlerType = GetEnabledKeyboardHandler(availableKeyboardHandlers);
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

                foreach (KeyboardHandlerConfigElement configElement in tasRunner.KeyboardHandlerCollection)
                {
                    availableKeyboardHandlers.Add(configElement);
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
                select new {lpClassName = game.LpClassName, windowCaption = game.WindowCaption};

            try
            {
                var firstElement = gameDetails.First();
                return (firstElement.lpClassName, firstElement.windowCaption);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public KeyboardHandlerTypes GetEnabledKeyboardHandler(List<KeyboardHandlerConfigElement> availableKeyboardHandlers)
        {
            // More sick LinQ
            var keyboardHandler = from handlers in availableKeyboardHandlers
                where handlers.Enabled == true
                select new { name = handlers.Name};
            try
            {
                var firstElement = keyboardHandler.First();
                string enabledHandlerName = firstElement.name;
                return (KeyboardHandlerTypes) Enum.Parse(typeof(KeyboardHandlerTypes), enabledHandlerName, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
        }
    }
}