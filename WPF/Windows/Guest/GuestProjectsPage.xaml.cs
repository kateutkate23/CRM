using System.Windows.Controls;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Windows.Guest
{
    public partial class GuestProjectsPage : Page
    {
        public GuestProjectsPage()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            Project selectedProject = (Project)border.DataContext;

            GuestChosenProjectPage page = new(selectedProject);
            NavigationService.Navigate(page);
        }
    }
}
