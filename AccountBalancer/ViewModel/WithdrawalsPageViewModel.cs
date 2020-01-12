using AccountBalancer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountBalancer
{
    public class WithdrawalsPageViewModel : BaseViewModel, IPageViewModel
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

        public ICommand OnAddWithdrawal
        {
            get;
            set;
        }

        public ICommand OnRemoveWithdrawal
        {
            get;
            set;
        }

        public AccountModel AccountModel
        {
            get;
            set;
        }

        public WithdrawalsPageViewModel(AccountModel accountModel)
        {
            GoToNextPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToNextPage", ""));
            GoToPreviousPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToPreviousPage", ""));
            OnRemoveWithdrawal = new RemoveItemCommand(accountModel.RemoveWithdrawal);
            OnAddWithdrawal = new AddItemCommand(accountModel.AddWithdrawal);
            AccountModel = accountModel;
        }
    }
}
