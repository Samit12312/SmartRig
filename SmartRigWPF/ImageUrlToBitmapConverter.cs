using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SmartRigWPF
{
    public class ImageUrlToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string url = $"http://localhost:5195/Images/Computers/{value.ToString()}";
            if (value == null)
                return null;

            try
            {
                return new BitmapImage(new Uri(url, UriKind.Absolute));
            }
            catch
            {
                return null; // או תמונת ברירת מחדל
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
