using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.SignUp;
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
    public class SignUpViewModel : BindableBase
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

        private string _checkPassword;
        public string CheckPassword
        {
            get { return _checkPassword; }
            set { SetProperty(ref _checkPassword, value); }
        }

        private string _text = "Default Text";
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
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

            _user = new UserModel();
            ButtonClickCommand = new DelegateCommand<string>(Execute).ObservesCanExecute(() => IsActive);
        }

        #region Public
        #endregion


        #region Overrides
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            /*if (args.PropertyName == nameof(User))
            {
                
            }*/
            IsActive = User.Login + User.Password != string.Empty;
        }
        #endregion

        #region Private

        private async void Execute(string parameter)
        {
            Text = parameter+User.Login+User.Password;
            var result = await _repository.AddAsync<UserModel>(User);
            Text = result.ToString();
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

        #endregion
    }
}
