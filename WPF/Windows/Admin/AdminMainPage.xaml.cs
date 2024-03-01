using System.Windows;
using System.Windows.Controls;
using WPF.ViewModels;

namespace WPF.Windows.Admin
{
    public partial class AdminMainPage : Page
    {
        private readonly ApplicationVM vm;
        public AdminMainPage()
        {
            InitializeComponent();
            vm = new();
            DataContext = vm;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            TitleTextBlock.Visibility = Visibility.Collapsed;
            EditTextBox.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Visible;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.EditTitle(EditTextBox.Text);
            EditTextBox.Visibility = Visibility.Collapsed;
            SaveBtn.Visibility = Visibility.Collapsed;
            TitleTextBlock.Visibility = Visibility.Visible;
        }
    }
}
