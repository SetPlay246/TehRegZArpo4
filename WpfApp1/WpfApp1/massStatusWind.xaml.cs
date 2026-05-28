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
    /// Логика взаимодействия для massStatusWind.xaml
    /// </summary>
    public partial class massStatusWind : Window
    {
        int uId;
        toSQL SQL = new toSQL();
        List<FullIncident> AllIncidents = new List<FullIncident>();
        List<Status> statuses = new List<Status>();
        public massStatusWind(int uId)
        {
            InitializeComponent();
            refresh();
            statusCombo.SelectedIndex = 0;
            this.uId = uId;
        }

        void refresh()
        {
            AllIncidents = SQL.GetAllFullIncident().ToList();
            massGrid.ItemsSource = AllIncidents;
            statuses = SQL.GetAllStatus().ToList();
            statusCombo.ItemsSource = statuses;
        }
        
        List<FullIncident> incidents = new List<FullIncident>();
        private void massGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }


        private void updateButt_Click(object sender, RoutedEventArgs e)
        {
            var selectedIncidents = massGrid.SelectedItems.OfType<FullIncident>().ToList();
            incidents = selectedIncidents;
            //string Idis = "";
            //for (int i = 0; i < incidents.Count; i++)
            //{
            //    Idis += incidents[i].Id + " ";
            //}
            //selectedIdis.Text = Idis;
            if (MessageBox.Show($"изменить статус \"{incidents.Count}\" записям?", "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    for (int i = 0; i < incidents.Count; i++)
                    {
                        Incident temp = SQL.GetIncidentById(incidents[i].Id);
                        temp.StatusId = statusCombo.SelectedIndex + 1;
                        SQL.UpdateIncident(temp);
                        SQL.CreateHistory(new History { IncidentId = temp.Id, EmployeId = temp.EmployeId, Description = $"статус изменен на {SQL.GetStatusNameById(temp.StatusId)}" });
                    }
                    MessageBox.Show($"Записи обновлены", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                    refresh();
                }
                catch {
                    MessageBox.Show($"Записи не обновлены", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void exitButt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void refreshButt_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }
    }
}
