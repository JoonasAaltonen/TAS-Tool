using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Botti
{
    public partial class Form1 : Form
    {
        private Runner runner;
        private delegate void DebugDelegate(string message);
        //private delegate void TextBoxEditDelegate(int elapsedMilliseconds, int commandIndex);
        private delegate void TextBoxEditDelegate(int commandIndex);

        public static string SelectedTrack = "";
        public Form1()
        {
            InitializeComponent();
            PopulateComboBox();
            this.runner = new Runner((Form1)this);
        }


        // Send a series of key presses to the Calculator application.
        private void StartButtonClick(object sender, EventArgs e)
        {
            runner.Run();
        }

        public void DebugMessage(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new DebugDelegate(DebugMessage), new object[] {message});
                return;
            }
            DebugTextBox.Text = message;
            Console.WriteLine(message);
            CheckStatus();
        }

        public void SetElapsedTime(int commandIndex)
        {
            if (InvokeRequired)
            {
                this.Invoke(new TextBoxEditDelegate(SetElapsedTime), new object[] {commandIndex});
                return;
            }

            //ElapsedMsBox.Text = elapsedMilliseconds.ToString();
            CmdIndexBox.Text = commandIndex.ToString();

        }

        public void CheckStatus()
        {
            StatusBox.Text = TrialsRunner.running.ToString();
        }


        private void StopButtonClick(object sender, EventArgs e)
        {
            runner.Stop();
        }

        private void TrackComboboxSelectedItemChanged(object sender, EventArgs e)
        {
            SelectedTrack = trackComboBox.SelectedItem.ToString();
        }

        private void PopulateComboBox()
        {
            string path = System.Configuration.ConfigurationSettings.AppSettings["trackJsonLocation"];
            string[] files = Directory.GetFiles(path);

            foreach (string filePath in files)
            {
                trackComboBox.Items.Add(Path.GetFileName(filePath));
            }

            if (trackComboBox.Items.Count != 0)
            {
                SelectedTrack = trackComboBox.Items[0].ToString();
            }
        }
    }
}
