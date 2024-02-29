using System.Windows.Controls;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Windows.Guest
{
    public partial class GuestChosenProjectPage : Page
    {
        private readonly ProjectVM? _vm;
        public GuestChosenProjectPage()
        {
            InitializeComponent();
        }
        public GuestChosenProjectPage(Project selected)
        {
            InitializeComponent();
            _vm = new ProjectVM(selected);
            DataContext = _vm;
        }
    }
}
