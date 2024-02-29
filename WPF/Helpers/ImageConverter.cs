using System.Globalization;
using System.Windows.Data;

namespace WPF.Helpers
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string localUrl)
            {
                string globalUrl = "C:\\Users\\1\\Desktop\\skillbox\\C#\\CRM\\API\\" + localUrl;

                return new Uri(globalUrl, UriKind.RelativeOrAbsolute);
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
