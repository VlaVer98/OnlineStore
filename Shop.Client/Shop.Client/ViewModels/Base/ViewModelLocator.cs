﻿using Shop.Client.Services.Catalog;
using Shop.Client.Services.Dialog;
using Shop.Client.Services.Identity;
using Shop.Client.Services.Navigation;
using Shop.Client.Services.RequestProvider;
using Shop.Client.Services.Settings;
using System;
using System.Globalization;
using System.Reflection;
using TinyIoC;
using Xamarin.Forms;

namespace Shop.Client.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static TinyIoCContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public static bool UseMockService { get; set; }

        static ViewModelLocator()
        {
            _container = new TinyIoCContainer();

            // View models - by default, TinyIoC will register concrete classes as multi-instance.
            _container.Register<LoginViewModel>();
            _container.Register<MainViewModel>();

            // Services - by default, TinyIoC will register interface registrations as singletons.
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IDialogService, DialogService>();
            _container.Register<ISettingsService, SettingsService>();
            _container.Register<IIdentityService, IdentityService>();
            _container.Register<IRequestProvider, RequestProvider>();
            _container.Register<ICatalogService, CatalogMockService>();
        }

        public static void UpdateDependencies(bool useMockServices)
        {
            // Change injected dependencies
            if (useMockServices)
            {
                //_container.Register<ICatalogService, CatalogMockService>();

                UseMockService = true;
            }
            else
            {
                //_container.Register<ICatalogService, CatalogService>();

                UseMockService = false;
            }
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
