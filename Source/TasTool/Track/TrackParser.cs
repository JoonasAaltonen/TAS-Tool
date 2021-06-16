using System;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using TasTool.Interfaces;

namespace TasTool.Track
{
    public class TrackParser : ITrackParser
    {
        private string ReadJson(string trackName)
        {
            string path = String.Concat(ConfigurationSettings.AppSettings["trackJsonLocation"], trackName);
            StreamReader reader = new StreamReader(path);
            string contents = reader.ReadToEnd();
            reader.Close();
            return contents;
        }
        public TrackCommands ParseTrack(string trackName)
        {
            string trackData = ReadJson(trackName);

            return JsonConvert.DeserializeObject<TrackCommands>(trackData);
        }
    }
}