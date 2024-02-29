using System.Windows;

namespace WPF.Windows.Guest
{
    public partial class GuestMainWindow : Window
    {
        public GuestMainWindow()
        {
            InitializeComponent();
        }

        private void ContactsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.Content != null)
            {
                mainFrame.NavigationService.RemoveBackEntry();
            }
            mainFrame.Content = new GuestContactsPage();
        }

        private void MainBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.Content != null)
            {
                mainFrame.NavigationService.RemoveBackEntry();
            }
            mainFrame.Content = new GuestMainPage();
        }

        private void ServicesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.Content != null)
            {
                mainFrame.NavigationService.RemoveBackEntry();
            }
            mainFrame.Content = new GuestServicesPage();
        }

        private void ProjectsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.Content != null)
            {
                mainFrame.NavigationService.RemoveBackEntry();
            }
            mainFrame.Content = new GuestProjectsPage();
        }

        private void BlogBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainFrame.Content != null)
            {
                mainFrame.NavigationService.RemoveBackEntry();
            }
            mainFrame.Content = new GuestBlogsPage();
        }
    }
}
