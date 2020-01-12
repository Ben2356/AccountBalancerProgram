using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AccountBalancer.Converters
{
    public class SuccessImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue)
            {
                return null;
            }
            float total = (float)values[0];
            float newAccountRegisterBalance = (float)values[1];

            if (total == newAccountRegisterBalance)
            {
                return new BitmapImage(new Uri(@"/Resources/dolphin.jpg", UriKind.Relative)); ;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
