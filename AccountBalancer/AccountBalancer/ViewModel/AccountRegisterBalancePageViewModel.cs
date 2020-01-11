using AccountBalancer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountBalancer
{
    public class AccountRegisterBalancePageViewModel : BaseViewModel, IPageViewModel
    {
        public ICommand GoToNextPage
        {
            get;
            set;
        }

        public AccountModel AccountModel
        {
            get;
            set;
        }

        public AccountRegisterBalancePageViewModel(AccountModel accountModel)
        {
            GoToNextPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToNextPage", ""));
            AccountModel = accountModel;
        }
    }
}
