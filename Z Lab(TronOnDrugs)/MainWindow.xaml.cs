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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            motorList.Add(new Motor(100, 500, 0));
            //motorList.Add(new Motor(1000, 500, 0));
            //motorList.Add(new Motor(900, 500, 0));
            logic = new GameLogic(motorList, grid.ActualWidth, grid.ActualHeight);
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            logic = new GameLogic(motorList, grid.ActualWidth, grid.ActualHeight);
            display.SetupLogic(logic);
            display.SetupSizes(new Size(grid.ActualWidth, grid.ActualHeight));
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                motorList[0].TurnLeft();
                display.InvalidateVisual();
            }
            else if (e.Key == Key.Right)
            {
                motorList[0].TurnRight();
                display.InvalidateVisual();
            }
            else if (e.Key == Key.A)
            {
                motorList[1].TurnLeft();
                display.InvalidateVisual();
            }
            else if (e.Key == Key.D)
            {
                motorList[1].TurnRight();
                display.InvalidateVisual();
            }
            else if (e.Key == Key.J)
            {
                motorList[2].TurnLeft();
                display.InvalidateVisual();
            }
            else if (e.Key == Key.L)
            {
                motorList[2].TurnRight();
                display.InvalidateVisual();
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
