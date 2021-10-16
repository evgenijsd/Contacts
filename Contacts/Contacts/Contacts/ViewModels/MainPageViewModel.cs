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
    class MainPageViewModel : BindableBase, IInitializeAsync
    {
        
        private INavigationService _navigationService { get; }
        private UserModel _user;
        public UserModel User { get => _user; set => SetProperty(ref _user, value); }
        private ObservableCollection<UserModel> _userList;
        private readonly IRepository _repository;
        public ObservableCollection<UserModel> UserList
        {
            get => _userList;
            set => SetProperty(ref _userList, value);
        }

        public MainPageViewModel(IRepository repository)
        {
            _repository = repository;
        }

        public void Initialize(INavigationParameters parameters)
        {
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var userList = await _repository.GetAllAsync<UserModel>();
            UserList = new ObservableCollection<UserModel>(userList);
        }
    }
}
