using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer
{
    public class RemoveItemCommand : ICommand
    {
        private readonly Action<int> RemoveItem;

        public event EventHandler CanExecuteChanged;

        public RemoveItemCommand(Action<int> RemoveItem)
        {
            this.RemoveItem = RemoveItem;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ListBoxItem listBoxItem = parameter as ListBoxItem;
            Tuple<float, int> itemTuple = listBoxItem.Content as Tuple<float, int>;
            RemoveItem(itemTuple.Item2);
        }
    }
}
