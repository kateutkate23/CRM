using System.Windows;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Windows.Admin
{
    public partial class EditServiceWindow : Window
    {
        private readonly ServiceVM _vm;
        public EditServiceWindow()
        {
            InitializeComponent();
            _vm = new ServiceVM();
        }
        public EditServiceWindow(string? token, Service? selected)
        {
            InitializeComponent();
            _vm = new ServiceVM(token)
            {
                Selected = selected
            };
            DataContext = _vm;
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(await _vm.EditService(_vm.Selected ?? new(), ServiceNameTxtBox.Text, ServiceDescriptionTxtBox.Text))
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
