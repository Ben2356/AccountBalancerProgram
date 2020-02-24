using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer
{
    /// <summary>
    /// Generic command for removing a ListBoxItem from a collection 
    /// </summary>
    public class RemoveItemCommand : ICommand
    {
        private readonly Action<int> RemoveItem;

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Constructs a new RemoveItemCommand with the provided remove item function
        /// </summary>
        /// <param name="RemoveItem">The remove item function with the signature: <c>void RemoveItem(int)</c></param>
        /// <exception cref="ArgumentException">when the RemoveItem action is null</exception>
        public RemoveItemCommand(Action<int> RemoveItem)
        {
            if(RemoveItem == null)
            {
                throw new ArgumentException("RemoveItem action is null");
            }
            this.RemoveItem = RemoveItem;
        }

        /// <summary>
        /// Determines whether the command can be executed
        /// </summary>
        /// <param name="parameter">The ListBoxItem to be removed</param>
        /// <returns>True</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Removes the ListBoxItem from the collection using the RemoveItem function
        /// </summary>
        /// <param name="parameter">The ListBoxItem to be removed</param>
        /// <exception cref="ArgumentException">if the parameter does not have the correct type</exception>
        public void Execute(object parameter)
        {
            if(!(parameter is ListBoxItem))
            {
                throw new ArgumentException("parameter is not a ListBoxItem");
            }
            ListBoxItem listBoxItem = parameter as ListBoxItem;
            Tuple<decimal, int> itemTuple = listBoxItem.Content as Tuple<decimal, int>;
            RemoveItem(itemTuple.Item2);
        }
    }
}
