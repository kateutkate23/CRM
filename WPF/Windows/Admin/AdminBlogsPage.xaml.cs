using System.Windows;
using System.Windows.Controls;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Windows.Admin
{
    public partial class AdminBlogsPage : Page
    {
        private readonly BlogVM _vm;
        public AdminBlogsPage()
        {
            InitializeComponent();
            _vm = new BlogVM();
        }
        public AdminBlogsPage(string? token)
        {
            InitializeComponent();
            _vm = new BlogVM(token);
        }
        private async void DeleteBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Controls.Button deleteButton = (System.Windows.Controls.Button)sender;
            Blog selectedBlog = (Blog)deleteButton.DataContext;

            if (selectedBlog != null)
            {
                if (await _vm.DeleteBlog(selectedBlog))
                {
                    Refresh();
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так");
                }
            }
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button editButton = (System.Windows.Controls.Button)sender;
            Blog selectedBlog = (Blog)editButton.DataContext;

            EditBlogWindow window = new(_vm.Token, selectedBlog);
            window.Closed += (s, args) =>
            {
                Refresh();
            };
            window.Show();
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddBlogWindow window = new(_vm.Token);
            window.Closed += (s, args) =>
            {
                Refresh();
            };
            window.Show();
        }

        private async void Refresh()
        {
            await _vm.GetAllBlogsAsync();
            BlogsItemsControl.ItemsSource = _vm.BlogsCollection;
        }
    }
}
