using System;

namespace TasTool.Handlers
{
    public interface IWindowHandler
    {
        IntPtr FindAndActivateWindow(Tuple<string, string> windowProperties, out string message);
    }
}