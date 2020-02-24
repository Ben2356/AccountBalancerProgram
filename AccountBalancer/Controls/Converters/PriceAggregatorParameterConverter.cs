using System;
using System.Globalization;
using System.Windows.Data;

namespace AccountBalancer.Controls.Converters
{
    /// <summary>
    /// Converter to return a clone of the array passed in for the PriceAggregatorControl
    /// </summary>
    public class PriceAggregatorParameterConverter : IMultiValueConverter
    {
        /// <summary>
        /// Returns a clone of the provided array
        /// </summary>
        /// <exception cref="ArgumentNullException">if the supplied values array is null</exception>
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            return values.Clone();
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
