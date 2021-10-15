using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.SignIn;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.ViewModels
{
    public class SignInViewModel : BindableBase, INavigationAware, IInitialize
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }
        private ICheckAuthorization _checkAuthorization { get; set; }
        private UserModel _user;
        private IRepository _repository { get; }
        public DelegateCommand MainListCommand { get; set; }
        public DelegateCommand SignUpCommand { get; set; }
        public DelegateCommand SetCommand { get; set; }
        

        public SignInViewModel(INavigationService navigationService, IPageDialogService dialogs, ICheckAuthorization checkAuthorization, IRepository repository, UserModel user)
        {
            _navigationService = navigationService;
            _repository = repository;
            _dialogs = dialogs;
            _checkAuthorization = checkAuthorization;
            UserId = _checkAuthorization.UserId;
            _user = user;

            MainListCommand = new DelegateCommand(MainListAction).ObservesCanExecute(() => IsActive);
            SignUpCommand = new DelegateCommand(SignUpAction);
        }

        #region Property
        private ObservableCollection<UserModel> _userList;
        public ObservableCollection<UserModel> UserList
        {
            get => _userList;
            set => SetProperty(ref _userList, value);
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

        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

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
        #endregion

        #region Public
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("UserId"))
            {
                UserId = int.Parse(parameters.GetValue<string>("UserId"));
                _checkAuthorization.UserId = UserId;
            }
            if (parameters.ContainsKey("pUserId"))
            {
                UserId = parameters.GetValue<int>("pUserId");
                Login = parameters.GetValue<string>("pLogin");
                Password = parameters.GetValue<string>("pPassword");
                User = new UserModel { Id = UserId, Login = Login, Password = Password };
                UserList.Add(User);
            }
        }

        public async void Initialize(INavigationParameters parameters)
        {
            var userList = await _repository.GetAllAsync<UserModel>();
            UserList = new ObservableCollection<UserModel>(userList);
            if (UserId > 0)
            {
                User = UserList.FirstOrDefault(x => x.Id == UserId);
                Login = User.Login;
                Password = User.Password;
            }
            await Task.Delay(TimeSpan.FromSeconds(2));
            var p = new NavigationParameters();
            p.Add("mUserId", UserId);
            if (UserId > 0) await _navigationService.NavigateAsync("/NavigationPage/MainListView");
        }

        #endregion


        #region Overrides
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Login))
            {
                IsActive = Login != string.Empty && Password != string.Empty;
            } 
            if (args.PropertyName == nameof(Password))
            {
                IsActive = Login != string.Empty && Password != string.Empty;
            }
        }
        #endregion

        #region Private

        private async void MainListAction()
        {
            User = UserList.FirstOrDefault(x => x.Login == Login);
            if (User != null && User.Login == Login && User.Password == Password)
            {
                _checkAuthorization.UserId = User.Id;
                var p = new NavigationParameters();
                p.Add("mUserId", UserId);
                await _navigationService.NavigateAsync("/NavigationPage/MainListView");
            }
            else
            {
                await _dialogs.DisplayAlertAsync("Alert", "Invalid login or password!", "Ok");
                Password = string.Empty;
            }
            //
            //await _navigationService.NavigateAsync("/NavigationPage/MainListView");
        }

        private async void SignUpAction()
        {
            await _navigationService.NavigateAsync("SignUpView");
        }

        #endregion
    }
}
