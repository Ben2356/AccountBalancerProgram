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
    public class EllipseStyleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int currentStepIndex = (int)values[0];

            ContentPresenter contentPresenter = values[1] as ContentPresenter;
            ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(contentPresenter);
            int index = itemsControl.ItemContainerGenerator.IndexFromContainer(contentPresenter);

            Ellipse ellipse = values[2] as Ellipse;

            if(index < currentStepIndex)
            {
                ellipse.Fill = new SolidColorBrush(Colors.Green);
            }
            else if(index == currentStepIndex)
            {
                ellipse.Fill = new SolidColorBrush(Colors.White);
                ellipse.Stroke = new SolidColorBrush(Colors.Green);
                ellipse.StrokeThickness = 3;
            }
            else
            {
                ellipse.Fill = new SolidColorBrush(Colors.White);
                ellipse.Stroke = new SolidColorBrush(Colors.Gray);
                ellipse.StrokeThickness = 3;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
