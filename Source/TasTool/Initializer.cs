using System;
using TasTool.Handlers;
using TasTool.Interfaces;
using TasTool.Track;

namespace TasTool
{
    public class Initializer : IInitializer
    {
        public ITasConfig Config { get; }
        public TrackCommands TrackCommands { get; private set; }
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
            var gameWindowDetails = Config.GetGameWindowDetails(gameName);
            
            InitSuccessful = windowHandler.WindowFoundAndActivated(gameWindowDetails, out string debugMessage);
            DebugMessage = ConcatDebugMessage(debugMessage);

            TrackCommands = trackParser.ParseTrack(trackFilePath, out debugMessage);
            DebugMessage = ConcatDebugMessage(debugMessage);
        }

        private string ConcatDebugMessage(string nextMessage)
        {
            return string.Concat(DebugMessage, nextMessage, " | ");
            
        }

    }
}