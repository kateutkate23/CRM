using System.Windows;
using WPF.ViewModels;
using WPF.Windows.Admin;
using WPF.Windows.Guest;

namespace WPF.Windows
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private async void SignIn_Click(object sender, RoutedEventArgs e)
        {
            var _vm = new AccountVM();
            string? token = await _vm.SignInAsync(UsernameTextBox.Text, PasswordTextBox.Text);

            if (token != null)
            {
                AdminMainWindow mainWindow = new(token);
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так.");
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            GuestMainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }
}
