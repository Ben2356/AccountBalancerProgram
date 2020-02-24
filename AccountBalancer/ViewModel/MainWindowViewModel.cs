using AccountBalancer.Commands;
using AccountBalancer.Model;
using System.Collections.ObjectModel;

namespace AccountBalancer.ViewModel
{
    /// <summary>
    /// The main window view model which acts as the container for the entire application
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        private IPageViewModel currentPageViewModel;
        private int currentPageIndex;
        private AccountModel accountModel;

        /// <summary>
        /// Collection of all of the page view models
        /// </summary>
        public ObservableCollection<IPageViewModel> PageViewModels
        {
            get;
            set;
        }

        /// <summary>
        /// The current displayed page view model instance
        /// </summary>
        public IPageViewModel CurrentPageViewModel
        {
            get { return currentPageViewModel; }
            set
            {
                currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        /// <summary>
        /// The current page index in <see cref="PageViewModels"/>
        /// </summary>
        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                currentPageIndex = value;
                OnPropertyChanged("CurrentPageIndex");
            }
        }

        /// <summary>
        /// Collection of step text tags for the <see cref="AccountBalancer.Controls.StepProgressBar"/>
        /// </summary>
        public ObservableCollection<string> Steps
        {
            get;
            set;
        }

        /// <summary>
        /// OnMouseDown command
        /// </summary>
        public ChangeFocusCommand OnMouseDown
        {
            get;
            set;
        }

        /// <summary>
        /// Constructs the MainWindowViewModel instance 
        /// </summary>
        public MainWindowViewModel()
        {
            accountModel = new AccountModel();
            Mediator mediator = new Mediator();

            PageViewModels = new ObservableCollection<IPageViewModel>();
            PageViewModels.Add(new WelcomePageViewModel(mediator));
            PageViewModels.Add(new AccountRegisterBalancePageViewModel(accountModel, mediator));
            PageViewModels.Add(new DeductionsPageViewModel(accountModel, mediator));
            PageViewModels.Add(new CreditsPageViewModel(accountModel, mediator));
            PageViewModels.Add(new NewAccountRegisterBalancePageViewModel(accountModel, mediator));
            PageViewModels.Add(new StatementEndingBalancePageViewModel(accountModel, mediator));
            PageViewModels.Add(new AdditionalDepositsPageViewModel(accountModel, mediator));
            PageViewModels.Add(new WithdrawalsPageViewModel(accountModel, mediator));
            PageViewModels.Add(new SummaryPageViewModel(accountModel, mediator));

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

            mediator.Add("OnGoToNextPage", OnGoToNextPage);
            mediator.Add("OnGoToPreviousPage", OnGoToPreviousPage);

            OnMouseDown = new ChangeFocusCommand();
        }

        /// <summary>
        /// Increments the <see cref="CurrentPageIndex"/> and sets the <see cref="CurrentPageViewModel"/> to be the next page in <see cref="PageViewModels"/>
        /// </summary>
        /// <param name="argument">Optional argument (Unused)</param>
        private void OnGoToNextPage(object argument)
        {
            CurrentPageIndex++;
            CurrentPageViewModel = PageViewModels[currentPageIndex];
        }

        /// <summary>
        /// Decrements the <see cref="CurrentPageIndex"/> and sets the <see cref="CurrentPageViewModel"/> to be the previous page in <see cref="PageViewModels"/>
        /// </summary>
        /// <param name="argument">Optional argument (Unused)</param>
        private void OnGoToPreviousPage(object argument)
        {
            CurrentPageIndex--;
            CurrentPageViewModel = PageViewModels[currentPageIndex];
        }
    }
}
