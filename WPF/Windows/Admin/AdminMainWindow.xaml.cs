using System.Windows;
using WPF.Windows.Guest;

namespace WPF.Windows.Admin
{
    public partial class AdminMainWindow : Window
    {
        public string? Token { get; set; }
        public AdminMainWindow()
        {
            InitializeComponent();
        }
        public AdminMainWindow(string? token)
        {
            InitializeComponent();
            Token = token;
        }

        private void ApplicationsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.Content != null)
            {
                mainFrame.NavigationService.RemoveBackEntry();
            }
            mainFrame.Content = new AdminApplicationsPage(Token);
        }

        private void MainBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.Content != null)
            {
                mainFrame.NavigationService.RemoveBackEntry();
            }
            mainFrame.Content = new AdminMainPage();
        }

        private void ContactsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.Content != null)
            {
                mainFrame.NavigationService.RemoveBackEntry();
            }
            mainFrame.Content = new AdminContactsPage();
        }

        private void ServicesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.Content != null)
            {
                mainFrame.NavigationService.RemoveBackEntry();
            }
            mainFrame.Content = new AdminServicesPage(Token);
        }
    }
}
