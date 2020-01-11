using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer
{
    public class AddItemCommand : ICommand
    {
        private readonly Action<float> AddItem;

        public AddItemCommand(Action<float> AddItem)
        {
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

        public bool CanExecute(object parameter)
        {
            var parameterValues = parameter as object[];
            TextBox addDeductionTextBox = parameterValues[0] as TextBox;
            return addDeductionTextBox.Text.Length > 0;
        }

        public void Execute(object parameter)
        {
            var parameterValues = parameter as object[];
            TextBox addTextBox = parameterValues[0] as TextBox;
            AddItem(float.Parse(addTextBox.Text));
            ListBox listBox = parameterValues[1] as ListBox;
            listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
            addTextBox.Text = "";
        }
    }
}
