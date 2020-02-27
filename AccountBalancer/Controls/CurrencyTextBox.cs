using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer.Controls
{
    /// <summary>
    /// TextBox control for currency value styled input, eg. 1,000.00
    /// </summary>
    public class CurrencyTextBox : TextBox
    {
        public static readonly DependencyProperty IsInputValidProperty = DependencyProperty.Register("IsInputValid", typeof(bool), typeof(CurrencyTextBox), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Flag denoting whether the input is considered valid <see cref="isValidNumericInput" />
        /// </summary>
        public bool IsInputValid
        {
            get { return (bool)this.GetValue(IsInputValidProperty); }
            set { this.SetValue(IsInputValidProperty, value); }
        }

        /// <summary>
        /// Determines if the new input to the textbox will result in a valid numeric string following the patterns: 
        /// 1) single number must be present before allowing a comma 
        /// 2) a comma must be followed by 3 digits
        /// 3) if a decimal is present then only 2 digits max, first is required and second is optional, may follow
        /// </summary>
        /// <returns>True if the input is a valid currency string, False otherwise</returns>
        private bool isValidNumericInput()
        {
            Regex regex = new Regex("^[0-9]+((,[0-9][0-9][0-9])?)*((\\.)([0-9][0-9]?))?$");
            return regex.IsMatch(this.Text);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            IsInputValid = isValidNumericInput();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            if (IsInputValid)
            {
                this.Text = String.Format("{0:N}", decimal.Parse(this.Text));
            }
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() => this.SelectAll()));
            base.OnMouseDoubleClick(e);
        }
    }
}
