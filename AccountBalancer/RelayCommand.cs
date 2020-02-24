using System;
using System.Windows.Input;

namespace AccountBalancer
{
    /// <summary>
    /// A command relayer class that wraps a command
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> canExecute;
        private readonly Action<T> execute;

        /// <summary>
        /// Creates a RelayCommand wrapper with the Command Execute and a CanExecute that returns true
        /// </summary>
        /// <param name="execute">The Command.Execute method</param>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
            this.execute = execute;
        }

        /// <summary>
        /// Creates a RelayCommand wrapper with the Command Execute and CanExecute methods
        /// </summary>
        /// <param name="execute">The Command.Execute method</param>
        /// <param name="canExecute">The Command.CanExecute method</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if(execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Performs the CanExecute of the wrapped Command
        /// </summary>
        /// <param name="parameter">The parameter to pass to the wrapped Command.CanExecute</param>
        /// <returns>The result of the wrapped Command.CanExecute. If no CanExecute method is provided then true is returned</returns>
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute((T)parameter);
        }

        /// <summary>
        /// Performs the Execute of the wrapped Command
        /// </summary>
        /// <param name="parameter">The parameter to pass to the wrapped Command.Execute</param>
        public void Execute(object parameter)
        {
            execute((T)parameter);
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
    }
}
