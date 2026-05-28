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
        toSQL SQL = new toSQL();
        public string type = "";
        bool reduct = false;
        int sId;

        int selectedIndex = -1;
        bool isRefresh = false;

        Employe emp;
        Category cat;
        Subdivision sub;

        public multiWind(string type)
        {
            InitializeComponent();
            this.type = type;
            chekIsEmploye();
        }

        public void Refresh()
        {
            isRefresh = true;

            if (type == "Categories") { tableGrid.ItemsSource = SQL.GetAllCategory(); if (reduct) { tableGrid.SelectedItem = cat; } }
            if (type == "Subdivisions") { tableGrid.ItemsSource = SQL.GetAllSubdivision(); if (reduct) { tableGrid.SelectedItem = sub; } }
            if (type == "Employees") { tableGrid.ItemsSource = SQL.GetAllEmploye(); if (reduct) { tableGrid.SelectedItem = emp; } }
            if (reduct)
            {
                tableGrid.SelectedIndex = selectedIndex;
            }

            isRefresh = false;
        }

        public void chekIsEmploye()
        {
            try
            {
                typeBlock.Text = type;
                idBlock.Text = SQL.GetNewId(type).ToString();
                if (type == "Categories" || type == "Subdivisions" || type == "Statuses")
                {
                    if (type == "Categories") { tableGrid.ItemsSource = SQL.GetAllCategory(); }
                    if (type == "Subdivisions") { tableGrid.ItemsSource = SQL.GetAllSubdivision(); }
                    eBlock.Visibility = Visibility.Hidden;
                    eBox.Visibility = Visibility.Hidden;
                    lBlock.Visibility = Visibility.Hidden;
                    lBox.Visibility = Visibility.Hidden;
                    pBlock.Visibility = Visibility.Hidden;
                    pBox.Visibility = Visibility.Hidden;
                }
                else
                {
                    tableGrid.ItemsSource = SQL.GetAllEmploye();
                    eBlock.Visibility = Visibility.Visible;
                    eBox.Visibility = Visibility.Visible;
                    lBlock.Visibility = Visibility.Visible;
                    lBox.Visibility = Visibility.Visible;
                    pBlock.Visibility = Visibility.Visible;
                    pBox.Visibility = Visibility.Visible;
                }
            }
            catch { }
        }

        private void exitButt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void reductButt_Click(object sender, RoutedEventArgs e)
        {
            idBlock.Text = "";
            nBox.Text = "";
            eBox.Text = "";
            lBox.Text = "";
            pBox.Text = "";
            if (reduct)
            {
                reduct = false;
                dellButt.IsEnabled = false;
                reductButt.Content = "режим добавления нового";
                saveButt.Content = "добавить";
                idBlock.Text = SQL.GetNewId(type).ToString();
            }
            else
            {
                reduct = true;
                dellButt.IsEnabled = true;
                reductButt.Content = "режим изменения";
                saveButt.Content = "изменить";
            }
        }

        private void tableGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (reduct & !isRefresh)
            {
                try
                {
                    if (tableGrid.SelectedItem != null)
                    {
                        // Получаем значение свойства Id через рефлексию
                        var idProperty = tableGrid.SelectedItem.GetType().GetProperty("Id");
                        if (idProperty != null)
                        {
                            int id = (int)idProperty.GetValue(tableGrid.SelectedItem);
                            idBlock.Text = id.ToString();
                            sId = id;
                        }
                    }
                    if (type == "Employees")
                    {
                        emp = SQL.GetEmployeById(sId);
                        //idBlock.Text = emp.Id.ToString();
                        nBox.Text = emp.Name;
                        eBox.Text = emp.Email;
                        lBox.Text = emp.login;
                        pBox.Text = emp.password;
                    }
                    if (type == "Categories")
                    {
                        cat = SQL.GetCategoryById(sId);
                        //idBlock.Text = cat.Id.ToString();
                        nBox.Text = cat.Name;
                    }
                    if (type == "Subdivisions")
                    {
                        sub = SQL.GetSubdivisionById(sId);
                        //idBlock.Text = sub.Id.ToString();
                        nBox.Text = sub.Name;
                    }
                }
                catch { }
            }
        }

        private void saveButt_Click(object sender, RoutedEventArgs e)
        {
            bool canCreate = true;
            if (string.IsNullOrEmpty(nBox.Text))
            {
                canCreate = false;
                nBox.Background = Brushes.IndianRed;
            }
            else
            {
                nBox.Background = Brushes.White;
            }
            if (type == "Employees" & string.IsNullOrEmpty(eBox.Text))
            {
                canCreate = false;
                eBox.Background = Brushes.IndianRed;
            }
            else
            {
                eBox.Background = Brushes.White;
            }
            if (type == "Employees" & string.IsNullOrEmpty(lBox.Text))
            {
                if ((reduct & SQL.LoginIsUnique(lBox.Text) > 1) || (!reduct & SQL.LoginIsUnique(lBox.Text) > 0))
                {
                    canCreate = false;
                    lBox.Background = Brushes.IndianRed;
                }
            }
            else
            {
                lBox.Background = Brushes.White;
            }
            if (type == "Employees" & string.IsNullOrEmpty(pBox.Text))
            {
                canCreate = false;
                pBox.Background = Brushes.IndianRed;
            }
            else
            {
                pBox.Background = Brushes.White;
            }
            if (canCreate)
            {
                try
                {
                    if (type == "Employees")
                    {
                        emp = new Employe();
                        emp.Id = Convert.ToInt32(idBlock.Text);
                        emp.Name = nBox.Text;
                        emp.Email = eBox.Text;
                        emp.login = lBox.Text;
                        emp.password = pBox.Text;
                        if (reduct)
                        {
                            if (canCreate)
                            {
                                SQL.UpdateEmploye(emp);
                            }
                        }
                        else
                        {
                            if (canCreate)
                            {
                                SQL.CreateEmploye(emp);
                            }
                        }
                    }
                    if (type == "Categories")
                    {
                        cat.Name = nBox.Text;
                        if (reduct)
                        {
                            if (canCreate)
                            {
                                SQL.UpdateCategory(cat);
                            }
                        }
                        else
                        {
                            if (canCreate)
                            {
                                SQL.UpdateCategory(cat);
                            }
                        }
                    }
                    if (type == "Subdivisions")
                    {
                        sub.Name = nBox.Text;
                        if (reduct)
                        {
                            if (canCreate)
                            {
                                SQL.CreateSubdivision(sub);
                            }
                        }
                        else
                        {
                            if (canCreate)
                            {
                                SQL.CreateSubdivision(sub);
                            }
                        }
                    }
                    if (reduct)
                    {
                        if (MessageBox.Show("Записть изменена", "Успех",
                                MessageBoxButton.OK, MessageBoxImage.Question) == MessageBoxResult.OK) { selectedIndex = tableGrid.SelectedIndex;  }
                    }
                    else
                    {
                        MessageBox.Show("Записть создана", "Успех",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                catch { }
                Refresh();
            }

            

        }

        private void refreshButt_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void dellButt_Click(object sender, RoutedEventArgs e)
        {
            if (tableGrid.SelectedItem != null)
            {
                bool a = false;
                if (type == "Employees")
                {
                    if(SQL.CIWE(sId))
                    {
                        MessageBox.Show("У этой записи есть звязи", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        try
                        {
                            SQL.DeleteEmploye(sId);
                            a = true;
                        }
                        catch { }
                    }
                }
                if (type == "Categories")
                {
                    if (SQL.CIWC(sId))
                    {
                        MessageBox.Show("У этой записи есть звязи", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        try
                        {
                            SQL.DeleteCategory(sId);
                            a = true;
                        }
                        catch { }
                    }
                }
                if (type == "Subdivisions")
                {
                    if (SQL.CIWS(sId))
                    {
                        MessageBox.Show("У этой записи есть звязи", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        try
                        {
                            SQL.DeleteSubdivision(sId);
                            a = true;
                        }
                        catch { }
                    }
                }
                if(a)
                {
                    MessageBox.Show("Запись удалена", "Успех",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    reductButt_Click(null, null);
                    reductButt_Click(null, null);
                }
                
                
            }
        }
    }
}
