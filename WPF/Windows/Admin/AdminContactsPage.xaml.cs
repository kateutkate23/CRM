using System.Windows.Controls;
using System.Windows;
using WPF.ViewModels;

namespace WPF.Windows.Admin
{
    public partial class AdminContactsPage : Page
    {
        private readonly ContactsVM vm;

        public AdminContactsPage()
        {
            InitializeComponent();
            vm = new();
            DataContext = vm; 
        }

        private void EditBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ContactsTextBlock.Visibility = Visibility.Collapsed;
            EditTextBox.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Visible;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.EditContacts(EditTextBox.Text);
            EditTextBox.Visibility = Visibility.Collapsed;
            SaveBtn.Visibility = Visibility.Collapsed;
            ContactsTextBlock.Visibility = Visibility.Visible;
        }
    }
}
