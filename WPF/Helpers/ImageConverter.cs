using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPF.Helpers
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string localUrl)
            {
                string globalUrl = "C:\\Users\\1\\Desktop\\skillbox\\C#\\CRM\\API\\" + localUrl;

                if (File.Exists(globalUrl))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(globalUrl, UriKind.Absolute);
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();

                    return image;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
