using System;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using TasTool.Interfaces;

namespace TasTool.Track
{
    public class TrackParserJson : ITrackParserJson
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

        public TrackDataJson ParseTrack(string filePath, out string debugMessage, out bool success)
        {
            string trackData = ReadJson(filePath);
            TrackDataJson parsedData = JsonConvert.DeserializeObject<TrackDataJson>(trackData);
            if (IsParsingSuccessful(parsedData))
            {
                debugMessage = "Track command JSON successfully parsed.";
                success = true;
                return parsedData;
            }
            debugMessage = "Track command JSON parsing failed or file does not include commands.";
            success = false;
            return parsedData;
        }

        private bool IsParsingSuccessful(TrackDataJson data)
        {
            if (data.CommandInputs != null && data.CommandInputs.Count > 0 )
            {
                return true;
            }

            return false;
        }
    }
}