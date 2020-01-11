using AccountBalancer.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer
{
    public class DeductionsPageViewModel : BaseViewModel, IPageViewModel
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

        public ICommand OnAddDeduction
        {
            get;
            set;
        }

        public ICommand OnRemoveDeduction
        {
            get;
            set;
        }

        public AccountModel AccountModel
        {
            get;
            set;
        }

        public DeductionsPageViewModel(AccountModel accountModel)
        {
            GoToNextPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToNextPage", ""));
            GoToPreviousPage = new RelayCommand<object>(x => Mediator.Notify("OnGoToPreviousPage", ""));
            OnRemoveDeduction = new RemoveItemCommand(accountModel.RemoveDeduction);
            OnAddDeduction = new AddItemCommand(accountModel.AddDeduction);
            AccountModel = accountModel;
        }
    }
}
