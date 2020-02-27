using AccountBalancer.Model;
using System.Windows.Input;

namespace AccountBalancer.ViewModel
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

        public AccountRegisterBalancePageViewModel(AccountModel accountModel, Mediator mediator)
        {
            GoToNextPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToNextPage"));
            AccountModel = accountModel;
        }
    }
}
