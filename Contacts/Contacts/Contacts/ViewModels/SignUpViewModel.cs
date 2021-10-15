using Contacts.Models;
using Contacts.Services.Repository;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contacts.ViewModels
{
    public class SignUpViewModel : BindableBase, IInitializeAsync
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }
        private IRepository _repository { get; }
        private UserModel _user;
        public ICommand SetCommand { get; set; }

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

        private string _text = "Default Text";
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        private ObservableCollection<UserModel> _userList;
        public ObservableCollection<UserModel> UserList
        {
            get => _userList;
            set => SetProperty(ref _userList, value);
        }

        public DelegateCommand<string> ButtonClickCommand { get; private set; }

        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public SignUpViewModel(INavigationService navigationService, IPageDialogService dialogs, IRepository repository)
        {
            _navigationService = navigationService;
            _dialogs = dialogs;
            _repository= repository;
            //_repository.RemoveAsync<UserModel>(new UserModel { Id = 7, Login ="", Password = "" });

            _user = new UserModel();
            ButtonClickCommand = new DelegateCommand<string>(Execute).ObservesCanExecute(() => IsActive);
            SetCommand = new DelegateCommand(SetAction);
        }

        #region Public
        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var userList = await _repository.GetAllAsync<UserModel>();
            UserList = new ObservableCollection<UserModel>(userList);
        }
        #endregion


        #region Overrides
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Login))
            {
                IsActive = Login != string.Empty && Password != string.Empty && ConfirmPassword != string.Empty;
            }
            if (args.PropertyName == nameof(Password))
            {
                IsActive = Login != string.Empty && Password != string.Empty && ConfirmPassword != string.Empty;
            }
            if (args.PropertyName == nameof(ConfirmPassword))
            {
                IsActive = Login != string.Empty && Password != string.Empty && ConfirmPassword != string.Empty;
            }
        }
        #endregion

        #region Private

        private async void Execute(string parameter)
        {
            if (Login.Length < 4 || Login.Length > 16)
            {
                await _dialogs.DisplayAlertAsync("Alert", "Login less than 4 and more than 16 characters", "Ok");
                return;
            }
            if (Password.Length < 8 || Password.Length > 16)
            {
                await _dialogs.DisplayAlertAsync("Alert", "Password less than 8 and more than 16 characters", "Ok");
                return;
            }
            if (Password != ConfirmPassword)
            {
                await _dialogs.DisplayAlertAsync("Alert", "Password and confirm password do not coincide", "Ok");
                return;
            }
            if (UserList.FirstOrDefault(x => x.Login == Login) != null)
            {
                await _dialogs.DisplayAlertAsync("Alert", "This login is already taken", "Ok");
                return;
            }
            User.Login = Login;
            User.Password = Password;
            var result = await _repository.AddAsync<UserModel>(User);
            Text = result.ToString();
            var p = new NavigationParameters();
            p.Add("pUserId", User.Id);
            p.Add("pLogin", User.Login);
            p.Add("pPassword", User.Password);
            await _navigationService.GoBackAsync(p);
        }
        /*private async void SetAction()
        {
            
            await _navigationService.NavigateAsync("MainPage");
            var result = await _navigationService.NavigateAsync(path);

            if (!result.Success)
            {
                await _dialogs.DisplayAlertAsync("Error", result.Exception.Message, "Ok");
            }
        }*/
        private async void SetAction()
        {
            //UserId = 10;
            await _navigationService.NavigateAsync("MainPage");
        }



        #endregion
    }
}
