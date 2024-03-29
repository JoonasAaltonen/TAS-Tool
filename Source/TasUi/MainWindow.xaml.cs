﻿using System;
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
using TasTool;
using TasTool.ConfigElements;
using TasTool.Handlers;
using TasTool.Interfaces;

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
        private IInitializer tasInitializer;
        private ITasConfig Config;
        private CommandHandler commandHandler;

        public MainWindow()
        {
            InitializeComponent();
            tasInitializer = new TasFactory().CreateTasInitializer();
            Config = tasInitializer.Config;
            commandHandler = new CommandHandler(this, tasInitializer, Config.EnabledKeyboardHandlerType);
            PopulateComboBoxes();
        }

        private void OnClickStartButton(object sender, RoutedEventArgs e)
        {
            if (IsSelectionValid())
            {
                tasInitializer.Initialize(SelectedGame, SelectedTrack.filePath);
                
                SetDebugTextBoxBackgroundColor(tasInitializer.InitSuccessful);
                DebugTextBox.Text = tasInitializer.DebugMessage;
                
                if (tasInitializer.InitSuccessful)
                {
                    commandHandler.StartRun(tasInitializer.TrackData);
                    
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

        private void OnClickStopButton(object sender, RoutedEventArgs e)
        {
            commandHandler.StopRun();
        }
        
        private void PopulateComboBoxes()
        {
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
            SelectedTrack = (TrackComboBox.SelectedItem.ToString(),
                Config.AvailableTracks[TrackComboBox.SelectedItem.ToString()]);

        }
        private void OnGameComboBoxValueChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedGame = GameComboBox.SelectedItem.ToString();
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
    }
}
