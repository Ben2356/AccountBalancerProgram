using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AccountBalancer.Converters
{
    /// <summary>
    /// Converter to set the TextBlock text if the total is equal to the new account register balance
    /// </summary>
    public class StatusTextBlockConverter : IMultiValueConverter
    {
        /// <summary>
        /// Sets the TextBlock text and text color based on the if the total matches the new account register balance
        /// </summary>
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && (values[0] == DependencyProperty.UnsetValue))
            {
                return null;
            }
            VerifyValues(values);
            decimal total = Math.Abs((decimal)values[0]);
            decimal newAccountRegisterBalance = (decimal)values[1];
            decimal difference = Math.Abs(total - newAccountRegisterBalance);
            TextBlock textBlock = values[2] as TextBlock;

            if (difference == 0)
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

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies that the values[] contains a decimal, decimal, and a TextBlock
        /// </summary>
        /// <param name="values">The values to verify</param>
        /// <exception cref="ArgumentException">if the values are not the correct types</exception>
        private void VerifyValues(object[] values)
        {
            if (values.Length != 3)
            {
                throw new ArgumentException("Incorrect value count");
            }
            if (!(values[0] is decimal))
            {
                throw new ArgumentException("values[0] is not a decimal");
            }
            if (!(values[1] is decimal))
            {
                throw new ArgumentException("values[1] is not a decimal");
            }
            if (!(values[2] is TextBlock))
            {
                throw new ArgumentException("values[2] is not a TextBlock");
            }
        }
    }
}
