using System;
using System.Runtime.InteropServices;

namespace TasTool.Handlers
{
    public class WindowHandler : IWindowHandler
    {

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName,
            string windowCaption);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        // Get a handle to the application. The window class and window name were obtained using the Spy++ tool.
        // Setup your game window, (lpClassName, windowCaption)
        public Tuple<string, string> Trials = new Tuple<string, string>("MT_APPLICATION", "Trials 2 - Second Edition");
        public Tuple<string, string> Calculator = new Tuple<string, string>("ApplicationFrameWindow", "Calculator");
        public Tuple<string, string> HeaveHo = new Tuple<string, string>("UnityWndClass", "HeaveHo");
        public Tuple<string, string> DrunkenWrestlers = new Tuple<string, string>("UnityWndClass", "Drunken Wrestlers 2");
        public Tuple<string, string> RichardBurnsRally = new Tuple<string, string>("D3D Window", "Richard Burns Rally - DirectX9");

        public IntPtr FindAndActivateWindow(Tuple<string, string> windowProperties, out string message)
        {
            IntPtr gameHandle = FindWindow(windowProperties.Item1, windowProperties.Item2);

            // Verify that window was found.
            if (gameHandle == IntPtr.Zero)
            {
                message = $"Window with params {windowProperties.Item1}, {windowProperties.Item2} not found";
            }
            else
            {
                message = $"Window found with params {windowProperties.Item1}, {windowProperties.Item2}";
            }
            
            SetForegroundWindow(gameHandle);
            return gameHandle;

        }
    }
}