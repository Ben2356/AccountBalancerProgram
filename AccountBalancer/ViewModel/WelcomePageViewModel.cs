using System.Windows.Input;

namespace AccountBalancer.ViewModel
{
    public class WelcomePageViewModel : BaseViewModel, IPageViewModel
    {
        public ICommand GoToNextPage
        {
            get;
            set;
        }

        public WelcomePageViewModel(Mediator mediator)
        {
            GoToNextPage = new RelayCommand<object>(arg => mediator.Invoke("OnGoToNextPage"));
        }
    }
}
