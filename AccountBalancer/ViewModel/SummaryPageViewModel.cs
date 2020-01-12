using AccountBalancer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AccountBalancer
{
    public class SummaryPageViewModel : BaseViewModel, IPageViewModel
    {
        public ICommand Done
        {
            get;
            set;
        }

        public ICommand GoToPreviousPage
        {
            get;
            set;
        }

        public AccountModel AccountModel
        {
            get;
            set;
        }

        public SummaryPageViewModel(AccountModel accountModel)
        {
            Done = new CloseWindowCommand();
            GoToPreviousPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToPreviousPage", ""));
            AccountModel = accountModel;
        }

        internal class CloseWindowCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Window window = parameter as Window;
                window.Close();
            }
        }
    }
}
