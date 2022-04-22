using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Z_Lab_TronOnDrugs_
{
    /// <summary>
    /// Interaction logic for PlayerNum.xaml
    /// </summary>
    public partial class PlayerNum : Window
    {
        public PlayerNum()
        {
            InitializeComponent();
        }

        private void Player1_Button_Click(object sender, RoutedEventArgs e)
        {
            DifficultyWindow dif = new DifficultyWindow();
            MainWindow.oneplayer = true;
            MainWindow.threeplayer = false;
            MainWindow.twoplayer = false;
            dif.Show();
            Close();
        }

        private void Player2_Button_Click(object sender, RoutedEventArgs e)
        {
            DifficultyWindow dif = new DifficultyWindow();
            MainWindow.twoplayer = true;
            MainWindow.oneplayer = false;
            MainWindow.threeplayer = false;
            dif.Show();
            Close();
        }

        private void Player3_Button_Click(object sender, RoutedEventArgs e)
        {
            DifficultyWindow dif = new DifficultyWindow();
            MainWindow.threeplayer = true;
            MainWindow.twoplayer = false;
            MainWindow.oneplayer = false;
            dif.Show();
            Close();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
