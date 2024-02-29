using System.Windows;
using System.Windows.Controls;
using WPF.ViewModels;

namespace WPF.Windows.Guest
{
    public partial class GuestMainPage : Page
    {
        private readonly ApplicationVM _vm;
        public GuestMainPage()
        {
            InitializeComponent();
            _vm = new ApplicationVM();
        }

        private async void PostApplicationBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool res = await _vm.PostApplicationAsync(NameTxtBox.Text, EmailTxtBox.Text, MessageTxtBox.Text);

            if (res)
            {
                MessageBox.Show("Заявка успешно отправлена!");
                NameTxtBox.Text = "";
                EmailTxtBox.Text = "";
                MessageTxtBox.Text = "";
            }
            else
            {
                MessageBox.Show("Что-то пошло не так.");
            }
        }
    }
}
