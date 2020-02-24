using AccountBalancer.Model;
using System.Windows.Input;

namespace AccountBalancer.ViewModel
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

        public StatementEndingBalancePageViewModel(AccountModel accountModel, Mediator mediator)
        {
            GoToNextPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToNextPage"));
            GoToPreviousPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToPreviousPage"));
            AccountModel = accountModel;
        }
    }
}
