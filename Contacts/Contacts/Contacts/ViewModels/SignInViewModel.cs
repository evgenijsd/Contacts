using Contacts.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class SignInViewModel : BindableBase
    {
        private UserModel _user;
        private int _count;

        //private IVerificationOfAuthorizationService _verificationOfAuthorizationService;
        public SignInViewModel()
        {
            Count = 0;
            User = new UserModel { UserId = 0, Login = "Login", Password = "Password" };
            //_verificationOfAuthorizationService = verificationOfAuthorizationService;
            //User.Login = _verificationOfAuthorizationService.CheckAuthorization.ToString();
            //if (_verificationOfAuthorizationService.CheckAuthorization > 0) { }
        }

        #region Public
        public UserModel User { get => _user; set => SetProperty(ref _user, value); }
        public int Count { get => _count; set => SetProperty(ref _count, value); }

        public ICommand IncrementButtonTapCommand => new Command(OnIncrementButtonTap);
        #endregion


        #region Overrides
        #endregion

        #region Private
        private void OnIncrementButtonTap()
        {
            _count++;
            //User.Login = _count.ToString();
        }
        #endregion
    }
}
