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
        public GameOverWindow(IGameLogic logic)
        {
            InitializeComponent();

            if (logic.Motors.Count == 0)
            {
                gameover.Content = "U R LONELY :(";
            }
            else if (logic.Motors[0].name == "Player 1")
            {
                gameover.Content = "PLAYER 1 WON";
            }
            else if (logic.Motors[0].name == "Player 2")
            {
                gameover.Content = "PLAYER 2 WON";
            }
            else if (logic.Motors[0].name == "Player 3")
            {
                gameover.Content = "PLAYER 3 WON";
            }
        }

        private void Again_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
