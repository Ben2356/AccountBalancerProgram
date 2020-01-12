using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer
{
    class CurrencyTextBox : TextBox
    {
        private static readonly Regex regex = new Regex("^[0-9]+(\\.)?(([0-9])?[0-9])?$");
        private bool isValidNumericInput(string currentInput, string newInput, int caretIndex)
        {
            if (caretIndex == 0 && currentInput.Length > 0)
            {
                return !regex.IsMatch(newInput);
            }

            if (currentInput.Length == 0)
            {
                return !regex.IsMatch(currentInput + newInput);
            }
            string beforeCaretStr = currentInput.Substring(0, caretIndex);
            string afterCaretStr = currentInput.Substring(caretIndex);
            string newStr = beforeCaretStr + newInput + afterCaretStr;

            return !regex.IsMatch(newStr);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            int caretIndex = this.CaretIndex;
            e.Handled = isValidNumericInput(this.Text, e.Text, caretIndex);
            base.OnPreviewTextInput(e);
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() => this.SelectAll()));
            base.OnMouseDoubleClick(e);
        }
    }
}
