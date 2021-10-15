using Contacts.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class MainListViewModel : BindableBase
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }
        public ICommand AddEditCommand { protected set; get; }
        public ObservableCollection<ContactModel> Contacts { get; set; }


        public MainListViewModel(INavigationService navigationService, IPageDialogService dialogs)
        {
            _navigationService = navigationService;
            _dialogs = dialogs;

            AddEditCommand = new Command(AddEditActive);
            Contacts = new ObservableCollection<ContactModel>
            {
            new ContactModel {Id = 0, Image="user.png", NickName="UserNick 1", Name="UserName", Description="48000",  Date=DateTime.Now},
            new ContactModel {Id = 1, Image="user.png", NickName="UserNick 2", Name="UserName", Description="48000",  Date=DateTime.Now},
            new ContactModel {Id = 2, Image="user.png", NickName="UserNick 3", Name="UserName", Description="48000",  Date=DateTime.Now},
            new ContactModel {Id = 3, Image="user.png", NickName="UserNick 4", Name="UserName", Description="48000",  Date=DateTime.Now},
            new ContactModel {Id = 4, Image="user.png", NickName="UserNick 5", Name="UserName", Description="48000",  Date=DateTime.Now}
            };
        }

        #region Public
        #endregion


        #region Overrides
        #endregion

        #region Private
        private async void AddEditActive()
        {
            await _navigationService.NavigateAsync("AddEditProfileView");
        }
        #endregion
    }
}
