using AccountBalancer.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountBalancer
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IPageViewModel currentPageViewModel;
        private ObservableCollection<IPageViewModel> pageViewModels;
        private int currentPageIndex;
        private AccountModel accountModel;

        public ObservableCollection<IPageViewModel> PageViewModels
        {
            get
            {
                if(pageViewModels == null)
                {
                    pageViewModels = new ObservableCollection<IPageViewModel>();
                }
                return pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get { return currentPageViewModel; }
            set
            {
                currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                currentPageIndex = value;
                OnPropertyChanged("CurrentPageIndex");
            }
        }

        public ObservableCollection<string> Steps
        {
            get;
            set;
        }

        public ChangeFocusCommand OnMouseDown
        {
            get;
            set;
        }

        public MainWindowViewModel()
        {
            accountModel = new AccountModel();

            PageViewModels.Add(new WelcomePageViewModel());
            PageViewModels.Add(new AccountRegisterBalancePageViewModel(accountModel));
            PageViewModels.Add(new DeductionsPageViewModel(accountModel));
            PageViewModels.Add(new CreditsPageViewModel(accountModel));
            PageViewModels.Add(new NewAccountRegisterBalancePageViewModel(accountModel));
            PageViewModels.Add(new StatementEndingBalancePageViewModel(accountModel));
            PageViewModels.Add(new AdditionalDepositsPageViewModel(accountModel));
            PageViewModels.Add(new WithdrawalsPageViewModel(accountModel));
            PageViewModels.Add(new SummaryPageViewModel(accountModel));

            Steps = new ObservableCollection<string>();
            Steps.Add("Welcome");
            Steps.Add("Account register/checkbook balance");
            Steps.Add("Service charges and other deductions");
            Steps.Add("Add credits");
            Steps.Add("New account register balance");
            Steps.Add("Statement ending balance");
            Steps.Add("Additional deposits");
            Steps.Add("Withdrawals");
            Steps.Add("Summary");

            CurrentPageIndex = 0;

            CurrentPageViewModel = PageViewModels[CurrentPageIndex];

            Mediator.Subscribe("OnGoToNextPage", OnGoToNextPage);
            Mediator.Subscribe("OnGoToPreviousPage", OnGoToPreviousPage);

            OnMouseDown = new ChangeFocusCommand();
        }

        private void OnGoToNextPage(object obj)
        {
            CurrentPageIndex++;
            CurrentPageViewModel = PageViewModels[currentPageIndex];
        }

        private void OnGoToPreviousPage(object obj)
        {
            CurrentPageIndex--;
            CurrentPageViewModel = PageViewModels[currentPageIndex];
        }

        public class ChangeFocusCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Grid grid = parameter as Grid;
                grid.Focus();
            }
        }
    }
}
