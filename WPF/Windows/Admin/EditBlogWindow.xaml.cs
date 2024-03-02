using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using WPF.ViewModels;
using WPF.Models;

namespace WPF.Windows.Admin
{
    public partial class EditBlogWindow : Window
    {
        private readonly BlogVM _vm;
        private string? selectedImageUrl;
        public EditBlogWindow()
        {
            InitializeComponent();
            _vm = new BlogVM();
        }
        public EditBlogWindow(string? token, Blog? selected)
        {
            InitializeComponent();
            _vm = new BlogVM(token)
            {
                SelectedBlog = selected
            };
            DataContext = _vm;
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

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (await _vm.EditBlog(_vm.SelectedBlog ?? new(), BlogNameTxtBox.Text,
                BlogDescriptionTxtBox.Text, selectedImageUrl))
            {
                Close();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так.");
            }
        }
    }
}
