using System.Windows.Controls;
using System.Windows.Input;
using WPF.Models;

namespace WPF.Windows.Guest
{
    public partial class GuestBlogsPage : Page
    {
        public GuestBlogsPage()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            Blog selectedBlog = (Blog)border.DataContext;

            GuestChosenBlogPage page = new(selectedBlog);
            NavigationService.Navigate(page);
        }
    }
}
