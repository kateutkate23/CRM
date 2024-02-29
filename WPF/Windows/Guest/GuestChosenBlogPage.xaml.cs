using System.Windows.Controls;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Windows.Guest
{
    public partial class GuestChosenBlogPage : Page
    {
        private readonly BlogVM? _vm;
        public GuestChosenBlogPage()
        {
            InitializeComponent();
        }
        public GuestChosenBlogPage(Blog selected)
        {
            InitializeComponent();
            _vm = new BlogVM(selected);
            DataContext = _vm;
        }
    }
}
