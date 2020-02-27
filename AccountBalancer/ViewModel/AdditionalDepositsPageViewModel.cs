using AccountBalancer.Model;
using System.Windows.Input;

namespace AccountBalancer.ViewModel
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

        public AdditionalDepositsPageViewModel(AccountModel accountModel, Mediator mediator)
        {
            GoToNextPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToNextPage"));
            GoToPreviousPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToPreviousPage"));
            OnRemoveDeposit = new RemoveItemCommand(accountModel.RemoveDeposit);
            OnAddDeposit = new AddItemCommand(accountModel.AddDeposit);
            AccountModel = accountModel;
        }
    }
}
