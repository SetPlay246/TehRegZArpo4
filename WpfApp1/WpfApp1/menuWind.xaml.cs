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
    public partial class menuWind : Window
    {
        optionWind OW;
        multiWind MWE;
        multiWind MWS;
        multiWind MWC;
        bibltWind BW;
        InzedentWind IW;
        toSQL SQL = new toSQL();
        public int userId = 0;
        public menuWind()
        {
            
            InitializeComponent();
            UserBlock.Text = SQL.GetUserFullName(userId);
        }

        private void exitButt_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void optButt_Click(object sender, RoutedEventArgs e)
        {
            if(OW == null || !OW.IsVisible)
            {
                OW = new optionWind();
                OW.Show();
            }
            else
            {
                OW.Activate();
            }
        }

        private void regIncButt_Click(object sender, RoutedEventArgs e)
        {
            if (IW == null || !IW.IsVisible)
            {
                IW = new InzedentWind();
                IW.reduct = false;
                IW.Show();
            }
            else
            {
                IW.Activate();
            }
        }

        private void bilraryButt_Click(object sender, RoutedEventArgs e)
        {
            if (BW == null || !BW.IsVisible)
            {
                BW = new bibltWind();
                BW.Show();
            }
            else
            {
                BW.Activate();
            }
        }

        private void ispolnButt_Click(object sender, RoutedEventArgs e)
        {
            if (MWE == null || !MWE.IsVisible)
            {
                MWE = new multiWind();
                MWE.type = "Employe";
                MWE.chekIsEmploye();
                MWE.Show();
            }
            else
            {
                MWE.Activate();
            }
        }

        private void categButt_Click(object sender, RoutedEventArgs e)
        {
            if (MWC == null || !MWC.IsVisible)
            {
                MWC = new multiWind();
                MWC.type = "Category";
                MWC.chekIsEmploye();
                MWC.Show();
            }
            else
            {
                MWC.Activate();
            }
        }

        private void podrazButt_Click(object sender, RoutedEventArgs e)
        {
            if (MWS == null || !MWS.IsVisible)
            {
                MWS = new multiWind();
                MWS.type = "Subdivision";
                MWS.chekIsEmploye();
                MWS.Show();
            }
            else
            {
                MWS.Activate();
            }
        }
    }
}
