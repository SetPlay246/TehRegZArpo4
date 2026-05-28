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
using Dapper;

namespace WpfApp1
{
    public partial class InzedentWind : Window
    {
        public bool reduct;
        Employe User = new Employe();
        toSQL SQL = new toSQL();
        public Incident Inc;
        DateTime nowDate = DateTime.Now;

        

        public InzedentWind(Employe emp, bool rer, Incident inc)
        {

            InitializeComponent();
            reduct = rer;
            List<Status> statuses = SQL.GetAllStatus().ToList();
            statusCombo.ItemsSource = statuses;
            List<Category> categories = SQL.GetAllCategory().ToList();
            categCombo.ItemsSource = categories;
            List<Subdivision> subdivisions = SQL.GetAllSubdivision().ToList();
            podrazCombo.ItemsSource = subdivisions;
            
            User = emp;
            Inc= inc;
            if (reduct)
            {
                idBlock.Text = Inc.Id.ToString();
                AnameBox.Text = Inc.AuthorName;
                AemailBox.Text = Inc.AuthorEmail;
                descriBox.Text = Inc.Description;
                priorBox.Text = Inc.Priority.ToString();
                categCombo.SelectedIndex = Inc.CategoryId - 1;
                podrazCombo.SelectedIndex = Inc.SubdivisionId - 1;
                statusCombo.SelectedIndex = Inc.StatusId - 1;
                finalDate.SelectedDate = Inc.FinalDate;

            } else
            {
                createDate.Text = nowDate.ToString();
                idBlock.Text = SQL.GetNewId("Incidents").ToString();
                saveButt.Content = "создать";
                //statusCombo.IsReadOnly = true;
                statusCombo.SelectedIndex = 0;
                //statusCombo.IsTextSearchEnabled = false;
            }
            EmpBox.Text = User.Id + " " + User.Name + " " + User.Email;
        }


        private void saveButt_Click(object sender, RoutedEventArgs e)
        {

            bool canCreate = true;
            if (string.IsNullOrEmpty(AnameBox.Text))
            {
                canCreate = false;
                AnameBox.Background = Brushes.IndianRed;
            }
            else
            {
                AnameBox.Background = Brushes.White;
            }
            if (string.IsNullOrEmpty(AemailBox.Text))
            {
                canCreate = false;
                AemailBox.Background = Brushes.IndianRed;
            }
            else
            {
                AemailBox.Background = Brushes.White;
            }
            if (string.IsNullOrEmpty(descriBox.Text))
            {
                canCreate = false;
                descriBox.Background = Brushes.IndianRed;
            }
            else
            {
                descriBox.Background = Brushes.White;
            }
            if (!(int.TryParse(priorBox.Text, out int result) & 1 <= result & result <= 5))
            {
                canCreate = false;
                priorBox.Background = Brushes.IndianRed;
            }
            else
            {
                priorBox.Background = Brushes.White;
            }
            if (categCombo.SelectedItem == null)
            {
                canCreate = false;
                categCombo.BorderBrush = Brushes.IndianRed;
            }
            else
            {
                categCombo.BorderBrush = Brushes.White;
            }
            if (podrazCombo.SelectedItem == null)
            {
                canCreate = false;
                podrazCombo.BorderBrush = Brushes.IndianRed;
                
            }
            else
            {
                podrazCombo.BorderBrush = Brushes.White;
            }

            if (reduct & finalDate.SelectedDate == null)
            {
                canCreate = false;
                finalDate.Background = Brushes.IndianRed;
            }
            else if (finalDate.SelectedDate == null & finalDate.SelectedDate < nowDate)
            {
                canCreate = false;
                finalDate.Background = Brushes.IndianRed;
            }
            else
            {
                finalDate.Background = Brushes.White;
            }
            
            if (statusCombo.SelectedItem == null)
            {
                canCreate = false;
                statusCombo.BorderBrush = Brushes.IndianRed;
            }
            else
            {
                statusCombo.BorderBrush = Brushes.White;
            }

            if (!reduct & canCreate)
            {
                Inc = new Incident();
                Inc.AuthorName = AnameBox.Text;
                Inc.AuthorEmail = AemailBox.Text;
                Inc.Description = descriBox.Text;
                Inc.Priority = Convert.ToInt32(priorBox.Text);
                Inc.CategoryId = categCombo.SelectedIndex + 1;
                Inc.SubdivisionId = podrazCombo.SelectedIndex + 1;
                Inc.EmployeId = User.Id;
                Inc.StatusId = statusCombo.SelectedIndex + 1;
                Inc.CreateDate = nowDate;
                Inc.FinalDate = finalDate.SelectedDate;

                try
                {
                    SQL.CreateIncident(Inc);
                    if (fileList.Count > 0)
                    {
                        for (int i = 0; i < fileList.Count; i++)
                        {
                            SQL.CreateFile(fileList[i]);
                        }
                    }
                    SQL.CreateHistory(new History { IncidentId = Convert.ToInt32(idBlock.Text), EmployeId = User.Id, Description = $"созданно" });

                    if (MessageBox.Show("Записть добавлена", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                }
                catch { }

            }
            if(reduct & canCreate)
            {
                Inc.AuthorName = AnameBox.Text;
                Inc.AuthorEmail = AemailBox.Text;
                Inc.Description = descriBox.Text;
                Inc.Priority = Convert.ToInt32(priorBox.Text);
                Inc.CategoryId = categCombo.SelectedIndex + 1;
                Inc.SubdivisionId = podrazCombo.SelectedIndex + 1;
                Inc.EmployeId = User.Id;
                Inc.StatusId = statusCombo.SelectedIndex + 1;
                //Inc.CreateDate = nowDate;
                Inc.FinalDate = finalDate.SelectedDate;

                try
                {
                    SQL.UpdateIncident(Inc);

                    SQL.CreateHistory(new History { IncidentId = Convert.ToInt32(idBlock.Text), EmployeId = User.Id, Description = $"статус изменен на {SQL.GetStatusNameById(Inc.StatusId)}" });

                    RefreshHandler?.Invoke(this, false);
                    if (MessageBox.Show("Записть изменена", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                }
                catch { }
            }
            //MessageBox.Show($"конец операций, {canCreate}", "123",
            //            MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void exitButt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void statusCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!reduct)
            {
                statusCombo.SelectedIndex = 0;
            }
            else
            {
                if (statusCombo.SelectedIndex == 0)
                {
                    statusCombo.SelectedIndex = Inc.StatusId - 1;
                }
            }
        }

        private void finalDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            freshTime.Text = Convert.ToInt32((finalDate.SelectedDate - nowDate.Date).Value.TotalDays).ToString() + " дней";
        }

        filerWind files;
        List<IncidentFile> fileList = new List<IncidentFile>();
        private void addButt_Click(object sender, RoutedEventArgs e)
        {
            if (!reduct)
            {
                if (files == null || !files.IsVisible)
                {
                    files = new filerWind(Convert.ToInt32(idBlock.Text), reduct, fileList, false);
                    files.FileUpdateHandler += TakeTheFiles;
                    files.Show();
                }
                else
                {
                    files.Activate();
                }
            }
            else if (reduct)
            {
                if (files == null || !files.IsVisible)
                {
                    fileList = SQL.GetAllFileById(Convert.ToInt32(idBlock.Text)).ToList();
                    files = new filerWind(Convert.ToInt32(idBlock.Text), reduct, fileList, false);
                    files.FileUpdateHandler += TakeTheFiles;
                    files.Show();
                }
                else
                {
                    files.Activate();
                }
            }
        }
        public event EventHandler<bool> RefreshHandler;

        private void TakeTheFiles(object sender, List<IncidentFile> filesList)
        {
            if (!reduct)
            {
                fileList = filesList;
            }
            else if (reduct)
            {
                try
                {
                    var oldIds = new HashSet<int>(fileList.Select(f => f.Id));
                    var newIds = new HashSet<int>(filesList.Select(f => f.Id));

                    List<IncidentFile> delFileList = new List<IncidentFile>();
                    List<IncidentFile> addFileList = new List<IncidentFile>();


                    delFileList = fileList.Where(f => !newIds.Contains(f.Id)).ToList();
                    addFileList = filesList.Where(f => !oldIds.Contains(f.Id)).ToList();


                    for (int i = 0; i < delFileList.Count; i++)
                    {
                        SQL.DeleteFile(delFileList[i].Id);
                    }
                    for (int i = 0; (i < addFileList.Count); i++)
                    {
                        SQL.CreateFile(addFileList[i]);
                    }
                    MessageBox.Show("Файлы успешно обновлены", "Успех",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                    SQL.CreateHistory(new History { IncidentId = Inc.Id, EmployeId = User.Id, Description = $"состав файлов изменен" });
                }
                catch
                {
                    MessageBox.Show("Файлы не обновлены", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }
    }
}
