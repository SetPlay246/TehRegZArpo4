using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для bibltWind.xaml
    /// </summary>
    public partial class bibltWind : Window
    {
        toSQL SQL = new toSQL();
        Employe ui;
        int sId;
        public bibltWind(Employe ui)
        {
            InitializeComponent();
            incedentGrid.ItemsSource = SQL.GetAllFullIncident();

            
            this.ui = ui;
        }

        private void exitButt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void filButt1_Click(object sender, RoutedEventArgs e)
        {
            incedentGrid.Items.Filter = null;
        }

        private void filButt2_Click(object sender, RoutedEventArgs e)
        {
            incedentGrid.Items.Filter = item => ((FullIncident)item).EmloyeId == ui.Id;
        }

        private void filButt3_Click(object sender, RoutedEventArgs e)
        {
            incedentGrid.Items.Filter = item => ((FullIncident)item).FinalDate < DateTime.Now;
        }

        private void refreshButt_Click(object sender, RoutedEventArgs e)
        {
            incedentGrid.ItemsSource = SQL.GetAllFullIncident();
        }

        private void incedentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (incedentGrid.SelectedItem != null)
            {
                // Получаем значение свойства Id через рефлексию
                var idProperty = incedentGrid.SelectedItem.GetType().GetProperty("Id");
                if (idProperty != null)
                {
                    int id = (int)idProperty.GetValue(incedentGrid.SelectedItem);
                    sIdBox.Text = id.ToString();
                    sId = id;
                }
            }
        }
        InzedentWind IW;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sIdBox.Text != "?")
            {
                if (IW == null || !IW.IsVisible)
                {
                    IW = new InzedentWind(ui, true, SQL.GetIncidentById(sId));
                    IW.RefreshHandler += NeedRefresh;
                    IW.Show();
                }
                else
                {
                    IW.Activate();
                }
            }
        }

        private void NeedRefresh(object sender, bool a)
        {
            incedentGrid.ItemsSource = SQL.GetAllFullIncident();
        }

        private void toFileButt_Click(object sender, RoutedEventArgs e)
        {
            if (sIdBox.Text != "?")
            {
                filerWind FW = new filerWind(sId, true, null, true);
                //FW.RefreshHandler += NeedRefresh;
                FW.Show();
            }
        }

        massStatusWind mSW;
        private void massStatButt_Click(object sender, RoutedEventArgs e)
        {
            if (mSW == null || !mSW.IsVisible)
            {
                mSW = new massStatusWind(ui.Id);
                mSW.Show();
            }
            else
            {
                mSW.Activate();
            }
        }

        dashWind DW;
        private void statusStatButt_Click(object sender, RoutedEventArgs e)
        {

            if (DW == null || !DW.IsVisible)
            {
                DW = new dashWind();
                DW.Show();
            }
            else
            {
                DW.Activate();
            }

        }

        historyWind HW;
        private void historyButt_Click(object sender, RoutedEventArgs e)
        {
            if (sIdBox.Text != "?")
            {
                if (HW == null || !HW.IsVisible)
                {
                    HW = new historyWind(sId);
                    HW.Show();
                }
                else
                {
                    HW.Activate();
                }
            }
        }
    }
}
