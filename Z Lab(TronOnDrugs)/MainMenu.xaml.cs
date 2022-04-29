using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Z_Lab_TronOnDrugs_
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        bool isOn = true;
        SoundPlayer player;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow cw = new CloseWindow();
            cw.ShowDialog();
            if (cw.DialogResult == true)
            {
                Close();
            }
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            PlayerNum pn = new PlayerNum();
            pn.ShowDialog();
        }

        private void Controls_Button_Click(object sender, RoutedEventArgs e)
        {
            ControlsWindow cw = new ControlsWindow();
            cw.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            player = new SoundPlayer(Properties.Resources.music);
            player.PlayLooping();
        }

        private void Sound_Button_Click(object sender, RoutedEventArgs e)
        {
            if (isOn == true)
            {
                player.Stop();
                isOn = false;
            }
            else if (isOn == false)
            {
                player.PlayLooping();
                isOn = true;
            }
        }
    }
}
