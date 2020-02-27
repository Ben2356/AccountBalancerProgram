using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace AccountBalancer.Controls.Converters
{
    /// <summary>
    /// Converter to retrieve the current step text for the StepProgressBar
    /// </summary>
    public class CurrentStepTextConverter : IMultiValueConverter
    {
        /// <summary>
        /// Retrieves the current step text from the steps collection
        /// </summary>
        /// <inheritdoc/>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            VerifyValues(values);
            int currentStepIndex = (int)values[0];
            ObservableCollection<string> steps = (ObservableCollection<string>)values[1];
            return steps[currentStepIndex];
        }

        /// <inheritdoc/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies that the values[] contains an int and a ObservableCollection<string>
        /// </summary>
        /// <param name="values">The values to verify</param>
        /// <exception cref="ArgumentException">if the values are not the correct types or missing</exception>
        /// <exception cref="ArgumentOutOfRangeException">if the supplied index is out of bounds</exception>
        private void VerifyValues(object[] values)
        {
            if (values.Length != 2)
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
            int index = (int)values[0];
            ObservableCollection<string> collection = (ObservableCollection<string>)values[1];
            if (index >= collection.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException(null, "index " + index + " is out of bounds, size of collection is " + collection.Count);
            }
        }
    }
}
