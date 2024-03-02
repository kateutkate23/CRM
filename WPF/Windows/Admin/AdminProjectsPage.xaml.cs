using System.Windows;
using System.Windows.Controls;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Windows.Admin
{
    public partial class AdminProjectsPage : Page
    {
        private readonly ProjectVM _vm;
        public AdminProjectsPage()
        {
            InitializeComponent();
            _vm = new ProjectVM();
        }
        public AdminProjectsPage(string? token)
        {
            InitializeComponent();
            _vm = new ProjectVM(token);
        }
        private async void DeleteBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Controls.Button deleteButton = (System.Windows.Controls.Button)sender;
            Project selectedProject = (Project)deleteButton.DataContext;

            if (selectedProject != null)
            {
                if (await _vm.DeleteProject(selectedProject))
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
            Project selectedProject = (Project)editButton.DataContext;

            EditProjectWindow window = new(_vm.Token, selectedProject);
            window.Closed += (s, args) =>
            {
                Refresh();
            };
            window.Show();
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddProjectWindow window = new(_vm.Token);
            window.Closed += (s, args) =>
            {
                Refresh();
            };
            window.Show();
        }

        private async void Refresh()
        {
            await _vm.GetAllProjectsAsync();
            ProjectsItemsControl.ItemsSource = _vm.ProjectsCollection;
        }
    }
}
