using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AccountBalancer.Converters
{
    /// <summary>
    /// Converter to return the appropriate status icon
    /// </summary>
    public class StatusImageSourceConverter : IMultiValueConverter
    {
        /// <summary>
        /// If the total is equal to the new account register then a status ok icon is returned, otherwise an error icon is returned
        /// </summary>
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values[0] == DependencyProperty.UnsetValue)
            {
                return null;
            }
            VerifyValues(values);
            decimal total = (decimal)values[0];
            decimal newAccountRegisterBalance = (decimal)values[1];

            if(total == newAccountRegisterBalance)
            {
                return new BitmapImage(new Uri(@"/Resources/StatusOK_16x.png", UriKind.Relative));
            }
            return new BitmapImage(new Uri(@"/Resources/StatusInvalid_16x.png", UriKind.Relative));
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies that the values[] to ensure that it contains 2 decimal values
        /// </summary>
        /// <param name="values">The values to verify</param>
        /// <exception cref="ArgumentException">if the values are not the correct types</exception>
        private void VerifyValues(object[] values)
        {
            if (values.Length != 2)
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
        }
    }
}
