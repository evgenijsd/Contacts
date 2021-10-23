using Contacts.Services.AddEditProfile;
using Contacts.Services.MainList;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using Contacts.Services.SignIn;
using Contacts.Services.SignUp;
using Contacts.ViewModels;
using Contacts.Views;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

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
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<IAuthenticationId>(Container.Resolve<AuthenticationId>());
            containerRegistry.RegisterInstance<IAddUserBase>(Container.Resolve<AddUserBase>());
            containerRegistry.RegisterInstance<IAllSetting>(Container.Resolve<AllSetting>());
            containerRegistry.RegisterInstance<IAuthentication>(Container.Resolve<Authentication>());
            containerRegistry.RegisterInstance<IMainListService>(Container.Resolve<MainListService>());
            containerRegistry.RegisterInstance<IAddEditService>(Container.Resolve<AddEditService>());


            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<PopUpView, PopUpViewModel>();

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
