using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Linq;

namespace LastDz
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
        string sourceDir = string.Empty;
        string searchWord = string.Empty;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                sourceDir = dialog.FileName;
                MessageBox.Show("Ви вибрали директорію: " + sourceDir);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}