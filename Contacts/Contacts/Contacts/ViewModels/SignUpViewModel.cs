using Contacts.Models;
using Contacts.Services.SignUp;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;
using static Contacts.Services.SignUp.CheckType;

namespace Contacts.ViewModels
{
    public class SignUpViewModel : BindableBase
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }
        private IAddUserBase _addUserBase;

        public SignUpViewModel(INavigationService navigationService, IAddUserBase addUserBase, IPageDialogService dialogs)
        {
            _navigationService = navigationService;
            _addUserBase = addUserBase;
            _dialogs = dialogs;

            _user = new UserModel();
            SignInCommand = new DelegateCommand(OnSignInCommand).ObservesCanExecute(() => IsActive);
        }

        #region -- Public properties --

        private UserModel _user;
        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        private string _confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

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

        public DelegateCommand SignInCommand { get; set; }
        #endregion

        #region -- Overrides --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Login):
                case nameof(Password):
                case nameof(ConfirmPassword):
                    IsActive = !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(ConfirmPassword);
                    break;
            }
        }
        #endregion

        #region -- Private helpers --
        private async void OnSignInCommand()
        {
            var check = (CheckEnter)await _addUserBase.CheckTheCorrectnessAsync(Login, Password, ConfirmPassword);
            switch (check)
            {
                case CheckEnter.LoginLengthNotValid:
                    await _dialogs.DisplayAlertAsync("Alert", Resurces.Resource.AlertLoginLength, "Ok");
                    break;
                case CheckEnter.PasswordLengthNotValid:
                    await _dialogs.DisplayAlertAsync("Alert", Resurces.Resource.AlertPasswordLength, "Ok");
                    break;
                case CheckEnter.PasswordsNotEqual:
                    await _dialogs.DisplayAlertAsync("Alert", Resurces.Resource.AlerPasswordConfirm, "Ok");
                    break;
                case CheckEnter.LoginExist:
                    await _dialogs.DisplayAlertAsync("Alert", Resurces.Resource.AlertLoginTaken, "Ok");
                    break;
                case CheckEnter.LoginNotDigitalBegin:
                    await _dialogs.DisplayAlertAsync("Alert", Resurces.Resource.AlertLoginDigit, "Ok");
                    break;
                case CheckEnter.PasswordBigSmallLetterAndDigit:
                    await _dialogs.DisplayAlertAsync("Alert", Resurces.Resource.AlertPasLeterDigit, "Ok");
                    break;
                default:
                    {
                        User.Login = Login;
                        User.Password = Password;
                        int result = await _addUserBase.UserAddAsync(User);
                        if (result > 0)
                        {
                            var p = new NavigationParameters { { "pUser", User } };
                            await _navigationService.GoBackAsync(p);
                        }
                        else await _dialogs.DisplayAlertAsync("Alert", "Database failure!", "Ok");
                    }
                    break;
            }
        }
        #endregion
    }
}
