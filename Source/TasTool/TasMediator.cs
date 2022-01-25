using System;
using System.Collections.Generic;
using TasTool.ConfigElements;
using TasTool.Handlers;
using TasTool.InputRecording;
using TasTool.Interfaces;
using TasTool.Track;

namespace TasTool
{
    public class TasMediator : ITasMediator
    {
        public ITasConfig Config { get; }
        public IInputHandler InputHandler { get; }
        public List<CommandData> CommandData { get; private set; }
        public string DebugMessage { get; set; }
        public bool InitSuccessful { get; private set; }

        private ITrackParser trackParser;
        private readonly IWindowHandler windowHandler;

        public TasMediator(ITasConfig config, IInputHandler inputHandler, IWindowHandler windowHandler, ITrackParser trackParser)
        {
            Config = config;
            InputHandler = inputHandler;
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

            //TrackData = trackParserJson.ParseTrack(trackFilePath, out debugMessage, out bool success);
            CommandData = trackParser.ParseTrack(trackFilePath, out debugMessage, out bool success);
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