using Contacts.Models;
using Contacts.Services.SignIn;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Contacts.ViewModels
{
    public class SignInViewModel : BindableBase, INavigationAware, IInitialize
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }
        private IAuthenticationId _idAuthentication { get; set; }
        private IAuthentication _authentication;

        public SignInViewModel(INavigationService navigationService, IPageDialogService dialogs, IAuthenticationId idAuthentication, IAuthentication authentication, UserModel user)
        {
            _navigationService = navigationService;
            _dialogs = dialogs;
            _authentication = authentication;
            _idAuthentication = idAuthentication;
            UserId = _idAuthentication.UserId;
            _user = user;

            MainListCommand = new DelegateCommand(OnMainListCommand).ObservesCanExecute(() => IsActive);
            SignUpCommand = new DelegateCommand(OnSignUpCommand);
        }

        #region -- Public properties --
        private string _login = string.Empty;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }
        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private UserModel _user;
        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private bool _isActive = false;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        public DelegateCommand MainListCommand { get; set; }
        public DelegateCommand SignUpCommand { get; set; }
        #endregion

        #region Public
        public async void Initialize(INavigationParameters parameters)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            if (UserId > 0)
            {
                var p = new NavigationParameters { { "mUserId", UserId } };
                await _navigationService.NavigateAsync("/NavigationPage/MainListView");
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("UserIdNull"))
            {
                UserId = 0;
                _idAuthentication.UserId = UserId;
            }
            if (parameters.ContainsKey("pUser"))
            {
                User = parameters.GetValue<UserModel>("pUser");
                Login = User.Login;
                Password = User.Password;
            }
        }
        #endregion

        #region -- Overrides --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Login):
                case nameof(Password):
                    IsActive = !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
                    break;
            }
        }
        #endregion

        #region -- Private helpers --
        private async void OnMainListCommand()
        {
            int id = await _authentication.CheckAsync(Login, Password);
            if (id != 0)
            {
                UserId = id;
                _idAuthentication.UserId = id;
                var p = new NavigationParameters { { "mUserId", id } };
                await _navigationService.NavigateAsync("/NavigationPage/MainListView");
            }
            else
            {
                await _dialogs.DisplayAlertAsync("Alert", "Invalid login or password!", "Ok");
                Password = string.Empty;
            }
        }

        private async void OnSignUpCommand()
        {
            await _navigationService.NavigateAsync("SignUpView");
        }

        #endregion
    }
}
