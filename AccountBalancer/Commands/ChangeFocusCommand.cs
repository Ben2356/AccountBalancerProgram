using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer.Commands
{
    /// <summary>
    /// Command to change the focus to the the background Grid control of the window
    /// </summary>
    public class ChangeFocusCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether the command can be executed
        /// </summary>
        /// <param name="parameter">The Grid control to be put in focus</param>
        /// <returns>True</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Focuses on the Grid parameter
        /// </summary>
        /// <param name="parameter">The Grid control to put in focus</param>
        /// <exception cref="ArgumentException">when the parameter is not a Grid</exception>
        public void Execute(object parameter)
        {
            if(!(parameter is Grid))
            {
                throw new ArgumentException("parameter is not of type Grid");
            }
            Grid grid = parameter as Grid;
            grid.Focus();
        }
    }
}
