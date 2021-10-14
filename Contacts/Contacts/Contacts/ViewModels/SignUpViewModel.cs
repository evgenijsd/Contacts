using Contacts.Models;
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

namespace Contacts.ViewModels
{
    public class SignUpViewModel : BindableBase
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }
        //private IAddUserBase _addUserBase { get; }
        private UserModel _user;

        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public SignUpViewModel(INavigationService navigationService, IPageDialogService dialogs)
        {
            _navigationService = navigationService;
            _dialogs = dialogs;
            //_addUserBase = addUserBase;

            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        #region Public
        public DelegateCommand<string> NavigateCommand { get; }
        #endregion


        #region Overrides
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(User))
            {
                //_addUserBase.User = User;
            }
        }
        #endregion

        #region Private


        private async void OnNavigateCommandExecuted(string path)
        {
            /*if (await _addUserBase.AddUserBaseAsync(User) == 1)
            {
                await _dialogs.DisplayAlertAsync("Error", "save", "Ok");
                return;
            }*/
                 
            var result = await _navigationService.NavigateAsync(path);

            if (!result.Success)
            {
                await _dialogs.DisplayAlertAsync("Error", result.Exception.Message, "Ok");
            }
        }
        #endregion
    }
}
