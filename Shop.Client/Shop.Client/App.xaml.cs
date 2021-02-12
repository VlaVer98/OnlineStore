using Shop.Client.Services.Navigation;
using Shop.Client.Services.Settings;
using Shop.Client.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shop.Client
{
    public partial class App : Application
    {
        ISettingsService _settingsService;

        public App()
        {
            InitializeComponent();

            InitApp();
            if (Device.RuntimePlatform == Device.UWP)
            {
                InitNavigation();
            }
        }

        private void InitApp()
        {
            _settingsService = ViewModelLocator.Resolve<ISettingsService>();
            if (!_settingsService.UseMocks)
                ViewModelLocator.UpdateDependencies(_settingsService.UseMocks);
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            if (Device.RuntimePlatform != Device.UWP)
            {
                await InitNavigation();
            }

            base.OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

    }
}
