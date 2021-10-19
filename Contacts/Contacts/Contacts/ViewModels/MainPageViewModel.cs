using Contacts.Models;
using Contacts.Services.Repository;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ViewModels
{
    class MainPageViewModel : BindableBase, IInitialize
    {
        
        private INavigationService _navigationService { get; }
        private UserModel _user;
        public UserModel User { get => _user; set => SetProperty(ref _user, value); }
        private ObservableCollection<UserModel> _userList;
        private readonly IRepository _repository;
        public DelegateCommand<string> AddCommand { get; private set; }

        public ObservableCollection<UserModel> UserList
        {
            get => _userList;
            set => SetProperty(ref _userList, value);
        }

        public MainPageViewModel(IRepository repository)
        {
            _repository = repository;

            AddCommand = new DelegateCommand<string>(OnAddCommand);
        }

        public async void Initialize(INavigationParameters parameters)
        {
            var userList = await _repository.GetAllAsync<UserModel>();
            UserList = new ObservableCollection<UserModel>(userList);
        }

        private async void OnAddCommand(string parameter)
        {
            //await _dialogs.DisplayAlertAsync("Alert", $"Count {ContactList.Count}", "Ok");
            var p = new NavigationParameters
            {
                { "maUserId", 3 },
                { "maId", 5 }
            };
            await _navigationService.NavigateAsync("AddEditProfileView");
        }

    }
}
