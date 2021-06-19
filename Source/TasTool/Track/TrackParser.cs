using System;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using TasTool.Interfaces;

namespace TasTool.Track
{
    public class TrackParser : ITrackParser
    {
        private string ReadJson(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string contents = reader.ReadToEnd();
                reader.Close();
                return contents;
            }
        }
        public TrackCommands ParseTrack(string filePath, out string debugMessage)
        {
            try
            {
                string trackData = ReadJson(filePath);
                TrackCommands parsedCommands = JsonConvert.DeserializeObject<TrackCommands>(trackData);
                debugMessage = "Track command JSON successfully parsed.";
                return parsedCommands;
            }
            catch (Exception e)
            {
                debugMessage = "Exception in parsing track command JSON";
                throw e;
            }

        }
    }
}