using System;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace Botti
{
    public class TrackParser
    {
        private string ReadJson(string trackName)
        {
            string path = String.Concat(ConfigurationSettings.AppSettings["trackJsonLocation"], Form1.SelectedTrack);
            StreamReader reader = new StreamReader(path);
            string contents = reader.ReadToEnd();
            reader.Close();
            return contents;
        }
        public TrackCommands ParseTrack()
        {
            string track = ReadJson("ConstructionWork");

            return JsonConvert.DeserializeObject<TrackCommands>(track);
        }
    }
}