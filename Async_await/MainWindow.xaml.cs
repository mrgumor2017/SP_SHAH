using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Async_await
{
    public partial class MainWindow : Window
    {
        static Random random = new Random();
        string sourceDir = string.Empty;
        string backupDir = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            List.Items.Add(await GenerateValue());
        }
        Task<int> GenerateValue()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(random.Next(5000));
                return random.Next(1000);
            }

            );
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                sourceDir = dialog.FileName;  
                MessageBox.Show("Ви вибрали: " + sourceDir);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                backupDir = dialog.FileName;  
                MessageBox.Show("Ви вибрали: " + backupDir);
            }
        }


        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(sourceDir) || string.IsNullOrEmpty(backupDir))
            {
                MessageBox.Show("Будь ласка, виберіть файл і директорію для резервної копії.");
                return;
            }

            try
            {
                await CopyTo();
                MessageBox.Show("Файл успішно скопійовано до: " + backupDir); 
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                MessageBox.Show("Директорію не знайдено: " + dirNotFound.Message);
            }
            catch (IOException ioError)
            {
                MessageBox.Show("Помилка копіювання: " + ioError.Message); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невідома помилка: " + ex.Message); 
            }
        }
        int count=0;
        Task CopyTo()
        {
            return Task.Run(() =>
            {
                
                string fileName = Path.GetFileNameWithoutExtension(sourceDir)+(count++)+Path.GetExtension(sourceDir);

                File.Copy(sourceDir, Path.Combine(backupDir, fileName), true);
                Thread.Sleep(1000);
            });
        }

    }
}