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
        Employe User = new Employe();
        public menuWind(int ui)
        {
            InitializeComponent();
            User = SQL.GetEmployeById(ui);
            UserBlock.Text = User.Name;
            //optButt.IsEnabled = false;
        }
        private void exitButt_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void optButt_Click(object sender, RoutedEventArgs e)
        {

            if (OW == null || !OW.IsVisible)
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
            if (SQL.TestConnection(SQL.GetConnString()))
            {
                if (IW == null || !IW.IsVisible)
                {
                    IW = new InzedentWind(User, false, null);  
                    IW.reduct = false;
                    IW.Show();
                }
                else
                {
                    IW.Activate();
                }
            }
        }

        private void bilraryButt_Click(object sender, RoutedEventArgs e)
        {
            if (SQL.TestConnection(SQL.GetConnString()))
            {
                if (BW == null || !BW.IsVisible)
                {
                    BW = new bibltWind(User);
                    BW.Show();
                }
                else
                {
                    BW.Activate();
                }
            }
        }

        private void ispolnButt_Click(object sender, RoutedEventArgs e)
        {
            if (SQL.TestConnection(SQL.GetConnString()))
            {
                if (MWE == null || !MWE.IsVisible)
                {
                    MWE = new multiWind("Employees");
                    MWE.chekIsEmploye();
                    MWE.Show();
                }
                else
                {
                    MWE.Activate();
                }
            }
        }

        private void categButt_Click(object sender, RoutedEventArgs e)
        {
            if (SQL.TestConnection(SQL.GetConnString()))
            {
                if (MWC == null || !MWC.IsVisible)
                {
                    MWC = new multiWind("Categories");
                    MWC.chekIsEmploye();
                    MWC.Show();
                }
                else
                {
                    MWC.Activate();
                }
            }
        }

        private void podrazButt_Click(object sender, RoutedEventArgs e)
        {
            if (SQL.TestConnection(SQL.GetConnString()))
            {
                if (MWS == null || !MWS.IsVisible)
                {
                    MWS = new multiWind("Subdivisions");
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
}
