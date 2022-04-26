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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Z_Lab_TronOnDrugs_.Logic;

namespace Z_Lab_TronOnDrugs_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt;
        List<Motor> motorList = new List<Motor>();
        GameLogic logic;
        static public bool oneplayer;
        static public bool twoplayer;
        static public bool threeplayer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (oneplayer == true)
            {
                motorList.Add(new Motor(100, 500, 0));
            }
            else if (twoplayer == true)
            {
                motorList.Add(new Motor(100, 500, 0));
                motorList.Add(new Motor(1000, 500, 0));
            }
            else if (threeplayer == true)
            {
                motorList.Add(new Motor(100, 500, 0));
                motorList.Add(new Motor(1000, 500, 0));
                motorList.Add(new Motor(900, 500, 0));
            }
            logic = new GameLogic(motorList, grid.ActualWidth, grid.ActualHeight);
            logic.EndGame += Logic_EndGame;
            display.SetupLogic(logic);
            display.SetupSizes(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(30);
            dt.Tick += (sender, eventargs) =>
            {
                logic.Turn();
                display.InvalidateVisual();
            };
            dt.Start();
        }

        private void Logic_EndGame(object sender, EventArgs e)
        {
            dt.Stop();
            GameOverWindow gameOver = new GameOverWindow();
            gameOver.ShowDialog();
          
            if (gameOver.DialogResult == true)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            logic = new GameLogic(motorList, grid.ActualWidth, grid.ActualHeight);
            display.SetupLogic(logic);
            display.SetupSizes(new Size(grid.ActualWidth, grid.ActualHeight));
        }

        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                await motorList[0].TurnLeft();
                //display.InvalidateVisual();
            }
            else if (e.Key == Key.Right)
            {
                await motorList[0].TurnRight();
                //display.InvalidateVisual();
            }
            else if (e.Key == Key.Down)
            {
                motorList[0].UseAbility();
            }
            else if (e.Key == Key.A)
            {
                await motorList[1].TurnLeft();
                //display.InvalidateVisual();
            }
            else if (e.Key == Key.D)
            {
                await motorList[1].TurnRight();
                //display.InvalidateVisual();
            }
            else if (e.Key == Key.S)
            {
                motorList[1].UseAbility();
            }
            else if (e.Key == Key.J)
            {
                await motorList[2].TurnLeft();
                //display.InvalidateVisual();
            }
            else if (e.Key == Key.L)
            {
                await motorList[2].TurnRight();
                //display.InvalidateVisual();
            }
            else if (e.Key == Key.K)
            {
                motorList[2].UseAbility();
            }
            else if (e.Key == Key.Escape)
            {
                dt.Stop();
                PauseMenu pm = new PauseMenu();
                pm.ShowDialog();
                if (pm.DialogResult == true)
                {
                    dt.Start();
                }
                else if (pm.DialogResult == false)
                {
                    Close();
                }
            }
        }
    }
}
