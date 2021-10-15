using Contacts.Models;
using Contacts.Services.Repository;
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
            /*UserList = new ObservableCollection<UserModel>
            {
            new UserModel {Id = 0, Login="UserNick 1", Password="UserName" },
            new UserModel {Id = 1, Login="UserNick 2", Password="UserName" },
            new UserModel {Id = 2, Login="UserNick 3", Password="UserName" },
            new UserModel {Id = 3, Login="UserNick 4", Password="UserName" },
            new UserModel {Id = 4, Login="UserNick 5", Password="UserName" }
            };*/
            _repository = repository;
            
        }

        public void Initialize(INavigationParameters parameters)
        {
            ;
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var userList = await _repository.GetAllAsync<UserModel>();
            UserList = new ObservableCollection<UserModel>(userList);
        }
    }
}
