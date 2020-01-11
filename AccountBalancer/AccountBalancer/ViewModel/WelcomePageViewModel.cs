using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountBalancer
{
    public class WelcomePageViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand goToNextPage;

        public ICommand GoToNextPage
        {
            get
            {
                if(goToNextPage == null)
                {
                    return new RelayCommand<object>(x => Mediator.Notify("OnGoToNextPage", ""));
                }
                else
                {
                    return goToNextPage;
                }
            }
        }
    }
}
