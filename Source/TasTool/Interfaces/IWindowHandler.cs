namespace TasTool.Interfaces
{
    public interface IWindowHandler
    {
        bool WindowFoundAndActivated((string lpClassName, string windowCaption) windowProperties, out string message);
    }
}