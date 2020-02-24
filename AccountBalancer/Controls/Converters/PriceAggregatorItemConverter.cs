using System;
using System.Globalization;
using System.Windows.Data;

namespace AccountBalancer.Controls.Converters
{
    /// <summary>
    /// Converter to return the currency value of the entry for the PriceAggregatorControl
    /// </summary>
    public class PriceAggregatorItemConverter : IValueConverter
    {
        /// <summary>
        /// Returns the currency value of the (currency value, id) tuple
        /// </summary>
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            VerifyValue(value);

            Tuple<decimal, int> tuple = value as Tuple<decimal, int>;
            return tuple.Item1;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies the value is a Tuple<decimal,int>
        /// </summary>
        /// <param name="value">The value to verify</param>
        /// <exception cref="ArgumentException">if the value is not the correct type or missing</exception>
        private void VerifyValue(object value)
        {
            if (!(value is Tuple<decimal, int>))
            {
                throw new ArgumentException("value is not a tuple<decimal,int>");
            }
        }
    }
}
