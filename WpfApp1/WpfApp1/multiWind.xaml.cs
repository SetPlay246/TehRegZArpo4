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
    public partial class multiWind : Window
    {
        public string type = "";
        public multiWind()
        {
            InitializeComponent();
        }

        public void chekIsEmploye()
        {
            typeBlock.Text = type;
            if (type == "Category" || type == "Subdivision" || type == "Status")
            {
                eBlock.Visibility = Visibility.Hidden;
                eBox.Visibility = Visibility.Hidden;
                lBlock.Visibility = Visibility.Hidden;
                lBox.Visibility = Visibility.Hidden;
                pBlock.Visibility = Visibility.Hidden;
                pBox.Visibility = Visibility.Hidden;
            }
            else
            {
                eBlock.Visibility = Visibility.Visible;
                eBox.Visibility = Visibility.Visible;
                lBlock.Visibility = Visibility.Visible;
                lBox.Visibility = Visibility.Visible;
                pBlock.Visibility = Visibility.Visible;
                pBox.Visibility = Visibility.Visible;
            }
        }

        private void exitButt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
