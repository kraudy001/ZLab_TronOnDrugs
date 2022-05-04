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
        #region Properties
        DispatcherTimer dt;
        List<Motor> motorList = new List<Motor>();
        List<Vectors> stonesList = new List<Vectors>();
        GameLogic logic;
        #endregion

        #region Variables
        public static bool oneplayer;
        public static bool twoplayer;
        public static bool threeplayer;
        public static bool medium;
        public static bool hard;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.RandomRGB();

            #region Players
            if (oneplayer == true)
            {
                motorList.Add(new Motor(960, 880, 0, "Player 1"));
            }
            else if (twoplayer == true)
            {
                motorList.Add(new Motor(1580, 880, 0, "Player 1"));
                motorList.Add(new Motor(350, 880, 0, "Player 2"));
            }
            else if (threeplayer == true)
            {
                motorList.Add(new Motor(1580, 880, 0, "Player 1"));
                motorList.Add(new Motor(350, 880, 0, "Player 2"));
                motorList.Add(new Motor(960, 880, 0, "Player 3"));
            }
            #endregion

            #region Difficulty
            if (medium == true)
            {
                stonesList = Vectors.StoneGenerator(grid.ActualWidth, grid.ActualHeight - 100, 6);
                logic = new GameLogic(motorList, grid.ActualWidth, grid.ActualHeight - 100, stonesList, false);
            }
            else if (hard == true)
            {
                stonesList = Vectors.StoneGenerator(grid.ActualWidth, grid.ActualHeight - 100, 6);
                logic = new GameLogic(motorList, grid.ActualWidth, grid.ActualHeight - 100, stonesList, true);
            }
            else if (medium == false && hard == false) //easy
            {
                logic = new GameLogic(motorList, grid.ActualWidth, grid.ActualHeight - 100, stonesList, false);
            }
            #endregion

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
            GameOverWindow gameOver = new GameOverWindow(logic);
            gameOver.ShowDialog();
          
            if (gameOver.DialogResult == true)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        #region Controls
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (var motor in logic.Motors)
            {
                if (motor.name == "Player 1")
                {
                    if (e.Key == Key.Left)
                    {
                        motor.TurnLeft = true;
                    }
                    else if (e.Key == Key.Right)
                    {
                        motor.TurnRight = true;
                    }
                    else if (e.Key == Key.Down)
                    {
                        motor.UseAbility();
                    }
                }
                else if (motor.name == "Player 2")
                {
                    if (e.Key == Key.A)
                    {
                        motor.TurnLeft = true;
                    }
                    else if (e.Key == Key.D)
                    {
                        motor.TurnRight = true;
                    }
                    else if (e.Key == Key.S)
                    {
                        motor.UseAbility();
                    }
                }
                else if (motor.name == "Player 3")
                {
                    if (e.Key == Key.J)
                    {
                        motor.TurnLeft = true;
                    }
                    else if (e.Key == Key.L)
                    {
                        motor.TurnRight = true;
                    }
                    else if (e.Key == Key.K)
                    {
                        motor.UseAbility();
                    }
                }
            }
            //menu
            if (e.Key == Key.Escape)
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

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (var motor in logic.Motors)
            {
                if (motor.name == "Player 1")
                {
                    if (e.Key == Key.Left)
                    {
                        motor.TurnLeft = false;
                    }
                    else if (e.Key == Key.Right)
                    {
                        motor.TurnRight = false;
                    }
                    else if (e.Key == Key.Down)
                    {
                        motor.UseAbility();
                    }
                }
                else if (motor.name == "Player 2")
                {
                    if (e.Key == Key.A)
                    {
                        motor.TurnLeft = false;
                    }
                    else if (e.Key == Key.D)
                    {
                        motor.TurnRight = false;
                    }
                    else if (e.Key == Key.S)
                    {
                        motor.UseAbility();
                    }
                }
                else if (motor.name == "Player 3")
                {
                    if (e.Key == Key.J)
                    {
                        motor.TurnLeft = false;
                    }
                    else if (e.Key == Key.L)
                    {
                        motor.TurnRight = false;
                    }
                    else if (e.Key == Key.K)
                    {
                        motor.UseAbility();
                    }
                }
            }
        }
        #endregion
    }
}
