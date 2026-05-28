using System;
using System.IO;
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
using Microsoft.Win32;
using static WpfApp1.InzedentWind;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static System.Net.WebRequestMethods;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для filerWind.xaml
    /// </summary>
    public partial class filerWind : Window
    {
        int nextId = 0;

        int Iid;
        string newPath;
        bool reduct;
        toSQL SQL = new toSQL();

        public bool IsLookOnly = false;

        public event EventHandler<List<IncidentFile>> FileUpdateHandler;

        IncidentFile[] GetFileSourse()
        {
            return files.ToArray();
        }

        List<IncidentFile> files = new List<IncidentFile>();
        public filerWind(int Iid, bool reduct, List<IncidentFile> filesa, bool isLook)
        {
            InitializeComponent();
            this.Iid = Iid;
            this.reduct = reduct;
            if (!reduct)
            {
                files = filesa;
                //nextId = SQL.GetNewId("Files");
            }
            else if (reduct)
            {
                files = SQL.GetAllFileById(Iid).ToList();
                nextId = SQL.GetNewId("Files");
            }
            fileGrid.ItemsSource = GetFileSourse();
            IsLookOnly = isLook;
            if(IsLookOnly)
            {
                dellButt.IsEnabled = false;
                saveButt.IsEnabled = false;
                addFileButt.IsEnabled = false;
            }

        }
        int sId = -1;
        private void fileGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var idProperty = fileGrid.SelectedItem.GetType().GetProperty("Id");
            if (idProperty != null)
            {
                int id = (int)idProperty.GetValue(fileGrid.SelectedItem);
                idBlock.Text = id.ToString();
                sId = id;
            }
        }

        private void exitButt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        int GetId()
        {
            nextId++;
            return nextId-1;
        }

        long GetSize(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Length;
        }

        private void addFileButt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                int tryId = 0;
                if (!reduct) { tryId = files.Count(); }
                else if (reduct) { tryId = GetId(); }
                    newPath = filePath;
                files.Add(new IncidentFile
                {
                    Id = tryId,
                    IncidentId = Iid,
                    FileName = System.IO.Path.GetFileNameWithoutExtension(filePath),
                    ContentType = System.IO.Path.GetExtension(filePath),
                    FileData = System.IO.File.ReadAllBytes(filePath),
                    FileSize = GetSize(filePath)
                });
                //DBlock.Text = files[0].FileName.ToString();
                fileGrid.ItemsSource = GetFileSourse();
            }
        }

        private void saveButt_Click(object sender, RoutedEventArgs e)
        {
            FileUpdateHandler?.Invoke(this, files);
            
            //DBlock.Text = files.Count.ToString();
            this.Close();
        }

        private void dellButt_Click(object sender, RoutedEventArgs e)
        {
            if (sId >= 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i].Id == sId)
                    {
                        files.RemoveAt(i);
                        sId = -1;
                        break;
                    }
                }
            }
        }

        private void lookButt_Click(object sender, RoutedEventArgs e)
        {
            if (sId >= 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i].Id == sId)
                    {
                        OpenFileWithProgramChooser(files[i]);
                        break;
                    }
                }
            }
        }


        public static class FileOpener
        {
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile,
                string lpParameters, string lpDirectory, int nShowCmd);

            public static void OpenWith(string filePath)
            {
                ShellExecute(IntPtr.Zero, "openas", filePath, null, null, 1);
            }
        }

        // Использование в вашем коде
        private void OpenFileWithProgramChooser(IncidentFile file)
        {
            if (file == null || file.FileData == null) return;

            // Создаем временный файл
            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), file.FileName);
            System.IO.File.WriteAllBytes(tempPath, file.FileData);

            try
            {
                // Вызываем диалог "Открыть с помощью"
                FileOpener.OpenWith(tempPath);

                // Удаляем временный файл через 10 секунд
                Task.Delay(10000).ContinueWith(_ =>
                {
                    try
                    {
                        if (System.IO.File.Exists(tempPath))
                            System.IO.File.Delete(tempPath);
                    }
                    catch { }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
