using CsvHelper.Configuration;
using TasTool.Interfaces;
using TasTool.Track;

namespace TasTool.InputRecording
{
    public sealed class InputMapper : ClassMap<CommandData>, IInputMapper
    {
        public InputMapper()
        {
            Map(m => m.AbsoluteTime).Name("AbsoluteTime");
            Map(m => m.DeltaTime).Name("DeltaTime");
            Map(m => m.KeyCode).Name("KeyCode");
            Map(m => m.Action).Name("Action");
        }
        
    }
}