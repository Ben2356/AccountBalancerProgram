using System;
using System.Windows;
using System.Windows.Input;

namespace AccountBalancer.Commands
{
    /// <summary>
    /// Command to close the Window
    /// </summary>
    public class CloseWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether the command can be executed
        /// </summary>
        /// <param name="parameter">The Window element</param>
        /// <returns>True</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Closes the Window parameter
        /// </summary>
        /// <param name="parameter">The Window to be closed</param>
        public void Execute(object parameter)
        {
            if(!(parameter is Window))
            {
                throw new ArgumentException("parameter is not Window");
            }
            Window window = parameter as Window;
            window.Close();
        }
    }
}
