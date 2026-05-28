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
    /// <summary>
    /// Логика взаимодействия для dashWind.xaml
    /// </summary>
    public partial class dashWind : Window
    {
        toSQL SQL = new toSQL();

        List<CategoryStatistic> categoryStatistics = new List<CategoryStatistic>();
        public dashWind()
        {
            InitializeComponent();
            categoryStatistics = SQL.GetCategoryStatistic().ToList();
            StatGrid.ItemsSource = categoryStatistics;
        }

        private void exitButt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void refreshButt_Click(object sender, RoutedEventArgs e)
        {
            categoryStatistics = SQL.GetCategoryStatistic().ToList();
            StatGrid.ItemsSource = categoryStatistics;
        }
    }
}
