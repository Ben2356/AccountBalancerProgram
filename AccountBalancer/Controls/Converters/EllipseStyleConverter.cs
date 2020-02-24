using System;
using System.Windows.Data;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace AccountBalancer.Controls.Converters
{
    /// <summary>
    /// Converter to set the step ellipse styles for the StepProgressBar
    /// </summary>
    public class EllipseStyleConverter : IMultiValueConverter
    {
        /// <summary>
        /// Sets the step ellipse shape style to be: 
        ///     - Solid green color if the step is done
        ///     - White fill with a green outline if the step is in progress
        ///     - White fill with a grey outline if the step is not done
        /// </summary>
        /// <inheritdoc/>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            VerifyValues(values);

            int currentStepIndex = (int)values[0];
            ObservableCollection<string> collection = (ObservableCollection<string>)values[1];
            string currentStep = (string)values[2];
            int index = collection.IndexOf(currentStep);
            Ellipse ellipse = (Ellipse)values[3];

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

        /// <inheritdoc/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Verifies that the values[] contains an int, ObservableCollection<string>, string, and an Ellipse
        /// </summary>
        /// <param name="values">The values to verify</param>
        /// <exception cref="ArgumentException">if the values are not the correct types or missing</exception>
        private void VerifyValues(object[] values)
        {
            if (values.Length != 4)
            {
                throw new ArgumentException("Incorrect value count");
            }
            if (!(values[0] is int))
            {
                throw new ArgumentException("values[0] is not an int");
            }
            if (!(values[1] is ObservableCollection<string>))
            {
                throw new ArgumentException("values[1] is not an ObservableCollection<string>");
            }
            if (!(values[2] is string))
            {
                throw new ArgumentException("values[2] is not a string");
            }
            if (!(values[3] is Ellipse))
            {
                throw new ArgumentException("values[3] is not an Ellipse");
            }
        }
    }
}
