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
using TasTool.ConfigElements;

namespace TasUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string SelectedTrack = "";
        public string SelectedGame = "";
        private SolidColorBrush errorRed = new SolidColorBrush(Color.FromRgb(180, 80, 80));
        private SolidColorBrush defaultColor = new SolidColorBrush(Color.FromRgb(255,255,255));
        public MainWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();
        }

        private void OnClickStartButton(object sender, RoutedEventArgs e)
        {
            if (IsSelectionValid())
            {
                
            }
            else
            {
                
            }
        }

        private void OnClickStopButton(object sender, RoutedEventArgs e)
        {

        }
        
        private void PopulateComboBoxes()
        {
            TasGamesSection tasGames = ConfigurationManager.GetSection("TasGamesSection") as TasGamesSection;

            foreach (TasGameConfigElement tasGameConfigElement in tasGames.TasGameCollection)
            {
                // todo Move and do something useful to get these into the combobox...
                TasGameConfigElement asd = tasGameConfigElement; // Get full TasGame config object
                TasGameConfigElement trialsConfig = tasGames.TasGameCollection["Trials"]; // Find the Config object attributes with object name
            }

            string trackFolderPath = System.Configuration.ConfigurationManager.AppSettings["trackJsonLocation"];
            string[] trackFiles = Directory.GetFiles(trackFolderPath);

            foreach (string filePath in trackFiles)
            {
                TrackComboBox.Items.Add(System.IO.Path.GetFileName(filePath));
            }

            if (TrackComboBox.Items.Count != 0)
            {
                SelectedTrack = TrackComboBox.Items[0].ToString();
            }
        }

        private void OnTrackComboBoxValueChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedTrack = TrackComboBox.SelectedItem.ToString();
        }
        private void OnGameComboBoxValueChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedGame = GameComboBox.SelectedItem.ToString();
        }

        private bool IsSelectionValid()
        {
            if (!string.IsNullOrEmpty(SelectedGame) && !string.IsNullOrEmpty(SelectedTrack))
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
                DebugTextBox.Text = "Select a game and a track to play";
                DebugTextBox.Background = errorRed;
            }
        }
    }
}
