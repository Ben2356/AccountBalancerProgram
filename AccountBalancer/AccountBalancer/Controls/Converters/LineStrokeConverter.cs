using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace AccountBalancer.Converters
{
    public class LineStrokeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int currentStepIndex = (int)values[0];

            ContentPresenter contentPresenter = values[1] as ContentPresenter;
            ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(contentPresenter);
            int index = itemsControl.ItemContainerGenerator.IndexFromContainer(contentPresenter);

            bool isLeftLine = System.Convert.ToBoolean(parameter.ToString());

            if (index < currentStepIndex)
            {
                return new SolidColorBrush(Colors.Green);
            }
            else if(index == currentStepIndex)
            {
                if(isLeftLine)
                {
                    return new SolidColorBrush(Colors.Green);
                }
                else
                {
                    return new SolidColorBrush(Colors.Gray);
                }
            }
            else
            {
                return new SolidColorBrush(Colors.Gray);
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
