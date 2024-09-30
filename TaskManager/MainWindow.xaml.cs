using System.Diagnostics;
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

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grid.ItemsSource = Process.GetProcesses();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process selected = (Process)grid.SelectedItem;
            MessageBox.Show(selected.ProcessName,"Name");
            MessageBox.Show(selected.Id.ToString(), "Id");
            MessageBox.Show(selected.BasePriority.ToString(), "BasePriority");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Process selected = (Process)grid.SelectedItem;
            selected.Kill();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Process selected = (Process)grid.SelectedItem;
            selected.Close();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string filePath = prName.Text;

            if (File.Exists(filePath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = filePath,
                };

                try
                {
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при запуску файлу: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Файл не знайдено. Перевірте шлях до файлу.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}