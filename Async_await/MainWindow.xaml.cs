using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Linq;

namespace Async_await
{
    public partial class MainWindow : Window
    {
        static Random random = new Random();
        string sourceDir = string.Empty;
        string searchWord = string.Empty;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Вибір директорії для пошуку
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                sourceDir = dialog.FileName;
                MessageBox.Show("Ви вибрали директорію: " + sourceDir);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Введення слова для пошуку
            searchWord = SearchWordTextBox.Text;
            if (string.IsNullOrEmpty(searchWord))
            {
                MessageBox.Show("Будь ласка, введіть слово для пошуку.");
                return;
            }
            else
            {
                MessageBox.Show("Слово для пошуку: " + searchWord);
            }
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(sourceDir) || string.IsNullOrEmpty(searchWord))
            {
                MessageBox.Show("Будь ласка, виберіть директорію і введіть слово для пошуку.");
                return;
            }

            // Асинхронний пошук і аналіз файлів
            await SearchFilesAsync();
        }

        private Task SearchFilesAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    // Пошук всіх .txt файлів у директорії та піддиректоріях
                    string[] txtFiles = Directory.GetFiles(sourceDir, "*.txt", SearchOption.AllDirectories);

                    Application.Current.Dispatcher.Invoke(() => List.Items.Clear());

                    // Проходимо по всіх файлах
                    foreach (string file in txtFiles)
                    {
                        int wordCount = CountWordOccurrencesInFile(file, searchWord);
                        string fileName = Path.GetFileName(file);
                        string filePath = file;

                        // Додаємо результат у список
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            List.Items.Add($"Назва файлу: {fileName}");
                            List.Items.Add($"Шлях до файлу: {filePath}");
                            List.Items.Add($"Кількість входжень слова: {wordCount}");
                            List.Items.Add("---------------------------------");
                        });
                    }

                    if (txtFiles.Length == 0)
                    {
                        Application.Current.Dispatcher.Invoke(() => MessageBox.Show("Не знайдено жодного файлу .txt."));
                    }
                }
                catch (DirectoryNotFoundException dirNotFound)
                {
                    Application.Current.Dispatcher.Invoke(() => MessageBox.Show("Директорію не знайдено: " + dirNotFound.Message));
                }
            });
        }

        private int CountWordOccurrencesInFile(string filePath, string word)
        {
            int count = 0;
            try
            {
                // Читання всього вмісту файлу
                string content = File.ReadAllText(filePath);

                // Пошук кількості входжень слова
                count = content.Split(new string[] { word }, StringSplitOptions.None).Length - 1;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine("Помилка читання файлу: " + ioEx.Message);
            }

            return count;
        }
    }
}
