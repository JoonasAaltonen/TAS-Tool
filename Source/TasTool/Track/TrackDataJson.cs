using System.Collections.Generic;
using Newtonsoft.Json;

namespace TasTool.Track
{
    public class TrackDataJson
    {
        [JsonProperty("Timing")]
        public int Timing { get; set; }
        
        [JsonProperty("Length")]
        public int Length { get; set; }
        
        [JsonProperty("Inputs")]
        public List<Input> CommandInputs { get; set; }
    }

    public class Input
    {
        [JsonProperty("Key")]
        public CommandKey CommandKey { get; set; }
    }

    public class CommandKey
    {
        [JsonProperty("KeyName")]
        public string KeyName { get; set; }
        
        [JsonProperty("OnOffPattern")]
        public byte[] OnOffPattern { get; set; }
    }
}