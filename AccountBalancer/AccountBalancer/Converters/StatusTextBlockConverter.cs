using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AccountBalancer.Converters
{
    public class StatusTextBlockConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue)
            {
                return null;
            }
            float total = Math.Abs((float)values[0]);
            float newAccountRegisterBalance = (float)values[1];
            float difference = Math.Abs(total - newAccountRegisterBalance);
            TextBlock textBlock = values[2] as TextBlock;

            if (difference == 0F)
            {
                textBlock.Text = "Total matches account register balance";
                textBlock.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                textBlock.Text = "Total does NOT match account register balance. Difference of " + String.Format("{0:C}", difference);
                textBlock.Foreground = new SolidColorBrush(Colors.Red);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
