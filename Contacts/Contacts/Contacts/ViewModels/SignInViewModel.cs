using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.SignIn;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class SignInViewModel : BindableBase, INavigationAware, IInitialize
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }
        private ICheckAuthorization _checkAuthorization;
        private UserModel _user;
        private IRepository _repository { get; }
        public DelegateCommand MainListCommand { get; set; }
        public DelegateCommand SignUpCommand { get; set; }
        public DelegateCommand SetCommand { get; set; }

        public SignInViewModel(INavigationService navigationService, IPageDialogService dialogs, ICheckAuthorization checkAuthorization, IRepository repository, UserModel user)
        {
            _navigationService = navigationService;
            _dialogs = dialogs;
            _checkAuthorization = checkAuthorization;
            UserId = _checkAuthorization.UserId;
            _user = user;


            MainListCommand = new DelegateCommand(MainListAction);
            SignUpCommand = new DelegateCommand(SignUpAction);
            SetCommand = new DelegateCommand(SetAction);
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

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        #region Public
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("UserId"))
            {
                UserId = int.Parse(parameters.GetValue<string>("UserId"));
                //await _dialogs.DisplayAlertAsync("Error", UserId.ToString(), "Ok");
            }
        }

        public async void Initialize(INavigationParameters parameters)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            //await _dialogs.DisplayAlertAsync("Error", "save", "Ok");
            if (UserId > 0) await _navigationService.NavigateAsync("/NavigationPage/MainListView");
        }

        #endregion


        #region Overrides
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(UserId))
            {
                _checkAuthorization.UserId = UserId;
            }
            if (args.PropertyName == nameof(User))
            {
                IsActive = User.Login + User.Password != string.Empty;
            }
        }
        #endregion

        #region Private

        private async void MainListAction()
        {
            await _navigationService.NavigateAsync("/NavigationPage/MainListView");
        }

        private async void SignUpAction()
        {
            await _navigationService.NavigateAsync("SignUpView");
        }

        private async void SetAction()
        {
            UserId = 10;
            await _navigationService.NavigateAsync("MainPage");
        }




        #endregion
    }
}
