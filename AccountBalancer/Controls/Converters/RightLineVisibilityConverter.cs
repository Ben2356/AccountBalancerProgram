using System;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace AccountBalancer.Controls.Converters
{
    /// <summary>
    /// Converter to determine the right line of the step ellipses for the StepProgressBar
    /// </summary>
    public class RightLineVisibilityConverter : IMultiValueConverter
    {
        /// <summary>
        /// Returns hidden visibility if the current step is the last one
        /// </summary>
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            VerifyValues(values);

            string currentStringContent = (string)values[0];
            ObservableCollection<string> collection = (ObservableCollection<string>)values[1];
            if(collection.IndexOf(currentStringContent) == collection.Count - 1)
            {
                return Visibility.Hidden;
            }
            return Visibility.Visible;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies that the values[] contains a string and an ObservableCollection<string>
        /// </summary>
        /// <param name="values">The values to verify</param>
        /// <exception cref="ArgumentException">if the values are not the correct types or missing</exception>
        private void VerifyValues(object[] values)
        {
            if (values.Length != 2)
            {
                throw new ArgumentException("Incorrect value count");
            }
            if (!(values[0] is string))
            {
                throw new ArgumentException("values[0] is not a string");
            }
            if (!(values[1] is ObservableCollection<string>))
            {
                throw new ArgumentException("values[1] is not an ObservableCollection<string>");
            }
        }
    }
}
