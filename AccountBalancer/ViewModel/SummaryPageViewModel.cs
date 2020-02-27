using AccountBalancer.Commands;
using AccountBalancer.Model;
using System.Windows.Input;

namespace AccountBalancer.ViewModel
{
    public class SummaryPageViewModel : BaseViewModel, IPageViewModel
    {
        public ICommand Done
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

        public SummaryPageViewModel(AccountModel accountModel, Mediator mediator)
        {
            Done = new CloseWindowCommand();
            GoToPreviousPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToPreviousPage"));
            AccountModel = accountModel;
        }
    }
}
