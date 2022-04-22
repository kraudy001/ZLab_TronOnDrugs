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
using Z_Lab_TronOnDrugs_.Renderer;

namespace Z_Lab_TronOnDrugs_
{
    /// <summary>
    /// Interaction logic for DifficultyWindow.xaml
    /// </summary>
    public partial class DifficultyWindow : Window
    {
        public DifficultyWindow()
        {
            InitializeComponent();
        }

        private void Easy_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Display.medium = false;
            Display.hard = false;
            mw.Show();
            Close();
        }

        private void Medium_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Display.medium = true;
            Display.hard = false;
            mw.Show();
            Close();
        }

        private void Hard_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Display.medium = false;
            Display.hard = true;
            mw.Show();
            Close();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            PlayerNum pn = new PlayerNum();
            pn.Show();
            Close();
        }
    }
}
