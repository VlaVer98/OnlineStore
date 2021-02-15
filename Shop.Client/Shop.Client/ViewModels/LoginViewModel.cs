using IdentityModel.Client;
using Shop.Client.Services.Identity;
using Shop.Client.Services.Settings;
using Shop.Client.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shop.Client.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private bool _isLogin;
        private string _authUrl;

        private IIdentityService _identityService;
        private ISettingsService _settingsService;

        public LoginViewModel(
            IIdentityService identityService,
            ISettingsService settingsService)
        {
            _identityService = identityService;
            _settingsService = settingsService;
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }


        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
            set
            {
                _isLogin = value;
                RaisePropertyChanged(() => IsLogin);
            }
        }

        public string LoginUrl
        {
            get
            {
                return _authUrl;
            }
            set
            {
                _authUrl = value;
                RaisePropertyChanged(() => LoginUrl);
            }
        }

        public ICommand SignInCommand => new Command(async () => await SignInAsync());
        public ICommand NavigateCommand => new Command<string>(async (url) => await NavigateAsync(url));

        private async Task SignInAsync()
        {
            IsBusy = true;

            await Task.Delay(100);

            LoginUrl = _identityService.CreateAuthorizationRequest();

            IsLogin = true;
            IsBusy = false;
        }

        private async Task NavigateAsync(string url)
        {
            var unescapedUrl = System.Net.WebUtility.UrlDecode(url);

            if (unescapedUrl.Equals(GlobalSetting.Instance.LogoutCallback))
            {
                _settingsService.AuthAccessToken = string.Empty;
                _settingsService.AuthIdToken = string.Empty;
                IsLogin = false;
                LoginUrl = _identityService.CreateAuthorizationRequest();
            }
            else if (unescapedUrl.Contains(GlobalSetting.Instance.Callback))
            {
                var authResponse = new AuthorizeResponse(url);
                if (!string.IsNullOrWhiteSpace(authResponse.Code))
                {
                    var userToken = await _identityService.GetTokenAsync(authResponse.Code);
                    string accessToken = userToken.AccessToken;

                    if (!string.IsNullOrWhiteSpace(accessToken))
                    {
                        _settingsService.AuthAccessToken = accessToken;
                        _settingsService.AuthIdToken = authResponse.IdentityToken;
                        await NavigationService.NavigateToAsync<MainViewModel>();
                        await NavigationService.RemoveLastFromBackStackAsync();
                    }
                }
            }
        }
    }
}
