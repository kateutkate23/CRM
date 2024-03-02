using System.Windows.Controls;
using System.Windows.Forms;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Windows.Admin
{
    public partial class AdminServicesPage : Page
    {
        private readonly ServiceVM _vm;
        public AdminServicesPage()
        {
            InitializeComponent();
            _vm = new ServiceVM();
        }
        public AdminServicesPage(string? token)
        {
            InitializeComponent();
            _vm = new ServiceVM(token);
        }

        private async void DeleteBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Controls.Button deleteButton = (System.Windows.Controls.Button)sender;
            Service selectedService = (Service)deleteButton.DataContext;

            if (selectedService != null)
            {
                if (await _vm.DeleteService(selectedService))
                {
                    Resresh();
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так");
                }
            }
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddServiceWindow window = new(_vm.Token);
            window.Closed += (s, args) =>
            {
                Resresh();
            };
            window.Show();
        }

        private async void Resresh()
        {
            await _vm.GetAllServicesAsync();
            ServicesItemsControl.ItemsSource = _vm.ServicesCollection;
        }

        private void EditBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Controls.Button editButton = (System.Windows.Controls.Button)sender;
            Service selectedService = (Service)editButton.DataContext;

            EditServiceWindow window = new(_vm.Token, selectedService);
            window.Closed += (s, args) =>
            {
                Resresh();
            };
            window.Show();
        }
    }
}
