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

namespace WpfApp1
{
    public partial class optionWind : Window
    {
        toSQL SQL = new toSQL();
        public optionWind()
        {
            InitializeComponent();
            connBox.Text = SQL.GetConnString();
        }

        private void saveButt_Click(object sender, RoutedEventArgs e)
        {
            SQL.SetConnString(connBox.Text);
            this.Close();
        }

        private void cancelButt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void chekButt_Click(object sender, RoutedEventArgs e)
        {
            if (SQL.TestConnection(connBox.Text))
            {
                connBox.Background = Brushes.LightGreen;
            }
            else
            {
                connBox.Background= Brushes.IndianRed;
            }
        }
    }
}
