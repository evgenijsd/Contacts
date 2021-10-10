using Contacts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Contacts.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private UserModel user;

        public UserViewModel() => user = new UserModel();

        public string Login
        {
            get { return user.Login; }
            set
            {
                if (user.Login != value)
                {
                    user.Login = value;
                    OnPropertyChanged("Login");
                }
            }
        }
        public string Password
        {
            get { return user.Passsword; }
            set
            {
                if (user.Passsword != value)
                {
                    user.Passsword = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private System.Collections.IEnumerable contactModel;

        public System.Collections.IEnumerable ContactModel { get => contactModel; set => SetProperty(ref contactModel, value); }
    }
}
