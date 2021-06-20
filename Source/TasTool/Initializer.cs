using System;
using System.Collections.Generic;
using TasTool.ConfigElements;
using TasTool.Handlers;
using TasTool.Interfaces;
using TasTool.Track;

namespace TasTool
{
    public class Initializer : IInitializer
    {
        public ITasConfig Config { get; }
        public TrackData TrackData { get; private set; }
        public string DebugMessage { get; set; }
        public bool InitSuccessful { get; private set; }

        private ITrackParser trackParser;
        private readonly IWindowHandler windowHandler;

        public Initializer(ITasConfig config, IWindowHandler windowHandler, ITrackParser trackParser)
        {
            Config = config;
            this.windowHandler = windowHandler;
            this.trackParser = trackParser;
            DebugMessage = "";
        }

        public void Initialize(string gameName, string trackFilePath)
        {
            DebugMessage = "";
            List<bool> initSuccess = new List<bool>();
            var gameWindowDetails = Config.GetGameWindowDetails(gameName);
            
            initSuccess.Add(windowHandler.WindowFoundAndActivated(gameWindowDetails, out string debugMessage));
            DebugMessage = ConcatDebugMessage(debugMessage);

            TrackData = trackParser.ParseTrack(trackFilePath, out debugMessage, out bool success);
            DebugMessage = ConcatDebugMessage(debugMessage);
            initSuccess.Add(success);

            if (initSuccess.Contains(false))
            {
                InitSuccessful = false;
            }
            else
            {
                InitSuccessful = true;
            }

        }

        private string ConcatDebugMessage(string nextMessage)
        {
            return string.Concat(DebugMessage, nextMessage, " | ");
            
        }
    }
}