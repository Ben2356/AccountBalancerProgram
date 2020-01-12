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
    public class CreditsPageViewModel : BaseViewModel, IPageViewModel
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

        public ICommand OnAddCredit
        {
            get;
            set;
        }

        public ICommand OnRemoveCredit
        {
            get;
            set;
        }

        public AccountModel AccountModel
        {
            get;
            set;
        }

        public CreditsPageViewModel(AccountModel accountModel)
        {
            GoToNextPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToNextPage", ""));
            GoToPreviousPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToPreviousPage", ""));
            OnRemoveCredit = new RemoveItemCommand(accountModel.RemoveCredit);
            OnAddCredit = new AddItemCommand(accountModel.AddCredit);
            AccountModel = accountModel;
        }
    }
}
