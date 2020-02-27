using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace AccountBalancer.Controls.Converters
{
    /// <summary>
    /// Converter to return the appropriate progress line color for the StepProgressBar
    /// </summary>
    public class LineStrokeConverter : IMultiValueConverter
    {
        /// <summary>
        /// Returns the appropriate progress line color for either the left or right line of the current step ellipse based on the following: 
        ///     - If the index is less than the current step index then both the left and right line should be green
        ///     - If the index is at the current step index then the left line should be green and the right line should be gray
        ///     - If the index is greater than the current step index then both the left and right line should be gray
        /// </summary>
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            VerifyValues(values);
            VerifyParameter(parameter);

            int currentStepIndex = (int)values[0];
            ObservableCollection<string> collection = (ObservableCollection<string>)values[1];
            string currentStep = (string)values[2];
            int index = collection.IndexOf(currentStep);
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

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Verifies that the values[] contains an int, ObservableCollection<string>, and a string
        /// </summary>
        /// <param name="values">The values to verify</param>
        /// <exception cref="ArgumentException">if the values are not the correct types or missing</exception>
        private void VerifyValues(object[] values)
        {
            if (values.Length != 3)
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
        }

        /// <summary>
        /// Verifies that the parameter is a bool
        /// </summary>
        /// <param name="parameter">The parameter to verify</param>
        /// <exception cref="ArgumentException">if the parameter is not a bool or cannot be converted to a bool</exception>
        private void VerifyParameter(object parameter)
        {
            try
            {
                System.Convert.ToBoolean(parameter.ToString());
            } catch (Exception exception)
            {
                throw new ArgumentException("parameter is not a bool", exception);
            }
        }
    }
}
