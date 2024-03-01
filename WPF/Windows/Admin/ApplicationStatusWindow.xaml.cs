using System.Windows;
using System.Windows.Controls;
using WPF.Models;
using WPF.ViewModels;
using Application = WPF.Models.Application;

namespace WPF.Windows.Admin
{
    public partial class ApplicationStatusWindow : Window
    {
        private readonly ApplicationVM _vm;
        public Application? Selected { get; set; }
        public ApplicationStatusWindow()
        {
            InitializeComponent();
            _vm = new ApplicationVM();
        }
        public ApplicationStatusWindow(string? token, Application application)
        {
            InitializeComponent();
            _vm = new ApplicationVM(token);
            Selected = application;
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedStatus = ((ComboBoxItem)statusBox.SelectedItem).Tag.ToString();
            var status = selectedStatus switch
            {
                "Received" => ApplicationStatus.Received,
                "AtWork" => ApplicationStatus.AtWork,
                "Completed" => ApplicationStatus.Completed,
                "Rejected" => ApplicationStatus.Rejected,
                "Cancelled" => ApplicationStatus.Cancelled,
                _ => ApplicationStatus.Received,
            };

            if (await _vm.EditApplicationStatus(Selected ?? new Application(), status))
            {
                Close();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так");
                Close();
            }
        }
    }
}
