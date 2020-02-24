using AccountBalancer.Model;
using System.Windows.Input;

namespace AccountBalancer.ViewModel
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

        public CreditsPageViewModel(AccountModel accountModel, Mediator mediator)
        {
            GoToNextPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToNextPage"));
            GoToPreviousPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToPreviousPage"));
            OnRemoveCredit = new RemoveItemCommand(accountModel.RemoveCredit);
            OnAddCredit = new AddItemCommand(accountModel.AddCredit);
            AccountModel = accountModel;
        }
    }
}
