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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        toSQL SQL = new toSQL();
        int userId = 0;
        optionWind OW;
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool ChekUser()
        {
            int ui = SQL.UserIsTrue(logBox.Text, pasBox.Text);
            if (ui == 0)
            {
                mesBlock.Text = "пользователь не найден"; 
            }
            else
            {
                userId = ui;
                mesBlock.Text = "успешный вход";
                return true;
            }
            return false;
        }

        private void enterButt_Click(object sender, RoutedEventArgs e)
        {
            if(ChekUser()){
                menuWind newWind = new menuWind(userId);
                newWind.Show();
                if (OW != null)
                {
                    OW.Close();
                }
                
                this.Close();
            }
            
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

        private void exitButt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
