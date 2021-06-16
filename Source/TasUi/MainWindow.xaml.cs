using System;
using System.Collections.Generic;
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
using Path = System.Windows.Shapes.Path;

namespace TasUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string SelectedTrack = "";
        public MainWindow()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        private void OnClickStartButton(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnClickStopButton(object sender, RoutedEventArgs e)
        {

        }
        
        private void PopulateComboBox()
        {
            string path = System.Configuration.ConfigurationSettings.AppSettings["trackJsonLocation"];
            string[] files = Directory.GetFiles(path);

            foreach (string filePath in files)
            {
                TrackComboBox.Items.Add(System.IO.Path.GetFileName(filePath));
            }

            if (TrackComboBox.Items.Count != 0)
            {
                SelectedTrack = TrackComboBox.Items[0].ToString();
            }
        }

        private void OnComboBoxValueChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedTrack = TrackComboBox.SelectedItem.ToString();
        }
    }
}
