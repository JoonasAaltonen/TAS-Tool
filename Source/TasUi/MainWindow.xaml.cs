using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using TasTool;
using TasTool.ConfigElements;
using TasTool.Handlers;
using TasTool.Interfaces;
using TasTool.Track;
using TasUi.InputRecording;

namespace TasUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public (string fileName, string filePath) SelectedTrack;
        public string SelectedGame = "";
        private SolidColorBrush errorRed = new SolidColorBrush(Color.FromRgb(180, 80, 80));
        private SolidColorBrush defaultColor = new SolidColorBrush(Color.FromRgb(255,255,255));
        private ITasMediator tasMediator;
        private ITasConfig Config;
        private CommandHandler commandHandler;
        private KeyboardHookHandler keyboardHook;

        private delegate void SetTextBoxValueDelegate(TextBox textBoxName, string value);

        public MainWindow()
        {
            InitializeComponent();
            tasMediator = new TasFactory().CreateTasMediator();
            Config = tasMediator.Config;
            commandHandler = new CommandHandler(this, tasMediator, Config.EnabledKeyboardHandlerType);
            PopulateComboBoxes();
            keyboardHook = new KeyboardHookHandler(tasMediator);
        }

        private async void OnClickStartButton(object sender, RoutedEventArgs e)
        {
            if (IsSelectionValid())
            {
                tasMediator.Initialize(SelectedGame, SelectedTrack.filePath);
                
                SetDebugTextBoxBackgroundColor(tasMediator.InitSuccessful);
                DebugTextBox.Text = tasMediator.DebugMessage;
                
                if (tasMediator.InitSuccessful)
                {
                    await commandHandler.StartRun(tasMediator.CommandData);
                }
                else
                {
                    
                }
            }
            else
            {
                DebugTextBox.Text = "Select a game and a track to play";
            }
        }

        private async void OnClickStopButton(object sender, RoutedEventArgs e)
        {
            commandHandler.StopRun();
        }
        
        private void PopulateComboBoxes()
        {
            if (!TrackComboBox.Items.IsEmpty)
            {
                TrackComboBox.Items.Clear();
            }

            if (!GameComboBox.Items.IsEmpty)
            {
                GameComboBox.Items.Clear();
            }

            foreach (KeyValuePair<string, string> track in Config.AvailableTracks)
            {
                TrackComboBox.Items.Add(track.Key);
            }

            foreach (var tasGame in Config.AvailableGames)
            {
                GameComboBox.Items.Add(tasGame.Name);
            }

            if (TrackComboBox.Items.Count != 0 && GameComboBox.Items.Count != 0)
            {
                SelectedTrack.fileName = TrackComboBox.Items[0].ToString();
                SelectedTrack.filePath = Config.AvailableTracks[SelectedTrack.fileName];
                SelectedGame = GameComboBox.Items[0].ToString();
                TrackComboBox.Text = SelectedTrack.fileName;
                GameComboBox.Text = SelectedGame;
            }
        }

        private void OnTrackComboBoxValueChanged(object sender, SelectionChangedEventArgs e)
        {
            // There must be better way to fiddle around the string dictionaries
            if (TrackComboBox.SelectedItem != null)
            {
                SelectedTrack = (TrackComboBox.SelectedItem.ToString(),
                    Config.AvailableTracks[TrackComboBox.SelectedItem.ToString()]);
            }

        }
        private void OnGameComboBoxValueChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GameComboBox.SelectedItem != null)
            {
                SelectedGame = GameComboBox.SelectedItem.ToString();
            }
        }

        private bool IsSelectionValid()
        {
            if (!string.IsNullOrEmpty(SelectedGame) && !string.IsNullOrEmpty(SelectedTrack.fileName))
            {
                SetDebugTextBoxBackgroundColor(true);
                return true;
            }
            SetDebugTextBoxBackgroundColor(false);
            return false;
        }

        private void SetDebugTextBoxBackgroundColor(bool isValid)
        {
            if (isValid)
            {
                DebugTextBox.Background = defaultColor;
            }
            else
            {
                DebugTextBox.Background = errorRed;
            }
        }

        public void InvokeTextBoxValueChange(TextBox textBoxName, string value)
        {
            object[] objs = {textBoxName, value};
            Dispatcher.Invoke(new SetTextBoxValueDelegate(SetTextBoxValue), objs);
        }

        public void SetTextBoxValue(TextBox textBoxName, string value)
        {
            textBoxName.Text = value;
        }

        private void OnClickStartInputRecordingButton(object sender, RoutedEventArgs e)
        {
            RecordIcon.Fill = Brushes.Red;
            keyboardHook.StartInputRecording();
        }

        private void OnClickStopInputRecordingButton(object sender, RoutedEventArgs e)
        {
            RecordIcon.Fill = defaultColor;
            keyboardHook.StopInputRecording();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            commandHandler.TestHolding();
        }

        private void OnClickRefresh(object sender, RoutedEventArgs e)
        {
            tasMediator.Config.GetConfiguration();
            PopulateComboBoxes();
        }
    }
}
