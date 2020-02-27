using AccountBalancer.Controls;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer
{
    /// <summary>
    /// Generic command for adding items to a collection from a TextBox control
    /// </summary>
    public class AddItemCommand : ICommand
    {
        private readonly Action<decimal> AddItem;

        /// <summary>
        /// Constructs a new AddItemCommand with the provided add item function
        /// </summary>
        /// <param name="AddItem">The add item function with the signature: <c>void AddItem(decimal)</c></param>
        /// <exception cref="ArgumentException">when the AddItem action is null</exception>
        public AddItemCommand(Action<decimal> AddItem)
        {
            if(AddItem == null)
            {
                throw new ArgumentException("AddItem action is null");
            }
            this.AddItem = AddItem;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Determines whether the command can be executed
        /// </summary>
        /// <param name="parameter">An array containing the TextBox the value is supplied to and the ListBox displaying the value</param>
        /// <returns>True if the textbox parameter is not empty, false otherwise</returns>
        public bool CanExecute(object parameter)
        {
            VerifyParameter(parameter);
            object[] parameterValues = parameter as object[];
            CurrencyTextBox addTextBox = parameterValues[0] as CurrencyTextBox;
            return addTextBox.Text.Length > 0;
        }

        /// <summary>
        /// Adds the item using the AddItem function, resets the TextBox text to be empty, and then scrolls the listbox displaying the value so that the newly added value is visible
        /// </summary>
        /// <param name="parameter">An array of the Textbox the value is supplied to and the ListBox displaying the value</param>
        public void Execute(object parameter)
        {
            VerifyParameter(parameter);
            object[] parameterValues = parameter as object[];
            TextBox addTextBox = parameterValues[0] as TextBox;
            AddItem(decimal.Parse(addTextBox.Text));
            ListBox listBox = parameterValues[1] as ListBox;
            listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
            addTextBox.Text = "";
        }

        /// <summary>
        /// Verifies the command parameter to make sure that it is an object[] of a CurrencyTextBox and a ListBox
        /// </summary>
        /// <param name="parameter">The parameter of the command to be verified</param>
        /// <exception cref="ArgumentException">if the parameter does not have the correct types</exception>
        private void VerifyParameter(object parameter)
        {
            if (!(parameter is object[]))
            {
                throw new ArgumentException("parameter is not an object[]");
            }
            if(((object[])parameter).Length != 2)
            {
                throw new ArgumentException("Incorrect parameter count");
            }
            if (!(((object[])parameter)[0] is CurrencyTextBox))
            {
                throw new ArgumentException("parameter[0] is not a CurrencyTextBox");
            }
            if (!(((object[])parameter)[1] is ListBox))
            {
                throw new ArgumentException("parameter[1] is not a ListBox");
            }
        }
    }
}
