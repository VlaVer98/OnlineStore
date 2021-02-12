using IdentityModel.Client;
using Shop.Client.Models.User;
using Shop.Client.Services.Identity;
using Shop.Client.Services.OpenUrl;
using Shop.Client.Services.Settings;
using Shop.Client.Validations;
using Shop.Client.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shop.Client.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        public LoginViewModel()
        {
        }

        public ICommand GoToMainCommand => new Command(async () => await GoToMainAsync());


        private async Task GoToMainAsync()
        {
            await NavigationService.NavigateToAsync<MainViewModel>();
            await NavigationService.RemoveLastFromBackStackAsync();
        }
    }
}
