using Shop.Client.Models.Navigation;
using Shop.Client.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shop.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public override Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            if (navigationData is TabParameter)
            {
                // Change selected application tab
                var tabIndex = ((TabParameter)navigationData).TabIndex;
                MessagingCenter.Send(this, MessageKeys.ChangeTab, tabIndex);
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
