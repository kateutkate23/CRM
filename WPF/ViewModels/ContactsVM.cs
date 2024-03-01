using System.IO;
using WPF.Helpers;

namespace WPF.ViewModels
{
    public class ContactsVM : VMBase
    {
        private string? _contactInfoText;
        public string ContactInfoText
        {
            get { return _contactInfoText ?? ""; }
            set
            {
                _contactInfoText = value;
                OnPropertyChanged(nameof(ContactInfoText));
            }
        }

        public ContactsVM()
        {
            string filePath = "C:\\Users\\1\\Desktop\\skillbox\\C#\\CRM\\WPF\\Resources\\contacts.txt";
            if (File.Exists(filePath))
            {
                ContactInfoText = File.ReadAllText(filePath);
            }
        }

        public void EditContacts(string newText)
        {
            ContactInfoText = newText;
            OnPropertyChanged(nameof(ContactInfoText));
            string path = "C:\\Users\\1\\Desktop\\skillbox\\C#\\CRM\\WPF\\Resources\\contacts.txt";

            File.WriteAllText(path, newText);
        }
    }
}
