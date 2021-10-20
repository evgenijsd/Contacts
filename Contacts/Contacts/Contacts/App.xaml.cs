﻿using Contacts.Services.Repository;
using Contacts.Services.Settings;
using Contacts.Services.SignIn;
using Contacts.Services.SignUp;
using Contacts.ViewModels;
using Contacts.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts
{
    public partial class App : PrismApplication
    {
        public App()
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/SignInView");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<IAuthenticationId>(Container.Resolve<AuthenticationId>());
            containerRegistry.RegisterInstance<IAddUserBase>(Container.Resolve<AddUserBase>());
            containerRegistry.RegisterInstance<ISortSetting>(Container.Resolve<SortSetting>());
            containerRegistry.RegisterInstance<IAuthentication>(Container.Resolve<Authentication>());


            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
