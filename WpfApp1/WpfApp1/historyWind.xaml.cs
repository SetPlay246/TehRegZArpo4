using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// <summary>
    /// Логика взаимодействия для historyWind.xaml
    /// </summary>
    public partial class historyWind : Window
    {
        toSQL SQL = new toSQL();
        int id;
        List<History> histories;
        public historyWind(int sId)
        {
            InitializeComponent();
            id = sId;
            histories = SQL.GetHistoryById(sId).ToList();
            histGrid.ItemsSource = histories;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            histories = SQL.GetHistoryById(id).ToList();
            histGrid.ItemsSource = histories;
        }
    }
}
