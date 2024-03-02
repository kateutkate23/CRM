using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using WPF.ViewModels;

namespace WPF.Windows.Admin
{
    public partial class AddBlogWindow : Window
    {
        private readonly BlogVM _vm;
        private string? selectedImageUrl;
        public AddBlogWindow()
        {
            InitializeComponent();
            _vm = new BlogVM();
        }
        public AddBlogWindow(string? token)
        {
            InitializeComponent();
            _vm = new BlogVM(token);
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (await _vm.AddBlog(BlogTitleTxtBox.Text, BlogDescriptionTxtBox.Text, selectedImageUrl))
            {
                Close();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так.");
            }
        }

        private void SelectImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage image = new(new Uri(openFileDialog.FileName));
                selectedImageUrl = openFileDialog.FileName;
                SelectedImage.Source = image;
            }
        }
    }
}
