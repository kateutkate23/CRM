using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Windows.Admin
{
    public partial class EditProjectWindow : Window
    {
        private readonly ProjectVM _vm;
        private string? selectedImageUrl;
        public EditProjectWindow()
        {
            InitializeComponent();
            _vm = new ProjectVM();
        }
        public EditProjectWindow(string? token, Project? selected)
        {
            InitializeComponent();
            _vm = new ProjectVM(token)
            {
                SelectedProject = selected
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
            if (await _vm.EditProject(_vm.SelectedProject ?? new(), ServiceNameTxtBox.Text, 
                ServiceDescriptionTxtBox.Text, selectedImageUrl))
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
