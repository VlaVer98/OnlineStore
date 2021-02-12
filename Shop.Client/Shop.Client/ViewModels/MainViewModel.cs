using Shop.Client.Models.Navigation;
using Shop.Client.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shop.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand GoToLoginCommand => new Command(async () => await GoToLoginAsync());

        private async Task GoToLoginAsync()
        {
            await NavigationService.NavigateToAsync<LoginViewModel>();
        }
    }
}
