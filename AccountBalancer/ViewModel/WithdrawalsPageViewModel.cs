using AccountBalancer.Model;
using System.Windows.Input;

namespace AccountBalancer.ViewModel
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

        public WithdrawalsPageViewModel(AccountModel accountModel, Mediator mediator)
        {
            GoToNextPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToNextPage"));
            GoToPreviousPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToPreviousPage"));
            OnRemoveWithdrawal = new RemoveItemCommand(accountModel.RemoveWithdrawal);
            OnAddWithdrawal = new AddItemCommand(accountModel.AddWithdrawal);
            AccountModel = accountModel;
        }
    }
}
