using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Botti
{
    public class TrialsRunner
    {
        private KeyboardHandler keyboardHandler;
        private Timer timer;
        public static bool running;
        private Form1 mainView;
        public TrialsRunner(Form1 mainView)
        {
            this.keyboardHandler = new KeyboardHandler();
            this.mainView = mainView;
        }
        public void RunTrials()
        {
            TrackParser parser = new TrackParser();
            TrackCommands commands = parser.ParseTrack();

            int index = 0;

            timer = new Timer(commands.Timing);
            timer.Start();
            running = true;
            mainView.CheckStatus();
            timer.Elapsed += (sender, args) => ReadNextCommands(commands, ref index);
        }

        private void ReadNextCommands(TrackCommands commands, ref int i)
        {
            if (i == commands.Length - 1)
            {
                StopRun();
            }
            byte keyW = (byte)Keys.W;
            byte keyA = (byte)Keys.A;
            byte keyS = (byte)Keys.S;
            byte keyD = (byte)Keys.D;
            byte nextState;

            // handle W
            nextState = commands.UpArrowOnOff[i];
            HandleKeys(keyW, nextState);

            nextState = commands.LeftArrowOnOff[i];
            HandleKeys(keyA, nextState);

            nextState = commands.DownArrowOnOff[i];
            HandleKeys(keyS, nextState);

            nextState = commands.RightArrowOnOff[i];
            HandleKeys(keyD, nextState);

            mainView.SetElapsedTime(i);
            i++;
        }

        public void HandleKeys(byte key, byte nextState)
        {
            if (nextState == 1 && keyboardHandler.IsKeyPressed(key) == false)
            {
                keyboardHandler.HoldKey(key);
            }
            if (nextState == 0 && keyboardHandler.IsKeyPressed(key) == true)
            {
                keyboardHandler.ReleaseKey(key);
            }
        }

        private void CheckOrSetKeyStatus(byte key)
        {
            
        }

        public void StopRun()
        {
            timer.Stop();
            keyboardHandler.ReleaseKey((byte)Keys.W);
            keyboardHandler.ReleaseKey((byte)Keys.A);
            keyboardHandler.ReleaseKey((byte)Keys.S);
            keyboardHandler.ReleaseKey((byte)Keys.D);
            running = false;
            mainView.DebugMessage("Run stopped");
            mainView.SetElapsedTime(0);
        }
    }
}