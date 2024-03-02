using System.Windows;
using WPF.ViewModels;

namespace WPF.Windows.Admin
{
    public partial class AddServiceWindow : Window
    {
        private readonly ServiceVM _vm;
        public AddServiceWindow()
        {
            InitializeComponent();
            _vm = new ServiceVM();
        }
        public AddServiceWindow(string? token)
        {
            InitializeComponent();
            _vm = new ServiceVM(token);
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(await _vm.AddService(ServiceNameTxtBox.Text, ServiceDescriptionTxtBox.Text))
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
