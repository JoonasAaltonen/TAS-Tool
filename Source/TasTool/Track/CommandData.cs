using CsvHelper.Configuration.Attributes;
using TasTool.InputRecording;

namespace TasTool.Track
{
    public class CommandData
    {
        [Name("AbsoluteTime")]
        public long AbsoluteTime { get; set; }

        [Name("DeltaTime")]
        public long DeltaTime { get; set; }

        [Name("KeyCode")]
        public string KeyCode { get; set; }

        [Name("Action")]
        public KeyAction Action { get; set; }
    }
}