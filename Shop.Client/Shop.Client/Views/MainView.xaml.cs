using Shop.Client.ViewModels;
using Shop.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shop.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : TabbedPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<MainViewModel, int>(this, MessageKeys.ChangeTab, (sender, arg) =>
            {
                switch (arg)
                {
                    case 0:
                        CurrentPage = CatalogView;
                        break;
                }
            });

            await ((CatalogViewModel)CatalogView.BindingContext).InitializeAsync(null);
        }

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (CurrentPage is CatalogView)
            {
                // Force basket view refresh every time we access it
                await (CatalogView.BindingContext as ViewModelBase).InitializeAsync(null);
            }
        }
    }
}