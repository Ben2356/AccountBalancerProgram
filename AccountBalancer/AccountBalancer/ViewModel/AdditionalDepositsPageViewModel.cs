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
    public class AdditionalDepositsPageViewModel : BaseViewModel, IPageViewModel
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

        public ICommand OnAddDeposit
        {
            get;
            set;
        }

        public ICommand OnRemoveDeposit
        {
            get;
            set;
        }

        public AccountModel AccountModel
        {
            get;
            set;
        }

        public AdditionalDepositsPageViewModel(AccountModel accountModel)
        {
            GoToNextPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToNextPage", ""));
            GoToPreviousPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToPreviousPage", ""));
            OnRemoveDeposit = new RemoveItemCommand(accountModel.RemoveDeposit);
            OnAddDeposit = new AddItemCommand(accountModel.AddDeposit);
            AccountModel = accountModel;
        }
    }
}
