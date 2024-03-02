using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using WPF.ViewModels;

namespace WPF.Windows.Admin
{
    public partial class AddProjectWindow : Window
    {
        private readonly ProjectVM _vm;
        private string? selectedImageUrl;
        public AddProjectWindow()
        {
            InitializeComponent();
            _vm = new ProjectVM();
        }
        public AddProjectWindow(string? token)
        {
            InitializeComponent();
            _vm = new ProjectVM(token);
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
            if (await _vm.AddProject(ProjectTitleTxtBox.Text, ProjectDescriptionTxtBox.Text, selectedImageUrl))
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
