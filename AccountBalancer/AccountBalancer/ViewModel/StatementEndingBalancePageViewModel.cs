using AccountBalancer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer
{
    public class StatementEndingBalancePageViewModel : BaseViewModel, IPageViewModel
    {
        public ICommand GoToNextPage
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

        public StatementEndingBalancePageViewModel(AccountModel accountModel)
        {
            GoToNextPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToNextPage", ""));
            GoToPreviousPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToPreviousPage", ""));
            AccountModel = accountModel;
        }
    }
}
