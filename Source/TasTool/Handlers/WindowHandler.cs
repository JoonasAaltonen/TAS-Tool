using System;
using System.Runtime.InteropServices;
using TasTool.Interfaces;

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

        public bool WindowFoundAndActivated((string lpClassName, string windowCaption) windowProperties, out string message)
        {
            IntPtr gameHandle = FindWindow(windowProperties.lpClassName, windowProperties.windowCaption);

            // Verify that window was found.
            if (gameHandle == IntPtr.Zero)
            {
                message = $"Window init failed. Window with params \"{windowProperties.lpClassName}, {windowProperties.windowCaption}\" not found.";
                return false;
            }
            
            message = $"Window init successful. Window found with params \"{windowProperties.lpClassName}, {windowProperties.windowCaption}\".";
            SetForegroundWindow(gameHandle);
            return true;

        }
    }
}