using System;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Botti
{
    public class Runner
    {
        private Form1 mainView;
        private WindowHandler handler;
        private KeyboardHandler _keyboardHandler;
        private TrialsRunner trialsRunner;
        public Runner(Form1 mainView)
        {
            this.mainView = mainView;
            this.handler = new WindowHandler();
            this._keyboardHandler = new KeyboardHandler();
            trialsRunner = new TrialsRunner(mainView);
        }

        public void Run()
        {
            string chosenGame = System.Configuration.ConfigurationSettings.AppSettings["selectedGame"].ToUpper();
            switch (chosenGame)
            {
                case "TRIALS":
                    RunTrials();
                    break;
                case "HEAVEHO":
                    RunHeaveHo();
                    break;
                case "DRUNKENWRESTLERS":
                    RunDrunkenWrestlers();
                    break;
                case "RICHARDBURNSRALLY":
                    RunRichardBurnsRally();
                    break;
                case "CALCULATOR":
                    RunCalculator();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown game set");
            }
        }

        public void Stop()
        {
            StopTrials();
            mainView.DebugMessage("Running stopped");
        }

        private void RunCalculator()
        {
            IntPtr gameHandle = handler.FindAndActivateWindow(handler.Calculator, out string message);
            mainView.DebugMessage(message);
            if (gameHandle != IntPtr.Zero)
            {
                SendKeys.SendWait("{1}");
                SendKeys.SendWait("{+}");
                SendKeys.SendWait("{1}");
                SendKeys.SendWait("{ENTER}");
            }
        }

        private void RunTrials()
        {
            IntPtr gameHandle = GetGameHandle(handler.Trials); ;
            if (gameHandle != IntPtr.Zero)
            {
                // Press reset once and start the run
                SendKeys.SendWait("{R}");
                System.Threading.Thread.Sleep(3000);
                trialsRunner.RunTrials();
            }
        }

        private void RunHeaveHo()
        {
            IntPtr gameHandle = GetGameHandle(handler.HeaveHo);
            if (gameHandle != IntPtr.Zero)
            {
                // Press reset once and start the run
                trialsRunner.RunTrials();
            }
        }

        private void RunDrunkenWrestlers()
        {
            IntPtr gameHandle = GetGameHandle(handler.DrunkenWrestlers);
            if (gameHandle != IntPtr.Zero)
            {
                // Press reset once and start the run
                trialsRunner.RunTrials();
            }
        }

        private void RunRichardBurnsRally()
        {
            IntPtr gameHandle = GetGameHandle(handler.RichardBurnsRally);
            if (gameHandle != IntPtr.Zero)
            {
                // Press reset once and start the run
                trialsRunner.RunTrials();
            }
        }

        private IntPtr GetGameHandle(Tuple<string, string> gameWindow)
        {
            IntPtr gameHandle = handler.FindAndActivateWindow(gameWindow, out string message);
            Console.WriteLine(message);
            mainView.DebugMessage(message);
            return gameHandle;
        }

        private void StopTrials()
        {
            trialsRunner.StopRun();
        }
    }
}