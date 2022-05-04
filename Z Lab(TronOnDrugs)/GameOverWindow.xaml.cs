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
using Z_Lab_TronOnDrugs_.Logic;
using Z_Lab_TronOnDrugs_.Renderer;

namespace Z_Lab_TronOnDrugs_
{
    /// <summary>
    /// Interaction logic for GameOverWindow.xaml
    /// </summary>
    public partial class GameOverWindow : Window
    {
        public GameOverWindow()
        {
            InitializeComponent();
            MainWindow mainWindow = new MainWindow();
        }

        private void Again_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
