using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Windows.Admin
{
    public partial class AdminApplicationsPage : Page
    {
        private readonly ApplicationVM vm;
        public AdminApplicationsPage()
        {
            InitializeComponent();
            vm = new ApplicationVM();
        }
        public AdminApplicationsPage(string? token)
        {
            InitializeComponent();
            vm = new ApplicationVM(token);
        }

        private async void ShowApplicationsByPeriod_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selectedStatus = ((ComboBoxItem)cmbTimePeriod.SelectedItem).Tag.ToString();
            var status = selectedStatus switch
            {
                "today" => DateTime.Now.ToShortDateString(),
                "yesterday" => DateTime.Now.AddDays(-1).ToShortDateString(),
                "this week" => DateTime.Now.AddDays(-7).ToShortDateString(),
                "all time" => DateTime.MinValue.ToShortDateString(),
                _ => DateTime.MinValue.ToShortDateString(),
            };

            if (status == DateTime.MinValue.ToShortDateString())
            {
                await vm.GetAllApplications();
            }
            else if (selectedStatus == "yesterday")
            {
                await vm.GetApplicationsInPeriod(status, DateTime.Now.AddDays(-1).ToShortDateString());
            }
            else
            {
                await vm.GetApplicationsInPeriod(status, DateTime.Now.ToShortDateString());
            }

            ApplicationsGrid.ItemsSource = vm.Applications;
        }

        private void EditStatus_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application? selected = (Application?)ApplicationsGrid.SelectedItem;

            if (selected != null)
            {
                ApplicationStatusWindow window = new(vm.Token, selected);
                window.Closed += ApplicationStatusWindow_Closed;
                window.Show();
            }
        }
        private async void ApplicationStatusWindow_Closed(object? sender, EventArgs e)
        {
            await vm.GetAllApplications();  
            ApplicationsGrid.ItemsSource = vm.Applications; 
        }
    }
}
