using AccountBalancer.Model;
using System.Windows.Input;

namespace AccountBalancer.ViewModel
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

        public DeductionsPageViewModel(AccountModel accountModel, Mediator mediator)
        {
            GoToNextPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToNextPage"));
            GoToPreviousPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToPreviousPage"));
            OnRemoveDeduction = new RemoveItemCommand(accountModel.RemoveDeduction);
            OnAddDeduction = new AddItemCommand(accountModel.AddDeduction);
            AccountModel = accountModel;
        }
    }
}
