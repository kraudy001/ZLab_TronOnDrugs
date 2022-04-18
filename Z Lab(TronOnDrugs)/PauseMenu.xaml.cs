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
    /// Interaction logic for PauseMenu.xaml
    /// </summary>
    public partial class PauseMenu : Window
    {
        public PauseMenu()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.DialogResult = true;
                Close();
            }
            else if (e.Key == Key.Enter)
            {
                //this.DialogResult = false;
                MessageBoxResult result = MessageBox.Show("Ez még nincs kész");
                if (MessageBoxResult.OK == result)
                {
                    this.DialogResult = true;
                }
            }
        }
    }
}
